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
        //public byte DataLength { get; set; }
        //public byte InternalAddr { get; set; }
        //public byte ModuleType { get; set; }
	//	ControlMode : 1 byte
	//	Direction	: 1 byte
	//	DutyValue	: 1 byte
	//	VoltageValue: 1 byte
	//	
	typedef struct tag_MtrControllerPacket
	{
		BaseState Base;
		
		ControlMode ControlModeValue;
		Direction DirectionValue;
		uint8_t DutyValue;
		uint8_t VoltageValue;
		
		//ControlMode get_ControlMode() const		{ return (ControlMode)pdata[3]; }
		//void set_ControlMode(ControlMode val)	{ pdata[3] = (uint8_t)val; }
			//
		//Direction get_Direction() const			{ return (Direction)pdata[4]; }
		//void set_Direciton(Direction val)		{ pdata[4] = (uint8_t)val; }
			//
		//uint8_t get_DutyValue() const			{ return pdata[5]; }
		//void set_DutyValue(uint8_t val)		{ pdata[5] = val; }	
			//
		//uint8_t get_VoltageValue() const		{ return *((uint8_t*)&pdata[6]); }
		//void set_VoltageValue(uint8_t val)	{ *((uint8_t*)&pdata[6]) = val; }
	} MtrControllerPacket;
	
}



#endif /* MTRPACKET_H_ */