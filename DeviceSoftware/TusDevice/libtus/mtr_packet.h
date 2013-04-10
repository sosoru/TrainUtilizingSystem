/*
 * mtrPacket.hpp
 *
 * Created: 2012/04/21 11:36:08
 *  Author: Administrator
 */ 


#ifndef MTRPACKET_H_
#define MTRPACKET_H_

#include "avr_base.hpp"
#include <PackPacket.hpp>
#include <tus.h>

#ifdef __cplusplus
extern "C"{
namespace MotorController
{
#endif

	enum ControlMode
	{
		DutySpecifiedMode = 0x01,
		CurrentFeedBackMode = 0x02,
		WaitingPulseMode = 0x03,
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
		
		uint8_t State[10];

	} MtrControllerPacket;
	
	typedef struct tag_MtrRunningState
	{
		Direction DirectionValue;
		uint8_t DutyValue;
		uint8_t VoltageValue;
		uint8_t ThresholdValue;
		DeviceID DestinationID;
		uint8_t MemoryAfterEntered;
		uint8_t DestinationMemory;
		
	} MtrRunningState;
		
#ifdef __cplusplus
	}
}
#endif


#endif /* MTRPACKET_H_ */