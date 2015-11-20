/*
* tus_mstcfg.h
*
* Created: 2012/01/30 15:19:24
*  Author: root
*/


#ifndef TUS_MSTCFG_H_
#define TUS_MSTCFG_H_

#include "../EthernetBridge.hpp"
#include "../avr_base.hpp"
#include "tus_mstspi.hpp"
#include "tus_mstrst.hpp"
#include "dispat_packet.hpp"
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

#define DEVICES_COUNT 2

namespace EthernetBridge{
	using namespace AVRCpp;
	
	typedef SoftSpi::SoftSpiModule< InputPin7<PortD>, OutputPin6<PortD>, OutputPin5<PortD> > SpiToModule;
	
	namespace EthernetBridgeConfig{
		using namespace AVRCpp;
		
		
		typedef OutputPin0<PortD> SlaveModuleA;
		typedef OutputPin1<PortD> SlaveModuleB;
		typedef OutputPin2<PortD> SlaveModuleC;
		typedef OutputPin3<PortD> SlaveModuleD;
		typedef OutputPin4<PortE> SlaveModuleE;
		typedef OutputPin5<PortE> SlaveModuleF;
		typedef OutputPin6<PortE> SlaveModuleG;
		typedef OutputPin7<PortE> SlaveModuleH;

		typedef Reset::ResetModule< Pin0<PortA>, 0 > ResetModuleA;
		typedef Reset::ResetModule< Pin1<PortA>, 1  > ResetModuleB;
		typedef Reset::ResetModule< Pin2<PortA>, 2  > ResetModuleC;
		typedef Reset::ResetModule< Pin3<PortA>, 3  > ResetModuleD;
		typedef Reset::ResetModule< Pin4<PortA>, 4  > ResetModuleE;
		typedef Reset::ResetModule< Pin5<PortA>, 5  > ResetModuleF;
		typedef Reset::ResetModule< Pin6<PortA>, 6  > ResetModuleG;
		typedef Reset::ResetModule< Pin7<PortA>, 7  > ResetModuleH;
		
		template<
		class slave_module,
		class reset_module,
		uint8_t device_child_id
		>
		class ChildModule
		{
			private:
			static const uint8_t buffer_count = 3;
			static const uint8_t message_size = 1;
			
			public:
			typedef reset_module Reset;
			typedef EthernetBridge::DispatchBuffer::PacketDispatcher<buffer_count, message_size, device_child_id> Dispatcher;
			
			static bool Ignored;
			
			static inline void Init()
			{
				Reset::Init();
				Dispatcher::Init();
				
				Ignored = !Reset::CheckModuleExist();
			}
			
			static inline void SpiSelect()
			{
				SpiToModule::SelectSlave<slave_module>();
			}
			
			static inline bool IsStocked()
			{
				return Dispatcher::Count() != 0;
			}
			
			static inline bool Stock(const EthPacket* ppacket)
			{
				if(ppacket->destId.ModuleAddr != device_child_id)
					return false;
				
				EthPacket* pbuffer;
				if(Dispatcher::PushPacket(&pbuffer))
				{
					memcpy((void*)pbuffer, (void*)(ppacket), sizeof(EthPacket));
				}
				
				return true;
			}
			
			static inline bool IsReceivedCorrectly(EthPacket& packet)
			{
				return packet.srcId.ModuleAddr == device_child_id;
			}
			
			static bool Transmit(EthPacket& received)
			{
				EthPacket* psend;
				
				if(Dispatcher::PopPacket(&psend))
				{
					PORTB ^= _BV(PORTB6);
					SpiToModule::TransData<slave_module>(psend, received);
				}
				else
				{
					SpiToModule::TransData<slave_module>(NULL, received);
				}

				return IsReceivedCorrectly(received);
			}
		};
		
		template<
		class slave_module,
		class reset_module,
		uint8_t device_child_id
		> bool ChildModule<slave_module, reset_module, device_child_id>::Ignored;

	}
	
	typedef EthernetBridgeConfig::ChildModule<EthernetBridgeConfig::SlaveModuleA, EthernetBridgeConfig::ResetModuleA, 1> ModuleA;
	typedef EthernetBridgeConfig::ChildModule<EthernetBridgeConfig::SlaveModuleB, EthernetBridgeConfig::ResetModuleB, 2> ModuleB;
	typedef EthernetBridgeConfig::ChildModule<EthernetBridgeConfig::SlaveModuleC, EthernetBridgeConfig::ResetModuleC, 3> ModuleC;
	typedef EthernetBridgeConfig::ChildModule<EthernetBridgeConfig::SlaveModuleD, EthernetBridgeConfig::ResetModuleD, 4> ModuleD;
	typedef EthernetBridgeConfig::ChildModule<EthernetBridgeConfig::SlaveModuleE, EthernetBridgeConfig::ResetModuleE, 5> ModuleE;
	typedef EthernetBridgeConfig::ChildModule<EthernetBridgeConfig::SlaveModuleF, EthernetBridgeConfig::ResetModuleF, 6> ModuleF;
	typedef EthernetBridgeConfig::ChildModule<EthernetBridgeConfig::SlaveModuleG, EthernetBridgeConfig::ResetModuleG, 7> ModuleG;
	typedef EthernetBridgeConfig::ChildModule<EthernetBridgeConfig::SlaveModuleH, EthernetBridgeConfig::ResetModuleH, 8> ModuleH;
}

#endif /* TUS_MSTCFG_H_ */