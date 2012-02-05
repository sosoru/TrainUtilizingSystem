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

typedef struct tag_TUPLE_PORT
{
	volatile uint8_t *pport;
	uint8_t num;
} TUPLE_PORT;

#define cbi_p(tup) (cbi(*(tup).pport, (tup).num))
#define sbi_p(tup) (sbi(*(tup).pport, (tup).num))
#define read_p(tup) ((*(tup).pport & (1<<(tup).num)) >> (tup).num)


typedef struct tag_SPI_TRANS_PORT
{
	TUPLE_PORT SCK;
	TUPLE_PORT MOSI;
	TUPLE_PORT MISO;
} SPI_TRANS_PORT;

typedef SPI_TRANS_PORT SPI_TRANS_DIRECTION;

typedef struct tag_SPI_SLAVE_PORT
{
	TUPLE_PORT nSS;
	TUPLE_PORT nRESET;
} SPI_SLAVE_PORT;

typedef SPI_SLAVE_PORT SPI_SLAVE_DIRECTION;

#define SPI_TRANS_PORTINST {{&PORTD, 5}, {&PORTD, 6}, {&PORTD, 7}}
#define SPI_TRANS_DIRINST {{&DDRD, 5}, {&DDRD, 6}, {&DDRD, 7}}

#define SPI_SLAVE_PORTINST_A {{&PORTD, 0}, {&PORTA, 0}}
#define SPI_SLAVE_PORTINST_B {{&PORTD, 1}, {&PORTA, 1}}
#define SPI_SLAVE_PORTINST_C {{&PORTD, 2}, {&PORTA, 2}}
#define SPI_SLAVE_PORTINST_D {{&PORTD, 3}, {&PORTA, 3}}
#define SPI_SLAVE_PORTINST_E {{&PORTE, 4}, {&PORTA, 4}}
#define SPI_SLAVE_PORTINST_F {{&PORTE, 5}, {&PORTA, 5}}
#define SPI_SLAVE_PORTINST_G {{&PORTE, 6}, {&PORTA, 6}}
#define SPI_SLAVE_PORTINST_H {{&PORTE, 7}, {&PORTA, 7}}
	
#define SPI_SLAVE_DIRINST_A {{&DDRD, 0}, {&DDRA, 0}}
#define SPI_SLAVE_DIRINST_B {{&DDRD, 1}, {&DDRA, 1}}
#define SPI_SLAVE_DIRINST_C {{&DDRD, 2}, {&DDRA, 2}}
#define SPI_SLAVE_DIRINST_D {{&DDRD, 3}, {&DDRA, 3}}
#define SPI_SLAVE_DIRINST_E {{&DDRE, 4}, {&DDRA, 4}}
#define SPI_SLAVE_DIRINST_F {{&DDRE, 5}, {&DDRA, 5}}
#define SPI_SLAVE_DIRINST_G {{&DDRE, 6}, {&DDRA, 6}}
#define SPI_SLAVE_DIRINST_H {{&DDRE, 7}, {&DDRA, 7}}
	
#define SPI_SLAVE_PORT_ARRAY  {SPI_SLAVE_PORTINST_A, \
							SPI_SLAVE_PORTINST_B,\
							SPI_SLAVE_PORTINST_C,\
							SPI_SLAVE_PORTINST_D,\
							SPI_SLAVE_PORTINST_E,\
							SPI_SLAVE_PORTINST_F,\
							SPI_SLAVE_PORTINST_G,\
							SPI_SLAVE_PORTINST_H}
							
#define SPI_SLAVE_DIR_ARRAY {SPI_SLAVE_DIRINST_A, \
								SPI_SLAVE_DIRINST_B, \
								SPI_SLAVE_DIRINST_C, \
								SPI_SLAVE_DIRINST_D, \
								SPI_SLAVE_DIRINST_E, \
								SPI_SLAVE_DIRINST_F, \
								SPI_SLAVE_DIRINST_G, \
								SPI_SLAVE_DIRINST_H}

#endif /* TUS_MSTCFG_H_ */