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

template
<class t_sens, uint8_t t_mnum>
void SensorProcess()
{
	spi_send_object *pspi_send;
	EthPacket *packet;
				
	cli();
	tus_spi_lock_send_buffer(&pspi_send);
	packet = &pspi_send->packet;
		
	packet->srcId.raw = dst_id.raw;
	packet->srcId.InternalAddr = (t_mnum << 4);	
	packet->destId.raw = src_id.raw;
	packet->moduletype = 0x14;
	packet->devID.raw = packet->srcId.raw;
	
	for(uint8_t i=0; i<4; ++i)
	{		
		packet->pdata[i*2] = t_sens::OnState[i];	
		packet->pdata[i*2+1] = t_sens::OffState[i];
	}		
	
	pspi_send->is_locked = FALSE;
	sei();
	//_delay_ms(10);
	
}

uint8_t check_buf[2];

template
<class t_sens, uint8_t t_mnum>
void MonitoringProcess()
{
	uint8_t curr = t_sens::CheckSensors() ;
	
	if(curr > 0 || check_buf[t_mnum]++ > 5)
	{
		SensorProcess<t_sens, t_mnum>();
		check_buf[t_mnum] = 0;
	}
}

void spi_received(args_received *e)
{
	if(e->ppack->destId.ModuleAddr == 0)
		return;
	
	if(received > 0)
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
	
	TrainSensorA::Init();
	TrainSensorB::Init();
	
	TrainSensorA::UartInit();
	TrainSensorA::TimerInit();
	
	TrainSensorA::ModuleOff();
	TrainSensorB::ModuleOff();
	TrainSensorC::ModuleOff();
	TrainSensorD::ModuleOff();
	
	TrainSensorA::LedOff();
	TrainSensorB::LedOff();
	
	src_id.ParentPart = 9;
	src_id.ModuleAddr = 0;
	
	dst_id.ParentPart = 24;
	dst_id.ModuleAddr = 4;
	
    while(1)
    {			
			
		MonitoringProcess<TrainSensorA, 1>();
		tus_spi_process_packets();		
		MonitoringProcess<TrainSensorB, 2>();
		tus_spi_process_packets();
		
		received = 0;
    }
}