/*
 * switch.hpp
 *
 * Created: 2012/05/23 18:14:32
 *  Author: Administrator
 */ 


#ifndef DIRSWITCH_H_
#define DIRSWITCH_H_

#include "../avr_base.hpp"

namespace EthernetBridge
{
	namespace Switch
	{
		enum SwitchDirection
		{
			None	= 0x00,
				
			Up		= 0x01,
			Left	= 0x02,
			Down	= 0x04,
			Right	= 0x08,
		};
		
		template <
				class t_up_pin,
				class t_down_pin,
				class t_right_pin,
				class t_left_pin
				>
		class DirectionSwitch
		{
			public:			
			
			static void Init()
			{
				t_up_pin::InitInput();
				t_down_pin::InitInput();
				t_right_pin::InitInput();
				t_left_pin::InitInput();
			}
			
			static inline SwitchDirection GetState()
			{
				uint8_t res;
				
				if(t_up_pin::IsSet())
					res |= Up;
				
				if(t_down_pin::IsSet())
					res |= Down;
					
				if(t_right_pin::IsSet())
					res |= Right;
					
				if(t_left_pin::IsSet())
					res |= Left;
			
				return (SwitchDirection)res;
			}
			
			static inline bool IsPressed(SwitchDirection dir)
			{
				return GetState() && dir;
			}
			
			static inline bool IsAnyPressed()
			{
				return IsPressed(Up | Down | Left | Right);
			}
			
		};
	}
}



#endif /* DIRSWITCH_H_ */