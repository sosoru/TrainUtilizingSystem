/*
 * tus_mstrst.hpp
 *
 * Created: 2012/04/24 11:12:30
 *  Author: Administrator
 */ 


#ifndef TUS_MSTRST_H_
#define TUS_MSTRST_H_

#include "../avr_base.hpp"

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
			RESETpin::InitOutput();	// nRESET
			RESETpin::Set();
			
		}
		
		static inline void ModuleOff()
		{
			RESETpin::Clear();
		}
		
		static inline void ModuleOn()
		{
			RESETpin::Set();
		}
		
	};
	}			
}



#endif /* TUS_MSTRST_H_ */