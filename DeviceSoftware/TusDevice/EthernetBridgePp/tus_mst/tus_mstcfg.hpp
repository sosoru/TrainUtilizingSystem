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
		typedef OutputPin3<PortE> SlaveModuleE;
		typedef OutputPin3<PortE> SlaveModuleF;
		typedef OutputPin3<PortE> SlaveModuleG;
		typedef OutputPin3<PortE> SlaveModuleH;

		typedef Reset::ResetModule< OutputPin0<PortA> > ResetModuleA;
		typedef Reset::ResetModule< OutputPin1<PortA> > ResetModuleB;
		typedef Reset::ResetModule< OutputPin2<PortA> > ResetModuleC;
		typedef Reset::ResetModule< OutputPin3<PortA> > ResetModuleD;
		typedef Reset::ResetModule< OutputPin4<PortA> > ResetModuleE;
		typedef Reset::ResetModule< OutputPin5<PortA> > ResetModuleF;
		typedef Reset::ResetModule< OutputPin6<PortA> > ResetModuleG;
		typedef Reset::ResetModule< OutputPin7<PortA> > ResetModuleH;
			
		template<
				class slave_module,
				class reset_module,
				uint8_t device_child_id
				>
		class ChildModule
		{
			private:
				static const uint8_t buffer_count = 2;
				static const uint8_t message_size = 64;
			public:
				typedef reset_module Reset;
				typedef EthernetBridge::DispatchBuffer::PacketDispatcher<buffer_count, message_size, device_child_id> Dispatcher;
				
				static inline void Init()
				{
					reset_module::Init();
					Dispatcher::Init();
				}
			
				static inline void SpiSelect()
				{
					SpiToModule::SelectSlave<slave_module>();
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
								
				static inline bool Transmit(EthPacket& received)
				{
					EthPacket* psend;
										
					if(Dispatcher::PopPacket(&psend))
					{
						SpiToModule::TransData<slave_module>(psend, received);
					}else
					{
						SpiToModule::TransData<slave_module>(NULL, received);
					}			
					
					return true;
				}
		};
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