/*
 * tus_spi.h
 *
 * Created: 2012/01/23 6:42:43
 *  Author: root
 */ 


#ifndef TUS_SPI_H_
#define TUS_SPI_H_

#include "packet.h"

typedef struct tag_args_received
{
	EthPacket *ppack;
	uint8_t pos;
} args_received;

typedef struct tag_spi_send_object
{
	EthPacket packet;
	uint8_t is_locked;
	uint8_t is_sent;
} spi_send_object;

typedef void (*spi_received_handler)(args_received* e) ;

void tus_spi_init();
void tus_spi_process_packets();
void tus_spi_set_handler(spi_received_handler handler);
uint8_t tus_spi_lock_send_buffer(spi_send_object ** ppsendobj);

#endif /* TUS_SPI_H_ */