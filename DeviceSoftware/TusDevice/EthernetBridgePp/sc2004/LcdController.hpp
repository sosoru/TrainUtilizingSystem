/*
 * LcdController.hpp
 *
 * Created: 2012/05/26 1:08:40
 *  Author: Administrator
 */ 


#ifndef LCDCONTROLLER_H_
#define LCDCONTROLLER_H_

namespace EthernetBridge {namespace Lcd {

template<
			class   t_display,
			uint8_t t_width,
			uint8_t t_height
		>
class LcdController
{
	static char* buf[t_width * t_height];
	
	template <uint8_t line>
	static void WriteLine()
	{
		
	}
};

}





#endif /* LCDCONTROLLER_H_ */