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
#include "avrlibdefs.h"
#include "avrlibtypes.h"
#include "../libtus/tus_spi.h"

#include <Timer.h>
#include <stdlib.h>
#include <string.h>
#include <avr/interrupt.h>
#include <util/delay.h>

using namespace AVRCpp::Timer;
using namespace module_PointController;
using namespace module_PointController::Config;

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
inline void ApplyState(const ptrPacket &packet)
{	
	t_point::ApplyState(packet);
}

template<class t_point>
inline void CopyState(ptrPacket &packet)
{
	PointModuleState *pstate = packet.get_State();
	
	memcpy(pstate, &t_point::State, sizeof(PointModuleState));
}

void CopyState(uint8_t ptnum, ptrPacket &packet)
{
	if(ptnum == A)		{ CopyState<PointA>(packet); }
	else if(ptnum == B) { CopyState<PointB>(packet); }
	else if(ptnum == C) { CopyState<PointC>(packet); }
	else if(ptnum == D) { CopyState<PointD>(packet); }
	else if(ptnum == E) { CopyState<PointE>(packet); }
	else if(ptnum == F) { CopyState<PointF>(packet); }
	else if(ptnum == G) { CopyState<PointG>(packet); }
	else if(ptnum == H) { CopyState<PointH>(packet); }
}

void DispatchState(uint8_t ptnum, const ptrPacket &packet)
{	
	if(ptnum == A)		 { ApplyState<PointA>(packet);}
	else if(ptnum == B)  { ApplyState<PointB>(packet); }	
	else if(ptnum == C)	 { ApplyState<PointC>(packet); }	
	else if(ptnum == D)  { ApplyState<PointD>(packet); }	
	else if(ptnum == E)  { ApplyState<PointE>(packet); }	
	else if(ptnum == F)  { ApplyState<PointF>(packet); }	
	else if(ptnum == G)  { ApplyState<PointG>(packet); }	
	else if(ptnum == H)  { ApplyState<PointH>(packet); }	
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

void ReplyStateToSource(const ptrPacket &packet)
{
	spi_send_object *psend;
	ptrPacket *psendpack;
		
	tus_spi_lock_send_buffer(&psend);
	psendpack = (ptrPacket*)&psend->packet;
	psendpack->destId.raw = packet.srcId.raw; // replying
	psendpack->srcId.raw = packet.destId.raw;
	
	psendpack->devID.raw = psendpack->srcId.raw;
	psendpack->moduletype = 0x14;
	
	CopyState(psendpack->srcId.InternalAddr, *psendpack);	
	
	psend->is_locked = FALSE;
	
}

void spi_received(args_received *e)
{
	if(e->ppack->destId.ModuleAddr == 0)
		return;
	
	ptrPacket *ppacket = (ptrPacket*)e->ppack;
	PointModuleState* pstate = (PointModuleState*)ppacket->pdata;
	
	DispatchState(ppacket->destId.InternalAddr, *ppacket);
	
	ReplyStateToSource(*ppacket);
}

//void test_packet(ptrPacket& packet, PositionEnum pos)
//{
	//uint8_t i;
	//PointModuleState *pstates;
	//
	//memset((void*)&packet, 0x00, sizeof(ptrPacket));
	//packet.set_StateArrayLength(8);
	//pstates = packet.get_StateArray();
	//
	//for(i=0; i<8; ++i)
	//{
		//pstates[i].DeadTime = 10;
		//pstates[i].ChangingTime = 2;
		//pstates[i].Position = pos;		
	//}
	//
//}
//
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
		Execute();
	}
	
}