/*
 * led_packet.h
 *
 * Created: 2013/08/09 22:58:45
 *  Author: Administrator
 */ 


#ifndef LED_PACKET_H_
#define LED_PACKET_H_

#include <packet.h>

typedef struct tag_LedState
{
	BaseState Base;

	uint8_t DutyValue;
} LedState;


#endif /* LED_PACKET_H_ */