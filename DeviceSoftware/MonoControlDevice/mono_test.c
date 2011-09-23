#include "HardwareProfile.h"
#include "MonoDevice.h"
#include "../Headers/PortMapping.h"

void IoTest()
{
	BYTE i, size = 3 * 6 + 1;
	
	TRISA = TRISB = TRISC = TRISD = 0x00;
	PORTA = PORTB = PORTC = PORTD = 0x00;
	LATA = LATB = LATC = LATD = 0x00;
	
	ADCON1 = 0x0F;
	CMCON = 0x07; //disable comparators
	
	for(i=1; i<size; ++i)
	{
		setTris(i, INPUT_PIN);
		if(getTris(i) != INPUT_PIN)
			break;
			
		setTris(i, OUTPUT_PIN);
		if(getTris(i) != OUTPUT_PIN)
			break;
			
		setLat(i, 0);
		if(getLat(i) !=0)
			break;
			
		setLat(i, 1);
		if(getLat(i) != 1)
			break;
	} 
	
	i--;
}
