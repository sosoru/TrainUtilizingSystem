/*
 * module_MotorController.c
 *
 * Created: 2012/02/15 19:10:35
 *  Author: root
 */ 

#include "module_MotorController.h"
//#include "../libtus/tus.h"
#include "motor_adc.h"
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <avr/io.h>
#include <avr/wdt.h>
#include <util/delay.h>

MOTOR_PORTS	g_motors[]		= MOTOR_PORTS_ARRAY;

uint8_t is_received = 0;

uint8_t mtr_base_voltages[255];

static int uart_putchar(char c, FILE *stream);
uint8_t uart_getchar(void);

static FILE mystdout = FDEV_SETUP_STREAM(uart_putchar, NULL, _FDEV_SETUP_WRITE);

static int uart_putchar(char c, FILE *stream)
{
    if (c == '\n') uart_putchar('\r', stream);
  
    while(!(UCSR0A & BV(UDRE0)));
    UDR0 = c;
    
    return 0;
}

uint8_t uart_getchar(void)
{
    while( !(UCSR0A & (1<<RXC0)) );
    return(UDR0);
}

void tus_packet_received(args_received* e)
{
	EthPacket *ppack = e->ppack;
	
	is_received++;
}

void USART_Init( unsigned int baud )
{
	/* Set baud rate */
	UBRR0H = (unsigned char)(baud >> 8);
	UBRR0L = (unsigned char)baud;
	/* Enable receiver and transmitter */
	UCSR0B = (1<<RXEN0)|(1<<TXEN0);
	/* Set frame format: 8data, 2stop bit */
	UCSR0C = (1<<USBS0)|(3<<UCSZ00);
	
	stdout = &mystdout; //Required for printf init
}

void set_base_voltage()
{
	uint8_t i, ocr_buffer;
	
	mtr_standby();
	
	ocr_buffer = OCR1AL;
	for(i=0; i<255; ++i)
	{
		OCR1AL = i;
		_delay_ms(100);
		
		adc_start();
		adc_wait();
		
		mtr_base_voltages[i] = adc_result();
		printf("set volt: i = %d, adc = %d\n", i, adc_result());
	}
	
	OCR1AL = ocr_buffer;
	mtr_start();
	_delay_ms(50);
}

int main(void)
{
	uint8_t i;
	float fb_bef = 0, fb_bef2 = 0, fb_cur = 0, internal_duty = 0;
		
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	wdt_disable();
	USART_Init(129); // baud = 9600bps
	
	DDRD = 0xFF;
	DDRB |= 0x01;
	PORTB |= 0x01;
	
	//tus_spi_init();
	//tus_spi_set_handler(tus_packet_received);
	//
	adc_init();
	adc_change(3);
	
	uint8_t counter = FALSE;
	mtr_init();
	mtr_change(&g_motors[0]);
	
	mtr_standby();
	mtr_drive_cw();
	mtr_start();
	mtr_on();
	
	//set_base_voltage();
	
    while(1)
    {
		uint8_t duty = 5 ;
		
		//tus_spi_process_packets();
		//if(1)
		//{
			//spi_send_object *pobj;
			//if(tus_spi_lock_send_buffer(&pobj))
			//{
				//uint8_t i;
				//
				//pobj->packet.srcId.SubnetAddr = 24;
				//pobj->packet.srcId.ModuleAddr = 0;
				//pobj->packet.destId.SubnetAddr = 105;
				//pobj->packet.destId.ModuleAddr = 0;
				//
				//for(i=0; i<ETH_DATA_LEN; ++i)
				//{
					//pobj->packet.pdata[i] = i+0x7F;
				//}
				//
				//pobj->is_locked = FALSE;
			//}
			//
			//is_received = 0;
		//}
		//
		//mtr_off();
		_delay_us(50);
		adc_start();
		adc_wait();
		
		//mtr_on();
		
		fb_bef2 = fb_bef;
		fb_bef = fb_cur;
		fb_cur = (float)(duty) - (float)adc_result();
		
		internal_duty +=  ((fb_cur - fb_bef) * 2.0f)  + (fb_cur * 0.8f) + ((fb_cur-fb_bef) - (fb_bef-fb_bef2)) * 0.6f;
		
		if(internal_duty > 200.0f)
			internal_duty = 200.0f;
		else if(internal_duty < -200.0f)
			internal_duty = -200.0f;
							
		if(internal_duty<0)
			OCR1AL = 0;
		else
			OCR1AL = (uint8_t)internal_duty;
			
		
		//OCR1AL = 150;
		//printf("internal = %d, ADC_result = %d\n", (int)internal_duty, adc_result());
		_delay_ms(1);
		
    }
}