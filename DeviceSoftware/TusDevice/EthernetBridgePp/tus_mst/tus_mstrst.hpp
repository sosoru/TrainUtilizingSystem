/*
 * tus_mstrst.hpp
 *
 * Created: 2012/04/24 11:12:30
 *  Author: Administrator
 */ 


#ifndef TUS_MSTRST_H_
#define TUS_MSTRST_H_

#include "../avr_base.hpp"
#include "../../libtus/avrlibdefs.h"
#include <ADC.h>

namespace EthernetBridge
{
	namespace Reset
	{
		using namespace AVRCpp;
		using namespace AnalogToDigital;
		
		template<class RESETpin, uint8_t channel>
		class ResetModule
	{
		public:
		
		static inline void Init()
		{
			//sbi(SFIOR, PUD);
			//RESETpin::Output::InitOutput();	// nRESET
			//RESETpin::Output::Set();
			RESETpin::Input::InitDefaultInput();
		}
		
		// must check before Init()
		static inline bool CheckModuleExist()
		{
			RESETpin::Input::InitDefaultInput(); // Hi-Z
			
			return RESETpin::Input::IsSet();
			
			//RESETpin::Output::InitOutput();
			//RESETpin::Output::Clear();
			//_delay_ms(1);
			//RESETpin::Input::InitDefaultInput();
			//
			//AnalogToDigital::ControlSetUp(ADCEnable, StartLater, FreeRunStopped, InterruptDisable, Div32);
			//AnalogToDigital::SelectionSetUp(AVCC, AlignLeft, (AnalogChannel)channel);
			//AnalogToDigital::StartConversion();
			//AnalogToDigital::WaitWhileConverting();
						//
			//return ADCH > 0x80; // 2.5V‚®‚ç‚¢
		}
		
		static inline void ModuleOff()
		{
			RESETpin::Output::Clear();
		}
		
		static inline void ModuleOn()
		{
			RESETpin::Output::Set();
		}
		
	};
	}			
}



#endif /* TUS_MSTRST_H_ */