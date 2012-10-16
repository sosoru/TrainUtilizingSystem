/*
 * UIController.hpp
 *
 * Created: 2012/06/13 10:00:06
 *  Author: Administrator
 */ 


#ifndef UICONTROLLER_H_
#define UICONTROLLER_H_

#include "../EthernetBridge.hpp"
#include "UIbase.hpp"
//#include "UIConfig.hpp"
#include "../switch/dirswitch.hpp"
#include "../config/SwitchCfg.hpp"

namespace EthernetBridge {
 namespace UI {	
	
	class UIController
	{
		private :
			bool m_entered;
			
			UIView& m_view;
			uint8_t m_ind_views;
			
		public :
			UIController(UIView& view)
				: m_ind_views(0), m_entered(false), m_view(view)
			{			
			}
			
			void set_view(const UIView& view)
			{
				m_view = view;
			}
			
			void Refresh()
			{
				using namespace Switch;
				
				UIViewParameters params;
				SwitchDirection state;
				
				state = Config::DirectionSwitch::GetState();
				
				if(state == Up && !m_entered) // entering 
				{
					m_view.EnteringScreen();
				} 
				else if (state == Down && m_entered) // leaving
				{
					m_view.LeavingScreen();
				}
				
				params.state_button = state;
				params.entered = m_entered;
				
				m_view.RefreshScreen(params);
			}
	};
	
}}


#endif /* UICONTROLLER_H_ */