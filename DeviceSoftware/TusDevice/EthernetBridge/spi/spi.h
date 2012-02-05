/*
 * spi.h
 *
 * Created: 2012/01/21 7:31:50
 *  Author: root
 */ 


#ifndef SPI_H_
#define SPI_H_

#include "global.h"

typedef struct tag_TUPLE_PORT
{
	volatile uint8_t *pport;
	uint8_t num;
} TUPLE_PORT;

typedef struct tag_SPI_PARAMS
{
	TUPLE_PORT CS;
	TUPLE_PORT SCK;
	TUPLE_PORT MISO;
	TUPLE_PORT MOSI;
	
} SPI_PARAMS;

#endif /* SPI_H_ */