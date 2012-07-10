/*
 * LcdController.hpp
 *
 * Created: 2012/06/07 18:46:08
 *  Author: Administrator
 */ 


#ifndef LCDCONTROLLER_H_
#define LCDCONTROLLER_H_

#include "avr_base.hpp"
#include "sc2004/sc2004.hpp"
#include <stdlib.h>
#include <string.h>
#include <avr/pgmspace.h>

namespace EthernetBridge
{
	namespace Lcd
	{
	template<
		class t_data_port,	// Port<> class
		class t_rs_outpin,
		class t_rw_outpin,
		class t_enable_outpin
	>
	class LcdController
		: public Lcd_sc2004<t_data_port, t_rs_outpin, t_rw_outpin, t_enable_outpin>
		{
			private :
			
			typedef Lcd_sc2004<t_data_port, t_rs_outpin, t_rw_outpin, t_enable_outpin> base;
			
			static char lcd_buf[base::WIDTH * base::HEIGHT];
			static uint8_t lcd_buf_pos;
			
			static inline uint8_t writeStringInner(const char* buf, uint8_t line, void*(fn)(void*, const void*, size_t))
			{
				uint8_t pos=0, bufpos=0, copied;
				
				while(bufpos < base::WIDTH * base::HEIGHT
					 && line < 4
					 )
				{				
					pos = GetPositionOfLine(line);
					++line;
					
					for(copied=0; copied<base::WIDTH; ++copied)
					{
						fn(lcd_buf+(pos++), (prog_void*)buf+(bufpos++), sizeof(char));
						
						if(buf[bufpos]==0x00)
							break;
					}
														
				}
								
				return bufpos;			
			}
			
			public :
			
			static inline uint8_t GetPositionOfLine(uint8_t line)
			{
				return 0x40 * (line & 1) + base::WIDTH * (line >> 1);				
			}
			

			static void Init()
			{
				base::InitPort();
				base::InitLcd();
				
				memset(lcd_buf, 0x20, sizeof(lcd_buf));
				lcd_buf_pos = 0;
			}
			
			static void StepRendering()
			{
				if(lcd_buf_pos >= sizeof(lcd_buf))
					lcd_buf_pos = 0;
					
				WriteData(lcd_buf[lcd_buf_pos++]);
				
			}
						
			static uint8_t WriteString(char* buf, uint8_t line)
			{
				return writeStringInner((char*)buf, line, memcpy);
			}
			
			static uint8_t WriteStringProg(const prog_char* buf, uint8_t line)
			{
				return writeStringInner((prog_char*)buf, line, memcpy_P);
			}
		
		};

	template <
		class t_data_port,	// Port<> class
		class t_rs_outpin,
		class t_rw_outpin,
		class t_enable_outpin
		>
	char LcdController<t_data_port, t_rs_outpin, t_rw_outpin, t_enable_outpin>::lcd_buf[base::WIDTH * base::HEIGHT];
	
	template <
		class t_data_port,	// Port<> class
		class t_rs_outpin,
		class t_rw_outpin,
		class t_enable_outpin
		>
	uint8_t LcdController<t_data_port, t_rs_outpin, t_rw_outpin, t_enable_outpin>::lcd_buf_pos;

	}
}



#endif /* LCDCONTROLLER_H_ */