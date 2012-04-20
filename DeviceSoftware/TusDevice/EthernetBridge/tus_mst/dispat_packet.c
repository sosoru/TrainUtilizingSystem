/*
 * dispat_packet.c
 *
 * Created: 2012/01/30 16:56:23
 *  Author: root
 */ 

#include <stdlib.h>
#include <string.h>
#include "dispat_packet.h"

static DEVICE_BUFFER dev_buffers[DEVICES_COUNT];

void dispat_init_buffer(uint8_t id)
{
	if(id > DEVICES_COUNT)
		return;
		
	memset((void*)&dev_buffers[id], 0x00, sizeof(DEVICE_BUFFER));
}

uint8_t dispat_inc_buffer(uint8_t id, EthPacket** pppacket)
{
	DEVICE_BUFFER *pstored = &dev_buffers[id];
	
	if(id >= DEVICES_COUNT)
		return FALSE;
	
	if(pstored->buffer_cnt >= PACKET_BUFFER_COUNT)
		return FALSE;
		
	*pppacket = &pstored->buffer[pstored->buffer_cnt];
	pstored->buffer_cnt++;
	
	return TRUE;
}

uint8_t dispat_pop_buffer(uint8_t id, EthPacket ** pppacket)
{
	DEVICE_BUFFER *pstored = &dev_buffers[id];
	
	if(id >= DEVICES_COUNT)
		return FALSE;
	
	if(dev_buffers[id].buffer_cnt == 0)
		return FALSE;
		
	pstored->buffer_cnt--;
	*pppacket = &pstored->buffer[pstored->buffer_cnt];
	
	return TRUE;
	
}

uint8_t dispat_set_message(uint8_t id, EthPacket *ppacket)
{
	if(id > DEVICES_COUNT
		|| ppacket->command != ETHCMD_MESSAGE)
		return FALSE;

	memcpy((void*)dev_buffers[id].last_message, (void*)ppacket->pdata, PACKET_MESSAGE_SIZE );	
	return TRUE;	
}

uint8_t dispat_pop_message(uint8_t id, char** pstr)
{
	if(id > DEVICES_COUNT)
		return FALSE;
		
	*pstr = dev_buffers[id].last_message;
	return TRUE;
}
