/*
 * spi.c
 *
 * Created: 2012/01/21 7:32:37
 *  Author: root
 */ 

#include "spi.h"
#include <string.h>

SPI_PARAMS params_;

#define SCK {sbi_p(params_.SCK); cbi_p(params_.SCK);}
#define SS_IN cbi_p(params_.CS)
#define SS_OUT sbi_p(params_.CS)

void softspi_set_params(SPI_PARAMS *pparams)
{
	memcpy((void*)&params_, (void*)pparams, sizeof(SPI_PARAMS));
}

void softspi_send(BYTE data)
{
	uint8_t i;

	SS_IN;
	
	sbi_p(params_.MISO);
	cbi_p(params_.MISO);
	
	for(i=0; ++i<8; )
	{
		cbi_p(params_.MOSI);
		*params_.MOSI.pport |= (data & 1) << params_.MOSI.num;
		
		SCK;
		data >>= 1;
	}
	
	SS_OUT;
}	

uint8_t softspi_receive()
{
	uint8_t i;
	uint8_t buf =0;
	
	SS_IN;
	
	for(i=0; ++i<8;)
	{
		buf |= ((*params_.MISO.pport >> params_.MISO.num) & 1) << i;
	}
	
	SS_OUT;
}