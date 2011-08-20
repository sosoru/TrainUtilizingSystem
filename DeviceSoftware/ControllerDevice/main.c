#include "HardwareProfile.h"
#include "../Headers/SpiTransmit.h"
#include "../Headers/ControllerModule.h"
#include <timers.h>

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
 //#pragma config ICPRT = OFF          //Dedicated In-Circuit Debug/Programming Port (ICPORT) disabled
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
 
void ModuleInit();
void DeviceInit();
void high_isr(void);
void SpiProcess(SpiPacket * ppacket);
 
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
		
		id.ParentPart = g_mbState.ParentId;
		for(i = 0; i < MODULE_COUNT; ++i)
		{
			id.ModulePart = i;
			
			GET_FUNC_TABLE(i)->fninterrupt(&id);
		}
		TMR3H |= ~TMR3H;
		TMR3L = 0;
		
		PIE2bits.TMR3IE = 1;
	}
	
	if(PIR1bits.SSPIF)
	{
		BYTE received;
		SpiPacket packet;
		
		PIR1bits.SSPIF = 0;
		received = SSPBUF;
		
		if(SUCCEEDED(PacketReady(&packet)))
			SpiProcess(&packet);
	}

}
#pragma code 

//remoted packets receiving for RemoteModule
void SpiProcess(SpiPacket * ppacket)
{
	DeviceID devid;			

	LATAbits.LATA2 = 1;
	if(ppacket->devid.ParentPart == g_mbState.ParentId)
	{			
		if(ppacket->mode == MODE_CREATE)
		{
			GET_FUNC_TABLE((BYTE)(devid.ModuleAddr))->fncreate(&devid, (PMODULE_DATA)(ppacket->data));
			
			SendSpiPacket(ppacket);
		}
		else
		{
			GET_FUNC_TABLE((BYTE)(devid.ModuleAddr))->fnstore(&devid, (PMODULE_DATA)(ppacket->data));
		}
		
	}		
}

void ModuleInit()
{
	BYTE i;
	DeviceID devid;
	SetFuncTable();
	
	devid.ParentPart = g_mbState.ParentId;
	for(i=0; i<MODULE_COUNT; i++)
	{
		devid.ModulePart = i;
		if(FAILED(GET_FUNC_TABLE(i)->fninit(&devid)))
		{
			// do nothing
		}
	}
}

void DeviceInit()
{
	INTCONbits.GIE = 1;
    INTCONbits.PEIE = 1;   
    
    PIR1bits.SSPIF = 0;
    SSPBUF = 0x00;
    PIE1bits.SSPIE = 1;
    
    TRISAbits.TRISA2 = OUTPUT_PIN;
    TRISAbits.TRISA3 = OUTPUT_PIN;
    LATAbits.LATA2 = 0;
    LATAbits.LATA3 = 0;
	
	OpenTimer3(TIMER_INT_ON & T3_8BIT_RW & T3_SOURCE_INT & T3_PS_1_2 & T3_SYNC_EXT_OFF);
}

void main()
{
	ModuleInit();
	DeviceInit();
	
	LATAbits.LATA3 = 1;
	while(1);
}