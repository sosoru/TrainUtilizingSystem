/*
 * PointModule.hpp
 *
 * Created: 2012/04/30 22:11:45
 *  Author: Administrator
 */ 


#ifndef POINTMODULE_H_
#define POINTMODULE_H_

#include "module_PointController.hpp"
#include "PointPacket.hpp"
#include <avr/delay.h>
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
		
		static inline void Stop()
		{
			ControllApin::Clear();
			ControllBpin::Clear();
		}
		
		static inline void Brake()
		{
			ControllApin::Set();
			ControllBpin::Clear();
		}
		
		static inline void Positive()
		{
			Brake();
			_dynamic_delay_ms<1>(State.DeadTime);
			
			ControllApin::Set();
			ControllBpin::Clear();
			
			_dynamic_delay_ms<10>(State.ChangingTime);
			Stop();
		}
		
		static inline void Negative()
		{
			Brake();
			_dynamic_delay_ms<1>(State.DeadTime);
			
			ControllApin::Clear();
			ControllBpin::Set();
			
			_dynamic_delay_ms<10>(State.ChangingTime);
			Stop();
		}
		
		static inline void Change()
		{
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
		}
		
		static inline void ApplyState(const ptrPacket &packet)
		{
			memcpy((void*)&State, (void*)packet.get_State(module_number), sizeof(PointModuleState));	
		}
	};
	
	template<
			class ControllApin,
			class ControllBpin,
			ModuleNumberEnum module_number
	> PointModuleState PointModule<ControllApin, ControllBpin, module_number>::State;

}



#endif /* POINTMODULE_H_ */