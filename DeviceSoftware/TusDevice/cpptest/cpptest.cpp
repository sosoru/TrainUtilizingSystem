/*
 * cpptest.cpp
 *
 * Created: 2012/03/19 22:25:51
 *  Author: Administrator
 */ 

#include <IO.h>
#include <DiverseIO.h>
#include <Assembler.h>
#include <Interrupt.h>
#include <ExternalInterrupt.h>
#include <Timer.h>
#include <Sleeping.h>
#include <ADC.h>
#include <EEPROM.h>
#include <USART.h>
#include <Delegate.h>
#include <StaticArray.h>
#include <DynamicArray.h>
#include <StaticQueue.h>
#include <PeekableStaticQueue.h>
using namespace AVRCpp;
using namespace CppDelegate;


int main(void)
{
    while(1)
    {		
        //TODO:: Please write your application code 
		
		OutputPin1<PortC> p;
		
		p.SetTo(true);
		
    }
	return 0;
}