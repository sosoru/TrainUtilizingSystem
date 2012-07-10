/*
 * module_PointController.cpp
 *
 * Created: 2012/04/30 21:58:43
 *  Author: Administrator
 */ 

#include "module_PointController.hpp"
#include "PointModule.hpp"
#include "PointPacket.hpp"
#include "PointConfig.hpp"

#include <stdlib.h>
#include <util/delay.h>

using namespace module_PointController;
using namespace module_PointController::Config;

void ApplyState(const ptrPacket &packet)
{
	if(packet.get_StateArrayLength() != 8)
		return;
	
	PointA::ApplyState(packet);
	PointB::ApplyState(packet);
	PointC::ApplyState(packet);
	PointD::ApplyState(packet);
	PointE::ApplyState(packet);
	PointF::ApplyState(packet);
	PointG::ApplyState(packet);
	PointH::ApplyState(packet);
}

void Execute()
{
	PointA::Change();
	PointB::Change();
	PointC::Change();
	PointD::Change();
	PointE::Change();
	PointF::Change();
	PointG::Change();
	PointH::Change();
}

void InitBoard()
{
	DDRA = 0xFF;
	DDRD = 0xFF; // PortA and PortD are set to output for point module 
}

void test_packet(ptrPacket& packet, PositionEnum pos)
{
	uint8_t i;
	PointModuleState *pstates;
	
	memset((void*)&packet, 0x00, sizeof(ptrPacket));
	packet.set_StateArrayLength(8);
	pstates = packet.get_StateArray();
	
	for(i=0; i<8; ++i)
	{
		pstates[i].DeadTime = 10;
		pstates[i].ChangingTime = 2;
		pstates[i].Position = pos;		
	}
	
}

int main(void)
{
	ptrPacket packet;
	PointModuleState *pstates;	
	
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	InitBoard();	
    
	while(1)
    {
		test_packet(packet, PositivePosition);
		ApplyState(packet);
		Execute();
		_delay_ms(500);
		
		test_packet(packet, NegativePosition);
		ApplyState(packet);
		Execute();	
		_delay_ms(500);
	}
	
}