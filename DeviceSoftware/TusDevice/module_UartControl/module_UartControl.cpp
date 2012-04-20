/*
 * module_UartControl.cpp
 *
 * Created: 2012/03/23 5:09:20
 *  Author: Administrator
 */ 

#include "module_UartControl.h"
#include <util/delay.h>

using namespace AVRCpp;
using namespace CppDelegate;
using namespace USART;

USART0 uart;

int main(void)
{
	
	uart.SetupAsynchronous(64, ReceiverEnable, TransmitterEnable,
						NoParityCheck, NormalStopBit, CharacterSize8,
						DoubleSpeed, SingleProcessor);
	
    while(1)
    {
        //TODO:: Please write your application code 
		
		_delay_ms(10);
		
		{
			Uart
		}
		
		uart.Write(0x01);
    }
}