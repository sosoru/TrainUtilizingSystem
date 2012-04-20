/*
 * tus_mstcfg.h
 *
 * Created: 2012/01/30 15:19:24
 *  Author: root
 */ 


#ifndef TUS_MSTCFG_H_
#define TUS_MSTCFG_H_

#include <avr/io.h>
#include "../global.h"
#include "../../libtus/tus.h"

#define DEVICES_COUNT 2

typedef struct tag_SPI_TRANS_PORT
{
	TUPLE_PORT SCK;
	TUPLE_PORT MOSI;
	TUPLE_PORT MISO;
} SPI_TRANS_PORT;

typedef struct tag_SPI_SLAVE_PORT
{
	TUPLE_PORT nSS;
	TUPLE_PORT nRESET;
} SPI_SLAVE_PORT;

#define SPI_TRANS_PORTINST {{&PORTD, 5}, {&PORTD, 6}, {&PORTD, 7}}

#define SPI_SLAVE_PORTINST_A {{&PORTD, 0}, {&PORTA, 0}}
#define SPI_SLAVE_PORTINST_B {{&PORTD, 1}, {&PORTA, 1}}
#define SPI_SLAVE_PORTINST_C {{&PORTD, 2}, {&PORTA, 2}}
#define SPI_SLAVE_PORTINST_D {{&PORTD, 3}, {&PORTA, 3}}
#define SPI_SLAVE_PORTINST_E {{&PORTE, 4}, {&PORTA, 4}}
#define SPI_SLAVE_PORTINST_F {{&PORTE, 5}, {&PORTA, 5}}
#define SPI_SLAVE_PORTINST_G {{&PORTE, 6}, {&PORTA, 6}}
#define SPI_SLAVE_PORTINST_H {{&PORTE, 7}, {&PORTA, 7}}
	
#define SPI_SLAVE_PORT_ARRAY  {SPI_SLAVE_PORTINST_A, \
							SPI_SLAVE_PORTINST_B,\
							SPI_SLAVE_PORTINST_C,\
							SPI_SLAVE_PORTINST_D,\
							SPI_SLAVE_PORTINST_E,\
							SPI_SLAVE_PORTINST_F,\
							SPI_SLAVE_PORTINST_G,\
							SPI_SLAVE_PORTINST_H}

#endif /* TUS_MSTCFG_H_ */