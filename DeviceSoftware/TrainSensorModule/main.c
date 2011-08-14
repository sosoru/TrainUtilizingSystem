#include <p18cxxx.h>
#include "../Headers/CommonDefs.h"

 //#pragma config USBDIV = NO           //USB Clock source from the 48MHz
 #pragma config FOSC = HS     //Oscillator Selection bits - HS oscillator
 #pragma config FCMEN = OFF          //Fail-Safe Clock Monitor Enable bit: Disable
 #pragma config PLLEN = ON
 //#pragma config IESO = OFF           //Internal/External Oscillator Switchover bit: Disable
 //#pragma config PWRT = ON            //Power Up timer enabled
 #pragma config MCLRE = OFF           //Master clear enabled
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
 #pragma config WRTB = OFF            //Table write protect bootloader code 
 #pragma config WRTC = OFF 
 #pragma config WRTD = OFF 
 #pragma config EBTR0 = OFF 
 #pragma config EBTR1 = OFF 
 #pragma config EBTRB = OFF

#define BYTE unsigned char

#define INPUT_PIN 1
#define OUTPUT_PIN 0

#define TRIS_PORT1 TRISAbits.TRISA3
#define TRIS_PORT2 TRISCbits.TRISC5
#define TRIS_PORT3 TRISCbits.TRISC4
#define TRIS_PORT4 TRISCbits.TRISC3
#define TRIS_PORTACK TRISCbits.TRISC6

#define TRIS_LEDA TRISBbits.TRISB7
#define TRIS_LEDB TRISCbits.TRISC7

#define LAT_PORT1 PORTAbits.RA3
#define LAT_PORT2 LATCbits.LATC5
#define LAT_PORT3 LATCbits.LATC4
#define LAT_PORT4 LATCbits.LATC3
#define LAT_PORTACK LATCbits.LATC6

#define LAT_LEDA LATBbits.LATB7
#define LAT_LEDB LATCbits.LATC7

#define OUTPUT_PORT_COUNT 8

near BYTE * TRIS_Array[] =
{
	&TRISA,
	&TRISA,
	&TRISC,
	&TRISC,
	&TRISC,
	&TRISB,
	&TRISB,
	&TRISB
};

BYTE BITS_Array[] =
{
	1,0,0,1,2,4,5,6
};

near BYTE * LAT_Array[] =
{
	&LATA,
	&LATA,
	&LATC,
	&LATC,
	&LATC,
	&LATB,
	&LATB,
	&LATB
};

#define WRITE_TRIS_OUTPORT(ind, value) (*TRIS_Array[(ind)] = ((*TRIS_Array[(ind)]) & (~(1 << (ind)))) |  ((value&1) << (ind)))
#define WRITE_LAT_OUTPORT(ind, value) (*LAT_Array[(ind)] = ((*LAT_Array[(ind)]) & (~(1 << (ind)))) |  ((value&1) << (ind)))
#define READ_TRIS_OUTPORT(ind) (((*TRIS_Array[(ind)]) >> BITS_Array[(ind)]) & 1)
#define READ_LAT_OUTPORT(ind) (((*LAT_Array[(ind)]) >> BITS_Array[(ind)]) & 1)

void Init();
void PortClear();
void main();
BYTE ReadInput();

void Init()
{
	BYTE i;
	
	TRISA = 0xFF; //all input temporalily
	TRISB = 0xFF;
	TRISC = 0xFF;
	
	for(i=0; i<OUTPUT_PORT_COUNT; ++i)
	{
		WRITE_TRIS_OUTPORT(i, OUTPUT_PIN);
	}

	TRIS_PORTACK = OUTPUT_PIN;
	TRIS_LEDA = OUTPUT_PIN;
	TRIS_LEDB = OUTPUT_PIN;
	
	LAT_PORTACK = 0;
	LAT_LEDA = 0;
	LAT_LEDB = 0;
	PortClear();
}

void PortClear()
{
	BYTE i;
	for(i=0; i<OUTPUT_PORT_COUNT; ++i)
	{
		WRITE_TRIS_OUTPORT(i, 0);
	}
}

BYTE ReadInput()
{
	return (LAT_PORT1 << 0)
			| (LAT_PORT2 << 1)
			| (LAT_PORT3 << 2)
			| (LAT_PORT4 << 3);
}

void main()
{
	BYTE cache;
	
	Init();
	LAT_LEDA = 1;
	
	while(1)
	{
		BYTE tmp = ReadInput();
		if(cache != tmp)
		{
			LAT_LEDB = 1;
			LAT_PORTACK = 0;
			
			if(tmp < OUTPUT_PORT_COUNT)
			{
				PortClear();
				WRITE_TRIS_OUTPORT(tmp, 1);
				
				LAT_LEDB = 0;
				LAT_PORTACK = 1;
			}
			cache = tmp; //sync	
		}
	}
}