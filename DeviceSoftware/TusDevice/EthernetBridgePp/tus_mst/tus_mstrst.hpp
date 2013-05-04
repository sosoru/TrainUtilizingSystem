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

namespace EthernetBridge
{
	namespace Reset
	{
		template<class RESETpin>
		class ResetModule
	{
		public:
		
		static inline void Init()
		{
			sbi(SFIOR, PUD);
			RESETpin::Output::InitOutput();	// nRESET
			RESETpin::Output::Set();
		}
		
		// must check before Init()
		static inline bool CheckModuleExist()
		{
			RESETpin::Input::InitInput();	// when PUD enable, DDxn = 0, PORTx = 1 
			return RESETpin::Input::IsSet();
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