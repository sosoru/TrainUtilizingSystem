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
#include <stdio.h>
#include <avr/io.h>
#include <avr/delay.h>
#include <avr/interrupt.h>
//#include <avr/timer.h>

// The flag of the end of communication. I can't imagine to detect the end of that by reading register or interruption.
// In the last resort, this flag is set on user code.
static uint8_t is_communicated = FALSE;

//static EthPacket recv_buffer[BUF_COUNT];
//static uint16_t recv_buffer_bytepos = 0;
//#define RECEIVE_BUFFER_BYTESIZE ((uint16_t)((uint16_t)sizeof(EthPacket) * (uint16_t)BUF_COUNT))

static spi_recv_object recv_buffer[BUF_COUNT];
static uint8_t recv_buffer_pos = 0;
static uint8_t recv_buffer_startflag = FALSE;
static spi_recv_object *precving_obj = NULL;

static spi_send_object send_buffer[BUF_COUNT];
static uint8_t send_buffer_pos = 0;
static spi_send_object *psending_obj = NULL;

static spi_received_handler SpiReceive = 0;

//debug
//static char dbgbuffer[2048];
//static int16_t dbgbufferpos = 0;

static inline uint8_t seek_sending_buffer(uint8_t index)
{
	if(!send_buffer[index].is_locked && !send_buffer[index].is_sent)
	{
		psending_obj = &send_buffer[index];
		send_buffer_pos = 1;
		SPDR = send_buffer[index].packet.raw_array[0];
		
		//dbgbufferpos += sprintf(dbgbuffer + dbgbufferpos, "setbuffer %d\n", index);
		return 1; // true		
	}	
	return 0; //false
}

// !SS pin changed interruption
ISR(PCINT1_vect)
{
	uint8_t sspin = TUS_CONTROL_PIN & (1<<TUS_CONTROL_SS);
	
	if(sspin == 0) // !SS pin is low
	{
		recv_buffer_pos = 0;
		if(psending_obj != NULL && send_buffer_pos != sizeof(EthPacket)) // before transmition is aborted incompletely, reset sending buffer
		{
			send_buffer_pos = 1;
			SPDR = psending_obj->packet.raw_array[0];
		}
	}
	else // !SS pin is high
	{
		//receive繰り出し
		if(recv_buffer_pos >= sizeof(EthPacket) && precving_obj->packet.srcId.raw != 0)
		{
			precving_obj->is_recved = TRUE;
			
			if(!recv_buffer[0].is_locked && !recv_buffer[0].is_recved)
			{
				precving_obj = &recv_buffer[0];
				recv_buffer_pos= 0;
			}else if (!recv_buffer[1].is_locked && !recv_buffer[1].is_recved)
			{
				precving_obj = &recv_buffer[1];
				recv_buffer_pos= 0;
			}else if (!recv_buffer[2].is_locked && !recv_buffer[2].is_recved)
			{
				precving_obj = &recv_buffer[2];
				recv_buffer_pos= 0;
			}else if (!recv_buffer[3].is_locked && !recv_buffer[3].is_recved)
			{
				precving_obj = &recv_buffer[3];
				recv_buffer_pos= 0;
			}
		}
		
		//send繰り出し
		//ISR入る前に繰り出しておくこと
		if(send_buffer_pos >= sizeof(EthPacket))
		{
			if(psending_obj != NULL)
				psending_obj->is_sent = TRUE;
			
			if(seek_sending_buffer(0)){}
			else if(seek_sending_buffer(1)){}
			else if(seek_sending_buffer(2)){}
			else if(seek_sending_buffer(3)){}
			else{
				psending_obj = NULL; // どのバッファも送れない
				SPDR = 0;
			}
		}

	}
}

ISR(SPI_STC_vect)
{
	//uint8_t i;
	//uint8_t spi_status = SPSR;
	//uint8_t received = SPDR;
	
	//TCNT2 = 0; // reset timeout
	//
	
	sbi(TUS_CONTROL_DDR, TUS_CONTROL_MISO);
	//receive
	if(recv_buffer_pos < sizeof(EthPacket))
	{
		//if(recv_buffer_startflag == 0)
		//recv_buffer_startflag++; //1バイト目は遅れる?
		//else
		precving_obj->packet.raw_array[recv_buffer_pos++] = SPDR;
	}
	//send
	cbi(SPSR, SPIF);
	if(psending_obj != NULL && send_buffer_pos < sizeof(EthPacket) )
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
		send_buffer[i].is_locked = FALSE;
		send_buffer[i].is_sent = TRUE;
		recv_buffer[i].is_locked = FALSE;
		recv_buffer[i].is_recved = FALSE;
	}
	
	//init spi on slave mode
	cbi(TUS_CONTROL_DDR, TUS_CONTROL_SS); // set them input(0)
	sbi(TUS_CONTROL_PORT, TUS_CONTROL_SS); // pull up
	cbi(TUS_CONTROL_DDR, TUS_CONTROL_MOSI);
	cbi(TUS_CONTROL_DDR, TUS_CONTROL_SCK);
	cbi(TUS_CONTROL_DDR, TUS_CONTROL_MISO);
	
	SPCR =	1 << SPE	// spi enable
	| 1 << SPIE // spi interrupt enable
	//| 1 << DORD	// lsb first
	| 0 << CPHA // modify clock phase
	| 0 << CPOL
	;
	
	cbi(SPSR, SPIF); // clear interrupt flag
	sbi(SPSR, SPI2X);// set double speed flag
	
	psending_obj = &send_buffer[0];
	precving_obj = &recv_buffer[0];
	send_buffer_pos = sizeof(EthPacket);
	recv_buffer_pos = sizeof(EthPacket);
	
	//TCCR2A = 0b11; //fase pwm
	//TCCR2B = 0b100; //fodc / 64
	//TCNT2 = 0;
	//TIMSK2 = 1<<TOIE2; // enable interrupt
	//
	
	//EICRA = 1<<ISC11|0<<ISC10; // PCINT1 falling edge
	PCICR |= 1<<PCIE1; // enable Port B interrupt
	PCMSK1 |= 1<<PCINT12; // !SS pin
	
	
	sei(); //enable global interruption
	
}

void tus_spi_process_packets()
{
	uint8_t reg_cache = SREG;
	uint8_t i, recv_pos_obj, recv_pos_spare;
	args_received e;
	
	//while(IS_SPI_COMMUNICATING) {
		//_delay_us(1);
	//}
	//
	if(SpiReceive != NULL )	//delegate the packet processes if the handler is set
	{
		for(i=0; i<BUF_COUNT; ++i)
		{
			spi_recv_object *precv = &recv_buffer[i];
			
			if(!precv->is_locked && precv->is_recved)
			{
				cli(); //enter critical section
				precv->is_locked = TRUE;
				SREG = reg_cache; //leave critical section
				
				uint8_t bufind = 0;
				e.psrcId = &precv->packet.srcId;
				e.pdstId = &precv->packet.destId;
				e.pos = i;
				e.pos_in_packet = 0;
				
				//extract packet
				while(bufind <= ETH_DATA_LEN && precv->packet.pdata[bufind] != 0x00)
				{
					e.ppack = (uint8_t*)(&precv->packet.pdata[bufind]);
					
					SpiReceive(&e);
					
					bufind += *e.ppack; // += precv->packet.pdata[bufind];
					e.pos_in_packet++;
				}
				
				cli(); //enter critical section
				precv->is_recved = FALSE;
				precv->is_locked = FALSE;
				SREG = reg_cache; //leave critical section
			}
		}
		
	}

	//leave critical section
	//SREG = reg_cache;
}

void tus_spi_set_handler(spi_received_handler handler)
{
	//uint8_t reg_cache = SREG;
	//
//while(IS_SPI_COMMUNICATING) {_delay_us(1);};
//
//cli();
SpiReceive = handler;
//SREG = reg_cache;
}

uint8_t tus_spi_lock_send_buffer(spi_send_object ** ppsendobj)
{
	int8_t i;
	uint8_t reg_cache = SREG;

//while(IS_SPI_COMMUNICATING) { _delay_us(1);}
//
	cli();
//
	for(i=BUF_COUNT-1; i>=0; --i) //繰り出しは昇順
	{
		spi_send_object *obj = &send_buffer[i];
		
		if(obj->is_sent && !obj->is_locked) // the sent objects are available
		{
			obj->is_locked = TRUE;	// the is_locked flag must be cleared when the packet is ready
			obj->is_sent = FALSE;
			
			//dbgbufferpos += sprintf(dbgbuffer + dbgbufferpos, "sendbuffer %d\n", i);
			
			*ppsendobj = obj;
			SREG = reg_cache;
			return TRUE;
		}
	}	
	
	SREG = reg_cache;
	return FALSE;
}
