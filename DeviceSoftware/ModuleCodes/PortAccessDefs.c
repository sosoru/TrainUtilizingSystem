#include <p18cxxx.h>
#include "HardwareProfile.h"
#include "../Headers/PortMapping.h"

#ifdef VERSION_REV1

near BYTE * ModuleTris[] = 
{ 
	//MB
	0,
	//PORTA
	&TRISA,	&TRISA,	&TRISA,	&TRISA,	
	//PORTB
	&TRISA,	&TRISE,	&TRISE,	&TRISE,
	//PORTC
	&TRISA,	&TRISB,	&TRISC,	&TRISC,
	//PORTD
	&TRISB,	&TRISB,	&TRISB,	&TRISB,
	//PORTE
	&TRISD,	&TRISB, &TRISB,	&TRISB,
	//PORTF
	&TRISC,	&TRISD,	&TRISD,	&TRISD,
	//PORTG
	&TRISD,	&TRISD,	&TRISD,	&TRISC
};

near BYTE * ModulePort[] = 
{ 
	//MB
	0,
	//PORTA
	&PORTA,	&PORTA,	&PORTA,	&PORTA,	
	//PORTB
	&PORTA,	&PORTE,	&PORTE,	&PORTE,
	//PORTC
	&PORTA,	&PORTB,	&PORTC,	&PORTC,
	//PORTD
	&PORTB,	&PORTB,	&PORTB,	&PORTB,
	//PORTE
	&PORTD,	&PORTB, &PORTB,	&PORTB,
	//PORTF
	&PORTC,	&PORTD,	&PORTD,	&PORTD,
	//PORTG
	&PORTD,	&PORTD,	&PORTD,	&PORTC
};

near BYTE * ModuleLat[] =
{
	//MB
	0,
	//PORTA
	&LATA,	&LATA,	&LATA,	&LATA,	
	//PORTB
	&LATA,	&LATE,	&LATE,	&LATE,
	//PORTC
	&LATA,	&LATB,	&LATC,	&LATC,
	//PORTD
	&LATB,	&LATB,	&LATB,	&LATB,
	//PORTE
	&LATD,	&LATB,  &LATB,	&LATB,
	//PORTF
	&LATC,	&LATD,	&LATD,	&LATD,
	//PORTG
	&LATD,	&LATD,	&LATD,	&LATC
};

BYTE ModuleShift[]=
{
	//MB
	0,
	//PORTA
	0,1,2,3,
	//PORTB
	5,0,1,2,
	//PORTC
	4,0,1,2,
	//PORTD
	2,3,1,4,
	//PORTE
	7,5,6,7,
	//PORTF
	7,4,5,6,
	//PORTG
	1,2,3,6
};

#elif defined VERSION_REV2

near BYTE * ModuleTris[] = 
{ 
	//MB
	0,
	//PORTA
	&TRISA,	&TRISD,	&TRISB,	&TRISB, &TRISB, &TRISB,	
	//PORTB
	&TRISA,	&TRISD,	&TRISB,	&TRISB, &TRISB, &TRISB,	
	//PORTC
	&TRISA,	&TRISD,	&TRISD,	&TRISD, &TRISD, &TRISD	
};

near BYTE * ModuleLat[] =
{
	//MB
	0,
	//PORTA
	&LATA,	&LATD,	&LATB,	&LATB, &LATB, &LATB,
	//PORTB
	&LATA,	&LATD,	&LATB,	&LATB, &LATB, &LATB,	
	//PORTC
	&LATA,	&LATD,	&LATD,	&LATD, &LATD, &LATD	
};

near BYTE * ModulePort[] =
{
	//MB
	0,
	//PORTA
	&PORTA,	&PORTD,	&PORTB,	&PORTB, &PORTB, &PORTB,
	//PORTB
	&PORTA,	&PORTD,	&PORTB,	&PORTB, &PORTB, &PORTB,	
	//PORTC
	&PORTA,	&PORTD,	&PORTD,	&PORTD, &PORTD, &PORTD	
};

BYTE ModuleShift[]=
{
	//MB
	0,
	//PORTA
	1,7,0,1,2,3,
	//PORTB
	0,6,4,5,6,7,
	//PORTC
	2,5,0,1,2,3
};

near BYTE * ModuleDataPort[] =
{
	0,
	&PORTB,
	&PORTB,
	&PORTD
};

near BYTE * ModuleDataLat[] =
{
	0,
	&LATB,
	&LATB,
	&LATD
};

BYTE ModuleDataPortShift[] =
{
	0,
	0,
	4,
	0
};

#endif
