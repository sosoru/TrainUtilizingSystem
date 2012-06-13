/*
 * UIConfig.hpp
 *
 * Created: 2012/06/13 10:01:06
 *  Author: Administrator
 */ 


#ifndef UICONFIG_H_
#define UICONFIG_H_

#include "../EthernetBridge.hpp"
#include "UIbase.hpp"
#include "setting/mac_addr.hpp"

#include <boost/mpl/list.hpp>

namespace EthernetBridge { namespace UI
{
	using namespace boost;
	
	typedef mpl::list<
			Ui_View_mac_addr,
			Ui_View_testconfig
			> control_list;
	
}}



#endif /* UICONFIG_H_ */