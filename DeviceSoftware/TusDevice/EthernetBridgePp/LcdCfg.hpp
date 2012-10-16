/*
 * LcdCfg.hpp
 *
 * Created: 2012/05/18 10:53:42
 *  Author: Administrator
 */ 


#ifndef LCDCFG_H_
#define LCDCFG_H_

#include "avr_base.hpp"
#include "LcdController.hpp"

namespace EthernetBridge
{
	namespace Lcd
	{
		
		typedef LcdController< PortC, OutputPin6<PortF>, OutputPin3<PortE>, OutputPin2<PortE> > Display;
		
	}
}



#endif /* LCDCFG_H_ */