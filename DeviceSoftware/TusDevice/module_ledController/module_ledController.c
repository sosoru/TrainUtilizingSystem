/*
 * module_ledController.c
 *
 * Created: 2013/06/06 16:21:29
 *  Author: Administrator
 */ 

#include "avrlibdefs.h"
#include <avr/io.h>
#include <avr/delay.h>
#include <string.h>
#include <tus.h>
#include <led_packet.h>

#define PORT_DATA			PORTA
#define PORT_CLK			PORTD
#define PORT_ENABLE			PORTC
#define PORT_ENABLE_NUM		0

#define LED_COUNT			30

LedState g_State[LED_COUNT];

uint8_t led_count_remains[LED_COUNT];
uint8_t led_count_init[LED_COUNT];
uint8_t led_duty = 0;

#define APPLY_TO_LED(clk,ind)				if(led_count_remains[clk*8+ind] >0){ \
				sbi(led_signal_buffer, ind); \
				led_count_remains[clk*8+ind]--; \
			}else{ \
				cbi(led_signal_buffer, ind);} 

void device_init()
{
	DDRA = 0xFF;
	DDRD = 0xFF;
	DDRC = 0xFF;
	
	cbi(PORT_ENABLE, PORT_ENABLE_NUM); // enable D-flipflops
		
	memset(led_count_init, led_duty, sizeof(led_count_init));
	memset(led_count_remains, 0x00, sizeof(led_count_remains));
}

 void init_led_status()
{
	memcpy(led_count_remains, led_count_init, sizeof(led_count_init));
}

 void apply_led_status()
{
	uint8_t led_signal_buffer;
	
	PORT_CLK = 1<<0;
	APPLY_TO_LED(0,0);
	APPLY_TO_LED(0,1);
	APPLY_TO_LED(0,2);
	APPLY_TO_LED(0,3);
	APPLY_TO_LED(0,4);
	APPLY_TO_LED(0,5);
	APPLY_TO_LED(0,6);
	APPLY_TO_LED(0,7);
	PORT_DATA = led_signal_buffer;
	PORT_CLK = 1<<1;
	APPLY_TO_LED(1,0);
	APPLY_TO_LED(1,1);
	APPLY_TO_LED(1,2);
	APPLY_TO_LED(1,3);
	APPLY_TO_LED(1,4);
	APPLY_TO_LED(1,5);
	APPLY_TO_LED(1,6);
	APPLY_TO_LED(1,7);
	PORT_DATA = led_signal_buffer;
	PORT_CLK = 1<<2;
	APPLY_TO_LED(2,0);
	APPLY_TO_LED(2,1);
	APPLY_TO_LED(2,2);
	APPLY_TO_LED(2,3);
	APPLY_TO_LED(2,4);
	APPLY_TO_LED(2,5);
	APPLY_TO_LED(2,6);
	APPLY_TO_LED(2,7);
	PORT_DATA = led_signal_buffer;
	PORT_CLK = 1<<3;
	APPLY_TO_LED(3,0);
	APPLY_TO_LED(3,1);
	APPLY_TO_LED(3,2);
	APPLY_TO_LED(3,3);
	APPLY_TO_LED(3,4);
	APPLY_TO_LED(3,5);
	//APPLY_TO_LED(3,6);
	//APPLY_TO_LED(3,7);
	PORT_DATA = led_signal_buffer;
	PORT_CLK = 0;

}

void spi_received(args_received *e)
{

}

int main(void)
{	
	uint8_t i,j;
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	tus_spi_init();
	tus_spi_set_handler(spi_received);
	
	device_init();

    while(1)
    {	
		memset(led_count_init, led_duty, LED_COUNT);
		//tus_spi_process_packets();
		for(j=0; j<200; ++j)
		{
			init_led_status();
			for(i=0; i<128; ++i)
			{
				apply_led_status();
				_delay_us(30);
			}
		}
		led_duty++;
		if(led_duty == 128)
			led_duty = 0;
    }
}