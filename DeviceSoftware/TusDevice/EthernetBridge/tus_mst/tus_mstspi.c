/*
 * tus_mstspi.c
 *
 * Created: 2012/01/30 15:18:28
 *  Author: root
 */ 

#include <avr/io.h>
#include <avr/pgmspace.h>
#include <util/delay.h>
#include <stdlib.h>
#include <string.h>
#include "tus_mstspi.h"

PROGMEM SPI_TRANS_PORT pgm_trans_port = SPI_TRANS_PORTINST;
PROGMEM SPI_SLAVE_PORT pgm_slave_port[] = SPI_SLAVE_PORT_ARRAY;

SPI_TRANS_PORT cur_trans;
SPI_SLAVE_PORT cur_slave;

void tus_mstspi_trans_init()
{
	memcpy_PF((void*)&cur_trans, (PGM_VOID_P)&pgm_trans_port, (size_t)sizeof(SPI_TRANS_PORT));
	
	//init
	dir_in(cur_trans.MISO);	//MISO : input
	
	dir_out(cur_trans.MOSI);	//MOSI : output, low
	cbi_p(cur_trans.MOSI);
	
	dir_out(cur_trans.SCK);	//SCK : output, low
	cbi_p(cur_trans.SCK);
}

void tus_mstspi_slave_init(uint8_t id)
{
	if(id >= DEVICES_COUNT)
		return;
	
	memcpy_P((void*)&cur_slave, (PGM_VOID_P)&pgm_slave_port[id], sizeof(SPI_SLAVE_PORT));
	
	//init
	dir_out(cur_slave.nSS);	//nSS : output. high
	sbi_p(cur_slave.nSS);
	dir_out(cur_slave.nRESET);//RESET : output. high
	sbi_p(cur_slave.nRESET);
}

uint8_t tus_mstspi_byte_trans(uint8_t data)
{
	uint8_t receive = 0;
	uint8_t i;
		
	for(i = 0; i< 8; ++i)
	{
		if(data & 1)
		{
			sbi_p(cur_trans.MOSI);
		}
		else
		{
			cbi_p(cur_trans.MOSI);
		}
		
		data >>= 1;	
		
		sbi_p(cur_trans.SCK);
		receive |= read_p(cur_trans.MISO) << 7-i;
		cbi_p(cur_trans.SCK);
	}
	
	return receive;
}

void tus_mstspi_trans(EthPacket *ppack, EthPacket *preceived)
{
	uint8_t i;

	cbi_p(cur_slave.nSS); // notify starting transfer for slave 
	
	for(i=0; i< sizeof(EthPacket); ++i)
	{
		uint8_t send;
		uint8_t byte_recev;
		
		if(ppack == NULL)
		{
			send = 0;
		}
		else
		{
			send = ppack->raw_array[i];
		}
		
		byte_recev = tus_mstspi_byte_trans(send);
		
		preceived->raw_array[i] = byte_recev;
	}
	
	sbi_p(cur_slave.nSS);
}


