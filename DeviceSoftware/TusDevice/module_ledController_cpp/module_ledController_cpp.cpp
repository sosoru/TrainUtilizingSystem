/*
 * module_ledController_cpp.cpp
 *
 * Created: 2013/07/03 17:21:19
 *  Author: Administrator
 */ 


#include "avr_base.hpp"
#include <avr/io.h>
#include <avr/delay.h>
#include <avr/interrupt.h>
#include <Timer.h>
#include <string.h>
#include <tus.h>
#include <led_packet.h>
using namespace AVRCpp::Timer;

#define PORT_DATA			PORTA
#define PORT_CLK			PORTD
#define PORT_ENABLE			PORTC
#define PORT_ENABLE_NUM		0

#define LED_COUNT			30

LedState g_States[LED_COUNT];
DeviceID g_myDevID;

uint8_t led_count_remains[LED_COUNT];
uint8_t led_count_init[LED_COUNT];

#define APPLY_TO_LED(clk,ind)				if(led_count_remains[(clk+1)*8-ind-1] >0){ \
sbi(led_signal_buffer, ind); \
led_count_remains[(clk+1)*8-ind-1]--; \
			}else{ \
				cbi(led_signal_buffer, ind);} 
				
ISR(TIMER1_COMPA_vect)
{
	//if(!IS_SPI_COMMUNICATING)
	//{
		tus_spi_process_packets();
	//}
}


void device_init()
{
	DDRA = 0xFF;
	DDRD = 0xFF;
	DDRC = 0xFF;
	
	cbi(PORT_ENABLE, PORT_ENABLE_NUM); // enable D-flipflops
		
	memset(led_count_init, 0, sizeof(led_count_init));
	memset(led_count_remains, 0x00, sizeof(led_count_remains));
}

 void init_led_status()
{
	uint8_t cache = SREG;
	cli();
	memcpy(led_count_remains, led_count_init, sizeof(led_count_init));
	SREG = cache;
}

 void apply_led_status()
{
	uint8_t led_signal_buffer;
	
	APPLY_TO_LED(0,0);
	APPLY_TO_LED(0,1);
	APPLY_TO_LED(0,2);
	APPLY_TO_LED(0,3);
	APPLY_TO_LED(0,4);
	APPLY_TO_LED(0,5);
	APPLY_TO_LED(0,6);
	APPLY_TO_LED(0,7);
	PORT_DATA = led_signal_buffer;
	PORT_CLK = 1<<0;
	
	APPLY_TO_LED(1,0);
	APPLY_TO_LED(1,1);
	APPLY_TO_LED(1,2);
	APPLY_TO_LED(1,3);
	APPLY_TO_LED(1,4);
	APPLY_TO_LED(1,5);
	APPLY_TO_LED(1,6);
	APPLY_TO_LED(1,7);
	PORT_DATA = led_signal_buffer;
	PORT_CLK = 1<<1;
	
	APPLY_TO_LED(2,0);
	APPLY_TO_LED(2,1);
	APPLY_TO_LED(2,2);
	APPLY_TO_LED(2,3);
	APPLY_TO_LED(2,4);
	APPLY_TO_LED(2,5);
	APPLY_TO_LED(2,6);
	APPLY_TO_LED(2,7);
	PORT_DATA = led_signal_buffer;
	PORT_CLK = 1<<2;
	
	//APPLY_TO_LED(3,0);
	//APPLY_TO_LED(3,1);
	APPLY_TO_LED(3,2);
	APPLY_TO_LED(3,3);
	APPLY_TO_LED(3,4);
	APPLY_TO_LED(3,5);
	APPLY_TO_LED(3,6);
	APPLY_TO_LED(3,7);
	PORT_DATA = led_signal_buffer;
	PORT_CLK = 1<<3;
	PORT_CLK = 0;

}

//TODO: support reply memory states
//void SendKernelPacket(DeviceID *psrcid, DeviceID *pdstid)
//{
	//uint8_t i;
	//KernalState state;
	//MemoryState pmemstate = (MemoryState*)state.pdata;
//
	//state.Base.DataLength = sizeof(KernalState);
	//state.KernelCommand = ETHCMD_MEMORY;
	//pmemstate.CurerntMemory = 0;
	//pmemstate.MemoryLimit = 1;
	//
//}
//
//bool ProcessKernelPacket(KernalState *pstate , DeviceID* psrcid, DeviceID* pdstid)
//{
	//if(pstate->Base.ModuleType != MODULETYPE_KERNEL
	//&& pstate->Base.InternalAddr > LED_COUNT)
		//return false;
	//
	//switch(pstate->KernelCommand)
	//{
		//case ETHCMD_REPLY:
			//SendKernelPacket(pdstid, psrcid);	//reply current memory
		//break;
	//}
	//
	//return true;
//}
//

bool ProcessLedPacket(LedState *pstate)
{
	if(pstate->Base.ModuleType != MODULETYPE_LED
		&& pstate->Base.InternalAddr > LED_COUNT
		&& pstate->Base.InternalAddr == 0)
		return false;
	
	led_count_init[pstate->Base.InternalAddr-1] = pstate->DutyValue;
	
	return true;
}

void spi_received(args_received *e)
{
	g_myDevID.raw  = e->pdstId->raw;

	//if (ProcessKernelPacket((KernalState*)e->ppack, e->psrcId, e->pdstId)){}
	ProcessLedPacket((LedState*)e->ppack);

}

int main(void)
{	
	uint8_t i,j;
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	tus_spi_init();
	tus_spi_set_handler(spi_received);
	
	device_init();
	TimerCounter1::SetUp(NoPrescaleB, Normal16, NormalPortOperationA, NormalPortOperationB, Off, Fall);
	TimerCounter1::CompareMatchAInterrupt::Enable();

    while(1)
    {	
		init_led_status();
		for(i=0; i<128; ++i)
		{
			apply_led_status();
			_delay_us(100);
		}
	}
}