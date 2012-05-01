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

int main(void)
{
	ptrPacket packet;
	PointModuleState *pstates;	
	
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	InitBoard();
	
	memset((void*)&packet, 0x00, sizeof(ptrPacket));
	packet.set_StateArrayLength(8);
	pstates = packet.get_StateArray();
	
	pstates[0].DeadTime = 50;
	pstates[0].ChangingTime = 20;
	pstates[0].Position = PositivePosition;
	
	ApplyState(packet);
    
	while(1)
    {
		Execute();
		_delay_ms(1000);
	}
	
}