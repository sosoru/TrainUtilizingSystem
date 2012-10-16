/*
 * UIbase.hpp
 *
 * Created: 2012/05/26 12:15:18
 *  Author: Administrator
 */ 


#ifndef UIBASE_H_
#define UIBASE_H_

#include "../EthernetBridge.hpp"

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
						
			public:
									
			virtual void RefreshScreen(const UIViewParameters &params){}// =0;
			virtual void EnteringScreen()	{}//=0;
			virtual void LeavingScreen()		{}//=0;

		};
	
		

	}
}



#endif /* UIBASE_H_ */