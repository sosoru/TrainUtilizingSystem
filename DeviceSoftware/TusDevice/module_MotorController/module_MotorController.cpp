/*
 * module_MotorController.cpp
 *
 * Created: 2012/04/06 22:57:36
 *  Author: Administrator
 */ 

#include "module_MotorController.hpp"
#include <util/delay.h>
#include <stdlib.h>
#include <avr/interrupt.h>
#include <tus.h>

using namespace MotorController;

MotorControllerA mtrA;
MotorControllerB mtrB;
MotorControllerC mtrC;
MotorControllerD mtrD;

//void sample_process(Direction dir)
//{
	//MtrControllerPacket pack;
	//
	//pack.ControlModeValue = (DutySpecifiedMode);
	//pack.DirectionValue = (dir);
	//pack.DutyValue = (250);
	//
	//mtrA.set_Packet(&pack);
	//mtrB.set_Packet(&pack);
	//mtrC.set_Packet(&pack);
	//mtrD.set_Packet(&pack);
//}
//
void DispatchPacket(uint8_t devnum, const MtrControllerPacket *ppacket)
{
	if(devnum == 1) { mtrA.set_Packet(ppacket); }
	else if (devnum == 2) { mtrB.set_Packet(ppacket); }
	else if (devnum == 3) { mtrC.set_Packet(ppacket); }
	else if (devnum == 4) { mtrD.set_Packet(ppacket); }
}

void ChangeMemory(uint8_t devnum, const MemoryState *pmstate)
{
	if(devnum == 1) { mtrA.MemoryProcess(pmstate); }
	else if (devnum == 2) { mtrB.MemoryProcess(pmstate); }
	else if (devnum == 3) { mtrC.MemoryProcess(pmstate); }
	else if (devnum == 4) { mtrD.MemoryProcess(pmstate); }	
}

void CopyFrom(uint8_t devnum, MtrControllerPacket *ppacket)
{
	if(devnum == 1) { mtrA.get_Packet(ppacket); }
	else if(devnum == 2) { mtrB.get_Packet(ppacket); }
	else if(devnum == 3) { mtrC.get_Packet(ppacket); }
	else if(devnum == 4) { mtrD.get_Packet(ppacket); }
}

void CreatePacket(uint8_t beginid, uint8_t endid, DeviceID* psrcid, DeviceID* pdstid)
{
	uint8_t i;
	spi_send_object *psend;
	EthPacket *ppacket;
	
	tus_spi_lock_send_buffer(&psend);
	
	ppacket = (EthPacket*)&psend->packet;
	ppacket->srcId.raw = psrcid->raw;
	ppacket->srcId.InternalAddr = 0;
	ppacket->destId.raw = pdstid->raw;
	ppacket->devID.raw = psrcid->raw;
	
	for(i=beginid; i<=endid; ++i)
	{		
		MtrControllerPacket *pstate = (MtrControllerPacket*)(ppacket->pdata + ((i - beginid) * 7));
		
		pstate->Base.ModuleType = MODULETYPE_MOTOR;
		pstate->Base.DataLength = 7;
		pstate->Base.InternalAddr = i;
		
		CopyFrom(i, pstate);	
	}
	
	psend->is_locked = false;
	
	
}

bool ProcessMtrPacket(MtrControllerPacket *ppacket)
{
	if(ppacket->Base.ModuleType != MODULETYPE_MOTOR)
		return false;
		
	DispatchPacket(ppacket->Base.InternalAddr, ppacket);
	return true;
}

bool ProcessKernelPacket(KernalState *pstate, DeviceID* psrcid, DeviceID* pdstid)
{
	if(pstate->Base.ModuleType != MODULETYPE_KERNEL
		&& pstate->Base.InternalAddr > 4)
		return false;
	
	switch(pstate->KernelCommand)
	{
		case ETHCMD_REPLY:
			CreatePacket(1, 2, pdstid, psrcid);
			CreatePacket(3, 4, pdstid, psrcid);
		break;
		case ETHCMD_MEMORY:
			ChangeMemory(pstate->Base.InternalAddr, (MemoryState*)pstate->pdata);
		break;
	}
	
	return true;
}

void spi_received(args_received *e)
{	
	if(ProcessMtrPacket((MtrControllerPacket*)e->ppack)){}
	else if (ProcessKernelPacket((KernalState*)e->ppack, e->psrcId, e->pdstId)){}
	
}

int main(void)
{	
	uint8_t i;
	
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	mtrA.Init();
	mtrB.Init();
	mtrC.Init();
	mtrD.Init();
	
	tus_spi_init();
	tus_spi_set_handler(spi_received);
	
	while(1)
	{	
		tus_spi_process_packets();

		mtrA.Process();
		mtrB.Process();
		mtrC.Process();
		mtrD.Process();
    }
}