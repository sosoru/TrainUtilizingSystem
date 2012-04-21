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

using namespace MotorController;

MotorControllerA mtrA;

void setPacket(MtrControllerPacket *ppacket)
{
	ppacket->set_Direciton(Positive);
	ppacket->set_DutyValue(100);
	ppacket->set_ControlMode(DutySpecifiedMode);
}

#define SAMPLING_FREQ  19531.25f
int main(void)
{	
	MtrControllerPacket packet;
	
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	while(1){
		
		mtrA.Init();
		mtrA.set_Packet(&packet);
		
		mtrA.Process();
		
    }
}