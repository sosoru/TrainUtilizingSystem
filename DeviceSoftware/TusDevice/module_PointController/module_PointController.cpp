/*
 * module_PointController.cpp
 *
 * Created: 2012/04/30 21:58:43
 *  Author: Administrator
 */ 

#include "module_PointController.hpp"
#include "PointModule.hpp"
#include "PointConfig.hpp"
#include "avrlibdefs.h"
#include "avrlibtypes.h"
#include <tus_spi.h>
#include <ptr_packet.h>

#include <Timer.h>
#include <stdlib.h>
#include <string.h>
#include <avr/interrupt.h>
#include <util/delay.h>
#include <PackPacket.hpp>

using namespace AVRCpp::Timer;
using namespace module_PointController;
using namespace module_PointController::Config;

Tus::PacketPacker g_packer;

ISR(TIMER1_COMPA_vect)
{
	if(!IS_SPI_COMMUNICATING)
	{
		tus_spi_process_packets();	
	}
}

void InitBoard()
{
	DDRA = 0xFF;
	DDRD = 0xFF; // PortA and PortD are set to output for point module 
}

template<class t_point>
inline void ApplyState(const PointModuleState *pstate)
{
	t_point::ApplyState(pstate);
}

template<class t_point>
inline void PackState(DeviceID *psrc, DeviceID *pdst)
{
	t_point::PackState(&g_packer, psrc, pdst);
}

void DispatchState(uint8_t ptnum, const PointModuleState *pstate)
{	
	if(ptnum == A)		 { ApplyState<PointA>(pstate);}
	else if(ptnum == B)  { ApplyState<PointB>(pstate); }	
	else if(ptnum == C)	 { ApplyState<PointC>(pstate); }	
	else if(ptnum == D)  { ApplyState<PointD>(pstate); }	
	else if(ptnum == E)  { ApplyState<PointE>(pstate); }	
	else if(ptnum == F)  { ApplyState<PointF>(pstate); }	
	else if(ptnum == G)  { ApplyState<PointG>(pstate); }	
	else if(ptnum == H)  { ApplyState<PointH>(pstate); }	
}

void ReplyingProcess(args_received *e)
{
	g_packer.Init();
	
	PackState<PointA>(e->pdstId, e->psrcId);
	PackState<PointB>(e->pdstId, e->psrcId);
	PackState<PointC>(e->pdstId, e->psrcId);
	PackState<PointD>(e->pdstId, e->psrcId);
	PackState<PointE>(e->pdstId, e->psrcId);
	PackState<PointF>(e->pdstId, e->psrcId);
	PackState<PointG>(e->pdstId, e->psrcId);
	PackState<PointH>(e->pdstId, e->psrcId);
	
	g_packer.Send(e->pdstId, e->psrcId);		
}

void spi_received(args_received *e)
{	
	BaseState *pbase = (BaseState*)e->ppack;
	
	switch(pbase->ModuleType)
	{
		case MODULETYPE_SWITCH :
			DispatchState(pbase->InternalAddr, (PointModuleState*)pbase);
			break;
		case MODULETYPE_KERNEL :
			ReplyingProcess(e);
			break;
	}
}

int main(void)
{	
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	InitBoard();	
    
	tus_spi_init();
	tus_spi_set_handler(spi_received);
	
	TimerCounter1::SetUp(NoPrescaleB, Normal16, NormalPortOperationA, NormalPortOperationB, Off, Fall);
	TimerCounter1::CompareMatchAInterrupt::Enable();
	
	while(1)
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
}