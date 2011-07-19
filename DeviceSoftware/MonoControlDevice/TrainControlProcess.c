#include "MonoDevice.h"
#include <timers.h>
#include <pwm.h>

#define DUTY_RESOLUTION_BITCOUNT 10
#define DUTY_RESOLUTION_BITMASK 0x03FF
#define PRESCALE_DEFAULT T2_PS_1_1

//single module in a device
TrainControllerState g_cacheState;

void SetPWM();
void ChangePWM();
void ChangeTimer();

HRESULT GetFuncTableTrainController(BYTE module, ModuleFuncTable* table)
{
	table->fncreate = CreateTrainControllerState;
	table->fnstore = StoreTrainControllerState;
	table->fninit = InitTrainController;
	table->fninterrupt = InterruptEmpty;
	
	return S_OK;	
}

HRESULT InitTrainController(BYTE module)
{
	memset(&g_cacheState, 0x00, (size_t)sizeof(g_cacheState)); 
	
	g_cacheState.duty = 0;
	g_cacheState.dutyEnabledBits = DUTY_RESOLUTION_BITCOUNT;
	g_cacheState.frequency = CLOCK_FREQ / 1000000;
	g_cacheState.period = 0xFF;
	g_cacheState.prescale = PRESCALE_DEFAULT;
	g_cacheState.direction = DIRECTION_TRAINCONTROLLER_NEGATIVE;
	
	ChangeTimer();
	ChangePWM();
	SetPWM();
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
	BYTE periodChanged=FALSE, prescaleChanged=FALSE, dutyChanged=FALSE;
	
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
		SetPWM();
	}
	
}

void ChangeTimer()
{
	CloseTimer2();
	OpenTimer2(TIMER_INT_OFF & g_cacheState.prescale);	
}

void ChangePWM()
{
		
// PWM output is alternative
//if (PORTC_C == PWM)
//{
//	PORTC_A = 1
//	PORTC_B = 0
//} else if(PORTC_D == PWM)
//{
//	PORTC_A = 0
//	PORTC_B = 1
//}
		if(g_cacheState.direction == DIRECTION_TRAINCONTROLLER_POSITIVE)
		{
			ClosePWM2();
			TRIS_PORTC_B = INPUT_PIN;
			TRIS_PORTC_A = OUTPUT_PIN;
			PORT_PORTC_A = 0;
			OpenPWM1(g_cacheState.period);
		}
		else if (g_cacheState.direction == DIRECTION_TRAINCONTROLLER_NEGATIVE)
		{
			ClosePWM1();
			TRIS_PORTC_A = INPUT_PIN;
			TRIS_PORTC_B = OUTPUT_PIN;
			PORT_PORTC_B = 0;
			OpenPWM2(g_cacheState.period);
		}
}

void SetPWM()
{
		if(g_cacheState.direction == DIRECTION_TRAINCONTROLLER_POSITIVE)
			SetDCPWM1(g_cacheState.duty);
		else if(g_cacheState.direction == DIRECTION_TRAINCONTROLLER_NEGATIVE)
			SetDCPWM2(g_cacheState.duty);	
}
