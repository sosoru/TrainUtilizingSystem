/*
 * UartModule.h
 *
 * Created: 2012/05/07 19:20:02
 *  Author: Administrator
 */ 


#ifndef UARTMODULE_H_
#define UARTMODULE_H_

#include "module_UartControl.h"
#include <PackPacket.hpp>
#include <uart_packet.h>
#include <tus.h>
#include <util/delay.h>
#include <stdlib.h>

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
				uint8_t t_internal_id,
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
			private:
			static inline void CreateUartPacket(UsartPacket &packet)
			{
				packet.header.type = 0;
				packet.header.number = 0;
				packet.header.packet_size = 0;
			}

			public :			
				static UsartPacket ReceivedArray[t_module_count];
				
				static UartSettingPacket setting_g;
				static TrainSensorPacket packets_g[t_module_count];
							
				static void Init()
				{
					t_module_enable_pin::InitOutput();
					t_module_led_pin::InitOutput();
					
					t_module_enable_pin::Set(); // but on board rev1, change enable pin to low
					
					setting_g.ModuleCount = 0;
				}
				
				static void TimerInit()
				{
					t_timer::ChannelAPin::InitOutput();
					t_timer::ChannelBPin::InitOutput();
		
					t_timer::OutputCompareA::Set(0x10); //1msec
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
				
				static inline uint8_t CheckSensors()
				{
					UsartPacket usartpack;
					
					if(setting_g.ModuleCount == 0)
						return 0;
				
					ModuleOn();
					CreateUartPacket(usartpack);
					
					//cli();	
					//while(setting_g.ModuleCount-1 != Communicate(usartpack));
					Communicate(usartpack);
					//sei();
					
					LedOn();
					_delay_ms(10);
	
					for(uint8_t i=0; i<setting_g.ModuleCount; ++i)
					{
						packets_g[i].OnState = ReceivedArray[i].data[0];
					}		
	
					CreateUartPacket(usartpack);
					
					//cli();
					//while(setting_g.ModuleCount-1 != Communicate(usartpack));
					Communicate(usartpack);
					//sei();
					
					LedOff();
					ModuleOff();
										
					for(uint8_t i=0; i<setting_g.ModuleCount; ++i)
					{
						packets_g[i].OffState = ReceivedArray[i].data[0];
					}		
									
					//check
					for(uint8_t i=0; i<setting_g.ModuleCount; ++i)
					{
						TrainSensorPacket *ppacket = &packets_g[i];
						
						int16_t sub = (((int16_t)ppacket->OnState) - ((int16_t)ppacket->OffState));

						if(abs(sub) >= ppacket->Threshold)
							return 1;
					}		
					return 0;
				}
				
				static inline void ApplyPacket(BaseState *pbstate)
				{
					uint8_t modind = pbstate->InternalAddr & 0x0F;
					
					if(modind == 0)
					{
						UartSettingPacket *ppacket = (UartSettingPacket *)pbstate;
						//setting
						if(ppacket->ModuleCount <= t_module_count)
						{
							setting_g.ModuleCount = ppacket->ModuleCount;
						}
					}
					else
					{				
						TrainSensorPacket *ppacket = (TrainSensorPacket *)pbstate;
						if(modind < setting_g.ModuleCount)
						{
							packets_g[modind-1].Threshold = ppacket->Threshold;
						}
						
					}
				}
				
				static inline void PackSettingPacket(Tus::PacketPacker *ppacker, DeviceID *psrc, DeviceID *pdst)
				{
					setting_g.Base.DataLength = sizeof(setting_g);
					setting_g.Base.InternalAddr = t_internal_id << 4;
					setting_g.Base.ModuleType = MODULETYPE_UART_MOUDLESETTING;
					
					if(false == ppacker->Pack((uint8_t*)&setting_g))
					{
						ppacker->Send(psrc, pdst);
						ppacker->Init();
						
						ppacker->Pack((uint8_t*)&setting_g);
					}
					
				}
				
				static inline void PackPacket(Tus::PacketPacker *ppacker, DeviceID* psrc, DeviceID* pdst)
				{
					uint8_t i;
					
					if(setting_g.ModuleCount == 0)
						return;
				
					for(i=0; i<setting_g.ModuleCount; ++i)
					{
						packets_g[i].Base.DataLength = sizeof(packets_g[i]);
						packets_g[i].Base.InternalAddr = (t_internal_id << 4) + (i+1);
						packets_g[i].Base.ModuleType = MODULETYPE_UART;
						
						if(false == ppacker->Pack((uint8_t*)&packets_g[i])) //if buffer is fulled
						{
							ppacker->Send(psrc, pdst);
							ppacker->Init();
							
							ppacker->Pack((uint8_t*)&packets_g[i]);
						}
					}
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
					for(m=0; m<setting_g.ModuleCount; ++m)
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
				uint8_t t_internal_id,
				class t_module_enable_pin,
				class t_module_led_pin,
				class t_uart,
				class t_timer,
				uint8_t t_module_count
			>
		UsartPacket
		 TrainSensorModule<
				t_internal_id,
				t_module_enable_pin,
				t_module_led_pin,
				t_uart,
				t_timer,
				t_module_count>
				::ReceivedArray[t_module_count];

		template <
				uint8_t t_internal_id,
				class t_module_enable_pin,
				class t_module_led_pin,
				class t_uart,
				class t_timer,
				uint8_t t_module_count
			>
		TrainSensorPacket
		 TrainSensorModule<
				t_internal_id,
				t_module_enable_pin,
				t_module_led_pin,
				t_uart,
				t_timer,
				t_module_count>
				::packets_g[t_module_count];
				
		template <
				uint8_t t_internal_id,
			class t_module_enable_pin,
			class t_module_led_pin,
			class t_uart,
			class t_timer,
			uint8_t t_module_count
		>
		UartSettingPacket
		TrainSensorModule<
				t_internal_id,
			t_module_enable_pin,
			t_module_led_pin,
			t_uart,
			t_timer,
			t_module_count>
		::setting_g;
					
	}
}



#endif /* UARTMODULE_H_ */