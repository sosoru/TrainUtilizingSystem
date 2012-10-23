/*
 * tus_spi.h
 *
 * Created: 2012/01/23 6:42:43
 *  Author: root
 */ 


#ifndef TUS_SPI_H_
#define TUS_SPI_H_

#include "packet.h"
#include "tus_cfg.h"

#ifdef __cplusplus
extern "C"
{
#endif

typedef struct tag_args_received
{
	DeviceID *psrcId;
	DeviceID *pdstId;
	
	uint8_t *ppack;
	uint8_t pos;
	uint8_t pos_in_packet;
} args_received;

typedef struct tag_spi_send_object
{
	EthPacket packet;
	uint8_t is_locked;
	uint8_t is_sent;
} spi_send_object;

// nSS is set to low while the hardwares are communicating.
#define IS_SPI_COMMUNICATING !(TUS_CONTROL_PIN & (1 << TUS_CONTROL_SS))

typedef void (*spi_received_handler)(args_received* e) ;


void tus_spi_init();
void tus_spi_process_packets();
void tus_spi_set_handler(spi_received_handler handler);
uint8_t tus_spi_lock_send_buffer(spi_send_object ** ppsendobj);

#ifdef __cplusplus
}	// end of extern
#endif
#endif /* TUS_SPI_H_ */