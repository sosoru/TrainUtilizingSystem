/*
 * dispat_packet.h
 *
 * Created: 2012/01/30 16:57:00
 *  Author: root
 */ 


#ifndef DISPAT_PACKET_H_
#define DISPAT_PACKET_H_

#include "dispat_packet.h"

#define DEVICES_COUNT 8
#define PACKET_BUFFER_COUNT 2
#define PACKET_MESSAGE_SIZE 64

typedef struct tag_DEVICE_BUFFER
{
	EthPacket buffer[PACKET_BUFFER_COUNT];
	uint8_t buffer_cnt;
	char last_message[PACKET_MESSAGE_SIZE];
} DEVICE_BUFFER;

void dispat_init_buffer(uint8_t id);
uint8_t dispat_inc_buffer(uint8_t id, EthPacket** pppacket);
uint8_t dispat_pop_buffer(uint8_t id, EthPacket ** ppacket);
uint8_t dispat_set_message(uint8_t id, EthPacket *ppacket);
uint8_t dispat_pop_message(uint8_t id, char** pstr);


#endif /* DISPAT_PACKET_H_ */