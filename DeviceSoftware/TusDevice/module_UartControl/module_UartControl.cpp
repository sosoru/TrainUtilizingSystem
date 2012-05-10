/*
 * module_UartControl.cpp
 *
 * Created: 2012/03/23 5:09:20
 *  Author: Administrator
 */ 

#include "module_UartControl.h"
#include "UartConfig.hpp"
#include <util/delay.h>

using namespace AVRCpp;
using namespace module_UartControl;
using namespace module_UartControl::Config;

int main(void)
{
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	TrainSensorA::Init();
	TrainSensorA::UartInit();
	TrainSensorA::TimerInit();
	
    while(1)
    {		
		for(uint8_t i=0; i<250; ++i)
		{
			TrainSensorA::TransmitData.number = i;
			TrainSensorA::TransmitData.prescale = 0;
			
			if(!TrainSensorA::Communicate())
				break;
		}
		
    }
}