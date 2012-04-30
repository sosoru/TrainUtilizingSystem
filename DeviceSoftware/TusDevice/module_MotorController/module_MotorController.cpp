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
void setPacket(MtrControllerPacket *ppacket, uint8_t duty)
{
	ppacket->set_Direciton(dir);
	ppacket->set_DutyValue(duty);
	ppacket->set_ControlMode(DutySpecifiedMode);
}

template <class t_cnt>
inline void sample_init(t_cnt *pcnt)
{
	pcnt->Init();
}

template < class t_cnt, Direction dir>
inline void sample_process(t_cnt *pcnt, uint8_t duty)
{
	MtrControllerPacket packet;
	
	setPacket<dir>(&packet, duty);
	pcnt->set_Packet(&packet);
	pcnt->Process();
	
};

int main(void)
{	
	uint8_t i;
	
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.

	sample_init<MotorControllerA>(&mtrA);
	sample_init<MotorControllerB>(&mtrB);
	sample_init<MotorControllerC>(&mtrC);
	sample_init<MotorControllerD>(&mtrD);
	
	while(1)
	{	
		sample_process<MotorControllerB, Positive>(&mtrB, 200);
		_delay_ms(1000);
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