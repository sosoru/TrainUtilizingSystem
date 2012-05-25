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

#include "tus_mst/tus_mstcfg.hpp"
#include "LcdCfg.hpp"
#include "EthConfig.hpp"

using namespace EthernetBridge;
using namespace EthernetBridge::Eth;

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


void BoardInit()
{
	uint8_t i;
	
#ifndef DEBUG
	_delay_ms(100);	//wait for launching eth device
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
	
	EthDevice::Parameters.ipaddress = {192,168,2,24};
	EthDevice::Parameters.macaddress = {0x54,0x55,0x58,0x10,0x00,0x24};
	EthDevice::Parameters.port = 8000;	
	
	EthDevice::EthernetInit();
	
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
		
	Lcd::Display::LcdInit();
} 

template < class t_module >
void DispatchModulePackets()
{
	EthPacket received;
	
	do
	{
		
		if(t_module::Transmit(received))
		{

			if(EthDevice::IsForChildren(received))
			{
				EthDevice::StockToChildren(&received);
			}
			else
			{
				PORTB |= _BV(PORTB7);
				EthDevice::SendToEthernet(&received);
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
	uint8_t i=0,j;
	
	MCUCSR = 0;
	wdt_disable();
	
//#ifndef DEBUG
	DDRB = 0xff;
	//_delay_ms(1000);
//#endif
	
    BoardInit();
	PORTB |= _BV(PORTB4);
		
	Lcd::Display::ReturnHome();
	_delay_ms(3);
	Lcd::Display::SetAddressOfDDRAM(0x40);
	_delay_us(37);
	
	while(1)
	{
		while(EthDevice::ReceiveFromEthernet());		
		
		DispatchProcess();
		
		//if(!Lcd::Display::IsBusy())
		{
			Lcd::Display::WriteData(0x41+i);
		
			if(++i > 0x27)
			{
				_delay_us(37);
				Lcd::Display::SetAddressOfDDRAM(0x00);
				i=0;
			}					
		}
	}			
	
	return 0;
}
