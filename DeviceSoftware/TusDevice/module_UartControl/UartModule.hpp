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
				NormalSpeed, SingleProcessor);
				
				//t_uart::SetupMasterSync(t_baudrate, ReceiverEnable, TransmitterEnable,
										//NoParityCheck, NormalStopBit, CharacterSize8,
										//ReceiveOnFall, SingleProcessor);
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
									32
									 >
		{
			
			public :
				static TrainSensorPacket_rcev ReceivedArray[t_module_count];
				static TrainSensorPacket_xmit TransmitData;
				
				static inline void SetTransmitNumber(uint8_t number)
				{
					memset(TransmitData.rawdata, number, sizeof(TransmitData.rawdata));
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
		
					t_timer::OutputCompareA::Set(0x10); //5msec
					t_timer::SetUp(Prescale1024B, FastPWM16BitsCount8, NormalPortOperationA, NormalPortOperationB, Off, Fall);
				}
				
				static inline void LedOn() { t_module_led_pin::Set(); }
				static inline void LedOff() { t_module_led_pin::Clear(); }
					
				static inline void ModuleOff()
				{
					LedOff();
					t_module_enable_pin::Set();
				}
			
				static inline bool Communicate()
				{										
					t_uart::Send(TransmitData);
					//
					uint8_t m=0;
					//for(m=0; m<t_module_count; ++m)
					//{
						uint8_t chksum = 0x00;
						//
						while(!t_uart::IsTransferCompleted());
						for(uint8_t i=0; i<sizeof(TrainSensorPacket_rcev); ++i)
						{							
							uint8_t received;
							
							if(!TimeoutedReceive(received))
							{
								return false;
							}				
							
							ReceivedArray[m].rawdata[i] = received;
						}
						
						if(ReceivedArray[m].number ^ ReceivedArray[m].result != chksum)
						{
							return false;				
						}
					//}	
										
					return true; //m == t_module_count;
				}
				
				private:
				
				static inline bool TimeoutedReceive(uint8_t &data)
				{
					t_timer::Counter::Set(0);
					t_timer::CompareMatchAInterrupt::ClearFlag();
					
					while(!t_uart::IsReceiveCompleted())
					{
						if(t_timer::CompareMatchAInterrupt::IsTriggered())
						{
							return false;
						}
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