#include "../Headers/MotherBoardModule.h"
#include "../Headers/TrainSensorModule.h"
#include <stdlib.h>
#include <adc.h>

//#define IS_VALID_PORT(m) ((m>=1 && m <= 8) || (m >=13 && m <= 16))

void ApplyTrainSensorSavedState(TrainSensorState* state, TrainSensorSavedState* saveSate);

void SetMeisureVoltage(BYTE module);
HRESULT GetChannel(BYTE module, unsigned char * channel);

HRESULT GetFuncTableTrainSensor(BYTE module, ModuleFuncTable* table)
{
	table->fncreate = CreateTrainSensorState;
	table->fnstore = StoreTrainSensorSavedState;
	table->fninit = InitTrainSensor;
//	table->fnclose = CloseTrainSensor;
	table->fninterrupt = InterruptTrainSensor;
	
	return S_OK;
}

void SetMeisureVoltage(BYTE module)
{
	HRESULT convert;
	unsigned char channel;
	convert = GetChannel(module, &channel);
	
	if(convert == E_FAIL)
		return;
		
	SetChanADC(channel);
	ConvertADC();
}

HRESULT GetChannel(BYTE module, unsigned char * channel)
{
	//valid for module port A, B, D
	switch(module) 
	{
		case 1:
			*channel = ADC_CH0;
			break;
		case 2:
			*channel = ADC_CH1;
			break;
		case 3:
			*channel = ADC_CH2;
			break;
		case 4:
			*channel = ADC_CH3;
			break;
		case 5:
			*channel = ADC_CH4;
			break;
		case 6:
			*channel = ADC_CH5;
			break;
		case 7:
			*channel = ADC_CH6;
			break;
		case 8:
			*channel = ADC_CH7;
			break;
		case 13:
			*channel = ADC_CH8;
			break;
		case 14:
			*channel = ADC_CH9;
			break;
		case 15:
			*channel = ADC_CH10;
			break;
		case 16:
			*channel = ADC_CH11;
			break;
		default:
			return E_FAIL;	
	}
	return S_OK;
}

HRESULT CreateTrainSensorState(BYTE module, PMODULE_DATA data)
{
	int decVoltage, flacVoltage;
	unsigned int voltage;
	TrainSensorSavedState romdata;
	TrainSensorState* argdata = (TrainSensorState*)data;
	int romdatasize = sizeof(TrainSensorState);
	unsigned int curTimer;
	
	SetMeisureVoltage(module);
	
	curTimer = ReadTimer0();
	memset((void *)data, (unsigned char)0x00, (size_t)SIZE_DATA);
	ReadModuleSavedState(module, &romdata);
	ApplyTrainSensorSavedState(argdata, &romdata);
	
	// take care of the size of data
	argdata->Timer = curTimer;
	argdata->OverflowedCount = Timer0OverflowCount;
	argdata->ReferenceVoltageMinus = 0;
	argdata->ReferenceVoltagePlus = 5;
	argdata->VoltageResolution = 10;
	
	while(BusyADC());
	voltage = ReadADC();
	
	argdata->CurrentVoltage = voltage;
	
	switch(romdata.Mode)
	{
		case MODE_TRAINSENSOR_MEISURING:
			//sprintf(data, PGMCSTR("Tm=%u,V=%u.%u\n"), curTimer, decVoltage, flacVoltage);
			return S_OK;
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
				 return S_OK;
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
			 	 return S_OK;
			 }
		break;
		
		default:
			//maybe missing settingdata
			//strcpypgm2ram(&cmdbuf[0], &defaultCmd[0]);
			memset(&romdata, 0x00, sizeof(romdata));
			romdata.Mode = MODE_TRAINSENSOR_MEISURING;
			WriteModuleSavedState(module, &romdata);
		break;
	}
	
	return E_FAIL;
}

void ApplyTrainSensorSavedState(TrainSensorState* state, TrainSensorSavedState* saveState)
{
	state->Mode = saveState->Mode;
	state->ThresholdVoltage = saveState->ThresholdVoltage;
}

HRESULT InitTrainSensor(BYTE module)
{	
	unsigned char channel;
	HRESULT res;
	
	res = GetChannel(module, &channel);
	if(res != S_OK)
		return E_FAIL;
	
	setTris(module, INPUT_PIN);
	OpenADC(ADC_FOSC_64 & ADC_RIGHT_JUST & ADC_8_TAD,
			channel & ADC_INT_OFF & ADC_VREFPLUS_VDD & ADC_VREFMINUS_VSS,
			0b0011);

	return S_OK;
}

void InterruptTrainSensor(BYTE module)
{
	
}

HRESULT StoreTrainSensorSavedState(BYTE module, PMODULE_DATA buf)
{
	TrainSensorSavedState saved;
	TrainSensorState* pstate;
	
	saved.Mode = pstate->Mode;
	saved.ThresholdVoltage = pstate->ThresholdVoltage;
	
	WriteModuleSavedState(module, &saved);
	
	return S_OK;
}