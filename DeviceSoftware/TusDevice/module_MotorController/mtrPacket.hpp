/*
 * mtrPacket.hpp
 *
 * Created: 2012/04/21 11:36:08
 *  Author: Administrator
 */ 


#ifndef MTRPACKET_H_
#define MTRPACKET_H_

#include "avr_base.hpp"
#include <tus.h>

namespace MotorController
{
	enum ControlMode
	{
		DutySpecifiedMode = 0x01,
		CurrentFeedBackMode = 0x02,
	};
	
	enum Direction
	{
		Standby = 0x00,
		Positive = 0x01,
		Negative = 0x02,
	};
	
	// pdata:
	//	ControlMode : 1 byte
	//	Direction	: 1 byte
	//	DutyValue	: 1 byte
	//	VoltageValue: 4 byte
	//	
	struct MtrControllerPacket
			 : public EthPacket
	{
		ControlMode get_ControlMode() const		{ return (ControlMode)pdata[0]; }
		void set_ControlMode(ControlMode val)	{ pdata[0] = (uint8_t)val; }
			
		Direction get_Direction() const			{ return (Direction)pdata[1]; }
		void set_Direciton(Direction val)		{ pdata[1] = (uint8_t)val; }
			
		uint8_t get_DutyValue() const			{ return pdata[2]; }
		void set_DutyValue(uint8_t val)		{ pdata[2] = val; }	
			
		uint8_t get_VoltageValue() const		{ return *((uint8_t*)&pdata[7]); }
		void set_VoltageValue(uint8_t val)	{ *((uint8_t*)&pdata[7]) = val; }
	}; 
	
}



#endif /* MTRPACKET_H_ */