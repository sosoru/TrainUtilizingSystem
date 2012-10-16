/*
 * tus_spi.c
 *
 * Created: 2012/01/21 11:46:27
 *  Author: root
 */ 


#define BUF_COUNT 4

#include "tus.h"
#include "tus_cfg.h"
#include "packet.h"
#include "tus_spi.h"
#include <stdint.h>
#include <stdlib.h>
#include <avr/io.h>
#include <avr/delay.h>
#include <avr/interrupt.h>

// The flag of the end of communication. I can't imagine to detect the end of that by reading register or interruption.
// In the last resort, this flag is set on user code.
static uint8_t is_communicated = FALSE;

static EthPacket recv_buffer[BUF_COUNT];
static uint16_t recv_buffer_bytepos = 0;
#define RECEIVE_BUFFER_BYTESIZE ((uint16_t)((uint16_t)sizeof(EthPacket) * (uint16_t)BUF_COUNT))

static spi_send_object send_buffer[BUF_COUNT];
static uint8_t send_buffer_pos = 0;
static spi_send_object *psending_obj = NULL;

static spi_received_handler SpiReceive = 0;

ISR(SPI_STC_vect)
{
	uint8_t i;
	//uint8_t spi_status = SPSR;
	//uint8_t received = SPDR;
			
	sbi(TUS_CONTROL_DDR, TUS_CONTROL_MISO);
	//receive
	if(recv_buffer_bytepos < RECEIVE_BUFFER_BYTESIZE)
	{
		((uint8_t*)recv_buffer)[recv_buffer_bytepos++] = SPDR;
		
		if(recv_buffer_bytepos >= RECEIVE_BUFFER_BYTESIZE)
		{
			recv_buffer_bytepos = 0;
		}
	}
	
	//send	
	cbi(SPSR, SPIF);
	if(psending_obj != NULL && send_buffer_pos < sizeof(EthPacket))
	{
		uint8_t wdata = psending_obj->packet.raw_array[send_buffer_pos++];
		SPDR = 	wdata;
	}
	else
	{
		SPDR = 0x00;
	}
	
}

void tus_spi_init()
{
	uint8_t i;
	
	for(i=0; i<BUF_COUNT; ++i)
	{
		send_buffer[i].is_sent = TRUE;
	}
	
	//init spi on slave mode
	cbi(TUS_CONTROL_DDR, TUS_CONTROL_SS); // set them input(0)
	cbi(TUS_CONTROL_DDR, TUS_CONTROL_MOSI);
	cbi(TUS_CONTROL_DDR, TUS_CONTROL_SCK);
	cbi(TUS_CONTROL_DDR, TUS_CONTROL_MISO); 
	
	SPCR =	1 << SPE	// spi enable
			| 1 << SPIE // spi interrupt enable
			| 1 << DDRD	// lsb first
			| 1 << CPHA // modify clock phase
			;
	
	cbi(SPSR, SPIF); // clear interrupt flag
	sbi(SPSR, SPI2X);// set double speed flag
	
	sei(); //enable global interruption
}

void tus_spi_process_packets()
{
	uint8_t reg_cache = SREG;
	uint8_t i, recv_pos_obj, recv_pos_spare;
	args_received e;
		
	while(IS_SPI_COMMUNICATING) { _delay_us(100); };
	
	//enter the spi critical section
	cli();
	
	recv_buffer_bytepos++; //to received size
	recv_pos_obj = recv_buffer_bytepos / sizeof(EthPacket);
	//recv_pos_spare = recv_buffer_bytepos % sizeof(EthPacket);
	//
	//if(recv_pos_spare < sizeof(EthPacket)-1 ) // if communication is aborted
	//{
		//recv_pos_obj--;
	//}
	
	if(psending_obj == NULL || send_buffer_pos >= sizeof(EthPacket))
	{
		if(psending_obj != NULL)
		{
			psending_obj->is_sent = TRUE;
		}
		
		psending_obj = NULL;
		send_buffer_pos = 1;	// set first data later
				
		for(i=0; i<BUF_COUNT; ++i)
		{
			spi_send_object *pobj = &send_buffer[i];
			
			if(!(pobj->is_locked || pobj->is_sent))
			{				
				psending_obj = pobj;
				SPDR = pobj->packet.raw_array[0];	// ready to send
				break;
			}
		}
	}
	
	if(SpiReceive != NULL)	//delegate the packet processes if the handler is set
	{
		for(i = 0; i < recv_pos_obj; ++i)
		{
			e.pos = i;
			e.ppack = &recv_buffer[i];
		
			SpiReceive(&e);
		}		
	}
	
	recv_buffer_bytepos = 0;
	
	//leave critical section
	SREG = reg_cache;
}

void tus_spi_set_handler(spi_received_handler handler)
{
	uint8_t reg_cache = SREG;
	
	while(IS_SPI_COMMUNICATING) {_delay_us(100);};
	
	cli();
	SpiReceive = handler;	
	SREG = reg_cache;
}

uint8_t tus_spi_lock_send_buffer(spi_send_object ** ppsendobj)
{	
	uint8_t i;
	uint8_t reg_cache = SREG;	

	while(IS_SPI_COMMUNICATING) { _delay_us(100);}		
	
	cli();
	
	for(i=0; i<BUF_COUNT; ++i)
	{
		spi_send_object *obj = &send_buffer[i];
		
		if(obj->is_sent) // the sent objects are available
		{
			obj->is_sent = FALSE;
			obj->is_locked = TRUE;	// the is_locked flag must be cleared when the packet is ready
			
			*ppsendobj = obj;
			break;			
		}
	}
	
	SREG = reg_cache;
	
	return *ppsendobj != NULL;
}
