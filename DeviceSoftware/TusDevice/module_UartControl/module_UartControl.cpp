/*
 * module_UartControl.cpp
 *
 * Created: 2012/03/23 5:09:20
 *  Author: Administrator
 */ 

#include "module_UartControl.h"
#include "UartConfig.hpp"
#include <util/delay.h>
#include "../module_MotorController/mtrPacket.hpp"
#include "tus.h"

using namespace AVRCpp;
using namespace module_UartControl;
using namespace module_UartControl::Config;


void CreatePacket(UsartPacket &packet)
{
	packet.header.type = 0;
	packet.header.number = 0;
	packet.header.packet_size = 4;
	for(uint8_t i=0; i<packet.header.packet_size; ++i)
	{
		packet.data[i] = 'a' + i;
	}
}

int main(void)
{
	uint16_t i;
	UsartPacket pack;
	
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	tus_spi_init();
	
	InputPin0<PortD>::InitDefaultInput();
	OutputPin1<PortD>::InitOutput();
	
	TrainSensorB::ModuleOff();
	TrainSensorC::ModuleOff();
	TrainSensorD::ModuleOff();
	
	TrainSensorA::Init();
	TrainSensorA::UartInit();
	TrainSensorA::TimerInit();
	
	TrainSensorA::LedOn();
	
    while(1)
    {			
		//_delay_ms(5);				
		cli();
		CreatePacket(pack);
		TrainSensorA::Communicate(pack);
		sei();
		
		_delay_ms(3);
		
    }
}