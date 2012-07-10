/*
 * mac_addr.hpp
 *
 * Created: 2012/06/13 8:25:08
 *  Author: Administrator
 */ 


#ifndef MAC_ADDR_H_
#define MAC_ADDR_H_

#include "../../EthernetBridge.hpp"
#include "../UIbase.hpp"
#include "../../EthConfig.hpp"
#include "../../switch/dirswitch.hpp"
#include <stdio.h>
#include <util/delay.h>

namespace EthernetBridge { namespace UI
{	
	using namespace Lcd;
	
	namespace data_mac_addr
	{
		char PROGMEM *title = "<Change MAC Address>";
		char PROGMEM *disp = "";
	}
	
	class Ui_View_mac_addr
		: public UIView
	{
		
		private:
		
		static const uint8_t INDENT_WIDTH =2;
		char str_mac_addr[INDENT_WIDTH + 5 + 2*6 + 2];
		uint8_t str_addr_pos;
		
		static inline uint8_t get_max_addr_pos()
		{
			return sizeof(str_mac_addr) - 1;
		}
		
		static inline uint8_t get_min_addr_pos()
		{
			return INDENT_WIDTH;
		}
		
		static inline int8_t xtoi8(const char c)
		{
			if('0' <= c && c <= '9')
			{
				return c - '0';
			}
			else if ('A' <= c && c <= 'F')
			{
				return c - 'A' + 10;
			}				
			else if ('a' <= c && c <= 'f')
			{
				return c - 'a' + 10;
			}				
						
			return -1;
		}
		
		static inline char i8tox(const int8_t c)
		{
			if(0 <= c && c <= 9)
			{
				return c + '0';
			}
			else if(10 <= c && c <= 15)
			{
				return c + 'a';
			}
			
			return '0';
		}
		
		void refresh_str_mac_addr(const uint8_t *paddr)
		{
			uint8_t pos=0;
			
			for(uint8_t i=0; i<INDENT_WIDTH; ++i)
			{
				str_mac_addr[i] = 0x20;
			}
			
			for(uint8_t i=0; i<6; ++i)
			{
				uint8_t val = paddr[i];
				
				pos += sprintf(&str_mac_addr[pos], "\2.x:", val);
			}
			
			str_mac_addr[pos] = 0x20;
		}
		
		void shift_address(ShiftDirection dir)
		{
			//shift guard
			if(get_min_addr_pos() < str_addr_pos && str_addr_pos < get_max_addr_pos() -1)
			{				
				Display::CursorDisplayShift(ShiftCursor, dir);

				if(dir == ShiftToRight) // increment dir is right
				{
					++str_addr_pos;
					if(str_addr_pos%3 == 2) // passing sepalating char
						++str_addr_pos;
				}
				else
				{
					--str_addr_pos;
					if(str_addr_pos%3 == 2)
						--str_addr_pos;
				}
			}			
		}
		
		void count_address(int8_t v)
		{
			int8_t current = xtoi8(str_mac_addr[str_addr_pos]);
			int8_t newer = current + v;
			
			if(newer < 0)
				newer = 0;
			else if (newer > 15)
				newer = 15;
				
			uint8_t set_val = (uint8_t)newer;
			if(str_addr_pos % 3 == 0) // left side
			{
				set_val = ((uint8_t)(newer << 4)) | 0x0F;
			}
			else // right side ingnoring spalating char
			{
				set_val = (uint8_t)newer;
			}
			
			Eth::EthDevice::Parameters.macaddress[str_addr_pos/3] &= set_val;
		}
		
		public:
		
		void RefreshScreen(const UIViewParameters &params)
		{
			refresh_str_mac_addr(Eth::EthDevice::Parameters.macaddress);
			
			Display::WriteStringProg(data_mac_addr::title, 0);
			Display::WriteString(str_mac_addr, 1);
			
			if(params.entered)
			{
				switch(params.state_button)
				{
					case Switch::Right:
						shift_address(ShiftToRight);
						break;
					case Switch::Left:
						shift_address(ShiftToLeft);
						break;
					case Switch::Up:
						count_address(1);
						break;
					case Switch::Down:
						count_address(-1);
						break;
				}				
			}
		}
		
		void EnteringScreen()
		{
			Display::ReturnHome();
			_delay_ms(1.52);
			
			Display::DisplayMode(DisplayOn, CursorShown, CursorBlinking);
			_delay_us(37);
			
			for(uint8_t i=0; i<Display::GetPositionOfLine(1) + INDENT_WIDTH; ++i)
			{
				Display::CursorDisplayShift(ShiftCursor, ShiftToRight);
				_delay_us(37);
			}
			
			str_addr_pos = INDENT_WIDTH;
		}
		
		void LeavingScreen()
		{
			Display::DisplayMode(DisplayOn, CursorHidden, CursorNotBlinking);
			_delay_us(37);
		}
		
	};
}



#endif /* MAC_ADDR_H_ */