/*
 * tus_spi.h
 *
 * Created: 2012/01/23 6:42:43
 *  Author: root
 */ 


#ifndef TUS_SPI_H_
#define TUS_SPI_H_

#include "../global.h"
#include "../packet.h"

typedef struct tag_args_received
{
	EthPacket *ppack;
	uint8_t pos;
	uint8_t len;
} args_received;

typedef void (*spi_received_handler)(args_received* e) ;


#endif /* TUS_SPI_H_ */