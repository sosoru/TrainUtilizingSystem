/*
 * module_UartControl.cpp
 *
 * Created: 2012/03/23 5:09:20
 *  Author: Administrator
 */ 

#include "module_UartControl.h"
#include "UartConfig.hpp"
#include "PackPacket.hpp"
#include <util/delay.h>
#include "tus.h"
#include "tus_spi.h"

using namespace AVRCpp;
using namespace module_UartControl;
using namespace module_UartControl::Config;

Tus::PacketPacker packer_g;

//template
//<class t_sens, uint8_t t_mnum, uint8_t t_cnt>
//void MonitoringProcess()
//{
	//uint8_t curr = t_sens::CheckSensors() ;
	//
	//if(curr > 0 || check_buf[t_mnum]++ > 5)
	//{
		//SensorProcess<t_sens, t_mnum, t_cnt>();
		//check_buf[t_mnum] = 0;
	//}
//}
//

void SendState(DeviceID *psrc, DeviceID *pdst)
{
	packer_g.Init();
	
	TrainSensorA::PackSettingPacket(&packer_g, psrc, pdst);
	TrainSensorB::PackSettingPacket(&packer_g, psrc, pdst);
	TrainSensorC::PackSettingPacket(&packer_g, psrc, pdst);
	TrainSensorD::PackSettingPacket(&packer_g, psrc, pdst);
	
	TrainSensorA::PackPacket(&packer_g, psrc, pdst);
	TrainSensorB::PackPacket(&packer_g, psrc, pdst);
	TrainSensorC::PackPacket(&packer_g, psrc, pdst);
	TrainSensorD::PackPacket(&packer_g, psrc, pdst);
	
	packer_g.Send(psrc, pdst);
}

bool ProcessKernalPacket(args_received *preceived)
{
	KernalState *pstate = (KernalState*)preceived->ppack;
	
	if(pstate->Base.ModuleType != MODULETYPE_KERNEL)
		return false;
		
	switch(pstate->KernelCommand)
	{
		case ETHCMD_REPLY :
			SendState(preceived->pdstId, preceived->psrcId);
		break;
	}
	
	return true;
}

bool ProcessUartPacket(args_received *preceived)
{
	BaseState *pstate = (BaseState*)preceived->ppack;
	
	if(pstate->ModuleType != MODULETYPE_UART
		&& pstate->ModuleType != MODULETYPE_UART_MOUDLESETTING)
		return false;
		
	if((pstate->InternalAddr & 0x0F) == 0)
	{
		switch(pstate->InternalAddr & 0xF0)
		{
			case 0x10:
				TrainSensorA::ApplyPacket(pstate);
			break;
			case 0x20:
				TrainSensorB::ApplyPacket(pstate);
			break;
			case 0x30:
				TrainSensorC::ApplyPacket(pstate);
			break;
			case 0x40:
				TrainSensorD::ApplyPacket(pstate);
			break;
		}
	}	

	return true;
}

void spi_received(args_received *e)
{
	if(ProcessKernalPacket(e)){}
	else if (ProcessUartPacket(e)){}	
}

int main(void)
{
	uint16_t i;
	UsartPacket pack;
	
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
		
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
	TrainSensorC::LedOff();
	TrainSensorD::LedOff();
	
	tus_spi_init();
//	tus_spi_set_handler(spi_received);
		
    while(1)
    {						
		//tus_spi_process_packets();		
		
		TrainSensorA::CheckSensors();
		TrainSensorB::CheckSensors();
		TrainSensorC::CheckSensors();
		TrainSensorD::CheckSensors();
    }
}