/*
 * PointPacket.hpp
 *
 * Created: 2012/04/30 22:16:38
 *  Author: Administrator
 */ 


#ifndef POINTPACKET_H_
#define POINTPACKET_H_

#include "module_PointController.hpp"

namespace module_PointController
{
	enum PositionEnum
	{
		StayPosition = 0,
		PositivePosition = 1,
		NegativePosition = 2,
	};
	
	struct PointModuleState
	{
		uint8_t DeadTime;
		uint8_t ChangingTime;
		PositionEnum Position;
	};
			
	enum ModuleNumberEnum
	{
		A = 0,
		B = 1,
		C = 2,
		D = 3,
		E = 4,
		F = 5,
		G = 6,
		H = 7,
	};

	struct ptrPacket
		: public EthPacket
	{
		// StateArrayLen : 0
		// StateArray
		
		uint8_t get_StateArrayLength() const { return pdata[0]; }
		void	set_StateArrayLength(const uint8_t val) { pdata[0] = val ;}
			
		PointModuleState *get_StateArray() const { return (PointModuleState*)&pdata[1]; } 
		PointModuleState *get_State(uint8_t index) const { return (PointModuleState*)&get_StateArray()[index]; }
	};
}



#endif /* POINTPACKET_H_ */