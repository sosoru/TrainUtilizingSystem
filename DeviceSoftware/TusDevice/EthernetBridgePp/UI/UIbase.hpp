/*
 * UIbase.hpp
 *
 * Created: 2012/05/26 12:15:18
 *  Author: Administrator
 */ 


#ifndef UIBASE_H_
#define UIBASE_H_

#include "../EthernetBridge.hpp"
#include "../LcdController.hpp"

namespace EthernetBridge
{
	namespace UI
	{		
		struct UIViewParameters
		{
			bool entered;
			uint8_t state_button;
		};
		
		class UIView
		{
			protected:
			
			static UIViewParameters _Parameters;
			
			public:
						
			static void SetLcdBuffer()
			{								
				//Lcd::Display::WriteStringProg(_Parameters.ptitle, 0);
				//Lcd::Display::WriteStringProg(_Parameters.pdesc, 1);
			}
						
		};
		
		UIViewParameters UIView::_Parameters;
	}
}



#endif /* UIBASE_H_ */