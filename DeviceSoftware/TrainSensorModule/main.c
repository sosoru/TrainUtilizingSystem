#include <htc.h>

__CONFIG(FOSC_HS & WDTE_OFF & BOREN_OFF & MCLRE_ON & CP_OFF & LVP_OFF);

#define BYTE unsigned char

#define INPUT_PIN 1
#define OUTPUT_PIN 0

#define TRIS_PORT1 TRISB0
#define TRIS_PORT2 TRISB1
#define TRIS_PORT3 TRISB2
#define TRIS_PORT4 TRISB3
#define TRIS_PORTACK TRISB4

#define PORT_PORT1 RB0
#define PORT_PORT2 RB1
#define PORT_PORT3 RB2
#define PORT_PORT4 RB3
#define PORT_PORTACK RB4

#define OUTPUT_PORT_COUNT 8

volatile BYTE * TRIS_Array[] =
{
	&TRISA,
	&TRISA,
	&TRISA,
	&TRISA,
	&TRISA,
	&TRISB,
	&TRISB,
	&TRISB
};

BYTE BITS_Array[] =
{
	1,0,2,3,4,5,6,7
};

volatile BYTE * PORT_Array[] =
{
	&PORTA,
	&PORTA,
	&PORTA,
	&PORTA,
	&PORTA,
	&PORTB,
	&PORTB,
	&PORTB
};

#define WRITE_TRIS_OUTPORT(ind, value) (*TRIS_Array[(ind)] = ((*TRIS_Array[(ind)]) & (~(1 << (ind)))) |  ((value&1) << (ind)))
#define WRITE_PORT_OUTPORT(ind, value) (*PORT_Array[(ind)] = ((*PORT_Array[(ind)]) & (~(1 << (ind)))) |  ((value&1) << (ind)))
#define READ_TRIS_OUTPORT(ind) (((*TRIS_Array[(ind)]) >> BITS_Array[(ind)]) & 1)
#define READ_PORT_OUTPORT(ind) (((*PORT_Array[(ind)]) >> BITS_Array[(ind)]) & 1)

void Init();
void PortClear();
void main();
BYTE ReadInput();

void PortClear()
{
	BYTE i;
	for(i=0; i<OUTPUT_PORT_COUNT; ++i)
	{
		WRITE_TRIS_OUTPORT(i, 0);
	}
}

void Init()
{
	BYTE i;
	
	PORTA = 0x00;
	PORTB = 0x00;
	
	CMCON = 0b00000111;
	VRCON = 0b00000000;
	nRBPU = 1; // disable internal pullup
	
	TRISA = 0x00; //all output temporalily
	TRISB = 0x00;
		
	TRIS_PORT1 = INPUT_PIN;
	TRIS_PORT2 = INPUT_PIN;
	TRIS_PORT3 = INPUT_PIN;
	TRIS_PORT4 = INPUT_PIN;

	TRIS_PORTACK = OUTPUT_PIN;
	
	PORT_PORTACK = 0;
	PortClear();
}

BYTE ReadInput()
{
//	return (LAT_PORT1 << 0)
//			| (LAT_PORT2 << 1)
//			| (LAT_PORT3 << 2)
//			| (LAT_PORT4 << 3);
	return PORTB & 0b00000111;
}

void main()
{
	BYTE cache = 0x01;
	
	Init();
	WRITE_PORT_OUTPORT(cache,1);
	PORT_PORTACK = 1;
	
	while(1)
	{
		while(PORT_PORT4==1);
		
		BYTE tmp = ReadInput();
		if(cache != tmp)
		{
			PORT_PORTACK = 0;
			
			WRITE_PORT_OUTPORT(cache, 0);
			if(tmp < OUTPUT_PORT_COUNT)
			{
				WRITE_PORT_OUTPORT(tmp, 1);
				
				cache = tmp; //sync	
			}
		}
		else
		{
			PORT_PORTACK = 1;		
		}
	}
}