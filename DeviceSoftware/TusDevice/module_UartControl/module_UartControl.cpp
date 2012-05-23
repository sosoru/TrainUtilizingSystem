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

uint8_t get_value(bool &error)
{
	if(TrainSensorA::Communicate())
	{
		error = false;
		return TrainSensorA::ReceivedArray[0].result;
	}
	error = true;
	return 0;
}

bool is_train_there()
{
	uint8_t i;
	uint16_t before, current=0;
	bool errored;
	uint8_t succeeded = 0;
	
	TrainSensorA::SetTransmitNumber(1);		
	TrainSensorA::LedOn();		
	//_delay_ms(1);
	
	for(i=0; i<3; ++i)
	{
		current += get_value(errored);
		
		if(!errored)
			succeeded++;
	} 
	
	current /= succeeded;
	
	return current > 220;

}

void int_delay(uint16_t d)
{
	sei();
	_delay_ms(d);
	cli();
}

int main(void)
{
	uint16_t i;
	
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
		if(is_train_there())
		{
			for(i=0; i<50; ++i)
			{
				send_mtr(0);
				tus_spi_process_packets();
				int_delay(100);				
			}
			
			for(i=0; i<20; ++i)
			{
				send_mtr(140);
				tus_spi_process_packets();
				int_delay(100);
				
			}		
		}
		else
		{
			send_mtr(140);
			tus_spi_process_packets();
		}
		sei();
		
    }
}