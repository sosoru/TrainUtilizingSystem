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

void spi_received(args_received *e)
{
	if(e->ppack->destId.ModuleAddr != 1)
		return;
		
	MtrControllerPacket *ppacket = (MtrControllerPacket*)e->ppack;
	ppacket->set_Direciton(Positive);

	mtrA.set_Packet(ppacket);
}

int main(void)
{	
	uint8_t i;
	
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	//tus_spi_init();
	//tus_spi_set_handler(spi_received);
	
	mtrA.Init();
	mtrB.Init();
	mtrC.Init();
	mtrD.Init();
	
	while(1)
	{	
		sample_process(Positive);
		//tus_spi_process_packets();
		mtrA.Process();
		mtrB.Process();
		mtrC.Process();
		mtrD.Process();
		_delay_ms(2500);
		
		sample_process(Negative);
		mtrA.Process();
		mtrB.Process();
		mtrC.Process();
		mtrD.Process();
		
		_delay_ms(2500);

    }
}