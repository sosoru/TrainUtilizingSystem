#include "HardwareProfile.h"
#include "../Headers/TrainController.h"
#include "../Headers/PortMapping.h"
#include <stdlib.h>
#include <string.h>
#include <timers.h>
#include <delays.h>
#include <pwm.h>
#include <adc.h>

#define VOLTAGE_RESOLUTION_BITCOUNT 10
#define VOLTAGE_RESOLUTION_BITMASK 0x03FF
#define DUTY_RESOLUTION_BITCOUNT 10
#define DUTY_RESOLUTION_BITMASK 0x03FF
#define PRESCALE_DEFAULT T2_PS_1_1

#if defined VERSION_REV2

#define FEEDBACK_CHANNELA ADC_CH5
#define FEEDBACK_CHANNELB ADC_CH6

#define PORT_DIRECTION_POS PORTDbits.RD0
#define PORT_DIRECTION_NEG PORTCbits.RC0
#define PORT_FEEDBACKA PORTEbits.RE1
#define PORT_FEEDBACKB PORTEbits.RE0
#define PORT_PWMSIGNALB PORTCbits.RC1
#define PORT_PWMSIGNALA PORTCbits.RC2

#define TRIS_DIRECTION_POS TRISDbits.TRISD0
#define TRIS_DIRECTION_NEG TRISCbits.TRISC0
#define TRIS_FEEDBACKA TRISEbits.TRISE1
#define TRIS_FEEDBACKB TRISEbits.TRISE0
#define TRIS_PWMSIGNALB TRISCbits.TRISC1
#define TRIS_PWMSIGNALA TRISCbits.TRISC2

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


void ChangeTimer()
{
	OpenTimer2(TIMER_INT_OFF & g_cacheState.prescale);	
}

void ChangePWM()
{
	if(g_cacheState.direction == DIRECTION_TRAINCONTROLLER_POSITIVE)
	{
		ClosePWM2();
#if defined VERSION_REV2
		
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

#if defined VERSION_REV2
		
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
	
	memset((void*)&g_cacheState, 0x00, (size_t)sizeof(g_cacheState)); 
	
	g_cacheState.duty = 0;
	g_cacheState.dutyEnabledBits = DUTY_RESOLUTION_BITCOUNT;
	g_cacheState.frequency = CLOCK_FREQ / 1000000;
	g_cacheState.period = 0xFF;
	g_cacheState.prescale = PRESCALE_DEFAULT;
	g_cacheState.direction = DIRECTION_TRAINCONTROLLER_NEGATIVE;
	
	g_cacheState.mode = MODE_TRAINCONTROLLER_DUTY;
	g_cacheState.voltage = 0;
	g_cacheState.voltageEnabledBits = VOLTAGE_RESOLUTION_BITCOUNT;
	g_cacheState.meisuredvoltage = 0;
	
	g_cacheState.paramp = 1.0f;
	g_cacheState.parami = 0.0f;
//	g_cacheState.paramd = 0.0f;
			
#if defined VERSION_REV2

	TRIS_DIRECTION_POS = OUTPUT_PIN;
	TRIS_DIRECTION_NEG = OUTPUT_PIN;
	TRIS_FEEDBACKA = INPUT_PIN;
	TRIS_FEEDBACKB = INPUT_PIN;
	TRIS_PWMSIGNALA = OUTPUT_PIN;
	TRIS_PWMSIGNALB = OUTPUT_PIN;
	
	OpenADC(ADC_FOSC_64 & ADC_RIGHT_JUST & ADC_8_TAD,
	FEEDBACK_CHANNELA & ADC_INT_OFF & ADC_VREFPLUS_VDD & ADC_VREFMINUS_VSS,
	AD_PORT);
	
	OpenADC(ADC_FOSC_64 & ADC_RIGHT_JUST & ADC_8_TAD,
	FEEDBACK_CHANNELB & ADC_INT_OFF & ADC_VREFPLUS_VDD & ADC_VREFMINUS_VSS,
	AD_PORT);

#endif
				
	ChangeTimer();
	ChangePWM();
	SetPWM();
	
	settingState =FALSE;
	return S_OK;
}

HRESULT CreateTrainControllerState(DeviceID * pid, PMODULE_DATA data)
{
	TrainControllerState * pstate = (TrainControllerState *)data;
	
	memcpy((void*)pstate, (void*)&g_cacheState, (size_t)sizeof(TrainControllerState));
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
	//g_cacheState.paramd = pstate->paramd;
	 
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
	if(settingState || g_usingAdc)
		return;
		
	BYTE i;
	unsigned int meisuringCount= 10;
	unsigned int AveVoltage =0;
	int df=0;
	
	if(++waitingCount < 2)
		return;
	
	ClosePWM1();	
	ClosePWM2();
		
	#if defined VERSION_REV2
	{
		unsigned int tmpvoltA=0, tmpvoltB=0;
		
		PORT_DIRECTION_POS = 1;
		PORT_DIRECTION_NEG = 1;
		
		Delay1KTCYx(20);	
		
		g_usingAdc = TRUE;
		SetChanADC(FEEDBACK_CHANNELA);
		for(i=0; i<meisuringCount; ++i)
		{
			ConvertADC();
			while(BusyADC());
			tmpvoltA += ReadADC();
		}
		tmpvoltA /= meisuringCount;
		
		SetChanADC(FEEDBACK_CHANNELB);
		for(i=0; i<meisuringCount; ++i)
		{
			ConvertADC();
			while(BusyADC());
			tmpvoltB += ReadADC();
		}
		tmpvoltB /= meisuringCount;
		
		g_usingAdc = FALSE;
		
		g_cacheState.meisuredvoltage = tmpvoltA;
		g_cacheState.meisuredvoltage2 = tmpvoltB;
		//AveVoltage = (tmpvoltA > tmpvoltB) ? tmpvoltA : tmpvoltB;
	}
	#endif
		
	if(g_cacheState.mode == MODE_TRAINCONTROLLER_FOLLOWING)
	{
		if(AveVoltage == 1023) // if train is stopping, the bemf sticks to 0 or 1023
			AveVoltage = 0;
			
		df = ((int)(g_cacheState.voltage)) - ((int)AveVoltage);
		
		internal_duty += (g_cacheState.paramp * ((float)(df - voltageDif_1))  
					   + g_cacheState.parami * ((float)df)) ;
					   //+ g_cacheState.paramd * ((float)((voltageDif_1 - df) - (voltageDif_2 - voltageDif_1))));
		
		voltageDif_2 = voltageDif_1;
		voltageDif_1 = df;
		
		if(internal_duty < 0.0f)
			internal_duty = 0.0f;
		else if (internal_duty > 800.0f)
			internal_duty = 800.0f;
		
		g_cacheState.duty = (unsigned int)internal_duty;
		
	}
	ChangePWM();
	
	//g_cacheState.meisuredvoltage = AveVoltage;
	waitingCount = 0;
		
}
