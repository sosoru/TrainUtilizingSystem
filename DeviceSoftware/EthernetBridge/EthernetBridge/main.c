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

// please modify the following two lines. mac and ip have to be unique
// in your local area network. You can not have the same numbers in
// two devices:
static uint8_t mymac[6] = {0x54,0x55,0x58,0x10,0x00,0x24};
// how did I get the mac addr? Translate the first 3 numbers into ascii is: TUX
static uint8_t myip[4] = {192,168,2,24};

// listen port for udp
#define MYUDPPORT 1200

#define BUFFER_SIZE 550
static uint8_t buf[BUFFER_SIZE+1];

static sc2004_port port_sc2004;

#define LCD_BUF_SIZE (LCD_HEIGHT * LCD_WIDTH)
static char lcd_buf[LCD_BUF_SIZE];
static uint8_t bufptr = 0;

void timer_interrupt();

ISR(TIMER1_COMPA_vect)
{
	uint8_t addr;
	
	if(sc2004_ReadBusyAndAddress(&addr))
		return; // busy
		
	if(bufptr >= LCD_BUF_SIZE)
	{
		bufptr = 0;
		sc2004_SetAddr_DDRAM(0);
	}
			
	sc2004_WriteData(buf[bufptr]);
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

    /* enable PD2/INT0, as input */
    DDRD&= ~(1<<DDD2);

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
	port_sc2004.portno_rw = 5;
	
	port_sc2004.pport_enable = &PORTE;
	port_sc2004.ppin_enable = &PINE;
	port_sc2004.pddr_enable = &DDRE;
	port_sc2004.portno_enable = 4;
	
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
    TCCR1B = 0b00001010;

    TIMSK = 0b00010000;
	OCR1A = 250;
	sei();
}

void BoardInit()
{
	//EthernetInit();
	
	LCDInit();
} 

//void EthernetLoop()
//{
	        //
    //uint16_t plen;
    //uint16_t dat_p;
    //uint8_t i=0;
    //uint8_t cmd_pos=0;
    //int8_t cmd;
    //uint8_t payloadlen=0;
    //char str[30];
    //char cmdval;
//
	//// get the next new packet:
	//plen = enc28j60PacketReceive(BUFFER_SIZE, buf);
//
	///*plen will ne unequal to zero if there is a valid 
		//* packet (without crc error) */
	//if(plen==0){
			//return;
	//}
                        //
	//// arp is broadcast if unknown but a host may also
	//// verify the mac address by sending it to 
	//// a unicast address.
	//if(eth_type_is_arp_and_my_ip(buf,plen)){
			//make_arp_answer_from_request(buf);
			//return;
	//}
//
	//// check if ip packets are for us:
	//if(eth_type_is_ip_and_my_ip(buf,plen)==0){
			//return;
	//}
                //
	//if(buf[IP_PROTO_P]==IP_PROTO_ICMP_V && buf[ICMP_TYPE_P]==ICMP_TYPE_ECHOREQUEST_V){
			//// a ping packet, let's send pong
			//make_echo_reply_from_request(buf,plen);
			//return;
	//}
	////
	//// udp start, we listen on udp port 3840=0xF00
	//if (buf[IP_PROTO_P]==IP_PROTO_UDP_V&&buf[UDP_DST_PORT_H_P]==0x0F&&buf[UDP_DST_PORT_L_P]==0x00){
			//payloadlen=buf[UDP_LEN_L_P]-UDP_HEADER_LEN;
			//// you must sent a string starting with v
			//// e.g udpcom version 10.0.0.24
			//if (verify_password((char *)&(buf[UDP_DATA_P]))){
					//// find the first comma which indicates 
					//// the start of a command:
					//cmd_pos=0;
					//while(cmd_pos<payloadlen){
							//cmd_pos++;
							//if (buf[UDP_DATA_P+cmd_pos]==','){
									//cmd_pos++; // put on start of cmd
									//break;
							//}
					//}
					//// a command is one char and a value. At
					//// least 3 characters long. It has an '=' on
					//// position 2:
					//if (cmd_pos<2 || cmd_pos>payloadlen-3 || buf[UDP_DATA_P+cmd_pos+1]!='='){
							//strcpy(str,"e=no_cmd");
							//goto ANSWER;
					//}
					//// supported commands are
					//// t=1 t=0 t=?
					//if (buf[UDP_DATA_P+cmd_pos]=='t'){
							//cmdval=buf[UDP_DATA_P+cmd_pos+2];
							//if(cmdval=='1'){
									//PORTD|= (1<<PORTD7);// transistor on
									//strcpy(str,"t=1");
									//goto ANSWER;
							//}else if(cmdval=='0'){
									//PORTD &= ~(1<<PORTD7);// transistor off
									//strcpy(str,"t=0");
									//goto ANSWER;
							//}else if(cmdval=='?'){
									//if (PORTD & (1<<PORTD7)){
											//strcpy(str,"t=1");
											//goto ANSWER;
									//}
									//strcpy(str,"t=0");
									//goto ANSWER;
							//}
					//}
					//strcpy(str,"e=no_such_cmd");
					//goto ANSWER;
			//}
			//strcpy(str,"e=invalid_pw");
//ANSWER:
			//make_udp_reply_from_request(buf,str,strlen(str),MYUDPPORT);
	//
//}  
//
int main(void)
{
	int count = 0;
	
    BoardInit();

	while(1)
	{
		char str[64];
		
		if(count > 65000)
		{
			memset(lcd_buf, 0x20, LCD_BUF_SIZE);
			count = 0;
		}
				
		_delay_ms(255);
		_delay_ms(255);		
		
		sprintf(str, "Hello, world. peropero %d", count);
		strcpy(lcd_buf, str);
		
		++count;
		//EthernetLoop();
	}			
		
    return (0);
}
