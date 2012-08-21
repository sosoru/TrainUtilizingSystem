/*
 * ph_TrainSensor.c
 *
 * Created: 2012/02/07 5:55:08
 *  Author: root
 */ 

#include "global.h"
#include "string.h"
#include <avr/io.h>
#include <avr/interrupt.h>
#include <util/delay.h>
#include "suart.h"
#include "../module_UartControl/UartPacket.h"

#define nop() __asm__ __volatile__ ("nop")

#define UARTDEV_ID_TRAINSENSOR 0x01


#define SENDING_TYPE			UARTDEV_ID_TRAINSENSOR
#define SENDING_PACKET_SIZE		sizeof(UsartPacket_sensor)
#define ENTER_PARENT_TRANSFER	sbi(DDRB, 3);
#define LEAVE_PANRET_TRANSFER	cbi(DDRB, 3);

UsartDevicePacket_Header received_head;
uint8_t received_data[MAX_SIZE_USARTDATA];
uint8_t storing_results[STORINGDATA_SIZE];
uint8_t* pstoring_current;
uint8_t storing_results_checksum;

void device_init();
uint8_t validate_received_packet();
void receive_packet();
void send_parent_packet();
void send_parent_error();
void send_neighbor_packet();

//its completes a/d conversion
ISR(ADC_vect)
{
	if(++pstoring_current == storing_results + STORINGDATA_SIZE)
	{
		pstoring_current = storing_results;
	}
	
	storing_results_checksum ^= *pstoring_current;
	*pstoring_current = ADCL;
	storing_results_checksum ^= *pstoring_current;
	
	sbi(TIFR0, TOV0);
}

void send_neighbor_packet()
{
	uint8_t *ptr;
	
	received_head.number++;
	received_head.checksum_all++;

	xmit(received_head.type);
	xmit(received_head.packet_size);
	xmit(received_head.number);
	xmit(received_head.checksum_all);		

	ptr = received_data;
	while(ptr < received_data + received_head.packet_size)
	{
		xmit(*(ptr++));
	}
}

void send_parent_error()
{	
	ENTER_PARENT_TRANSFER
	
	xmit_parent(SENDING_TYPE);
	xmit_parent(0); //packet size
	xmit_parent(0xFF); //number
	xmit_parent(SENDING_TYPE ^ 0xFF);
	
	LEAVE_PANRET_TRANSFER
}

void send_parent_packet()
{	
	uint8_t checksum;
	uint8_t *pstoring = storing_results;	
	
	cli(); // disable interrupts
	
	checksum = (SENDING_TYPE ^ SENDING_PACKET_SIZE ^ received_head.number ^ storing_results_checksum);
	
	ENTER_PARENT_TRANSFER	
	xmit_parent(SENDING_TYPE);
	xmit_parent(SENDING_PACKET_SIZE);
	xmit_parent(received_head.number);
	xmit_parent(checksum);
	
	do
	{
		xmit_parent(*pstoring);
	}while(++pstoring < storing_results + sizeof(storing_results));
	
	LEAVE_PANRET_TRANSFER
	
	sei(); // restore interrupts
}

void receive_packet()
{		
	uint8_t *ptr;
	
	received_head.type = rcvr();
	received_head.packet_size = rcvr();
	received_head.number = rcvr();
	received_head.checksum_all = rcvr();
	
	if(received_head.packet_size > MAX_SIZE_USARTDATA)
		received_head.packet_size = MAX_SIZE_USARTDATA;
	
	ptr = received_data;
	while(ptr < received_data + received_head.packet_size)
	{
		*(ptr++) = rcvr();
	}
}

uint8_t validate_received_packet()
{
	uint8_t checking=0;
	uint8_t *ptr;
	
	checking ^= received_head.type;
	checking ^= received_head.number;
	checking ^= received_head.packet_size;
	
	ptr = received_data;
	while(ptr < received_data + received_head.packet_size)
	{
		checking ^= *(ptr++);
	}
	
	return checking == received_head.checksum_all;
}

void device_init()
{	
	PUEB	= 0b00000001;
	DDRB	= 0b11110100;

	// Timer 0 initializing
	OCR0AL = 126;			// 4msec at 8MHz
	TCCR0A |= 0b00000001;
	//TIMSK0 |= 0b00000001;	// interrupts enable
	TCCR0B |= 0b00011000;	// set to 10bit fast pwm (top=0x03FF)	
	TCCR0B |= 0b00000011;	// set prescale to f_osc/64 and starts Timer0
		
	// adc initialzing
	sbi(ADCSRA, ADIE); // ADC interrupt enable	
	ADMUX = 0;			
	sbi(DIDR0, ADC0D); // ADC0 enable
	ADCSRA |= 0b00000111; // CK/128
	ADCSRB = 0b100;		// triggering source is timer0 overflowing
	sbi(ADCSRA, ADATE); // Auto-trigger enabled
	sbi(ADCSRA, ADEN); // A/D converter enable
	sbi(ADCSRA, ADSC); // A/D convetsion is started
	
	memset(&received_head, 0x00, sizeof(received_head));
	memset(received_data, 0x00, sizeof(received_data));
	received_head.checksum_all = 0xff;
	
	pstoring_current = storing_results;
	memset(&storing_results, 0x00, sizeof(storing_results));
	
	sei();	// interrupt enable
	
}

void wait_collision()
{
	uint8_t i=200, result=1, current;
	
	while(i--)
	{
		nop();
		nop();
		nop();
		nop(); // 1us at 8MHz
		
		current = PINB & (1 << PINB1);
		if(result != current)
		{
			i=200;
		}
		result = current;
	}
	
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
			wait_collision();
		}
	}
	
	return 0;
}