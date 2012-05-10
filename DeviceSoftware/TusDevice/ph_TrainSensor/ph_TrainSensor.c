/*
 * ph_TrainSensor.c
 *
 * Created: 2012/02/07 5:55:08
 *  Author: root
 */ 

#include "global.h"
#include <avr/io.h>
#include <util/delay.h>
#include <stdio.h>
#include "suart.h"

#define UARTDEV_ID_TRAINSENSOR 0x01

int main(void)
{
	RSTFLR = 0;
	
	CCP = 0xD8;
    CLKMSR = 0x00;  // internal 8MHz
    CCP = 0xD8;
    CLKPSR = 0x00; // prescale = 1
	
	DDRB	= 0b11111100;

	sbi(ADCSRA, ADEN); // A/D converter enable
	sbi(DIDR0, ADC0D); // ADC0 enable
	ADCSRA |= 0b00000101; // CK/32
		
    while(1)
    {
		uint8_t adc_result;
		char c=0x01;
						
		c = rcvr();		
		
		sbi(ADCSRA, ADSC); // convertion started
		while(!(ADCSRA & BV(ADIF))); //wait convertion completed
		
		adc_result = ADCL;
		
		xmit_parent(UARTDEV_ID_TRAINSENSOR);
		xmit_parent(adc_result);
		
		c++;
		xmit(c);
		
	}
}