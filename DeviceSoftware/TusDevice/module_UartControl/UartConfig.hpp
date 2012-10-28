/*
 * UartConfig.hpp
 *
 * Created: 2012/05/10 15:48:17
 *  Author: Administrator
 */ 


#ifndef UARTCONFIG_H_
#define UARTCONFIG_H_

#include "module_UartControl.h"
#include "UartModule.hpp"

namespace module_UartControl
{
	namespace Config
	{
		using namespace AVRCpp;
		using namespace USART;
		using namespace Timer;
		
		typedef UartControl::TrainSensorModule<1, OutputPin1<PortA>, OutputPin2<PortA>, USART0, TimerCounter1, 8> TrainSensorA;
		typedef UartControl::TrainSensorModule<2, OutputPin0<PortA>, OutputPin3<PortA>, USART0, TimerCounter1, 8> TrainSensorB;
		typedef UartControl::TrainSensorModule<3, OutputPin4<PortA>, OutputPin5<PortA>, USART0, TimerCounter1, 8> TrainSensorC;
		typedef UartControl::TrainSensorModule<4, OutputPin7<PortA>, OutputPin6<PortA>, USART0, TimerCounter1, 8> TrainSensorD;
		
	}
}



#endif /* UARTCONFIG_H_ */