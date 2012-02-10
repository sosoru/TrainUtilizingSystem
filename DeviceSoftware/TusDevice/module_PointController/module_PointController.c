/*
 * module_PointController.c
 *
 * Created: 2011/12/16 14:18:09
 *  Author: root
 */ 

#include "PointController.h"
#include <avr/io.h>
#include <util/delay.h>

typedef struct tag_TUPLE_PORT
{
	volatile uint8_t *pport;
	uint8_t num;
} TUPLE_PORT;

#define cbi_p(tup) (cbi(*(tup).pport, (tup).num))
#define sbi_p(tup) (sbi(*(tup).pport, (tup).num))

#define stop(pt) cbi_p((pt)[0]); cbi_p((pt)[1])
#define brake(pt) sbi_p((pt)[0]); sbi_p((pt)[1])
#define pos(pt) sbi_p((pt)[0]); cbi_p((pt)[1]);
#define neg(pt) cbi_p((pt)[0]); sbi_p((pt)[1]);

#define change_delay stop(ppt); _delay_us(100); 

static TUPLE_PORT POINT_A[] = {{&PORTC, PORTC0}, {&PORTC, PORTC1}};
static TUPLE_PORT POINT_B[] = {{&PORTC, PORTC2}, {&PORTC, PORTC3}};
static TUPLE_PORT POINT_C[] = {{&PORTC, PORTC4}, {&PORTC, PORTC5}};
static TUPLE_PORT POINT_D[] = {{&PORTD, PORTD0}, {&PORTD, PORTD1}};
static TUPLE_PORT POINT_E[] = {{&PORTD, PORTD2}, {&PORTD, PORTD3}};
static TUPLE_PORT POINT_F[] = {{&PORTD, PORTD4}, {&PORTD, PORTD5}};
static TUPLE_PORT POINT_G[] = {{&PORTD, PORTD6}, {&PORTD, PORTD7}};
static TUPLE_PORT POINT_H[] = {{&PORTB, PORTB0}, {&PORTB, PORTB1}};

void DeviceInit()
{
	DDRB = 0b00000011; // PB0, 1 are output
	DDRC = 0xff; //all output
	DDRD = 0xff; //all output
	
}	
		

void toggle(TUPLE_PORT *ppt)
{
	uint8_t en_time = 200;
	
	brake(ppt);
	
	change_delay;
	pos(ppt);
	_delay_ms(en_time);
	
	change_delay;
	brake(ppt);
	_delay_ms(255);
	_delay_ms(255);
	_delay_ms(255);
	_delay_ms(255);
	
	change_delay;
	neg(ppt);
	_delay_ms(en_time);
	
	stop(ppt);
	_delay_ms(255);
	_delay_ms(255);
	_delay_ms(255);
	_delay_ms(255);
}

int main(void)
{
	sbi(PORTB, PORTB1);
    while(1)
    {
        //TODO:: Please write your application code   
		toggle(POINT_A);
    }
}