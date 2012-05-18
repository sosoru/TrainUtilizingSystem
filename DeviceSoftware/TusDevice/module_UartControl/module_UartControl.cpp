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

void send_mtr(uint8_t duty)
{
	using namespace MotorController;
	
	spi_send_object *spi_send;
	MtrControllerPacket *packet;
	
	tus_spi_lock_send_buffer(&spi_send);
	
	packet = (MtrControllerPacket *)&spi_send->packet;
	packet->srcId.ParentPart = 24;
	packet->srcId.ModuleAddr = 2;
	packet->destId.ParentPart = 24;
	packet->destId.ModuleAddr = 1;
	
	packet->set_ControlMode(DutySpecifiedMode);
	packet->set_Direciton(Positive);
	packet->set_DutyValue(duty);
	
	spi_send->is_locked = FALSE;
	
}

int main(void)
{
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
			_delay_ms(10);
			//send_mtr(200);
			cli();
			TrainSensorA::SetTransmitNumber(1);				
			if(TrainSensorA::Communicate())
			{
				if(TrainSensorA::ReceivedArray[0].result > 200)
					TrainSensorA::ReceivedArray[0].result = 200;
				
				send_mtr(TrainSensorA::ReceivedArray[0].result);
			
				tus_spi_process_packets();
			
			}
			sei();
			
				
			//_delay_ms(20);
		
    }
}