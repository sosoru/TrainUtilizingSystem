/*********************************************
 * vim:sw=8:ts=8:si:et
 * To use the above modeline in vim you must have "set modeline" in your .vimrc
 *
 * Author: Guido Socher 
 * Copyright: GPL V2
 * See http://www.gnu.org/licenses/gpl.html
 *
 * IP, Arp, UDP and TCP functions.
 *
 * The TCP implementation uses some size optimisations which are valid
 * only if all data can be sent in one single packet. This is however
 * not a big limitation for a microcontroller as you will anyhow use
 * small web-pages. The TCP stack is therefore a SDP-TCP stack (single data packet TCP).
 *
 * A big advantage of this stack is that it can actually handle really serious
 * traffic. You can have many (e.g 100) users accessing a web-page almost in parallel.
 *
 * Chip type           : atmega88/atmega168/atmega328 with enc28j60
 *********************************************/
#include <avr/io.h>
#include <avr/pgmspace.h>
#include <stdio.h>
#include <string.h>
#include "avr_compat.h"
#include "net.h"
#include "enc28j60.h"
#include "arp_table.h"

static uint8_t wwwport=80; // 80 is just a default value. Gets overwritten during init
static uint8_t macaddr[6];
static uint8_t ipaddr[4];
static int16_t info_hdr_len=0;
static int16_t info_data_len=0;

static uint8_t macempty[6] = {0, 0, 0, 0, 0, 0};
static uint8_t ipempty[4] =  { 0, 0, 0, 0};

// The Ip checksum is calculated over the ip header only starting
// with the header length field and a total length of 20 bytes
// unitl ip.dst
// You must set the IP checksum field to zero before you start
// the calculation.
// len for ip is 20.
//
// For UDP/TCP we do not make up the required pseudo header. Instead we 
// use the ip.src and ip.dst fields of the real packet:
// The udp checksum calculation starts with the ip.src field
// Ip.src=4bytes,Ip.dst=4 bytes,Udp header=8bytes + data length=16+len
// In other words the len here is 8 + length over which you actually
// want to calculate the checksum.
// You must set the checksum field to zero before you start
// the calculation.
// len for udp is: 8 + 8 + data length
// len for tcp is: 4+4 + 20 + option len + data length
//
// For more information on how this algorithm works see:
// http://www.netfor2.com/checksum.html
// http://www.msc.uky.edu/ken/cs471/notes/chap3.htm
// The RFC has also a C code example: http://www.faqs.org/rfcs/rfc1071.html
uint16_t checksum(uint8_t *buf, uint16_t len,uint8_t type){
        // type 0=ip 
        //      1=udp
        //      2=tcp
        uint32_t sum = 0;

        //if(type==0){
        //        // do not add anything
        //}
        if(type==1){
                sum+=IP_PROTO_UDP_V; // protocol udp
                // the length here is the length of udp (data+header len)
                // =length given to this function - (IP.scr+IP.dst length)
                sum+=len-8; // = real tcp len
        }
        if(type==2){
                sum+=IP_PROTO_TCP_V; 
                // the length here is the length of tcp (data+header len)
                // =length given to this function - (IP.scr+IP.dst length)
                sum+=len-8; // = real tcp len
        }
        // build the sum of 16bit words
        while(len >1){
                sum += 0xFFFF & (((uint32_t)*buf<<8)|*(buf+1));
                buf+=2;
                len-=2;
        }
        // if there is a byte left then add it (padded with zero)
        if (len){
                sum += ((uint32_t)(0xFF & *buf))<<8;
        }
        // now calculate the sum over the bytes in the sum
        // until the result is only 16bit long
        while (sum>>16){
                sum = (sum & 0xFFFF)+(sum >> 16);
        }
        // build 1's complement:
        return( (uint16_t) sum ^ 0xFFFF);
}

// you must call this function once before you use any of the other functions:
void init_ip_arp_udp_tcp(uint8_t *mymac,uint8_t *myip,uint8_t wwwp){
        uint8_t i=0;
        wwwport=wwwp;
        while(i<4){
                ipaddr[i]=myip[i];
                i++;
        }
        i=0;
        while(i<6){
                macaddr[i]=mymac[i];
                i++;
        }
		
		eth_init_arptable();
}

uint8_t eth_type_is_arp_and_my_ip(uint8_t *buf,uint16_t len){
        uint8_t i=0;
        //  
        if (len<41){
                return(0);
        }
        if(buf[ETH_TYPE_H_P] != ETHTYPE_ARP_H_V || 
           buf[ETH_TYPE_L_P] != ETHTYPE_ARP_L_V){
                return(0);
        }
        while(i<4){
                if(buf[ETH_ARP_DST_IP_P+i] != ipaddr[i]){
                        return(0);
                }
                i++;
        }
        return(1);
}

uint8_t eth_type_is_ip_and_my_ip(uint8_t *buf,uint16_t len){
        uint8_t i=0;
        //eth+ip+udp header is 42
        if (len<42){
                return(0);
        }
        if(buf[ETH_TYPE_H_P]!=ETHTYPE_IP_H_V || 
           buf[ETH_TYPE_L_P]!=ETHTYPE_IP_L_V){
                return(0);
        }
        if (buf[IP_HEADER_LEN_VER_P]!=0x45){
                // must be IP V4 and 20 byte header
                return(0);
        }
        while(i<4){
                if(buf[IP_DST_P+i]!=ipaddr[i]){
                        return(0);
                }
                i++;
        }
        return(1);
}
// make a return eth header from a received eth packet
void make_eth(uint8_t *buf, uint8_t *pdestmac)
{
        uint8_t i=0;
		
        //copy the destination mac from the source and fill my mac into src
		if(pdestmac == NULL)
		{
			pdestmac = &buf[ETH_SRC_MAC]; //swap mac addr
		}			
        while(i<6){
                buf[ETH_DST_MAC +i]=pdestmac[i];
                buf[ETH_SRC_MAC +i]=macaddr[i];
                i++;
        }
}

void make_arp(uint8_t *buf, uint8_t *pdestmac, uint8_t *pdestip)
{
    uint8_t i=0;

	buf[ETH_TYPE_L_P] = ETHTYPE_ARP_L_V;
	buf[ETH_TYPE_H_P] = ETHTYPE_ARP_H_V;
	buf[ETH_ARP_HTYPE_L_P] = ETH_ARP_HTYPE_L_V;
	buf[ETH_ARP_HTYPE_H_P] = ETH_ARP_HTYPE_H_V;
	buf[ETH_ARP_PTYPE_L_P] = ETH_ARP_PTYPE_L_V;
	buf[ETH_ARP_PTYPE_H_P] = ETH_ARP_PTYPE_H_V;
	buf[ETH_ARP_HLEN_P] = ETH_ARP_HLEN_V;
	buf[ETH_ARP_PLEN_P] = ETH_ARP_PLEN_V;

    // fill the mac addresses:
    while(i<6){
            buf[ETH_ARP_DST_MAC_P+i]=pdestmac[i];
            buf[ETH_ARP_SRC_MAC_P+i]=macaddr[i];
            i++;
    }
    i=0;
    while(i<4){
            buf[ETH_ARP_DST_IP_P+i]=pdestip[i];
            buf[ETH_ARP_SRC_IP_P+i]=ipaddr[i];
            i++;
    }

} 

void fill_ip_hdr_checksum(uint8_t *buf)
{
        uint16_t ck;
        // clear the 2 byte checksum
        buf[IP_CHECKSUM_P]=0;
        buf[IP_CHECKSUM_P+1]=0;
        buf[IP_FLAGS_P]=0x40; // don't fragment
        buf[IP_FLAGS_P+1]=0;  // fragement offset
        buf[IP_TTL_P]=64; // ttl
        // calculate the checksum:
        ck=checksum(&buf[IP_P], IP_HEADER_LEN,0);
        buf[IP_CHECKSUM_P]=ck>>8;
        buf[IP_CHECKSUM_P+1]=ck& 0xff;
}

// make a return ip header from a received ip packet
void make_ip(uint8_t *buf)
{
        uint8_t i=0;
        while(i<4){
                buf[IP_DST_P+i]=buf[IP_SRC_P+i];
                buf[IP_SRC_P+i]=ipaddr[i];
                i++;
        }
        fill_ip_hdr_checksum(buf);
}

void make_ip_from_arp(uint8_t *buf, arp_record* prec)
{
	uint8_t i;
	buf[IP_P]	= 0x45;
	buf[IP_P+1] = 0x00;
	
	for(i=0; i<4; ++i)
	{
		buf[IP_DST_P+i] = prec->ipAddr[i];
		buf[IP_SRC_P+i] = ipaddr[i];
	}	
	fill_ip_hdr_checksum(buf);
}

void make_arp_answer_from_request(uint8_t *buf)
{
        make_eth(buf, &buf[ETH_SRC_MAC]);
        buf[ETH_ARP_OPCODE_H_P]=ETH_ARP_OPCODE_REPLY_H_V;
        buf[ETH_ARP_OPCODE_L_P]=ETH_ARP_OPCODE_REPLY_L_V;
		
		make_arp(buf, &buf[ETH_ARP_SRC_MAC_P], &buf[ETH_ARP_SRC_IP_P]);

        // eth+arp is 42 bytes:
        enc28j60PacketSend(42,buf); 
}

void make_arp_request(uint8_t *buf, uint8_t * pdestip)
{
	//make_eth
	make_eth(buf, macempty);
			
	buf[ETH_ARP_OPCODE_H_P] = ETH_ARP_OPCODE_REQUEST_H_V;
	buf[ETH_ARP_OPCODE_L_P] = ETH_ARP_OPCODE_REQUEST_L_V;
	
	make_arp(buf, macempty, pdestip);
	    // eth+arp is 42 bytes:
    enc28j60PacketSend(42,buf); 
}

void make_echo_reply_from_request(uint8_t *buf,uint16_t len)
{
        make_eth(buf, NULL);
        make_ip(buf);
        buf[ICMP_TYPE_P]=ICMP_TYPE_ECHOREPLY_V;
        // we changed only the icmp.type field from request(=8) to reply(=0).
        // we can therefore easily correct the checksum:
        if (buf[ICMP_CHECKSUM_P] > (0xff-0x08)){
                buf[ICMP_CHECKSUM_P+1]++;
        }
        buf[ICMP_CHECKSUM_P]+=0x08;
        //
        enc28j60PacketSend(len,buf);
}

void make_udp_request(uint8_t *buf, char *data, uint8_t datalen,uint16_t srcport, uint16_t dstport, arp_record *parp)
{
	uint16_t ck;
	
	make_eth(buf, parp->macAddr);
	if(datalen>220)
		datalen=220;
	
	buf[ETH_TYPE_H_P] = ETHTYPE_IP_H_V;
	buf[ETH_TYPE_L_P] = ETHTYPE_IP_L_V;
		
	buf[IP_TOTLEN_H_P] = 0;
	buf[IP_TOTLEN_L_P] = IP_HEADER_LEN+UDP_HEADER_LEN+datalen;
	buf[IP_PROTO_P]	   = IP_PROTO_UDP_V;
	
	make_ip_from_arp(buf, parp);
	
	buf[UDP_DST_PORT_H_P] = dstport >> 8;
	buf[UDP_DST_PORT_L_P] = dstport & 0xff;
	buf[UDP_SRC_PORT_H_P] = srcport >> 8;
	buf[UDP_SRC_PORT_L_P] = srcport & 0xff;
	
	buf[UDP_LEN_H_P] = 0;
	buf[UDP_LEN_L_P] = UDP_HEADER_LEN + datalen;
	
	buf[UDP_CHECKSUM_H_P] = 0;
	buf[UDP_CHECKSUM_L_P] = 0; // clear
	 
	memcpy((void * )&buf[UDP_DATA_P], (void * )data, datalen);
	ck = checksum(&buf[IP_SRC_P], 16 + datalen, 1);
	buf[UDP_CHECKSUM_H_P] = ck >> 8;
	buf[UDP_CHECKSUM_L_P] = ck & 0xff;
	
	enc28j60PacketSend(ETH_HEADER_LEN + IP_HEADER_LEN + UDP_HEADER_LEN + datalen, buf);
}  

// you can send a max of 220 bytes of data
void make_udp_reply_from_request(uint8_t *buf,char *data,uint8_t datalen,uint16_t port)
{
        uint8_t i=0;
        uint16_t ck;
        make_eth(buf, &buf[ETH_SRC_MAC]);
        if (datalen>220){
                datalen=220;
        }
        // total length field in the IP header must be set:
        buf[IP_TOTLEN_H_P]=0;
        buf[IP_TOTLEN_L_P]=IP_HEADER_LEN+UDP_HEADER_LEN+datalen;
        make_ip(buf);
        // send to port:
        //buf[UDP_DST_PORT_H_P]=port>>8;
        //buf[UDP_DST_PORT_L_P]=port & 0xff;
        // sent to port of sender and use "port" as own source:
        buf[UDP_DST_PORT_H_P]=buf[UDP_SRC_PORT_H_P];
        buf[UDP_DST_PORT_L_P]= buf[UDP_SRC_PORT_L_P];
        buf[UDP_SRC_PORT_H_P]=port>>8;
        buf[UDP_SRC_PORT_L_P]=port & 0xff;
        // calculte the udp length:
        buf[UDP_LEN_H_P]=0;
        buf[UDP_LEN_L_P]=UDP_HEADER_LEN+datalen;
        // zero the checksum
        buf[UDP_CHECKSUM_H_P]=0;
        buf[UDP_CHECKSUM_L_P]=0;
        // copy the data:
        while(i<datalen){
                buf[UDP_DATA_P+i]=data[i];
                i++;
        }
        ck=checksum(&buf[IP_SRC_P], 16 + datalen,1);
        buf[UDP_CHECKSUM_H_P]=ck>>8;
        buf[UDP_CHECKSUM_L_P]=ck& 0xff;
        enc28j60PacketSend(UDP_HEADER_LEN+IP_HEADER_LEN+ETH_HEADER_LEN+datalen,buf);
}

// do some basic length calculations and store the result in static varibales
void init_len_info(uint8_t *buf)
{
        info_data_len=(((int16_t)buf[IP_TOTLEN_H_P])<<8)|(buf[IP_TOTLEN_L_P]&0xff);
        info_data_len-=IP_HEADER_LEN;
        info_hdr_len=(buf[TCP_HEADER_LEN_P]>>4)*4; // generate len in bytes;
        info_data_len-=info_hdr_len;
        if (info_data_len<=0){
                info_data_len=0;
        }
}

/* end of ip_arp_udp.c */
