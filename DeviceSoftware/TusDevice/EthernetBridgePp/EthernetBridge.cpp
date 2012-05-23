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
#include "EthernetBridge.hpp"
#include <avr/io.h>
#include "avr_base.hpp"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <avr/pgmspace.h>
#include <avr/interrupt.h>
#include <avr/wdt.h>
#include "eth/ip_arp_udp_tcp.hpp"
#include "eth/enc28j60.hpp"
#include "eth/timeout.hpp"
#include "eth/avr_compat.hpp"
#include "eth/net.hpp"
#include "eth/arp_table.hpp"

#include "tus_mst/tus_mstcfg.hpp"
#include "LcdCfg.hpp"

using namespace EthernetBridge;

// please modify the following two lines. mac and ip have to be unique
// in your local area network. You can not have the same numbers in
// two devices:
static uint8_t mymac[6] = {0x54,0x55,0x58,0x10,0x00,0x24};
// how did I get the mac addr? Translate the first 3 numbers into ascii is: TUX
static uint8_t myip[4] = {192,168,2,24};

BYTE g_parentid = 24;

// listen port for udp
#define MYUDPPORT 8000

#define BUFFER_SIZE 128
static uint8_t buf[BUFFER_SIZE+1];

//
//SPI_TRANS_PORT g_spi_trans_port = SPI_TRANS_PORTINST;
//
//SPI_SLAVE_PORT g_spi_slave_ports[] = SPI_SLAVE_PORT_ARRAY;
//
void timer_interrupt();

ISR(TIMER1_COMPA_vect)
{				
	//if(bufptr >= LCD_BUF_SIZE)
	//{
		//bufptr = 0;
		//sc2004_SetAddr_DDRAM(0);
	//}
			//
	//sc2004_WriteData(lcd_buf[bufptr++]);
}

void EthernetInit()
{
    _delay_loop_1(50); // 12ms

    /* enable PD4, as input */
	AVRCpp::InputPin4<AVRCpp::PortD>::InitInput();

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

void BoardInit()
{
	uint8_t i;
	
#ifndef DEBUG
	_delay_ms(100);
#endif
			
	DDRA = 0xff;
	DDRB = 0xff;
	DDRC = 0xff;
	DDRD = 0xff;
	DDRE = 0xff;
	//DDRF = 0xff;
	DDRG = 0xff;
	
	PORTA = 0;
	PORTB = 0;
	PORTC = 0;
	PORTD = 0;
	PORTE = 0;
	//PORTF = 0;
	PORTG = 0;
	
	EthernetInit();
	//LCDInit();

	SpiToModule::Init();
	// for module in range(A, H):
	ModuleA::Init();	
	ModuleB::Init();	
	ModuleC::Init();	
	ModuleD::Init();	
	ModuleE::Init();	
	ModuleF::Init();	
	ModuleG::Init();	
	ModuleH::Init();	
} 

inline bool IsForChildren(const EthPacket &packet)
{
	return packet.destId.SubnetAddr == g_parentid;
}

void StockToChildren(const EthPacket* ppacket)
{	
	if(ModuleA::Stock(ppacket)){}
	else if(ModuleB::Stock(ppacket)){}
	else if(ModuleC::Stock(ppacket)){}
	else if(ModuleD::Stock(ppacket)){}
	else if(ModuleE::Stock(ppacket)){}
	else if(ModuleF::Stock(ppacket)){}
	else if(ModuleG::Stock(ppacket)){}
	else if(ModuleH::Stock(ppacket)){}

}

uint8_t ReceiveFromEthernet()
{
	        
    uint16_t plen;
    uint8_t payloadlen=0;

	// get the next new packet:
	plen = enc28j60PacketReceive(BUFFER_SIZE, buf);

	/*plen will ne unequal to zero if there is a valid 
		* packet (without crc error) */
	if(plen==0){
			return false;
	}
    	                    
	// arp is broadcast if unknown but a host may also
	// verify the mac address by sending it to 
	// a unicast address.
	if(eth_type_is_arp_and_my_ip(buf,plen)){
			make_arp_answer_from_request(buf);
			arp_record * rec = (arp_record*)&buf[ETH_ARP_DST_MAC_P];
			eth_arptable_add(rec);
			return true;
	}

	// check if ip packets are for us:
	if(eth_type_is_ip_and_my_ip(buf,plen)==0){
			return true;
	}
                
	if(buf[IP_PROTO_P]==IP_PROTO_ICMP_V && buf[ICMP_TYPE_P]==ICMP_TYPE_ECHOREQUEST_V){
			// a ping packet, let's send pong
			make_echo_reply_from_request(buf,plen);
			return true;
	}
	//
	// udp start, we listen on udp port 8000=0x1F40
	if (buf[IP_PROTO_P]==IP_PROTO_UDP_V&&buf[UDP_DST_PORT_H_P]==0x1F&&buf[UDP_DST_PORT_L_P]==0x40){
			payloadlen=buf[UDP_LEN_L_P]-UDP_HEADER_LEN;
			
			EthPacket * ppacket = (EthPacket*)&buf[UDP_DATA_P];
			if(IsForChildren((const EthPacket&)ppacket))
			{
				StockToChildren(ppacket);
				return true;
			}							
	}			
	
	return false;
//ANSWER:
			//make_udp_reply_from_request(buf,str,strlen(str),MYUDPPORT);
	//
}  

uint8_t FindArcRecord(const EthPacket *ppacket, arp_record *parc)
{	
	memcpy((void*)&parc->ipAddr, (void*)myip, 4);
	parc->ipAddr[3] = 105;// ppacket->destId.SubnetAddr; // Parent part of device id equals last of Ip address
	
	if(eth_arptable_get(parc))
	{
		return true;
	}
	else
	{
		make_arp_request(buf, parc->ipAddr);
		return false;
	}
}

void SendPacket(EthPacket *ppacket, arp_record *parc)
{
	make_udp_request(buf, (char*)ppacket->raw_array, (uint8_t)sizeof(EthPacket), (uint16_t)MYUDPPORT, (uint16_t)MYUDPPORT, (parc));
}

void CreateEthPacketFromReceived(EthPacket * pdest, EthPacket * psrc)
{
	//swap their id
	pdest->srcId.raw = psrc->destId.raw;
	pdest->destId.raw = psrc->srcId.raw;
	
	pdest->command = psrc->command;
	
}

bool SendToEthernet(EthPacket *ppacket)
{
	arp_record rec;
	
	if(FindArcRecord(ppacket, &rec))
	{
		SendPacket(ppacket, &rec);
		return true;
	}
	else
	{
		return false;
	}
}

template < class t_module >
void DispatchModulePackets()
{
	EthPacket received;
	
	do
	{
		
		if(t_module::Transmit(received))
		{
			if(IsForChildren(received))
			{
				StockToChildren(&received);					
			}
			else
			{
				//SendToEthernet(&received);
			}			
		}
		
	}while(t_module::IsStocked());
	
}

void DispatchProcess()
{
	DispatchModulePackets<ModuleA>();
	DispatchModulePackets<ModuleB>();
	DispatchModulePackets<ModuleC>();
	DispatchModulePackets<ModuleD>();
	DispatchModulePackets<ModuleE>();
	DispatchModulePackets<ModuleF>();
	DispatchModulePackets<ModuleG>();
	DispatchModulePackets<ModuleH>();
}

int main(void)
{
	uint8_t i,j;
	
	MCUCSR = 0;
	wdt_disable();
	
//#ifndef DEBUG
	DDRB = 0xff;
	//_delay_ms(1000);
//#endif
	
    BoardInit();
	PORTB |= _BV(PORTB4);
		
	while(1)
	{
		//while(ReceiveFromEthernet());		
		
		DispatchProcess();
	}			
	
	return 0;
}
