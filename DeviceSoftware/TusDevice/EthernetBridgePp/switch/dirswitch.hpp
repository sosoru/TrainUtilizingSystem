/*
 * switch.hpp
 *
 * Created: 2012/05/23 18:14:32
 *  Author: Administrator
 */ 


#ifndef DIRSWITCH_H_
#define DIRSWITCH_H_

#include "../avr_base.h"

namespace EthernetBridge
{
	namespace Switch
	{
		enum SwitchDirection
		{
			Up = 0x01,
			Left = 0x02,
			Down = 0x04,
			Right = 0x08
		};
		
		template <
				class t_input_pins
				>
		class DirectionSwitch
		{
			static void Init
			{
				t_input_pins::InitInput();
			}
			
			static inline bool IsPressed(SwitchDirection dir)
			{
				return t_input_pins::flags & dir;
			}
			
			static inline bool IsAnyPressed()
			{
				return t_input_pins::IsAnySet();
			}
			
		};
	}
}



#endif /* DIRSWITCH_H_ */