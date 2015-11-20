
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

EthPacket ReceivedFromDevicePacket;

//
//SPI_TRANS_PORT g_spi_trans_port = SPI_TRANS_PORTINST;
//
//SPI_SLAVE_PORT g_spi_slave_ports[] = SPI_SLAVE_PORT_ARRAY;
//

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
	
	_delay_ms(100);	//wait for launching eth device
	
	DDRA = 0x00; // nRESET pins are initialized HiZ
	DDRB = 0xff;
	DDRC = 0xff;
	DDRD = 0xff;
	DDRE = 0xff;
	//DDRF = 0xff;
	DDRG = 0xff;
	
	PORTA = 0x00;
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
	
	Lcd::Display::Init();
}

template < class t_module >
void DispatchModulePackets()
{
	//uint8_t i=0, bufpos=0;
	//char printbuf[100];
	
	if(t_module::Ignored)
	return;
			
		if(t_module::Transmit(ReceivedFromDevicePacket))
		{
			//bool flag = false;
			//
			//for(i=0; i<40; i++)
			//{
				//if(received.raw_array[i] != 0)
				//{
					//flag = true;
					//goto flag_label;
				//}
			//}
			//
			//flag_label:
			//if(flag)
			//{
				//for(i=0; i<40; i++)
				//{
					//bufpos += sprintf(&printbuf[bufpos],"%2x", received.raw_array[i]);
				//}
				//Lcd::Display::WriteString(printbuf,0);
//
				PORTB ^= _BV(PORTB7);
				if(EthDevice::IsForChildren(ReceivedFromDevicePacket))
				{
					EthDevice::StockToChildren(&ReceivedFromDevicePacket);
				}
				else
				{
					EthDevice::SendToEthernet(&ReceivedFromDevicePacket);
				}
				
			//}
			
		}
		
}

void DispatchProcess()
{
	if(!ModuleA::Ignored) DispatchModulePackets<ModuleA>();
	if(!ModuleB::Ignored) DispatchModulePackets<ModuleB>();
	if(!ModuleC::Ignored) DispatchModulePackets<ModuleC>();
	if(!ModuleD::Ignored) DispatchModulePackets<ModuleD>();
	if(!ModuleE::Ignored) DispatchModulePackets<ModuleE>();
	if(!ModuleF::Ignored) DispatchModulePackets<ModuleF>();
	if(!ModuleG::Ignored) DispatchModulePackets<ModuleG>();
	if(!ModuleH::Ignored) DispatchModulePackets<ModuleH>();
}

void ShowModuleExistence(uint8_t line)
{
	char buf[9];
	
	buf[0] = ModuleA::Ignored ? 'X' : 'O';
	buf[1] = ModuleB::Ignored ? 'X' : 'O';
	buf[2] = ModuleC::Ignored ? 'X' : 'O';
	buf[3] = ModuleD::Ignored ? 'X' : 'O';
	buf[4] = ModuleE::Ignored ? 'X' : 'O';
	buf[5] = ModuleF::Ignored ? 'X' : 'O';
	buf[6] = ModuleG::Ignored ? 'X' : 'O';
	buf[7] = ModuleH::Ignored ? 'X' : 'O';
	buf[8] = '\0';
	
	Lcd::Display::WriteString(buf, line);
}

void ShowIpAddress(uint8_t line)
{
	char buf1[20], buf2[20];
	uint8_t* paddr = EthDevice::Parameters.ipaddress;
	uint8_t* pmac = EthDevice::Parameters.macaddress;
	
	sprintf(buf1, "I'm %d.%d.%d.%d", paddr[0],paddr[1],paddr[2],paddr[3]);
	sprintf(buf2, "(%02X:%02X:%02X:%02X:%02X:%02X)", pmac[0],pmac[1],pmac[2],pmac[3],pmac[4],pmac[5]);
	
	Lcd::Display::WriteString(buf1, line);
	Lcd::Display::WriteString(buf2, line+1);
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
		Lcd::Display::WriteString("T.U.S. Unit Box",0);
		ShowIpAddress(1);
		ShowModuleExistence(3);
		
		PORTB |= _BV(PORTB4);
		
		wdt_reset();
		while(1)
		{
			while(EthDevice::ReceiveFromEthernet())
			{
				wdt_reset(); // 長時間パケットない場合はリセット
			}
			
			Lcd::Display::StepRendering();
			DispatchProcess();
			//uicnt.Refresh();
		}
		
		return 0;
	}

	
};
}
