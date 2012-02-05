/*
 * tus_spi.c
 *
 * Created: 2012/01/21 11:46:27
 *  Author: root
 */ 


#define BUF_COUNT 4

// nSS is set to low while the hardwares are communicating.
#define IS_SPI_COMMUNICATING !(TUS_CONTROL_PORT & (1 << TUS_CONTROL_SS))

// The hardware always set the direction of MISO to input(0) when nSS changes L->H.
// Therefore, when we start to communicate the master and the hardware generates interrupt firstly,
// the direction of MISO is always input. The following define is detecting this direction.
#define IS_FIRST_COMMUNICATING !(TUS_CONTROL_DDR & (1 << TUS_CONTROL_MISO))

#include "tus_cfg.h"
#include "../packet.h"
#include "tus_spi.h"
#include <avr/io.h>
#include <avr/delay.h>

// The flag of the end of communication. I can't imagine to detect the end of that by reading register or interruption.
// In the last resort, this flag is set on user code.
static uint8_t is_communicated = FALSE;

static EthPacket recv_buffer[BUF_COUNT];
static uint8_t recv_buffer_pos = 0;
static uint16_t recv_buffer_bytepos = 0;

static EthPacket send_buffer[BUF_COUNT];
static uint8_t send_buffer_pos = 0;
static uint16_t send_buffer_bytepos = 0;

static spi_received_handler SpiReceive = 0;

ISR(SPI_STC_vect)
{
	uint8_t received = SPDR;
	
	if(IS_FIRST_COMMUNICATING)
	{
		sbi(TUS_CONTROL_DDR, TUS_CONTROL_MISO);
		recv_buffer_bytepos = 0;
		recv_buffer_pos = 0;
		
		is_communicated = TRUE;
	}
	
	//receive
	if(recv_buffer_bytepos < sizeof(EthPacket) * BUF_COUNT)
	{
		((uint8_t*)recv_buffer)[recv_buffer_bytepos++] = received;
	}
	
	//send
	if(send_buffer_bytepos < sizeof(EthPacket) * send_buffer_bytepos)
	{
		SPDR = ((uint8_t*)send_buffer)[send_buffer_bytepos++];	
	}	

	return (int)0;	
}

void tus_spi_init()
{
	//init spi on slave mode
	cbi(TUS_CONTROL_DDR, TUS_CONTROL_SS); // set them input(0)
	cbi(TUS_CONTROL_DDR, TUS_CONTROL_MOSI);
	cbi(TUS_CONTROL_DDR, TUS_CONTROL_SCK);
	cbi(TUS_CONTROL_DDR, TUS_CONTROL_MISO); 
	
	SPCR =	1 << SPE	// spi enable
			| 1 << SPIE // spi interrupt enable
			//| 1 << DDRD	// lsb first
			;
	
	cbi(SPSR, SPIF); // clear interrupt flag
	
	sei(); //enable global interruption
}

void tus_spi_process_packets()
{
	uint8_t i;
	args_received e;
		
	while(IS_SPI_COMMUNICATING) { _delay_ms(1); };
	
	recv_buffer_pos = recv_buffer_bytepos / sizeof(EthPacket);
	
	for(i = 0; i < recv_buffer_pos; ++i)
	{
		e.pos = i;
		e.len = recv_buffer_pos+1;
		e.ppack = &recv_buffer[i];
		
		if(!SpiReceive)	
			SpiReceive(&e);
	}
	
	recv_buffer_pos = 0;
	recv_buffer_bytepos = 0;
}

void tus_spi_set_handler(spi_received_handler handler)
{
	SpiReceive = handler;	
}

EthPacket* tus_spi_lock_send_buffer()
{
	if(IS_SPI_COMMUNICATING && send_buffer_pos >= BUF_COUNT)
		return 0;
		
	if(is_communicated)
	{
		is_communicated = FALSE;
		send_buffer_pos = 0;
		send_buffer_bytepos = 0;	
	}
		
	return &send_buffer[send_buffer_pos++];
}
