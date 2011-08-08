#include "ControllerDevice.h"

 /** Configuration Bits *******************************************/
 #pragma config PLLDIV = 5           //Configure for 20Mhz crystal, 20/5 = 4Mhz (required for USB)
 #pragma config CPUDIV = OSC1_PLL2   //Use 48MHz clock for system clock = 96MHz/2 = 48MHz
 #pragma config USBDIV = 2           //USB Clock source from the 96 MHz PLL divided by 2
 #pragma config FOSC = HSPLL_HS      //Oscillator Selection bits - HS oscillator, PLL enabled, HS used by USB
 #pragma config FCMEN = OFF          //Fail-Safe Clock Monitor Enable bit: Disable
 #pragma config IESO = OFF           //Internal/External Oscillator Switchover bit: Disable
 #pragma config PWRT = ON            //Power Up timer enabled
 #pragma config BOR = SOFT           //Brown-out Reset enabled and controlled by software (SBOREN is enabled)
 #pragma config BORV = 2             //Brown-out voltage = 2.7V
 #pragma config VREGEN = ON          //USB Voltage regulator enabled
 #pragma config WDT = OFF            //Watchdog timer HW Disabled - SW Controlled
 #pragma config WDTPS = 128          //Watchdog timer = 512ms (128 x 4ms)
 #pragma config MCLRE = ON           //Master clear enabled
 #pragma config LPT1OSC = OFF        //Disabled
 #pragma config PBADEN = OFF         //Port B0-4 are digital inputs on reset
 #pragma config CCP2MX = ON          //RC1
 #pragma config STVREN = ON          //Stack overflow reset enable
 #pragma config LVP = OFF            //Low voltage programming disabled
 #pragma config ICPRT = OFF          //Dedicated In-Circuit Debug/Programming Port (ICPORT) disabled
 #pragma config XINST = OFF          //Extended mode off
 #pragma config DEBUG = OFF          //Debug mode off
 #pragma config CP0 = OFF 
 #pragma config CP1 = OFF 
 #pragma config CP2 = OFF 
 #pragma config CP3 = OFF 
 #pragma config CPB = OFF 
 #pragma config CPD = OFF 
 #pragma config WRT0 = OFF 
 #pragma config WRT1 = OFF 
 #pragma config WRT2 = OFF 
 #pragma config WRT3 = OFF 
 #pragma config WRTB = ON            //Table write protect bootloader code 
 #pragma config WRTC = OFF 
 #pragma config WRTD = OFF 
 #pragma config EBTR0 = OFF 
 #pragma config EBTR1 = OFF 
 #pragma config EBTR2 = OFF 
 #pragma config EBTR3 = OFF 
 #pragma config EBTRB = OFF
 
#pragma code HIGH_VECTOR = 0x08
void high_interrupt()
{ _asm GOTO high_isr _endasm}
#pragma code

#pragma interrupt high_isr
void high_isr()
{
	BYTE i;
	DeviceID id;
	
	if(PIR2bits.TMR3IF)
	{
		PIE2bits.TMR3IE = 0;
		PIR2bits.TMR3IF = 0;
		
		TMR1H |= ~TMR1H;
		id.ParentPart = g_mbState.ParentId;
		for(i = 0; i < MODULE_COUNT; ++i)
		{
			id.ModulePart = i;
			
			GET_FUNC_TABLE(i)->fninterrupt(&id);
		}
		PIE2bits.TMR3IE = 1;
	}

}
#pragma code 


void ModuleInit()
{
	BYTE i;
	SetFuncTable(MODULE_COUNT);
	
	for(i=0; i<MODULE_COUNT; i++)
	{
		if(FAILED(GET_FUNC_TABLE(i)->fninit(i)))
		{
			// do nothing
		}
	}
}

void DeviceInit()
{
	INTCONbits.GIE = 1;
    INTCONbits.PEIE = 1;   
	
	OpenTimer3(TIMER_INT_ON & T3_8BIT_RW & T3_SOURCE_INT & T3_PS_1_8 & T3_SYNC_EXT_OFF);
}

void main()
{
	ModuleInit();
	DeviceInit();
	
	while(1);
}