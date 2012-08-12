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
		A = 1,
		B = 2,
		C = 3,
		D = 4,
		E = 5,
		F = 6,
		G = 7,
		H = 8,
	};

	struct ptrPacket
		: public EthPacket
	{		
		
		PointModuleState*	get_State() { return (PointModuleState*)&pdata[0]; }
	};
}



#endif /* POINTPACKET_H_ */