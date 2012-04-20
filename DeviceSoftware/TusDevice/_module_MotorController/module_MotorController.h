/*
 * MotorController.h
 *
 * Created: 2012/02/15 19:12:25
 *  Author: root
 */ 


#ifndef MOTORCONTROLLER_H_
#define MOTORCONTROLLER_H_

#define F_CPU        20000000UL               		// 20MHz processor

#include <avr/io.h>
#include "../libtus/tus.h"

typedef struct tag_MOTOR_PORTS
{
	TUPLE_PORT ctrl_in1;
	TUPLE_PORT ctrl_in2;
	TUPLE_PORT ctrl_sb;
	TUPLE_PORT alert;
	TUPLE_PORT pwm;
	uint8_t feedback;
	
	uint8_t adc_result;
} MOTOR_PORTS;

typedef MOTOR_PORTS MOTOR_DIR;

void mtr_init();
void mtr_change(MOTOR_PORTS *pmtr);
void mtr_on();
void mtr_off();

void mtr_standby();
void mtr_start();
void mtr_stop();
void mtr_brake();
void mtr_drive_cw();
void mtr_drive_ccw();

#define MOTOR_COUNT 1

#define MOTOR_PORTS_A	{{&PORTA, PORTA4},	{&PORTA, PORTA5},	{&PORTA, PORTA6},	{&PORTA, PORTA7},	{&PORTD, PORTD5},	3}

#define MOTOR_PORTS_ARRAY {MOTOR_PORTS_A}

extern MOTOR_PORTS	g_motors[]		;

#endif /* MOTORCONTROLLER_H_ */