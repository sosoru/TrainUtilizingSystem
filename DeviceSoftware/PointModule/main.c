#include "PointModule.h"
#include <timers.h>

 /** Configuration Bits *******************************************/
 #pragma config OSC = HSPLL
 #pragma config FCMEN = OFF          //Fail-Safe Clock Monitor Enable bit: Disable
 #pragma config IESO = OFF           //Internal/External Oscillator Switchover bit: Disable
 #pragma config PWRT = ON            //Power Up timer enabled
 #pragma config BORV = 2             //Brown-out voltage = 2.7V
 #pragma config WDT = OFF            //Watchdog timer HW Disabled - SW Controlled
 #pragma config WDTPS = 128          //Watchdog timer = 512ms (128 x 4ms)
 #pragma config MCLRE = ON           //Master clear enabled
 #pragma config LPT1OSC = OFF        //Disabled
 #pragma config PBADEN = OFF         //Port B0-4 are digital inputs on reset
 //#pragma config CCP2MX = ON          //RC1
 #pragma config STVREN = ON          //Stack overflow reset enable
 #pragma config LVP = OFF            //Low voltage programming disabled
 #pragma config XINST = OFF          //Extended mode off
 #pragma config DEBUG = OFF          //Debug mode off
 #pragma config CP0 = OFF 
 #pragma config CP1 = OFF 
 #pragma config CPB = OFF 
 #pragma config CPD = OFF 
 #pragma config WRT0 = OFF 
 #pragma config WRT1 = OFF 
 #pragma config WRTB = ON            //Table write protect bootloader code 
 #pragma config WRTC = OFF 
 #pragma config WRTD = OFF 
 #pragma config EBTR0 = OFF 
 #pragma config EBTR1 = OFF 
 #pragma config EBTRB = OFF
 
BYTE g_delayBreak = 0;
BYTE g_interrupted = 0;
PointInfo g_ReceivedInfo;

void high_isr();
void DeviceInit();
void Delay(unsigned int t);
void ApplyPoint(PointInfo* pinfo);
void RefreshPoint();
void ReadPointInfo(PointInfo* pinfo);
void LoadSavedDirection(PointInfo* pinfo);
void StoreDirection(PointInfo* pinfo);

void Delay(unsigned int t)
{	
	OpenTimer1(TIMER_INT_ON & T1_16BIT_RW & T1_SOURCE_INT & T1_PS_1_8 & T1_OSC1EN_OFF & T1_SYNC_EXT_OFF);
	
	WriteTimer1(0xFFFF - t);
	
	g_delayBreak = 0;
	while(!g_delayBreak);
	
	CloseTimer1();
}

void ApplyPoint(PointInfo * pinfo)
{
	volatile near BYTE* pUsingPort = (!(pinfo->PointValue & 0b00000100)) ? &PORT_OUTPUT_LOWER : &PORT_OUTPUT_UPPER;
	BYTE value = 1 << (((pinfo->PointValue & 0b00000011) << 1) + pinfo->PointDirection);
	
	*pUsingPort = ~value;
	
	Delay(0xFFFF);
	Delay(0xFFFF);
	Delay(0xFFFF);
	Delay(0xFFFF);

	*pUsingPort = 0xFF;
}

void ReadPointInfo(PointInfo * pinfo)
{
	pinfo->data = (PORT_RECEIVING && 0b00011110) >> 1 ;
}

void LoadSavedDirection(PointInfo* pinfo)
{
	BYTE intcache = INTCON;
	
	INTCONbits.GIE = 0;
	INTCONbits.PEIE = 0;

	pinfo->PointDirection = ReadEEPROM(pinfo->PointValue);
	
	INTCON = intcache;
}

void StoreDirection(PointInfo* pinfo)
{
	BYTE intcache = INTCON;
	
	INTCONbits.GIE = 0;
	INTCONbits.PEIE = 0;

	WriteEEPROM(pinfo->PointValue, pinfo->PointDirection);
		
	INTCON = intcache;
}

void DeviceInit()
{
	PointInfo info;
	
	OSCCON = 0x70;
	OSCTUNE = 0x40;
	ADCON1bits.PCFG = 0b1111; // all digital
	CMCON = 0x07; //disable comparator
	
	// set all I/O ports output temporarily 
	TRISA = 0x00;
	TRISB = 0x00;
	TRISC = 0x00;
	
	PORTA = 0x00;
	PORTB = 0xFF;
	PORTC = 0xFF;
	
	TRIS_RECEIVING = 0xFF; // input
	TRIS_ACK_DEVICE = OUTPUT_PIN;
	
	PORT_ACK_DEVICE = 1;
			
	INTCONbits.GIE = 1;
	INTCONbits.PEIE = 1;
	OpenTimer0(TIMER_INT_ON & T0_8BIT & T0_SOURCE_INT & T0_PS_1_1);
	
	g_ReceivedInfo.PointValue = 0;
	LoadSavedDirection(&g_ReceivedInfo); // initialize received data 
}

void RefreshPoint()
{
	BYTE i;
	PointInfo info;
	for(i=0; i<8; ++i)
	{
		info.PointValue = i;
		
		LoadSavedDirection(&info);
		ApplyPoint(&info);	
	}	
}

void main()
{
	DeviceInit();
	
	while(1)
	{
		BYTE i;
		
		RefreshPoint();
		
		//waiting
		for(i=0; i<200; ++i)
		{
			Delay(0xFFFF);
			
			if(g_interrupted)
			{
				g_interrupted = 0;
				break;
			}		
		}		
	}
}

#pragma code HIGH_VECTOR = 0x08
void high_interrupt()
{ _asm GOTO high_isr _endasm}
#pragma code

#pragma interrupt high_isr
void high_isr()
{
	if(INTCONbits.T0IF)
	{
		INTCONbits.T0IF = 0;
		
		//check ack
		if(PORT_ACK_HOST)
		{
			PointInfo receivedInfo, romInfo;
			
			ReadPointInfo(&receivedInfo);
			romInfo.PointValue = receivedInfo.PointValue;
			
			LoadSavedDirection(&romInfo);
			
			if(romInfo.PointDirection != receivedInfo.PointDirection)
			{
				PORT_ACK_DEVICE = 0;
				
				StoreDirection(&receivedInfo);
				
				PORT_ACK_DEVICE = 1;
				
				g_interrupted = 1;
			}
			g_ReceivedInfo.data = receivedInfo.data;
		}
	}
	
	if(PIR1bits.TMR1IF)
	{
		PIR1bits.TMR1IF = 0;
		g_delayBreak = 1;
	}
}
#pragma code 
