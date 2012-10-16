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

void sample_process(Direction dir)
{
	MtrControllerPacket pack;
	
	pack.set_ControlMode(DutySpecifiedMode);
	pack.set_Direciton(dir);
	pack.set_DutyValue(250);
	
	mtrA.set_Packet(&pack);
	mtrB.set_Packet(&pack);
	mtrC.set_Packet(&pack);
	mtrD.set_Packet(&pack);
}

void DispatchPacket(uint8_t devnum, const MtrControllerPacket *ppacket)
{
	if(devnum == 1) { mtrA.set_Packet(ppacket); }
	else if (devnum == 2) { mtrB.set_Packet(ppacket); }
	else if (devnum == 3) { mtrC.set_Packet(ppacket); }
	else if (devnum == 4) { mtrD.set_Packet(ppacket); }
}

void CopyFrom(uint8_t devnum, MtrControllerPacket *ppacket)
{
	if(devnum == 1) { mtrA.get_Packet(ppacket); }
	else if(devnum == 2) { mtrB.get_Packet(ppacket); }
	else if(devnum == 3) { mtrC.get_Packet(ppacket); }
	else if(devnum == 4) { mtrD.get_Packet(ppacket); }
}

void ReplyToSource(MtrControllerPacket *preceived)
{
	spi_send_object *psend;
	MtrControllerPacket *ppacket;
	
	tus_spi_lock_send_buffer(&psend);
	ppacket = (MtrControllerPacket*)&psend->packet;	
	
	ppacket->srcId.raw = preceived->destId.raw;
	ppacket->destId.raw = preceived->srcId.raw;
	
	ppacket->moduletype = 0x12;
	ppacket->devID.raw = ppacket->srcId.raw;
	
	CopyFrom(preceived->destId.InternalAddr, ppacket);
	
	psend->is_locked = false;
	
}

void spi_received(args_received *e)
{
	if(e->ppack->destId.ModuleAddr == 0)
		return;
		
	MtrControllerPacket *ppacket = (MtrControllerPacket*)e->ppack;

	DispatchPacket(ppacket->destId.InternalAddr, ppacket);
	
	ReplyToSource(ppacket);
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

    }
}