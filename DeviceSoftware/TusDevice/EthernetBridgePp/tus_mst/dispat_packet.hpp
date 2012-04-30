/*
 * dispat_packet.h
 *
 * Created: 2012/01/30 16:57:00
 *  Author: root
 */ 


#ifndef DISPAT_PACKET_H_
#define DISPAT_PACKET_H_

#include "../avr_base.hpp"
#include <stdlib.h>
#include <string.h>

namespace EthernetBridge{
	namespace DispatchBuffer
	{		
		template<	uint8_t buffer_count,
					uint8_t message_size,
					uint8_t device_child_id
				>
		class PacketDispatcher
		{
			private:
				static EthPacket buffer[buffer_count];
				static char last_message[message_size];
				
				static int8_t m_buffer_front;
				static int8_t m_buffer_back;
				
			public:
				static inline void Init()
				{										
					memset((void*)buffer, 0x00, sizeof(buffer));				
					memset((void*)last_message, 0x00, sizeof(last_message));
					m_buffer_front = 0;
					m_buffer_back = 0;
				}
				
				static inline uint8_t Count()
				{
					uint8_t tail = m_buffer_back;
					if(m_buffer_back > m_buffer_front)
						tail += buffer_count;
						
					return m_buffer_front - tail ;
				}
				
				static inline bool PushPacket(EthPacket** pppack)
				{
					if(Count() == buffer_count)
						return false;
					
					*pppack = &buffer[m_buffer_front];
					
					m_buffer_front++;
					m_buffer_front %= buffer_count;
											
					return true;				
				}
				
				static inline bool PopPacket(EthPacket** pppack)
				{
					if(Count() == 0)
						return false;
						
					*pppack = &buffer[m_buffer_back];
					
					m_buffer_back++;
					m_buffer_back %= buffer_count;
						
					return true;
				}
				
				static inline uint8_t SetMessage(const EthPacket& packet)
				{
					if(packet.command != ETHCMD_MESSAGE)
						return false;

					memcpy((void*)last_message, (void*)packet.pdata, message_size );	
					return true;	
				}
				
				static inline char* GetMessage() 
				{
					return last_message;
				}
		};
	
		template<	uint8_t buffer_count,
			uint8_t message_size,
			uint8_t device_child_id
		> EthPacket PacketDispatcher<buffer_count, message_size, device_child_id>::buffer[buffer_count];

		template<	uint8_t buffer_count,
			uint8_t message_size,
			uint8_t device_child_id
		> char PacketDispatcher<buffer_count, message_size, device_child_id>::last_message[message_size];

		template<	uint8_t buffer_count,
			uint8_t message_size,
			uint8_t device_child_id
		> int8_t PacketDispatcher<buffer_count, message_size, device_child_id>::m_buffer_front;
		
		template<	uint8_t buffer_count,
			uint8_t message_size,
			uint8_t device_child_id
		> int8_t PacketDispatcher<buffer_count, message_size, device_child_id>::m_buffer_back;
		

	}
}


#endif /* DISPAT_PACKET_H_ */