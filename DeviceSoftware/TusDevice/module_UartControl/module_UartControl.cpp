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

uint8_t received=0;
DeviceID src_id;
DeviceID dst_id;

void CreatePacket(UsartPacket &packet)
{
	packet.header.type = 0;
	packet.header.number = 0;
	packet.header.packet_size = 0;
}

void spi_received(args_received *e)
{
	if(e->ppack->destId.ModuleAddr != 1)
		return;
	
	src_id.raw = e->ppack->srcId.raw;
	dst_id.raw = e->ppack->destId.raw;
	
	received++;

}

int main(void)
{
	uint16_t i;
	UsartPacket pack;
	
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	tus_spi_init();
	tus_spi_set_handler(spi_received);
	
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
		while(!received)
		{
			tus_spi_process_packets();
		}
		
		UsartPacket pack;
		spi_send_object *pspi_send;
		EthPacket *packet;
		
		CreatePacket(pack);
		TrainSensorA::Communicate(pack);
		
		tus_spi_lock_send_buffer(&pspi_send);
		packet = &pspi_send->packet;
		
		packet->srcId.raw = dst_id.raw;
		packet->destId.raw = src_id.raw;	
		packet->pdata[0] = TrainSensorA::ReceivedArray[0].data[0];
		packet->pdata[1] = TrainSensorA::ReceivedArray[1].data[0];
		
		pspi_send->is_locked = FALSE;
		
		received = 0;
    }
}