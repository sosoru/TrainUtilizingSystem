#include <p18cxxx.h>
#include <spi.h>
#include "../../Headers/CommonDefs.h"

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


char spitestasslave();

char spitestasslave()
{
	BYTE i;
	
	while(1)
	{
		BYTE res;
				
		res = ReadSPI();
		
		PORTA = res & 0b00111111;
		PORTC = (res >> 6) & 0b00000011;
	}
	return -1;
}

void main()
{
	TRISA = 0;
	TRISC = 0;
	PORTA = 0;
	PORTC = 0;
	
	TRISBbits.TRISB7 = 0;
	PORTBbits.RB7 = 1;
	
	OpenSPI(SLV_SSOFF, MODE_01, SMPEND);
	spitestasslave();
	CloseSPI();
	while(1);
}

