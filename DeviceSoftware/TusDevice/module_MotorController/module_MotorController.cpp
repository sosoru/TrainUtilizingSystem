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

void spi_received(args_received *e)
{
	if(e->ppack->destId.ModuleAddr != 1)
		return;
		
	MtrControllerPacket *ppacket = (MtrControllerPacket*)e->ppack;
	
	mtrA.set_Packet(ppacket);
}

int main(void)
{	
	uint8_t i;
	
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	tus_spi_init();
	tus_spi_set_handler(spi_received);
	
	mtrA.Init();
	mtrB.Init();
	mtrC.Init();
	mtrD.Init();
	
	while(1)
	{	
		tus_spi_process_packets();
		mtrA.Process();
		
		_delay_ms(1);
		//for(i=0; i<150; ++i)
		//{
			//sample_process<MotorControllerC, Positive>(&mtrC, i);
			//_delay_ms(200);
		//}
		//
		//for(i=150; i>0; --i)
		//{	
			//sample_process<MotorControllerC, Positive>(&mtrC, i);
			//_delay_ms(200);
		//}			
		//
		//for(i=0; i<150; ++i)
		//{
			//sample_process<MotorControllerC, Negative>(&mtrC, i);
			//_delay_ms(200);
		//}
		//
		//for(i=150; i>0; --i)
		//{	
			//sample_process<MotorControllerC, Negative>(&mtrC, i);
			//_delay_ms(200);
		//}			

    }
}