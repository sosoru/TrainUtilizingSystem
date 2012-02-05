/*********************************************
 * vim:sw=8:ts=8:si:et
 * To use the above modeline in vim you must have "set modeline" in your .vimrc
 * Author: Guido Socher
 * Copyright: GPL V2
 * See http://www.gnu.org/licenses/gpl.html
 *
 * Ethernet remote device and sensor
 * UDP and HTTP interface 
        url looks like this http://baseurl/password/command
        or http://baseurl/password/
 *
 * Chip type           : Atmega88 or Atmega168 or Atmega328 with ENC28J60
 * Note: there is a version number in the text. Search for tuxgraphics
 
 *********************************************/
#include "global.h"
#include <avr/io.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <avr/pgmspace.h>
#include <avr/interrupt.h>
#include "timer.h"
#include "lcd_sc2004/sc2004.h"
#include "eth/ip_arp_udp_tcp.h"
#include "eth/enc28j60.h"
#include "eth/timeout.h"
#include "eth/avr_compat.h"
#include "eth/net.h"
#include "eth/arp_table.h"

#include "common/packet.h"
#include "EthernetBridge.h"
#include "tus_mst/tus_mstcfg.h"
#include "tus_mst/tus_mstspi.h"
#include "tus_mst/dispat_packet.h"
// please modify the following two lines. mac and ip have to be unique
// in your local area network. You can not have the same numbers in
// two devices:
static uint8_t mymac[6] = {0x54,0x55,0x58,0x10,0x00,0x24};
// how did I get the mac addr? Translate the first 3 numbers into ascii is: TUX
static uint8_t myip[4] = {192,168,2,24};

// listen port for udp
#define MYUDPPORT 8000

#define BUFFER_SIZE 128
static uint8_t buf[BUFFER_SIZE+1];

static sc2004_port port_sc2004;

#define LCD_BUF_SIZE (LCD_HEIGHT * LCD_WIDTH)
static char lcd_buf[LCD_BUF_SIZE];
static uint8_t bufptr = 0;

#define	TRANS_LCD(p)	cli(); \
						memcpy((void*)lcd_buf, (void*)(p), LCD_BUF_SIZE); \
						sei();


BYTE g_parentid = 24;

SPI_TRANS_PORT g_spi_trans_port = SPI_TRANS_PORTINST;
SPI_TRANS_DIRECTION g_spi_trans_dir = SPI_TRANS_DIRINST;

SPI_SLAVE_PORT g_spi_slave_ports[] = SPI_SLAVE_PORT_ARRAY;
SPI_SLAVE_DIRECTION g_spi_slave_dirs[] = SPI_SLAVE_DIR_ARRAY;

void timer_interrupt();

ISR(TIMER1_COMPA_vect)
{
	//if(sc2004_ReadBusyAndAddress(0))
		//return; // busy
				
	if(bufptr >= LCD_BUF_SIZE)
	{
		bufptr = 0;
		sc2004_SetAddr_DDRAM(0);
	}
			
	sc2004_WriteData(lcd_buf[bufptr++]);
}

void EthernetInit()
{
	// set the clock speed to "no pre-scaler" (8MHz with internal osc or 
    // full external speed)
    // set the clock prescaler. First write CLKPCE to enable setting of clock the
    // next four instructions.
    //CLKPR=(1<<CLKPCE); // change enable
    //CLKPR=0; // "no pre-scaler"
    _delay_loop_1(50); // 12ms

    /* enable PD4, as input */
    DDRD&= ~(1<<DDD4);

    /*initialize enc28j60*/
    enc28j60Init(mymac);
    enc28j60clkout(2); // change clkout from 6.25MHz to 12.5MHz
    _delay_loop_1(50); // 12ms
        
    /* Magjack leds configuration, see enc28j60 datasheet, page 11 */
    // LEDB=yellow LEDA=green
    //
    // 0x476 is PHLCON LEDA=links status, LEDB=receive/transmit
    // enc28j60PhyWrite(PHLCON,0b0000 0100 0111 01 10);
    enc28j60PhyWrite(PHLCON,0x476);
    _delay_loop_1(50); // 12ms
        
    /* set output to GND, red LED on */
    //PORTB &= ~(1<<PORTB1);
    //i=1;
//
    //init the ethernet/ip layer:
    init_ip_arp_udp_tcp(mymac,myip,80);

}

void LCDInit()
{
	port_sc2004.pport_data = &PORTC;
	port_sc2004.ppin_data = &PINC;
	port_sc2004.pddr_data = &DDRC;
	
	port_sc2004.pport_rs = &PORTF;
	port_sc2004.ppin_rs = &PINF;
	port_sc2004.pddr_rs = &DDRF;
	port_sc2004.portno_rs = 6;
	
	port_sc2004.pport_rw = &PORTE;
	port_sc2004.ppin_rw = &PINE;
	port_sc2004.pddr_rw = &DDRE;
	port_sc2004.portno_rw = 3;
	
	port_sc2004.pport_enable = &PORTE;
	port_sc2004.ppin_enable = &PINE;
	port_sc2004.pddr_enable = &DDRE;
	port_sc2004.portno_enable = 2;
	
	sc2004_setPort(&port_sc2004);
	sc2004_init();

	memset(lcd_buf, 0x20, LCD_BUF_SIZE);
	
	//timer0Init();
	//timer0SetPrescaler(TIMER_CLK_DIV8);
	//timerAttach(TIMER0OUTCOMPARE_INT, timer_interrupt);
	
	    // 15.11.1 タイマ／カウンタ1制御レジスタA (初期値は0x00なので必要ない)
    //         ++-------COM1A1:COM1A0 00 OC1A切断
    //         ||++---- COM1B1:COM1B0 00 OC1B切断
    //         ||||  ++ WGM11:WGM10   00 波形生成種別(4bitの下位2bit)
    TCCR1A = 0b00000000;

    // 15.11.2 タイマ／カウンタ1制御レジスタB
    //         +------- ICNC1          0
    //         |+------ ICES1          0
    //         || ++--- WGM13:WGM12    01  波形生成種別(4bitの上位2bit) CTC top=OCR1A
    //         || ||+++ CS12:CS11:CS10 010 8分周
    TCCR1B = 0b00001100;

    TIMSK = 0b00010000;
	OCR1A = 250;
	sei();
}

void BoardInit()
{
	uint8_t i;
	
	DDRA = 0xff;
	DDRB = 0xff;
	DDRC = 0xff;
	DDRD = 0xff;
	DDRE = 0xff;
	DDRF = 0xff;
	DDRG = 0xff;
	
	PORTA = 0;
	PORTB = 0;
	PORTC = 0;
	PORTD = 0;
	PORTE = 0;
	PORTF = 0;
	PORTG = 0;
	
	EthernetInit();
	LCDInit();
	
	for(i=0; i<DEVICES_COUNT; ++i)
	{
		dispat_init_buffer(i);
	}
	
	//spi
	tus_mstspi_trans_init(&g_spi_trans_port, &g_spi_trans_dir);
	for(i=0; i<DEVICES_COUNT; ++i)
	{
		tus_mstspi_slave_init(&g_spi_slave_ports[i], &g_spi_slave_dirs[i]);
	}
	
	
} 

uint8_t EthernetProcess()
{
	        
    uint16_t plen;
    uint8_t payloadlen=0;

	// get the next new packet:
	plen = enc28j60PacketReceive(BUFFER_SIZE, buf);

	/*plen will ne unequal to zero if there is a valid 
		* packet (without crc error) */
	if(plen==0){
			return FALSE;
	}
    	                    
	// arp is broadcast if unknown but a host may also
	// verify the mac address by sending it to 
	// a unicast address.
	if(eth_type_is_arp_and_my_ip(buf,plen)){
			make_arp_answer_from_request(buf);
			arp_record * rec = (arp_record*)&buf[ETH_ARP_DST_MAC_P];
			eth_arptable_add(rec);
			return TRUE;
	}

	// check if ip packets are for us:
	if(eth_type_is_ip_and_my_ip(buf,plen)==0){
			return TRUE;
	}
                
	if(buf[IP_PROTO_P]==IP_PROTO_ICMP_V && buf[ICMP_TYPE_P]==ICMP_TYPE_ECHOREQUEST_V){
			// a ping packet, let's send pong
			make_echo_reply_from_request(buf,plen);
			return TRUE;
	}
	//
	// udp start, we listen on udp port 8000=0x1F40
	if (buf[IP_PROTO_P]==IP_PROTO_UDP_V&&buf[UDP_DST_PORT_H_P]==0x1F&&buf[UDP_DST_PORT_L_P]==0x40){
			payloadlen=buf[UDP_LEN_L_P]-UDP_HEADER_LEN;
			
			EthPacket * ppacket = (EthPacket*)&buf[UDP_DATA_P];
			if(ppacket->destId.ParentPart == g_parentid)
			{
				uint8_t id = ppacket->destId.ModuleAddr;
				EthPacket * pdest;
				
				if(dispat_inc_buffer(id, &pdest))
				{
					memcpy((void*)pdest, (void*)ppacket, sizeof(EthPacket));
				}	
				
				return TRUE;
			}							
	}			
	
	return FALSE;
//ANSWER:
			//make_udp_reply_from_request(buf,str,strlen(str),MYUDPPORT);
	//
}  

uint8_t FindArcRecord(EthPacket *ppacket, arp_record *parc)
{	
	memcpy((void*)&parc->ipAddr, (void*)myip, 4);
	parc->ipAddr[3] = ppacket->destId.ParentPart; // Parent part of device id equals last of Ip address
	
	if(eth_arptable_get(parc))
	{
		return TRUE;
	}else
	{
		make_arp_request(buf, parc->ipAddr);
		return FALSE;
	}
}

void SendPacket(EthPacket *ppacket, arp_record *parc)
{
	make_udp_request(buf, (uint8_t*)ppacket, sizeof(EthPacket), MYUDPPORT, MYUDPPORT, parc);
}

//void eth_packet_received(EthPacket * ppack)
//{
	//if(ppack->destId.raw != 0)
		//return;
	//
	//if(ppack->command == ETHCMD_REPLY)
	//{
		//EthPacket reply;
		//CreateEthPacketFromReceived(&reply, ppack);
		//
		//sprintf(reply.pdata, "replyed");			
		//
		//reply.error = 0;
		//eth_packet_send(&reply);
	//}
//}
//
//void eth_packet_send(EthPacket *ppack)
//{
	//
//}
//
void CreateEthPacketFromReceived(EthPacket * pdest, EthPacket * psrc)
{
	//swap their id
	pdest->srcId.raw = psrc->destId.raw;
	pdest->destId.raw = psrc->srcId.raw;
	
	pdest->command = psrc->command;
	
}

int main(void)
{
	uint8_t i,j;
	
    BoardInit();
	
	while(1)
	{
		//char str[80];
							
		//for(i=0; i<40; ++i)
		//{
			//char tmpstr[3];
			//sprintf(tmpstr, "%2x", ((uint8_t*)(&g_arpTable[i/10]))[i%10]);
			//
			//memcpy((void*)&str[i*2], (void*)tmpstr, 2);
		//}
		//
		//cli();
		//memcpy((void*)lcd_buf, (void*)str, 80);
		//sei();
		//
		//_delay_ms(255);
		//_delay_ms(255);	
		
		//if(count % 10000 == 0)
		//{
			//uint8_t buf[42];
			//uint8_t destip[4] = {192, 168, 2, 19};
			//make_arp_request(buf, destip);
		//}	
		//
		EthernetProcess();
		
		//spi process
		for(i=0; i<DEVICES_COUNT; ++i)
		{
			EthPacket buffer[PACKET_BUFFER_COUNT];
			EthPacket *psend;
			
			tus_mstspi_slave_init(&g_spi_slave_ports[i], &g_spi_slave_dirs[i]);
			
			for(j=0; j<PACKET_BUFFER_COUNT; ++j)
			{
				if(dispat_pop_buffer(i, &psend))
				{
					tus_mstspi_trans(psend, &buffer[i]);						
				}
				else
				{
					tus_mstspi_trans(NULL, &buffer[i]);
				}
			}
						
			for(j=0; j<PACKET_BUFFER_COUNT; ++j)
			{
				EthPacket *pwrite;
				if(dispat_inc_buffer(i, &pwrite))
				{
					memcpy((void*)pwrite, (void*)&buffer[i], sizeof(EthPacket));	
				}
			}
		}
		
		//eth send process
		for(i=0; i<DEVICES_COUNT; ++i)
		{
			arp_record rec;
			EthPacket *ppacket;
				
			while(dispat_pop_buffer(i, &ppacket))
			{
				if(FindArcRecord(ppacket, &rec))
				{
					SendPacket(ppacket, &rec);
				}
			}
		}
	}			
					
    return (0);
}
