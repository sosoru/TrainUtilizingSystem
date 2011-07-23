#include "HardwareProfile.h"
#include "../Headers/TrainController.h"
#include "../Headers/PortMapping.h"
#include <timers.h>
#include <pwm.h>
#include <adc.h>

#define VOLTAGE_RESOLUTION_BITCOUNT 10
#define VOLTAGE_RESOLUTION_BITMASK 0x03FF
#define DUTY_RESOLUTION_BITCOUNT 10
#define DUTY_RESOLUTION_BITMASK 0x03FF
#define PRESCALE_DEFAULT T2_PS_1_1

#ifdef VERSION_REV1

#define FEEDBACK_CHANNEL 13

#define PORT_DIRECTION PORT_PORTC_A
#define PORT_FEEDBACK PORT_PORTC_B
#define PORT_FREE PORT_PORTC_C
#define PORT_PWMSIGNAL PORT_PORTC_D

#define TRIS_DIRECTION TRIS_PORTC_A
#define TRIS_FEEDBACK TRIS_PORTC_B
#define TRIS_FREE TRIS_PORTC_C
#define TRIS_PWMSIGNAL TRIS_PORTC_D

#endif

//single module in a device
TrainControllerState g_cacheState;
BYTE settingState = FALSE;
BYTE isMeisuring = FALSE;
BYTE waitingCount = 0;

void SetPWM();
void ChangePWM();
void ChangeTimer();

HRESULT GetFuncTableTrainController(BYTE module, ModuleFuncTable* table)
{
	table->fncreate = CreateTrainControllerState;
	table->fnstore = StoreTrainControllerState;
	table->fninit = InitTrainController;
	table->fninterrupt = InterruptTrainController;
	
	return S_OK;	
}

HRESULT InitTrainController(BYTE module)
{
	settingState = TRUE;
	
	memset(&g_cacheState, 0x00, (size_t)sizeof(g_cacheState)); 
	
	g_cacheState.duty = 0;
	g_cacheState.dutyEnabledBits = DUTY_RESOLUTION_BITCOUNT;
	g_cacheState.frequency = CLOCK_FREQ / 1000000;
	g_cacheState.period = 0xFF;
	g_cacheState.prescale = PRESCALE_DEFAULT;
	g_cacheState.direction = DIRECTION_TRAINCONTROLLER_NEGATIVE;
	
	g_cacheState.mode = MODE_TRAINCONTROLLER_DUTY;
	g_cacheState.voltage = 0;
	g_cacheState.voltageEnabledBits = VOLTAGE_RESOLUTION_BITCOUNT;
	
	TRIS_FEEDBACK = INPUT_PIN;
	
	OpenADC(ADC_FOSC_64 & ADC_RIGHT_JUST & ADC_8_TAD,
		FEEDBACK_CHANNEL & ADC_INT_ON & ADC_VREFPLUS_VDD & ADC_VREFMINUS_VSS,
		0b0000);
				
	ChangeTimer();
	ChangePWM();
	SetPWM();
	
	settingState =FALSE;
	return S_OK;
}

HRESULT CreateTrainControllerState(BYTE module, PMODULE_DATA data)
{
	TrainControllerState * pstate = (TrainControllerState *)data;
	
	memcpy(pstate, &g_cacheState, (size_t)sizeof(TrainControllerState));
	return S_OK;
}
	
HRESULT StoreTrainControllerState(BYTE module, PMODULE_DATA data)
{
	TrainControllerState * pstate = (TrainControllerState *) data;
	BYTE periodChanged=FALSE, prescaleChanged=FALSE, dutyChanged=FALSE,
	     modeChanged=FALSE, voltageChanged=FALSE;
	     
	settingState = TRUE;     
	 
	if(g_cacheState.mode != pstate->mode)
	{
		if(pstate->mode > 1)
			g_cacheState.mode = MODE_TRAINCONTROLLER_DUTY;
		else
			g_cacheState.mode = MODE_TRAINCONTROLLER_FOLLOWING;
		
		modeChanged = TRUE;
	}
	
	if(g_cacheState.voltage != pstate->voltage)
	{
		g_cacheState.voltage = pstate->voltage & VOLTAGE_RESOLUTION_BITMASK;
		voltageChanged = TRUE;
	}
	
	if(g_cacheState.prescale != pstate->prescale)
	{
		if(~(pstate->prescale) != 0)
			g_cacheState.prescale = pstate->prescale;
		else
			g_cacheState.prescale = PRESCALE_DEFAULT;
		
		prescaleChanged = TRUE;
	}
	
	if(g_cacheState.period != pstate->period
		|| g_cacheState.direction != pstate->direction)
	{		
		g_cacheState.period = pstate->period;
			
		if(pstate->direction > 1)
			g_cacheState.direction = DIRECTION_TRAINCONTROLLER_POSITIVE;
		else
			g_cacheState.direction = pstate->direction;
		
		periodChanged = TRUE;
	}
	
	if(g_cacheState.duty != pstate->duty)
	{
		g_cacheState.duty = pstate->duty & DUTY_RESOLUTION_BITMASK;
		dutyChanged = TRUE;
	}
	
	if(prescaleChanged)
	{
		ChangeTimer();
	}
	
	if(periodChanged)
	{
		ChangePWM();
	}
	
	if(dutyChanged || periodChanged)
	{
		if(g_cacheState.mode == MODE_TRAINCONTROLLER_DUTY)
			SetPWM();
	}
	
	settingState = FALSE;
	
}

void InterruptTrainController(BYTE module)
{
	if(settingState && 
		!g_usingAdc &&
		g_cacheState.mode == MODE_TRAINCONTROLLER_FOLLOWING)
	{
		BYTE i;
		BYTE meisuringCount= 5;
		unsigned int AveVoltage =0;
		
		if(++waitingCount < 100)
			return;
			
		if(isMeisuring)
		{
			isMeisuring = 1;
			PORT_FREE = 1;
		}
		else
		{
			g_usingAdc = TRUE;
			SetChanADC(FEEDBACK_CHANNEL);
			for(i=0; i<meisuringCount; ++i)
			{
				ConvertADC();
				while(BusyADC());		
				AveVoltage+= ReadADC();
			}
			g_usingAdc = FALSE;
			AveVoltage /= meisuringCount;
			
			if(AveVoltage > g_cacheState.voltage
				&& g_cacheState.duty > 0 )
			{
				g_cacheState.duty--;
				SetPWM();
			}
			else if(AveVoltage < g_cacheState.voltage
					&& g_cacheState.duty < VOLTAGE_RESOLUTION_BITMASK)
			{
				g_cacheState.duty++;
				SetPWM();
			}
			
			PORT_FREE = 0;
			waitingCount = 0;	
		}
		
	}
}

void ChangeTimer()
{
	CloseTimer2();
	OpenTimer2(TIMER_INT_OFF & g_cacheState.prescale);	
}

void ChangePWM()
{
	OpenPWM1(g_cacheState.period);
}

void SetPWM()
{
	SetDCPWM1(g_cacheState.duty);
}
