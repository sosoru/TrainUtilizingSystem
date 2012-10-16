/*
 * test_setting.hpp
 *
 * Created: 2012/05/26 12:35:48
 *  Author: Administrator
 */ 


#ifndef TEST_SETTING_H_
#define TEST_SETTING_H_

#include <avr/pgmspace.h>

#include "../../avr_base.hpp"
#include "../UIbase.hpp"

namespace EthernetBridge
{
	namespace UI
	{
		namespace test_setting
		{
			char PROGMEM title[]	=	"<test config>";
			char PROGMEM desc[]		=	"this is a test view of configuration";		
		
		}
		
		class Ui_View_testconfig
			: public UIView
		{
			public:
			
			static void RefreshUiParameters()
			{
				//_Parameters.pdesc = test_setting::desc;
				//_Parameters.ptitle = test_setting::title;
			}
			
			
		};
	}
}



#endif /* TEST_SETTING_H_ */