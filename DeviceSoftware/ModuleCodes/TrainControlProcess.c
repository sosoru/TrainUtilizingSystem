#include "HardwareProfile.h"
#include "../Headers/TrainController.h"
#include "../Headers/PortMapping.h"
#include <stdlib.h>
#include <math.h>
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

#define FEEDBACK_CHANNEL_A ADC_CH3
#define FEEDBACK_CHANNEL_B ADC_CH4
#define DUTYCONTROL_CHANNEL ADC_CH5

#define PORT_DIRECTION_A LATEbits.LATE1
#define PORT_DIRECTION_B LATEbits.LATE2
#define PORT_FEEDBACK_A PORTAbits.RA3
#define PORT_FEEDBACK_B PORTAbits.RA5
#define PORT_PWMSIGNAL_B PORTCbits.RC1
#define PORT_PWMSIGNAL_A PORTCbits.RC2
#define PORT_DUTYCONTROL PORTEbits.RE0
#define PORT_CONTROLSWITCH PORTAbits.RA4
#define PORT_DIRECTION PORTCbits.RC0

#define TRIS_DIRECTION_A TRISEbits.TRISE1
#define TRIS_DIRECTION_B TRISEbits.TRISE2
#define TRIS_FEEDBACK_A TRISAbits.TRISA3
#define TRIS_FEEDBACK_B TRISAbits.TRISA5
#define TRIS_PWMSIGNAL_B TRISCbits.TRISC1
#define TRIS_PWMSIGNAL_A TRISCbits.TRISC2
#define TRIS_DUTYCONTROL TRISEbits.TRISE0
#define TRIS_CONTROLSWITCH TRISAbits.TRISA4
#define TRIS_DIRECTION TRISCbits.TRISC0

#endif

#define STACK_SIZE 10

//single module in a device
TrainControllerState g_cacheState;
BYTE settingState = FALSE;
BYTE creatingState = FALSE;
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
		
		PORT_PWMSIGNAL_B = 0;
		PORT_DIRECTION_B = 0;
		
		PORT_DIRECTION_A = 1;
		
#endif
		OpenPWM1(g_cacheState.period);
		SetDCPWM1(g_cacheState.duty);
	}
	else
	{
		ClosePWM1();

#if defined VERSION_REV2
		
		PORT_PWMSIGNAL_A = 0;
		PORT_DIRECTION_A = 0;
		
		PORT_DIRECTION_B = 1;

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
	g_cacheState.meisuredvoltage2 = 0;
	
	g_cacheState.paramp = 70;
	g_cacheState.parami = 60;
	g_cacheState.paramd = 0;
			
#if defined VERSION_REV2

	TRIS_DIRECTION_A = OUTPUT_PIN;
	TRIS_DIRECTION_B = OUTPUT_PIN;
	TRIS_FEEDBACK_A = INPUT_PIN;
	TRIS_FEEDBACK_B = INPUT_PIN;
	TRIS_PWMSIGNAL_A = OUTPUT_PIN;
	TRIS_PWMSIGNAL_B = OUTPUT_PIN;
	TRIS_DUTYCONTROL = INPUT_PIN;
	TRIS_CONTROLSWITCH = INPUT_PIN;
	TRIS_DIRECTION = INPUT_PIN;
	
	OpenADC(ADC_FOSC_64 & ADC_RIGHT_JUST & ADC_8_TAD,
	FEEDBACK_CHANNEL_A & ADC_INT_OFF & ADC_VREFPLUS_VDD & ADC_VREFMINUS_VSS,
	AD_PORT);
	
	OpenADC(ADC_FOSC_64 & ADC_RIGHT_JUST & ADC_8_TAD,
	FEEDBACK_CHANNEL_B & ADC_INT_OFF & ADC_VREFPLUS_VDD & ADC_VREFMINUS_VSS,
	AD_PORT);
	
	OpenADC(ADC_FOSC_64 & ADC_RIGHT_JUST & ADC_8_TAD,
	DUTYCONTROL_CHANNEL & ADC_INT_OFF & ADC_VREFPLUS_VDD & ADC_VREFMINUS_VSS,
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
	
	creatingState = TRUE;
	
	memcpy((void*)pstate, (void*)&g_cacheState, (size_t)sizeof(TrainControllerState));
	
	creatingState = FALSE;
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

	return S_OK;	
}

void InterruptTrainController(DeviceID * pid)
{
	BYTE i;
	unsigned int meisuringCount= 5;
	 int AveVoltage =0;
	int df=0;

	if(creatingState || settingState || g_usingAdc)
		return;	
	
	if(++waitingCount < 10)
		return;
			
	ClosePWM1();	
	ClosePWM2();
		
	#if defined VERSION_REV2
	{
		g_cacheState.meisuredvoltage = 0;
		g_cacheState.meisuredvoltage2 = 0;
		
		PORT_DIRECTION_A = 1;
		PORT_DIRECTION_B = 1;
		
		Delay1KTCYx(1);	
		
		g_usingAdc = TRUE;
		
		for(i=0; i<meisuringCount; ++i)
		{
		
			SetChanADC(FEEDBACK_CHANNEL_B);
			ConvertADC();
			while(BusyADC());
			g_cacheState.meisuredvoltage2 += ReadADC();
			
			SetChanADC(FEEDBACK_CHANNEL_A);
			ConvertADC();
			while(BusyADC());
			g_cacheState.meisuredvoltage += ReadADC();
		}
		
		g_cacheState.meisuredvoltage /= meisuringCount;		
		g_cacheState.meisuredvoltage2 /= meisuringCount;
		
		g_usingAdc = FALSE;
		
		AveVoltage = (int)g_cacheState.meisuredvoltage - (int)g_cacheState.meisuredvoltage2;
		if(AveVoltage<0)
			AveVoltage = -AveVoltage;
		
	}
	#endif
				
	if (PORT_CONTROLSWITCH)
	{
		unsigned int vol;
				
		g_usingAdc = TRUE;
		
		SetChanADC(DUTYCONTROL_CHANNEL);
		ConvertADC();
		while(BusyADC());
		vol = (~ReadADC()) & 0x3f0;
		
		g_usingAdc = FALSE;
		
		if(vol < 150)
			vol = 0;
		else
			vol -= 150;
		
		g_cacheState.voltage = vol;
		g_cacheState.direction = PORT_DIRECTION;
		g_cacheState.mode = MODE_TRAINCONTROLLER_ONDEVICE;
		
		 
	}

	 if(PORT_CONTROLSWITCH || g_cacheState.mode == MODE_TRAINCONTROLLER_FOLLOWING)
	{
		if(AveVoltage == 1023) // if train is stopping, the bemf sticks to 0 or 1023
			AveVoltage = 0;
			
		df = ((int)(g_cacheState.voltage)) - ((int)AveVoltage);
		
		internal_duty += ((((float)g_cacheState.paramp) / 255.0f) * ((float)(df - voltageDif_1)) ) 
					   + ((((float)g_cacheState.parami) / 255.0f) * ((float)df)) 
					   + ((((float)g_cacheState.paramd) / 255.0f) * ((float)((voltageDif_1 - df) - (voltageDif_2 - voltageDif_1))));
		
		if(internal_duty < 0.0f)
			internal_duty = 0.0f;
		else if (internal_duty > 800.0f)
			internal_duty = 800.0f;

		voltageDif_2 = voltageDif_1;
		voltageDif_1 = df;
				
		g_cacheState.duty = (unsigned int)internal_duty;				 
	}
	
	
	ChangePWM();
	
	//g_cacheState.meisuredvoltage = AveVoltage;
	waitingCount = 0;
		
}
