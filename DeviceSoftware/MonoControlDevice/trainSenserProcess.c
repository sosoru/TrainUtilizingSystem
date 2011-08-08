#include "MonoDevice.h"

#include "../Headers/MotherBoardModule.h"
#include "../Headers/TrainSensorModule.h"
#include <stdlib.h>
#include <adc.h>


//#define IS_VALID_PORT(m) ((m>=1 && m <= 8) || (m >=13 && m <= 16))

void ApplyTrainSensorSavedState(TrainSensorState* state, TrainSensorSavedModuledState* savemState);

void SetMeisureVoltage(BYTE module, BYTE port);
HRESULT GetChannel(BYTE module, unsigned char * channel);

HRESULT GetFuncTableTrainSensor(DeviceID * pid, ModuleFuncTable* table)
{
	BYTE i, base;
	
	table->fncreate = CreateTrainSensorState;
	table->fnstore = StoreTrainSensorSavedState;
	table->fninit = InitTrainSensor;
//	table->fnclose = CloseTrainSensor;
	table->fninterrupt = InterruptTrainSensor;
	
	base = (pid->ModulePart-1) * PORT_PIN_COUNT;
	
	setTris(base, INPUT_PIN);
	for(i=1; i< PORT_PIN_COUNT; ++i)
	{
		setTris(base+i, OUTPUT_PIN);
	}
	
	return S_OK;
}

void SetUsingPort(BYTE module, BYTE port)
{	
	BYTE base = (module+1) * PORT_PIN_COUNT;
		
	setPort(base+4, 0);
	if(port > 8)
		return;
	
	setPort(base+1, (port & 0x01));
	setPort(base+2, (port & 0x02) >> 1);
	setPort(base+3, (port & 0x04) >> 2);
	setPort(base+4, 1);
	
	Delay10TCYx(10);
}

void SetMeisureVoltage(BYTE module, BYTE port)
{
	HRESULT convert;
	unsigned char channel;
	convert = GetChannel(module, &channel);
	
	if(convert == E_FAIL)
		return;
	
	while(g_usingAdc);
	g_usingAdc = TRUE;
	SetChanADC(channel);
	SetUsingPort(module, port);
	ConvertADC();
}

HRESULT GetChannel(BYTE module, unsigned char * channel)
{
	#if defined VERSION_REV1
	//valid for module port A, B, D
	switch(module) 
	{
		case 1:
			*channel = ADC_CH0;
			break;
		case 2:
			*channel = ADC_CH4;
			break;
		case 3:
			*channel = ADC_CH8;
			break;
		default:
			return E_FAIL;	
	}
	return S_OK;
	
	#elif defined VERSION_REV2
	
	#endif
}

HRESULT CreateTrainSensorState(DeviceID * pid, PMODULE_DATA data)
{
	unsigned int voltage;
	TrainSensorSavedModuledState romdata;
	TrainSensorState* argdata = (TrainSensorState*)data;
	int romdatasize = sizeof(TrainSensorState);
	unsigned int curTimer;
	BYTE module = pid->ModuleAddr, port = pid->InternalAddr;
	HRESULT res = (port==TRAINSENSOR_INNERMODULE_COUNT-1) ? REPEAT_TERMINATE : 0;
	
	SetMeisureVoltage(module, port);
	
	curTimer = ReadTimer0();
	memset((void *)data, (unsigned char)0x00, (size_t)SIZE_DATA);
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
			res |= S_OK;
		break;
		
		case MODE_TRAINSENSOR_DETECTING:
			if(!GET_TIMER_OCCUPIED(module)
			 && (
			 	voltage <= romdata.ThresholdVoltage
			 	)
			 )
			 {
				 //detecting train
				 SET_TIMER_OCCUPIED(module, 1);
				 argdata->IsDetected = FALSE;
				 //sprintf(data, PGMCSTR("Tm=%u,TmOf=%lu,Dg\n"), curTimer, Timer0OverflowCount);
				 res |= S_OK;
			 }
			 else if(GET_TIMER_OCCUPIED(module) 
			 		&& (
			 			voltage > romdata.ThresholdVoltage
					 	)
					 )
			 {
				 //detected train
				 SET_TIMER_OCCUPIED(module, 0);
				 argdata->IsDetected = TRUE;
				 //sprintf(data, PGMCSTR("Tm=%u,TmOf=%lu,Dd\n"), curTimer, Timer0OverflowCount);
			 	 res |= S_OK;
			 }
		break;
		
		default:
			//maybe missing settingdata
			//strcpypgm2ram(&cmdbuf[0], &defaultCmd[0]);
			memset(&romdata, 0x00, sizeof(romdata));
			romdata.Mode = MODE_TRAINSENSOR_MEISURING;
			WriteTrainSensorSavedModuledState(module, port, &romdata);
			res |= E_FAIL;
		break;
	}
	
	
	return res;
}

void ApplyTrainSensorSavedState(TrainSensorState* state, TrainSensorSavedModuledState* savemState)
{
	state->Mode = savemState->Mode;
	state->ThresholdVoltage = savemState->ThresholdVoltage;
}

HRESULT InitTrainSensor(DeviceID * pid)
{	
	unsigned char channel;
	HRESULT res;
	BYTE module = pid->ModuleAddr;
	
	res = GetChannel(module, &channel);
	if(res != S_OK)
		return E_FAIL;
	
	setTris(module, INPUT_PIN);
	OpenADC(ADC_FOSC_64 & ADC_RIGHT_JUST & ADC_8_TAD,
			channel & ADC_INT_OFF & ADC_VREFPLUS_VDD & ADC_VREFMINUS_VSS,
			0b0000);

	return S_OK;
}

void InterruptTrainSensor(DeviceID * pid)
{
	
}

HRESULT StoreTrainSensorSavedState(DeviceID * pid, PMODULE_DATA buf)
{
	TrainSensorSavedModuledState msaved;
	TrainSensorState* pstate;
	BYTE module = pid->ModulePart;
	
	if(pid->InternalAddr > TRAINSENSOR_INNERMODULE_COUNT)
		return E_FAIL;
		
	ApplyTrainSensorSavedState(pstate, &msaved);
	
	WriteTrainSensorSavedModuledState(module, pid->InternalAddr, &msaved);
	
	Port_SurfaceLedA = 1;
	return S_OK;
}