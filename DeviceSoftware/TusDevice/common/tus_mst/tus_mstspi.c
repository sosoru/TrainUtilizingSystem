/*
 * tus_mstspi.c
 *
 * Created: 2012/01/30 15:18:28
 *  Author: root
 */ 

#include <stdlib.h>
#include "tus_mstspi.h"

static SPI_TRANS_PORT cur_trans;
static SPI_SLAVE_PORT cur_slave;

#define CYC_SCK {sbi_p(cur_trans.SCK); cbi_p(cur_trans.SCK);}

void tus_mstspi_slave_init(SPI_SLAVE_PORT *pport, SPI_SLAVE_DIRECTION *pdir)
{
	memcpy((void*)&cur_slave, (void*)pport, sizeof(SPI_SLAVE_PORT));

	//init
	cbi_p(pdir->nSS);	//nSS : output. high
	sbi_p(cur_slave.nSS);
	cbi_p(pdir->nRESET);//RESET : output. high
	sbi_p(cur_slave.nRESET);
}

void tus_mstspi_trans_init(SPI_TRANS_PORT *pport, SPI_TRANS_DIRECTION *pdir)
{
	memcpy((void*)&cur_trans, (void*)pport, sizeof(SPI_TRANS_DIRECTION));
	
	//init
	sbi_p(pdir->MISO);	//MISO : input
	
	cbi_p(pdir->MOSI);	//MOSI : output, low
	cbi_p(cur_trans.MOSI);
	
	cbi_p(pdir->SCK);	//SCK : output, low
	cbi_p(cur_trans.SCK);
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
		}else
		{
			send = (*(uint8_t*)ppack)[i];
		}
		
		byte_recev = tus_mstspi_byte_trans(send);
		
		*(uint8_t*)preceived[i] = preceived;
	}
	
	sbi_p(cur_slave.nSS);
}

uint8_t tus_mstspi_byte_trans(uint8_t data)
{
	uint8_t receive = 0;
	uint8_t i;
	
	for(i = 0; i< sizeof(uint8_t); ++i)
	{
		if(data & 1)
		{
			sbi_p(cur_trans.MOSI);
		}else
		{
			cbi_p(cur_trans.MOSI);
		}
		
		data >>= 1;
		CYC_SCK();
		
		receive |= read_p(cur_trans.MISO);
		receive <<= 1;
	}
	
	return receive;
}


