/*
 * tus.h
 *
 * Created: 2012/01/23 6:47:34
 *  Author: root
 */ 


#ifndef TUS_H_
#define TUS_H_

#include <avr/io.h>
#include "avrlibdefs.h"
#include "avrlibtypes.h"

typedef struct tag_TUPLE_PORT
{
	volatile uint8_t *pport;
	uint8_t num;
} TUPLE_PORT;

#define cbi_p(tup)		(cbi(*((tup).pport), (tup).num))
#define sbi_p(tup)		(sbi(*((tup).pport), (tup).num))

#define dir_in(tup)		(cbi(*(DDR((tup).pport)), (tup).num))
#define dir_out(tup)	(sbi(*(DDR((tup).pport)), (tup).num))

#define read_p(tup)		((*(PIN((tup).pport)) & (1<<(tup).num)) >> (tup).num)

#include "tus_cfg.h"
#include "packet.h"
#include "tus_spi.h"

#endif /* TUS_H_ */