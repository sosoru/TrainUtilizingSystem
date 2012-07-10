/*
 * SwitchCfg.hpp
 *
 * Created: 2012/06/30 12:20:39
 *  Author: Administrator
 */ 


#ifndef SWITCHCFG_H_
#define SWITCHCFG_H_

#include "../EthernetBridge.hpp"
#include "../switch/dirswitch.hpp"

namespace EthernetBridge{ namespace Config {

	typedef Switch::DirectionSwitch
				<AVRCpp::InputPin0<PortF>,
				 AVRCpp::InputPin1<PortF>,
				 AVRCpp::InputPin2<PortF>,
				 AVRCpp::InputPin3<PortF> > DirectionSwitch;

}}

#endif /* SWITCHCFG_H_ */