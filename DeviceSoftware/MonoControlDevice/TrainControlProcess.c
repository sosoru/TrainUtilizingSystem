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

#define FEEDBACK_CHANNEL ADC_CH12

#define PORT_DIRECTION PORT_PORTC_A
#define PORT_FEEDBACK PORT_PORTC_B
#define PORT_PWMSIGNALB PORT_PORTC_C
#define PORT_PWMSIGNALA PORT_PORTC_D

#define TRIS_DIRECTION TRIS_PORTC_A
#define TRIS_FEEDBACK TRIS_PORTC_B
#define TRIS_PWMSIGNALB TRIS_PORTC_C
#define TRIS_PWMSIGNALA TRIS_PORTC_D

#elif VERSION_REV2

#define FEEDBACK_CHANNEL ADC_CH12

#define PORT_DIRECTION_POS PORTDbits.RD3
#define PORT_DIRECTION_NEG PORTDbits.RD2
#define PORT_FEEDBACK PORTBbits.RB0
#define PORT_PWMSIGNALB PORTCbits.RC1
#define PORT_PWMSIGNALA PORTCbits.RC2

#endif


#define STACK_SIZE 10

//single module in a device
TrainControllerState g_cacheState;
BYTE settingState = FALSE;
BYTE waitingCount = 0;

int voltageDif_1 = 0;
int voltageDif_2 = 0;
float internal_duty = 0.0f;

void SetPWM();
void ChangePWM();
void ChangeTimer();

HRESULT GetFuncTableTrainController(DeviceID * pid, ModuleFuncTable* table)
{
	table->fncreate = CreateTrainControllerState;
	table->fnstore = StoreTrainControllerState;
	table->fninit = InitTrainController;
	table->fninterrupt = InterruptTrainController;
	
	return S_OK;	
}

HRESULT InitTrainController(DeviceID * pid)
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
	
	g_cacheState.paramp = 1.0f;
	g_cacheState.parami = 0.0f;
	g_cacheState.paramd = 0.0f;
	
	TRIS_DIRECTION = OUTPUT_PIN;
	TRIS_FEEDBACK = INPUT_PIN;
	TRIS_PWMSIGNALA = OUTPUT_PIN;
	TRIS_PWMSIGNALB = OUTPUT_PIN;
	
	OpenADC(ADC_FOSC_64 & ADC_RIGHT_JUST & ADC_8_TAD,
		FEEDBACK_CHANNEL & ADC_INT_OFF & ADC_VREFPLUS_VDD & ADC_VREFMINUS_VSS,
		0b0000);
				
	ChangeTimer();
	ChangePWM();
	SetPWM();
	
	settingState =FALSE;
	return S_OK;
}

HRESULT CreateTrainControllerState(DeviceID * pid, PMODULE_DATA data)
{
	TrainControllerState * pstate = (TrainControllerState *)data;
	
	memcpy(pstate, &g_cacheState, (size_t)sizeof(TrainControllerState));
	return S_OK | REPEAT_TERMINATE;
}
	
HRESULT StoreTrainControllerState(DeviceID * pid, PMODULE_DATA data)
{
	TrainControllerState * pstate = (TrainControllerState *) data;
	BYTE periodChanged=FALSE, prescaleChanged=FALSE, dutyChanged=FALSE,
	     modeChanged=FALSE, voltageChanged=FALSE;
	     
	settingState = TRUE;     
	
	g_cacheState.paramp = pstate->paramp;
	g_cacheState.parami = pstate->parami;
	g_cacheState.paramd = pstate->paramd;
	 
	if(g_cacheState.mode != pstate->mode)
	{
		if(pstate->mode > 1)
			g_cacheState.mode = MODE_TRAINCONTROLLER_DUTY;
		else
			g_cacheState.mode = pstate->mode;
		
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
	
	if(prescaleChanged || modeChanged)
	{
		ChangeTimer();
	}
	
	if(periodChanged || modeChanged)
	{
		ChangePWM();
	}
	
	if(dutyChanged || periodChanged || modeChanged)
	{
		SetPWM();
	}
	
	settingState = FALSE;
	
}

void InterruptTrainController(DeviceID * pid)
{
	if(!settingState && 
		!g_usingAdc)
	{
		BYTE i;
		unsigned int meisuringCount= 10;
		unsigned int AveVoltage =0;
		int df=0, tmpduty;
		
		if(++waitingCount < 2)
			return;
		
		tmpduty = g_cacheState.duty;
		g_cacheState.duty = 0;
		SetPWM();
		
		Delay1KTCYx(4);
		
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
		
		if(g_cacheState.mode == MODE_TRAINCONTROLLER_FOLLOWING)
		{
			df = g_cacheState.voltage - AveVoltage;
			
			internal_duty += (g_cacheState.paramp * ((float)(df - voltageDif_1))  
						   + g_cacheState.parami * ((float)df) 
						   + g_cacheState.paramd * ((float)((voltageDif_1 - df) - (voltageDif_2 - voltageDif_1))));
			
			voltageDif_2 = voltageDif_1;
			voltageDif_1 = df;
			
			if(internal_duty < 0.0f)
				g_cacheState.duty = 0;
			else if (internal_duty > 800.0f)
				g_cacheState.duty = 800;
			else
				g_cacheState.duty = (unsigned int)internal_duty;
			
		}
		else
		{
			g_cacheState.duty = tmpduty;
		}
		SetPWM();
		
		g_cacheState.meisuredvoltage = AveVoltage;
		waitingCount = 0;
		
	}
}

void ChangeTimer()
{
	OpenTimer2(TIMER_INT_OFF & g_cacheState.prescale);	
}

void ChangePWM()
{
	if(g_cacheState.direction == DIRECTION_TRAINCONTROLLER_POSITIVE)
	{
		ClosePWM2();
#ifdef VERSION_REV1

		PORT_PWMSIGNALB = 0;
		PORT_DIRECTION = 0;

#elif VERSION_REV2
		
		PORT_PWMSIGNALB = 0;
		PORT_DIRECTION_NEG = 0;
		PORT_DIRECTION_POS = 1;
		
#endif
		OpenPWM1(g_cacheState.period);
		SetDCPWM1(g_cacheState.duty);
	}
	else
	{
		ClosePWM1();
#ifdef VERSION_REV1

		PORT_PWMSIGNALA = 0;
		PORT_DIRECTION = 1;

#elif VERSION_REV2
		
		PORT_PWMSIGNALA = 0;
		PORT_DIRECTION_POS = 0;
		PORT_DIRECTION_NEG = 1;

#endif		
		OpenPWM2(g_cacheState.period);
		SetDCPWM2(g_cacheState.duty);
	}
}

void SetPWM()
{
	if(g_cacheState.direction == DIRECTION_TRAINCONTROLLER_POSITIVE)
	{
		SetDCPWM1(g_cacheState.duty);
	}
	else
	{
		SetDCPWM2(g_cacheState.duty);
	}
}
