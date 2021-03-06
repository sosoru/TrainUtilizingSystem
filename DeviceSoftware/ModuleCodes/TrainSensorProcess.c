//#include "MonoDevice.h"
#include "HardwareProfile.h"
#include "../Headers/MotherBoardModule.h"
#include "../Headers/TrainSensorModule.h"
#include <stdlib.h>
#include <string.h>
#include <adc.h>
#include <timers.h>
#include <delays.h>

//#define IS_VALID_PORT(m) ((m>=1 && m <= 8) || (m >=13 && m <= 16))

void ApplyTrainSensorSavedState(TrainSensorState* state, TrainSensorSavedModuledState* savemState);
void ApplyTrainSensorState(TrainSensorState * state, TrainSensorSavedModuledState * savemState);

void SetMeisureVoltage(BYTE module, BYTE port);
HRESULT GetChannel(BYTE module, unsigned char * channel);

HRESULT GetFuncTableTrainSensor(DeviceID * pid, ModuleFuncTable* table)
{
	BYTE i;
	
	table->fncreate = CreateTrainSensorState;
	table->fnstore = StoreTrainSensorSavedState;
	table->fninit = InitTrainSensor;
//	table->fnclose = CloseTrainSensor;
	table->fninterrupt = InterruptTrainSensor;
	
	return S_OK;
}

void SetUsingPort(BYTE module, BYTE port)
{	
	BYTE cnt=0,  base = (module-1) * PORT_PIN_COUNT + 1;
		
	if(port > 8)
		return;
		
	setLat(base+4, 1);
	
	setLat(base+1, (port & 0x01));
	setLat(base+2, (port & 0x02) >> 1);
	setLat(base+3, (port & 0x04) >> 2);
//	LATAbits.LATA4 = 1;
//	LATA = ((port<<1) | (LATA & 0b11110001));
//	LATAbits.LATA4 = 0;
	setLat(base+4, 0);
	
	while(!getPort(base+5) && cnt++ < 100);
	Delay10TCYx(30); 
	
}

void SetMeisureVoltage(BYTE module, BYTE port)
{
	HRESULT convert;
	unsigned char channel;
	convert = GetChannel(module, &channel);
	
	if(convert == E_FAIL)
		return;
		
	OpenADC(ADC_FOSC_64 & ADC_RIGHT_JUST & ADC_20_TAD,
	channel & ADC_INT_OFF & ADC_VREFPLUS_VDD & ADC_VREFMINUS_VSS,
	AD_PORT);
	
	while(g_usingAdc);
	g_usingAdc = TRUE;
	SetChanADC(channel);
	SetUsingPort(module, port);
	ConvertADC();
}

HRESULT GetChannel(BYTE module, unsigned char * channel)
{
	switch(module) 
	{
		case 1:
			*channel = ADC_CH1;
			break;
		case 2:
			*channel = ADC_CH0;
			break;
		case 3:
			*channel = ADC_CH2;
			break;
		default:
			return E_FAIL;	
	}
	return S_OK;
	
	//#endif
}

HRESULT CreateTrainSensorState(DeviceID * pid, PMODULE_DATA data)
{
	unsigned int voltage;
	TrainSensorSavedModuledState romdata;
	TrainSensorState* argdata = (TrainSensorState*)data;
	int romdatasize = sizeof(TrainSensorState);
	unsigned int curTimer;
	BYTE module = pid->ModuleAddr, port = pid->InternalAddr;
	BYTE timeroccupied;
	HRESULT res =UNKNOWN;
				
	SetMeisureVoltage(module, port);
	
	curTimer = ReadTimer0();
	//memset((void *)data, (unsigned char)0x00, (size_t)SIZE_DATA);
	ReadTrainSensorSavedModuledState(module, port, &romdata);
	ApplyTrainSensorSavedState(argdata, &romdata);
	
	// take care of the size of data
	argdata->Timer = curTimer;
	argdata->OverflowedCount = Timer0OverflowCount;
	argdata->ReferenceVoltageMinus = 0;
	argdata->ReferenceVoltagePlus = 5;
	argdata->VoltageResolution = 10;
	
	while(BusyADC());
	g_usingAdc = FALSE;
	voltage = ReadADC();
	
	argdata->CurrentVoltage = voltage;
	
	switch(romdata.Mode)
	{
		case MODE_TRAINSENSOR_MEISURING:
			//sprintf(data, PGMCSTR("Tm=%u,V=%u.%u\n"), curTimer, decVoltage, flacVoltage);
			res = S_OK;
		break;
		
		case MODE_TRAINSENSOR_DETECTING:
			timeroccupied = (BYTE)GET_TIMER_OCCUPIED(module, port);
			if(!timeroccupied
			 && (
			 	voltage <= (((unsigned int)romdata.ThresholdVoltageLower) <<2)
			 	)
			 )
			 {
				 //detecting train
				 timeroccupied = TRUE;
				 SET_TIMER_OCCUPIED(module, port, 1);
			 }
			 else if(timeroccupied
			 			&& 
			 			(
			 			voltage > (((unsigned int)romdata.ThresholdVoltageHigher) <<2)
					 	)
					 )
			 {
				 //detected train
				 timeroccupied = FALSE;
				 SET_TIMER_OCCUPIED(module, port, 0);
			 }
			
			argdata->IsDetected = timeroccupied;
			res = S_OK;
		break;
		
		default:
			//maybe missing settingdata
			//strcpypgm2ram(&cmdbuf[0], &defaultCmd[0]);
			memset((void*)&romdata, 0x00, (size_t)sizeof(TrainSensorSavedModuledState));
			romdata.Mode = MODE_TRAINSENSOR_MEISURING;
			WriteTrainSensorSavedModuledState(module, port, &romdata);
			res = E_FAIL;
		break;
	}
	
	if(res == UNKNOWN)
		res = E_FAIL;
	
	return res | (port==TRAINSENSOR_INNERMODULE_COUNT-1) ? REPEAT_TERMINATE : 0;
}

void ApplyTrainSensorSavedState(TrainSensorState* state, TrainSensorSavedModuledState* savemState)
{
	state->Mode = savemState->Mode;
	state->ThresholdVoltageLower = savemState->ThresholdVoltageLower;
	state->ThresholdVoltageHigher = savemState->ThresholdVoltageHigher;
}

void ApplyTrainSensorState(TrainSensorState * state, TrainSensorSavedModuledState * savemState)
{
	savemState->Mode = state->Mode;
	savemState->ThresholdVoltageLower = state->ThresholdVoltageLower;
	savemState->ThresholdVoltageHigher = state->ThresholdVoltageHigher;
}

HRESULT InitTrainSensor(DeviceID * pid)
{	
	unsigned char channel;
	HRESULT res;
	BYTE module = pid->ModuleAddr, i, base;
	
	res = GetChannel(module, &channel);
	if(res != S_OK)
		return E_FAIL;	
			
	base = (pid->ModuleAddr-1) * PORT_PIN_COUNT + 1;
	
	setTris(base, INPUT_PIN);
	for(i=1; i< PORT_PIN_COUNT; ++i)
	{
		setTris(base+i, OUTPUT_PIN);
	}
	setTris(base+5, INPUT_PIN);
	//setTris(module, INPUT_PIN);

	return S_OK;
}

void InterruptTrainSensor(DeviceID * pid)
{

}

HRESULT StoreTrainSensorSavedState(DeviceID * pid, PMODULE_DATA buf)
{
	TrainSensorSavedModuledState msaved;
	TrainSensorState* pstate = (TrainSensorState*) buf;
	BYTE module = pid->ModuleAddr;
	
	if(pid->InternalAddr > TRAINSENSOR_INNERMODULE_COUNT)
		return E_FAIL;
		
	ApplyTrainSensorState(pstate, &msaved);
	
	WriteTrainSensorSavedModuledState(module, pid->InternalAddr, &msaved);
	
	//Port_SurfaceLedA = 1;
	return S_OK;
}