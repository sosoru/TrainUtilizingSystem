/*
 * ph_TrainSensor.c
 *
 * Created: 2012/02/07 5:55:08
 *  Author: root
 */ 

#include "global.h"
#include <avr/io.h>
#include <avr/interrupt.h>
#include <util/delay.h>
#include "suart.h"
#include "../module_UartControl/UartPacket.h"

#define UARTDEV_ID_TRAINSENSOR 0x01

TrainSensorPacket_xmit received;
TrainSensorPacket_rcev sending;

void device_init();
uint8_t validate_received_packet();
void receive_packet();
void send_parent_packet();
void send_parent_error();
void send_neighbor_packet();

ISR(ADC_vect)
{
	sbi(TIFR0, TOV0);
}

void send_neighbor_packet()
{
	uint8_t next_number = received.number[0]+1;
	
	xmit(next_number);
	xmit(next_number);
	xmit(next_number);
}

void send_parent_error()
{
	xmit_parent(0xff);
	xmit_parent(0xff);
	xmit_parent(0xff);
}

void send_parent_packet()
{
	uint8_t i;
	
	sending.checksum = 0x00;
	
	sending.checksum ^= (sending.number = received.number[0]);
	sending.checksum ^= (sending.result = ADCL);
	
	for(i=0; i<sizeof(sending); ++i)
	{
		xmit_parent(sending.rawdata[i]);
	}
}

void receive_packet()
{
	uint8_t i;
	
	for(i=0; i<sizeof(received); ++i)
	{
		received.number[i] = rcvr();
	}
}

uint8_t validate_received_packet()
{
	uint8_t i, checking;
	
	checking = received.rawdata[0];
	for(i=1; i<sizeof(received); ++i)
	{
		if(checking != received.rawdata[i])
			return FALSE;
	}
	
	return TRUE;
}

void device_init()
{	
	DDRB	= 0b11110011;

	// Timer 0 initializing
	OCR0AL = 126;			// 4msec at 8MHz
	TCCR0A |= 0b00000011;
	//TIMSK0 |= 0b00000001;	// interrupts enable
	TCCR0B |= 0b00011000;	// set to 8bit fast pwm (top=OCR0A)	
	TCCR0B |= 0b00000100;	// set prescale to f_osc/256 and starts Timer0
		
	// adc initialzing
	sbi(ADCSRA, ADIE); // ADC interrupt enable	
	ADMUX = 2;			
	sbi(DIDR0, ADC2D); // ADC2 enable
	ADCSRA |= 0b00000111; // CK/128
	ADCSRB = 0b100;		// triggering source is timer0 overflowing
	sbi(ADCSRA, ADATE); // Auto-trigger enabled
	sbi(ADCSRA, ADEN); // A/D converter enable
	sbi(ADCSRA, ADSC); // A/D convetsion is started
	
	sei();	// interrupt enable
	
}

int main(void)
{
	RSTFLR = 0;
	
	CCP = 0xD8;
    CLKMSR = 0x00;  // internal 8MHz
    CCP = 0xD8;
    CLKPSR = 0x00; // prescale = 1
	
	device_init();
			
    while(1)
    {
		receive_packet();
		
		if(validate_received_packet())
		{
			send_parent_packet();
			send_neighbor_packet();
		}		
		else
		{
			send_parent_error();
		}
	}
	
	return 0;
}