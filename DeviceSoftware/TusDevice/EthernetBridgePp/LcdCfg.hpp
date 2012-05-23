/*
 * LcdCfg.hpp
 *
 * Created: 2012/05/18 10:53:42
 *  Author: Administrator
 */ 


#ifndef LCDCFG_H_
#define LCDCFG_H_

#include "avr_base.hpp"
#include "sc2004/sc2004.hpp"

namespace EthernetBridge
{
	namespace Lcd
	{
		typedef Lcd_sc2004< PortC, OutputPin6<PortF>, OutputPin3<PortE>, OutputPin2<PortE> > Display;
		
	}
}



#endif /* LCDCFG_H_ */