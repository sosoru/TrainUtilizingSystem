/*
 * tus_mstspi.h
 *
 * Created: 2012/01/30 15:18:39
 *  Author: root
 */ 


#ifndef TUS_MSTSPI_H_
#define TUS_MSTSPI_H_

#include "../EthernetBridge.hpp"
#include "../avr_base.hpp"
#include <avr/delay.h>

namespace EthernetBridge
{
	namespace SoftSpi
	{
		
		template<class MISOpin,
					class MOSIpin,
					class SCKpin
					>
		class SoftSpiModule
		{
			public:
		
			static inline void Init()
			{	
				//init
				MISOpin::InitInput();	//MISO : input
	
				MOSIpin::InitOutput();	//MOSI : output, low
				MOSIpin::Clear();
	
				SCKpin::InitOutput();	//SCK : output, low
				SCKpin::Clear();

			}
		
			template<class SSpin>
			static inline void SelectSlave()
			{
				SSpin::InitOutput();	//nSS : output. high
				SSpin::Set();
			}
					
			template<class SSpin>
			static void TransData(EthPacket *ppack, EthPacket& received)
			{
				MISOpin::InitDefaultInput();
				MOSIpin::Clear();
				SCKpin::Clear();
				SSpin::Clear(); // notify starting transfer for slave 
				_delay_us(5);
				for(uint8_t i=0; i< sizeof(EthPacket); ++i)
				{
					uint8_t send;
					uint8_t byte_recev;
		
					if(ppack == NULL)
					{
						send = 0;
					}
					else
					{
						send = ppack->raw_array[i];
					}
		
					byte_recev = TransByte(send);
				
					received.raw_array[i] = byte_recev;
				}
	
				SSpin::Set();
			}
		
			static inline uint8_t TransByte(uint8_t data)
			{
				uint8_t receive = 0;
			
				//for i in range(0,8):
				_trans_byte<0>(data, receive);
				_trans_byte<1>(data, receive);
				_trans_byte<2>(data, receive);
				_trans_byte<3>(data, receive);
				_trans_byte<4>(data, receive);
				_trans_byte<5>(data, receive);
				_trans_byte<6>(data, receive);
				_trans_byte<7>(data, receive);
	
				return receive;
			}
		
			private:
		
			template <uint8_t i>
			inline static void _trans_byte(uint8_t &data, uint8_t &receive)
			{				
				if((data>>7) & 1)
				{
					MOSIpin::Set();
				}
				else
				{
					MOSIpin::Clear();
				}
		
				data <<= 1;	
				
				_delay_us(3);
				SCKpin::Set();
				_delay_us(2);
				
				if(MISOpin::IsSet())
				{
					receive |= 1 << (7-i);
				}				
				//receive |= MISOpin::IsSet() << (7-i);
				
				_delay_us(2);
				SCKpin::Clear();		
			}

		};
	}
}

#endif /* TUS_MSTSPI_H_ */