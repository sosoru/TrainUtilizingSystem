
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
#include "EthConfig.hpp"

using namespace EthernetBridge;
using namespace EthernetBridge::Eth;

ISR(TIMER1_COMPA_vect)
{
}

void BoardInit()
{	
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
	EthDevice::Parameters.ipaddress[3] = 27;
	
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
}

template < class t_module >
void DispatchModulePackets()
{
	EthPacket received;
	
	if(t_module::Ignored)
	return;
			
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

extern "C"
{

	int main(void)
	{
		MCUCSR = 0;
		wdt_enable(WDTO_2S);
		
		DDRB = 0xff;
		
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
		}
		
		return 0;
	}

	
};
