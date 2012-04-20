/*
 * MotorControl.c
 *
 * Created: 2012/02/16 5:49:30
 *  Author: root
 */ 

#include "../libtus/tus.h"
#include "module_MotorController.h"
#include "motor_adc.h"
#include <util/delay.h>
#include <avr/interrupt.h>

static MOTOR_PORTS	*pcur_mtr;
static uint8_t adc_current_channel = 0;

ISR(TIMER1_COMPA_vect)
{
	//adc_change(adc_current_channel);
	
}

static int adc_before = 0, adc_current = 0;
ISR(ADC_vect)
{	
	
	adc_current_channel++;
	
	if(adc_current_channel >= MOTOR_COUNT)
		adc_current_channel = 0;
}

void mtr_init()
{
	uint8_t i;
	
	for(i = 0; i < MOTOR_COUNT; ++i)
	{
		MOTOR_PORTS *pmtr = &g_motors[i];

		mtr_change(pmtr);
		
		dir_out(pmtr->ctrl_in1);	//ctrl_in1 : output
		dir_out(pmtr->ctrl_in2);	//ctrl_in2 : output
		dir_out(pmtr->ctrl_sb);	//ctrl_sb  : output
		dir_out(pmtr->pwm);		//pwm	   : output
		//cbi_p(pdir->feedback);	//feedback : input
		dir_in(pmtr->alert);		//alert	   : input
		
		mtr_standby();		
	}
	
	OCR1A	= 0;			// duty value = 0
	//TIMSK1	= 0b00000010;	// interrupt when  out compare A
	sei();
}

inline void mtr_on()
{
	TCCR1A	= 0b10000001;	// non-inverting, 8bit_fast-pwm
	TCCR1B	= 0b00001001;	// no-prescale, start	
}

inline void mtr_off()
{
	TCCR1A	= 0;
	TCCR1B	= 0;
}

inline void mtr_change(MOTOR_PORTS *pmtr)
{
	pcur_mtr = pmtr;
}	

inline void mtr_standby()
{
	cbi_p(pcur_mtr->ctrl_sb);
} 

inline void mtr_start()
{
	sbi_p(pcur_mtr->ctrl_sb);	
}	
	
void mtr_stop()
{
	cbi_p(pcur_mtr->ctrl_in1);
	cbi_p(pcur_mtr->ctrl_in2);
}

void mtr_brake()
{
	sbi_p(pcur_mtr->ctrl_in1);
	sbi_p(pcur_mtr->ctrl_in2);
}

void mtr_drive_cw()
{
	//_delay_us(1);
	sbi_p(pcur_mtr->ctrl_in1);
	//_delay_us(1);
	cbi_p(pcur_mtr->ctrl_in2);
}

void mtr_drive_ccw()
{
	_delay_us(1);
	cbi_p(pcur_mtr->ctrl_in1);
	_delay_us(1);
	sbi_p(pcur_mtr->ctrl_in2);
}


