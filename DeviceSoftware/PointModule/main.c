#include "PointModule.h"
#include <delays.h>

 /** Configuration Bits *******************************************/
 #pragma config OSC = HSPLL
// #pragma config PLLDIV = 5           //Configure for 20Mhz crystal, 20/5 = 4Mhz (required for USB)
// #pragma config CPUDIV = OSC1_PLL2   //Use 48MHz clock for system clock = 96MHz/2 = 48MHz
// #pragma config USBDIV = 2           //USB Clock source from the 96 MHz PLL divided by 2
// #pragma config FOSC = HSPLL_HS      //Oscillator Selection bits - HS oscillator, PLL enabled, HS used by USB
 #pragma config FCMEN = OFF          //Fail-Safe Clock Monitor Enable bit: Disable
 #pragma config IESO = OFF           //Internal/External Oscillator Switchover bit: Disable
 #pragma config PWRT = ON            //Power Up timer enabled
 //#pragma config BOR = SOFT           //Brown-out Reset enabled and controlled by software (SBOREN is enabled)
 #pragma config BORV = 2             //Brown-out voltage = 2.7V
 //#pragma config VREGEN = ON          //USB Voltage regulator enabled
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

volatile near BYTE* g_pCurrentModule = &PORT_DEFAULT;
BYTE g_ModuleNumber = 0;
PointInfo g_ParentCache;

void high_isr();
void DeviceInit();
void ApplyPoint(PointInfo* pinfo);

//upper bits |<- A B C D ->| lower bits

void main()
{
	DeviceInit();
	
	while(1)
	{
		PointInfo info;
		info.data = PARENT_VALUE;
		info.ACK = 0;
		
		if(info.data == g_ParentCache.data)
		{
			// waiting
		}else{
			PARENT_ACK_PORT = 0;
			ApplyPoint(&info);
			
			PARENT_ACK_PORT = 1;
			g_ParentCache.data = info.data;		
		}
	}
}

void DeviceInit()
{
	BYTE data[] = {ReadEEPROM(0), ReadEEPROM(1), ReadEEPROM(2)};
	BYTE i, j;
	PointInfo info;
	
	ADCON1bits.PCFG = 0b1111;
	
	//init point status
	PARENT_TRIS = 0xFF;
	PARENT_ACK_TRIS = OUTPUT_PIN;
	PARENT_ACK_PORT = 0;
	
	TRIS_POINT_A = 0x00;
	TRIS_POINT_B = 0x00;
	TRIS_POINT_C = 0x00;
	TRIS_POINT_DIRECTION = OUTPUT_PIN;
	
	PORT_POINT_A = 0x00;
	PORT_POINT_B = 0x00;
	PORT_POINT_C = 0x00;
	
	g_ParentCache.data = 0x00;
	
	for(i = 1; i <= 3; ++i)
	{
		info.ModuleDefine = 1;
		info.ModuleValue = i;
		ApplyPoint(&info);
		
		info.ModuleDefine = 0;
		for(j = 0 ; j < 7; ++j)
		{
			info.PointValue = j;
			info.PointDirection = GetBitValue(data[i], j);
			ApplyPoint(&info);
		}
	}
	
}

void ApplyPoint(PointInfo * pinfo)
{
	if(pinfo->ModuleDefine)
	{
		switch(pinfo->ModuleValue)
		{
			case 1:
				g_pCurrentModule = &PORT_POINT_A;
				break;
			case 2:
				g_pCurrentModule = &PORT_POINT_B;
				break;
			case 3:
				g_pCurrentModule = &PORT_POINT_C;
				break;
			default:
				g_pCurrentModule = &PORT_DEFAULT;
		}
		g_ModuleNumber = pinfo->ModuleValue;
	}
	else
	{
		BYTE buf = ReadEEPROM(g_ModuleNumber-1);
		
		PORT_POINT_DIRECTION = pinfo->PointDirection;
		Delay1KTCYx(1);
				
		*g_pCurrentModule = 0x00;
		//ensured cleared
		SetBitValue(*g_pCurrentModule, (near BYTE)pinfo->PointValue, 1);
		Delay10KTCYx(200); 
		Delay10KTCYx(200); // 100msec
		*g_pCurrentModule = 0x00;
		
		ClearBitValue(buf, pinfo->PointValue);
		SetBitValue(buf, pinfo->PointValue, pinfo->PointDirection);
		WriteEEPROM(g_ModuleNumber-1,buf);
	}
	
}

#pragma code HIGH_VECTOR = 0x08
void high_interrupt()
{ _asm GOTO high_isr _endasm}
#pragma code

#pragma interrupt high_isr
void high_isr()
{

}
#pragma code 
