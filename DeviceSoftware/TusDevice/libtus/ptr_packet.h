/*
 * PointPacket.hpp
 *
 * Created: 2012/04/30 22:16:38
 *  Author: Administrator
 */ 


#ifndef POINTPACKET_H_
#define POINTPACKET_H_

#ifdef __cplusplus
extern "C"{
namespace module_PointController
{
#endif
	
	enum PositionEnum
	{
		StayPosition = 0xff,
		PositivePosition = 0,
		NegativePosition = 1,
	};
	
	struct PointModuleState
	{
		BaseState Base;
		
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
	
#ifdef __cplusplus
	}
}
#endif

#endif /* POINTPACKET_H_ */