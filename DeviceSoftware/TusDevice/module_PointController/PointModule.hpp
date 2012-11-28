/*
 * PointModule.hpp
 *
 * Created: 2012/04/30 22:11:45
 *  Author: Administrator
 */ 


#ifndef POINTMODULE_H_
#define POINTMODULE_H_

#include "module_PointController.hpp"
#include <PackPacket.hpp>
#include <ptr_packet.h>
#include <avr/io.h>
#include <avr/delay.h>
#include <avr/interrupt.h>
#include <string.h>

namespace module_PointController
{
	using namespace AVRCpp;
	
	template<
				class ControllApin,
				class ControllBpin,
				ModuleNumberEnum module_number
			>
	class PointModule
	{
		private :
		
		template<uint8_t wait_base>
		static inline void _dynamic_delay_ms(uint8_t wait)
		{
			for(uint8_t i=0; i<wait; ++i)
			{
				_delay_ms(wait_base);
			}
		}
		
		public :
		
		static PointModuleState State;
		static bool IsChanged;
		
		static inline void Stop()
		{
			ControllApin::Clear();
			ControllBpin::Clear();
		}
		
		static inline void Brake()
		{
			ControllApin::Set();
			ControllBpin::Set();
		}
		
		static inline void Positive()
		{		
			Stop(); _delay_us(100);
			Brake();
			_dynamic_delay_ms<1>(State.DeadTime);
			
			Stop(); _delay_us(100);
			ControllApin::Set();
			
			_dynamic_delay_ms<10>(State.ChangingTime);
			Stop();
		}
		
		static inline void Negative()
		{
			Stop(); _delay_us(100);
			Brake();
			_dynamic_delay_ms<1>(State.DeadTime);
			
			Stop(); _delay_us(100);
			ControllBpin::Set();
			
			_dynamic_delay_ms<10>(State.ChangingTime);
			Stop();
		}
		
		static inline void Change()
		{
			if(IsChanged)
				return;
				
			switch(State.Position)
			{
				case PositivePosition:
					Positive();
					break;
				case NegativePosition:
					Negative();
					break;
				case StayPosition:
				default:
					break;
			}
			
			IsChanged = true;
		}
		
		static inline void ApplyState(const PointModuleState *pstate)
		{			
			//if(pstate->Position != State.Position)
			{
				IsChanged = false;
			}
			
			//memcpy((void*)&State, (const void*)(pstate), sizeof(PointModuleState));	
			State.ChangingTime = pstate->ChangingTime;
			State.DeadTime = pstate->DeadTime;
			State.Position = pstate->Position;
		}
		
		static void PackState(Tus::PacketPacker *ppack, DeviceID *psrc, DeviceID *pdst)
		{
			State.Base.DataLength = sizeof(State);
			State.Base.InternalAddr = module_number;
			State.Base.ModuleType = MODULETYPE_SWITCH;
			
			if(false == ppack->Pack((uint8_t*)&State))
			{
				ppack->Send(psrc, pdst);
				
				ppack->Init();
				ppack->Pack((uint8_t*)&State);
			}
		}
	};
	
	template<
			class ControllApin,
			class ControllBpin,
			ModuleNumberEnum module_number
	> PointModuleState PointModule<ControllApin, ControllBpin, module_number>::State;

	template<
			class ControllApin,
			class ControllBpin,
			ModuleNumberEnum module_number
	> bool PointModule<ControllApin, ControllBpin, module_number>::IsChanged;

}



#endif /* POINTMODULE_H_ */