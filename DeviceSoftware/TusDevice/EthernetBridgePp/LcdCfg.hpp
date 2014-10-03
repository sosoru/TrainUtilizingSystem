/*
 * LcdCfg.hpp
 *
 * Created: 2012/05/18 10:53:42
 *  Author: Administrator
 */ 


#ifndef LCDCFG_H_
#define LCDCFG_H_

#include "EthernetBridge.hpp"
#include "avr_base.hpp"
#include "LcdController.hpp"

namespace EthernetBridge
{
	namespace Lcd
	{
		
		typedef LcdController< AVRCpp::PortC, AVRCpp::OutputPin6<AVRCpp::PortF>, AVRCpp::OutputPin3<AVRCpp::PortE>, AVRCpp::OutputPin2<AVRCpp::PortE> > Display;
		
	}
}



#endif /* LCDCFG_H_ */