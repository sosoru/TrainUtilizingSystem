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
									10
									 >
		{
			
			public :
				static UsartPacket ReceivedArray[t_module_count];
							
				static void Init()
				{
					t_module_enable_pin::InitOutput();
					t_module_led_pin::InitOutput();
					
					t_module_enable_pin::Set(); // but on board rev1, change enable pin to low
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
					t_module_enable_pin::Clear(); // but on board rev1, change enable pin to high
				}
				
				static inline void ModuleOn()
				{
					t_module_enable_pin::Set();
				}
				
				static inline void SendUartPacket(UsartPacket &packet)
				{
					packet.header.checksum_all = CalcChecksum(packet);
					
					for(uint8_t i=0; i<sizeof(packet.header); ++i)
					{
						t_uart::Send(packet.header.rawdata[i]);
					}
					for(uint8_t i=0; i<packet.header.packet_size; ++i)
					{
						t_uart::Send(packet.data[i]);
					}
				}
			
				static inline int8_t Communicate(UsartPacket &pack_x)
				{
					SendUartPacket(pack_x);

					uint8_t received;
					int8_t m;
					for(m=0; m<t_module_count; ++m)
					{
						while(!t_uart::IsTransferCompleted());
						
						for(uint8_t i=0; i<sizeof(UsartDevicePacket_Header); ++i)
						{												
							if(!TimeoutedReceive(received))
							{
								return m-1;
							}									
							ReceivedArray[m].header.rawdata[i] = received;
						}
						
						
						uint8_t datasize = ReceivedArray[m].header.packet_size;
						if(datasize > MAX_SIZE_USARTDATA)
							datasize = MAX_SIZE_USARTDATA;
							
						for(uint8_t i=0; i<datasize; ++i)
						{
							if(!TimeoutedReceive(received))
							{
								return m-1;
							}
							ReceivedArray[m].data[i] = received;
						}
						
						if(CalcChecksum(ReceivedArray[m]) != ReceivedArray[m].header.checksum_all)
						{
							return m-1;				
						}
					}	
										
					return m-1;
				}
				
				private:
								
				static inline uint8_t CalcChecksum(UsartPacket& packet)
				{
					uint8_t chksum = 0;
					
					chksum ^= packet.header.type;
					chksum ^= packet.header.number;
					chksum ^= packet.header.packet_size;
					
					for(uint8_t i=0; i<packet.header.packet_size; ++i)
					{
						chksum ^= packet.data[i];
					}
					
					return chksum;
				}
							
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
		UsartPacket
		 TrainSensorModule<
				t_module_enable_pin,
				t_module_led_pin,
				t_uart,
				t_timer,
				t_module_count>
				::ReceivedArray[t_module_count];
	}
}



#endif /* UARTMODULE_H_ */