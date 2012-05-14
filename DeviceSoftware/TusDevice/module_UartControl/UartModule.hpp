/*
 * UartModule.h
 *
 * Created: 2012/05/07 19:20:02
 *  Author: Administrator
 */ 


#ifndef UARTMODULE_H_
#define UARTMODULE_H_

#include "module_UartControl.h"
#include "UartPacket.h"
#include <util/delay.h>

namespace module_UartControl
{
	namespace UartControl
	{
		using namespace AVRCpp;
		using namespace CppDelegate;
		using namespace USART;
		using namespace Timer;
		
		template <
					class t_module_enable_pin1,
					class t_module_enable_pin2,
					class t_uart,
					uint16_t t_baudrate
				>
		class UartModule
		{
			public:		
				
			static inline void UartInit()
			{
				t_uart::SetupAsynchronous(t_baudrate, ReceiverEnable, TransmitterEnable,
				NoParityCheck, NormalStopBit, CharacterSize8,
				DoubleSpeed, SingleProcessor);
			}
		};
		
		template <
				class t_module_enable_pin,
				class t_module_led_pin,
				class t_uart,
				class t_timer,
				uint8_t t_module_count
			>
		class TrainSensorModule
			: public UartModule<	t_module_enable_pin,
									t_module_led_pin,
									t_uart,
									64
									 >
		{
			
			public :
				static TrainSensorPacket_rcev ReceivedArray[t_module_count];
				static TrainSensorPacket_xmit TransmitData;
				
				static inline void SetTransmitNumber(uint8_t number)
				{
					memset(TransmitData.rawdata, number, sizeof(TransmitData));
				}
			
				static void Init()
				{
					t_module_enable_pin::InitOutput();
					t_module_led_pin::InitOutput();
					
					t_module_enable_pin::Clear();
				}
				
				static void TimerInit()
				{
					t_timer::ChannelAPin::InitOutput();
					t_timer::ChannelBPin::InitOutput();
		
					t_timer::OutputCompareA::Set(0x61);
					t_timer::SetUp(Prescale1024B, FastPWM16BitsCount8, NormalPortOperationA, NormalPortOperationB, Off, Fall);
				}
				
				static inline void LedOn() { t_module_led_pin::Set(); }
				static inline void LedOff() { t_module_led_pin::Clear(); }
			
				static inline bool Communicate()
				{										
					//t_module_enable_pin::Clear();
					t_uart::Send(TransmitData);
					t_timer::OutputCompareA::Set(0x61); //5msec
					
					uint8_t m;
					for(m=0; m<t_module_count; ++m)
					{
						uint8_t chksum = 0x00;
						
						for(uint8_t i=0; i<sizeof(TrainSensorPacket_rcev); ++i)
						{							
							uint8_t received;
							
							if(!TimeoutedReceive(received))
							{
								break;
							}				
							
							chksum ^= received;
							ReceivedArray[m].rawdata[i] = received;
						}
						
						if(ReceivedArray[m].checksum != chksum)
						{
							break;				
						}
					}	
										
					//t_module_enable_pin::Set();
					return m == t_module_count;
				}
				
				private:
				
				static inline bool TimeoutedReceive(uint8_t &data)
				{
					t_timer::Counter::Set(0);
					while(t_uart::IsReceiveCompleted())
					{
						if(t_timer::CompareMatchAInterrupt::IsTriggered())
						{
							return false;
						}
						_delay_us(1);
					}
					
					data = t_uart::Data::Get();
					return true;
				}
		};
		
		template <
				class t_module_enable_pin,
				class t_module_led_pin,
				class t_uart,
				class t_timer,
				uint8_t t_module_count
			>
		TrainSensorPacket_rcev
		 TrainSensorModule<
				t_module_enable_pin,
				t_module_led_pin,
				t_uart,
				t_timer,
				t_module_count>
				::ReceivedArray[t_module_count];

		template <
				class t_module_enable_pin,
				class t_module_led_pin,
				class t_uart,
				class t_timer,
				uint8_t t_module_count
			>
		TrainSensorPacket_xmit
		 TrainSensorModule<
				t_module_enable_pin,
				t_module_led_pin,
				t_uart,
				t_timer,
				t_module_count>
				::TransmitData;

	}
}



#endif /* UARTMODULE_H_ */