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

template
<class t_sens, uint8_t t_mnum>
void SensorProcess()
{
	UsartPacket pack;
	spi_send_object *pspi_send;
	EthPacket *packet;
	BYTE ondata [8];
		
	t_sens::LedOn();
	CreatePacket(pack);
	t_sens::Communicate(pack);
	
	for(uint8_t i=0; i<4; ++i)
	{
		ondata[i] = t_sens::ReceivedArray[i].data[0];
	}		
	
	t_sens::LedOff();
	CreatePacket(pack);
	t_sens::Communicate(pack);
	
	tus_spi_lock_send_buffer(&pspi_send);
	packet = &pspi_send->packet;
		
	packet->srcId.raw = dst_id.raw;
	packet->srcId.InternalAddr = (t_mnum << 4);	
	packet->destId.raw = src_id.raw;
	packet->moduletype = 0x14;
	packet->devID.raw = packet->srcId.raw;
	
	for(uint8_t i=0; i<4; ++i)
	{		
		packet->pdata[i*2] = ondata[i];
		packet->pdata[i*2+1] = t_sens::ReceivedArray[i].data[0];	
	}		
	
	pspi_send->is_locked = FALSE;
	//tus_spi_process_packets();
	//_delay_ms(10);
	
}

void spi_received(args_received *e)
{
	if(e->ppack->destId.ModuleAddr == 0)
		return;
	
	src_id.raw = e->ppack->srcId.raw;
	dst_id.raw = e->ppack->destId.raw;
	
	dst_id.InternalAddr = 0;
		
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
	
	TrainSensorA::ModuleOn();
	TrainSensorB::ModuleOn();
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
		
		SensorProcess<TrainSensorA, 1>();
		SensorProcess<TrainSensorB, 2>();
		
		received = 0;
    }
}