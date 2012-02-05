/*
 * tus_spi.h
 *
 * Created: 2012/01/23 6:42:43
 *  Author: root
 */ 


#ifndef TUS_SPI_H_
#define TUS_SPI_H_

#include "packet.h"

typedef tag_args_received
{
	EthPacket *ppack;
	uint8_t pos;
	uint8_t len;
} args_received;

typedef (void)(args_received* e) spi_received_handler;


#endif /* TUS_SPI_H_ */