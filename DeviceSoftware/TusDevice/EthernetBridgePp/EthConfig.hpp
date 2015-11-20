/*
 * EthConfig.hpp
 *
 * Created: 2012/05/18 11:07:27
 *  Author: Administrator
 */ 


#ifndef ETHCONFIG_H_
#define ETHCONFIG_H_

#include <avr/io.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <avr/pgmspace.h>
#include <avr/interrupt.h>
#include <avr/wdt.h>
#include "avr_base.hpp"
#include "eth/ip_arp_udp_tcp.hpp"
#include "eth/enc28j60.hpp"
#include "eth/timeout.hpp"
#include "eth/avr_compat.hpp"
#include "eth/net.hpp"
#include "eth/arp_table.hpp"

namespace EthernetBridge
{
	namespace Eth
	{
		struct NicParameters
		{
			uint8_t macaddress[6];
			uint8_t ipaddress[4];
			uint16_t send_port;
			uint16_t recv_port;
		};
		
		class EthDevice
		{
			private:
									
				static const uint8_t BUFFER_SIZE = 128;
				static uint8_t buf[BUFFER_SIZE];

			public:
			
			static NicParameters Parameters;
			
			static void EthernetInit()
			{
				_delay_loop_1(50); // 12ms

				/* enable PD4, as input */
				AVRCpp::InputPin4<AVRCpp::PortD>::InitInput();

				/*initialize enc28j60*/
				enc28j60Init(Parameters.macaddress);
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

			}
			
			static inline void NicParameterInit()
			{
				//init the ethernet/ip layer:
				init_ip_arp_udp_tcp(Parameters.macaddress, Parameters.ipaddress, Parameters.recv_port);
			}

			static inline bool IsForChildren(const EthPacket &packet)
			{
				return packet.destId.SubnetAddr == Parameters.ipaddress[3]
						&& packet.destId.ModuleAddr <= 8;
			}

			static uint8_t ReceiveFromEthernet()
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
				
				//a packet is comming
				PORTB ^= _BV(PORTB5);
				
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
				
				// udp start, we listen on udp port 8000=0x1F40
				if (buf[IP_PROTO_P]==IP_PROTO_UDP_V
					&& buf[UDP_DST_PORT_H_P]==(uint8_t)(Parameters.recv_port>>8)
					&& buf[UDP_DST_PORT_L_P]==(uint8_t)(Parameters.recv_port))
				{
					payloadlen = buf[UDP_LEN_L_P] - UDP_HEADER_LEN;
							
					EthPacket * ppacket = (EthPacket*)&buf[UDP_DATA_P];
												
					if(IsForChildren(*ppacket))
					{
								PORTB |= _BV(PORTB4);
						StockToChildren(ppacket);
						return true;
					}							
				}			
	
				return false;
			}  
					
			static void StockToChildren(const EthPacket* ppacket)
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
			
			static inline void SendPacket(EthPacket *ppacket, arp_record *parc)
			{
				make_udp_request(buf, (char*)ppacket->raw_array, (uint8_t)sizeof(EthPacket), Parameters.send_port, Parameters.send_port, (parc));
			}

			static bool SendToEthernet(EthPacket *ppacket)
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

			static uint8_t FindArcRecord(const EthPacket *ppacket, arp_record *parc)
			{	
				memcpy((void*)&parc->ipAddr, (void*)Parameters.ipaddress, 4);
				parc->ipAddr[3] = ppacket->destId.SubnetAddr; // Parent part of device id equals last of Ip address
	
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

		};
		
		
		NicParameters Eth::EthDevice::Parameters;
		uint8_t Eth::EthDevice::buf[Eth::EthDevice::BUFFER_SIZE];
		
	}
	
}


#endif /* ETHCONFIG_H_ */