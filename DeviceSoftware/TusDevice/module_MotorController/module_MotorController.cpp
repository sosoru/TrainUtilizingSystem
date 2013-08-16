/*
 * module_MotorController.cpp
 *
 * Created: 2012/04/06 22:57:36
 *  Author: Administrator
 */ 

#include "module_MotorController.h"
#include "MotorCfg.hpp"

#include <PackPacket.hpp>
#include <util/delay.h>
#include <stdlib.h>
#include <avr/interrupt.h>
#include <avr/wdt.h>
#include <tus.h>

using namespace Tus;
using namespace MotorController;

MotorControllerA mtrA;
MotorControllerB mtrB;
MotorControllerC mtrC;
MotorControllerD mtrD;

DeviceID g_myDeviceID;
PacketPacker g_packer;

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

void CopyPacket(uint8_t devnum, PacketPacker *ppack)
{
	if(devnum == 1) { mtrA.PackPacket(ppack); }
	else if(devnum == 2) { mtrB.PackPacket(ppack); }
	else if(devnum == 3) { mtrC.PackPacket(ppack); }
	else if(devnum == 4) { mtrD.PackPacket(ppack); }
}

void CreatePacket(uint8_t beginid, uint8_t endid, DeviceID* psrcid, DeviceID* pdstid)
{
	uint8_t i;
	
	g_packer.Init();
	
	for(i=beginid; i<=endid; ++i)
	{		
		CopyPacket(i, &g_packer);	
	}
	
	g_packer.Send(psrcid, pdstid);
}

void SendKernelPacket(DeviceID *psrcid, DeviceID *pdstid)
{
	KernalState state;
	state.KernelCommand = ETHCMD_MEMORY;
	
	g_packer.Init();
	mtrA.get_KernelState((KernalState*)&state);
	g_packer.Pack((uint8_t*)&state);
	mtrB.get_KernelState((KernalState*)&state);
	g_packer.Pack((uint8_t*)&state);
	g_packer.Send(psrcid, pdstid);
	
	g_packer.Init();
	mtrC.get_KernelState((KernalState*)&state);
	g_packer.Pack((uint8_t*)&state);
	mtrD.get_KernelState((KernalState*)&state);
	g_packer.Pack((uint8_t*)&state);
	g_packer.Send(psrcid, pdstid);
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
			SendKernelPacket(pdstid, psrcid);	//reply current memory 
			//CreatePacket(1, 2, pdstid, psrcid);
			//CreatePacket(3, 4, pdstid, psrcid);
		break;
		case ETHCMD_MEMORY:
			ChangeMemory(pstate->Base.InternalAddr, (MemoryState*)pstate->pdata);
		break;
	}
	
	return true;
}

void spi_received(args_received *e)
{	
	wdt_reset();
	
	g_myDeviceID.raw = e->pdstId->raw;
		
	if(ProcessMtrPacket((MtrControllerPacket*)e->ppack)){}
	else if (ProcessKernelPacket((KernalState*)e->ppack, e->psrcId, e->pdstId)){}
	
}

int main(void)
{	
	uint8_t i;
	
//	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	wdt_enable(WDTO_4S);
	
	mtrA.Init();
	mtrB.Init();
	mtrC.Init();
	mtrD.Init();
	
	//TimerCounter0::SetUp(Prescale1024B, FastPWM8, ClearA, ClearB);
	
	tus_spi_init();
	tus_spi_set_handler(spi_received);
	
	wdt_reset();
	while(1)
	{	
		tus_spi_process_packets();

		mtrA.Process();
		mtrB.Process();
		mtrC.Process();
		mtrD.Process();
		
		//if(TimerCounter0::OverflowInterrupt::IsTriggered())
		//{
			//TimerCounter0::OverflowInterrupt::ClearFlag();
		//}
    }
}