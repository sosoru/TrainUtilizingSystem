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
MotorControllerB mtrB;
MotorControllerC mtrC;
MotorControllerD mtrD;

template <Direction dir>
void setPacket(MtrControllerPacket *ppacket)
{
	ppacket->set_Direciton(dir);
	ppacket->set_DutyValue(100);
	ppacket->set_ControlMode(DutySpecifiedMode);
}

template <class t_cnt>
inline void sample_init(t_cnt *pcnt)
{
	pcnt->Init();
}

template < class t_cnt, Direction dir>
inline void sample_process(t_cnt *pcnt)
{
	MtrControllerPacket packet;
	
	setPacket<dir>(&packet);
	pcnt->set_Packet(&packet);
	pcnt->Process();
	
};

#define SAMPLING_FREQ  19531.25f
int main(void)
{	
	
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	sample_init<MotorControllerA>(&mtrA);
	sample_init<MotorControllerB>(&mtrB);
	sample_init<MotorControllerC>(&mtrC);
	sample_init<MotorControllerD>(&mtrD);
	while(1){
			
		sample_process<MotorControllerA, Positive>(&mtrA);
		sample_process<MotorControllerB, Positive>(&mtrB);
		sample_process<MotorControllerC, Positive>(&mtrC);
		sample_process<MotorControllerD, Positive>(&mtrD);
		
		_delay_ms(3000);
		
		sample_process<MotorControllerA, Negative>(&mtrA);
		sample_process<MotorControllerB, Negative>(&mtrB);
		sample_process<MotorControllerC, Negative>(&mtrC);
		sample_process<MotorControllerD, Negative>(&mtrD);

		_delay_ms(3000);

		sample_process<MotorControllerA, Standby>(&mtrA);
		sample_process<MotorControllerB, Standby>(&mtrB);
		sample_process<MotorControllerC, Standby>(&mtrC);
		sample_process<MotorControllerD, Standby>(&mtrD);
	
		_delay_ms(5000);

    }
}