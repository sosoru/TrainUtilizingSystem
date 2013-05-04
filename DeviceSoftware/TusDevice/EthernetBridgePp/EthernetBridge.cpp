
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
#include "UI/UIbase.hpp"
//#include "ui/setting/test_setting.hpp"
#include "ui/UIController.hpp"
#include "ui/setting/mac_addr.hpp"

using namespace EthernetBridge;
using namespace EthernetBridge::Eth;

//
//SPI_TRANS_PORT g_spi_trans_port = SPI_TRANS_PORTINST;
//
//SPI_SLAVE_PORT g_spi_slave_ports[] = SPI_SLAVE_PORT_ARRAY;
//

char lcd_buf [0x27];

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
	//_delay_ms(100);	//wait for launching eth device
#endif
			
	DDRA = 0x00; // nRESET pins are initialized HiZ
	DDRB = 0xff;
	DDRC = 0xff;
	DDRD = 0xff;
	DDRE = 0xff;
	//DDRF = 0xff;
	DDRG = 0xff;
	
	PORTA = 0xFF; 
	PORTB = 0;
	PORTC = 0;
	PORTD = 0;
	PORTE = 0;
	//PORTF = 0;
	PORTG = 0;
	
	EthDevice::Parameters.ipaddress[0] = 192;
	EthDevice::Parameters.ipaddress[1] = 168;
	EthDevice::Parameters.ipaddress[2] = 2;
	EthDevice::Parameters.ipaddress[3] = 26;
	
	EthDevice::Parameters.macaddress[0] = 0x54;
	EthDevice::Parameters.macaddress[1] = 0x55;
	EthDevice::Parameters.macaddress[2] = 0x58;
	EthDevice::Parameters.macaddress[3] = 0x10;
	EthDevice::Parameters.macaddress[4] = 0x00;
	EthDevice::Parameters.macaddress[5] = EthDevice::Parameters.ipaddress[3];
		
	EthDevice::Parameters.send_port = 8001;	
	EthDevice::Parameters.recv_port = 8000;
	
	EthDevice::EthernetInit();
	EthDevice::NicParameterInit();
	
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
		
	//Lcd::Display::Init();
} 

template < class t_module >
void DispatchModulePackets()
{
	EthPacket received;
	
	if(t_module::Ignored)
		return;
	
	do
	{
		if(t_module::Transmit(received))
		{
			PORTB ^= _BV(PORTB7);
			if(EthDevice::IsForChildren(received))
			{
				EthDevice::StockToChildren(&received);
			}
			else
			{
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

extern "C"
{

	int main(void)
	{
		using namespace UI;
	
		//Ui_View_mac_addr ui_mac;
		//UIController uicnt(ui_mac);
		
		MCUCSR = 0;
		wdt_enable(WDTO_2S);
		//fprintf(stderr, "hoge");
	//#ifndef DEBUG
		DDRB = 0xff;
		//_delay_ms(1000);
	//#endif
	
		BoardInit();
		PORTB |= _BV(PORTB4);
	
		wdt_reset();
		while(1)
		{
			while(EthDevice::ReceiveFromEthernet())
			{
				wdt_reset(); // 長時間パケットない場合はリセット
			}					
				
			DispatchProcess();
		
			//uicnt.Refresh();
		}			
	
		return 0;
	}

	
};
}
