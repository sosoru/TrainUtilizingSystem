/*
 * PackPacket.hpp
 *
 * Created: 2012/10/26 8:50:02
 *  Author: Administrator
 */ 


#ifndef PACKPACKET_H_
#define PACKPACKET_H_

#include <string.h>
#include "../libtus/tus.h"

namespace Tus
{
	class PacketPacker
	{
		private :
			static spi_send_object *psend_g;
			static uint8_t ind_state_g;
			
		public:
			
			static bool Init()
			{
				if(IsInitialized())
					return false;
				
				tus_spi_lock_send_buffer(&psend_g);
				ind_state_g = 0;
				return true;
			}	
			
			static bool IsInitialized()
			{
				return psend_g != NULL;
			}
			
			static bool IsFull(uint8_t len)
			{
				return ind_state_g + len > ETH_DATA_LEN;
			}
			
			static bool Pack(const uint8_t* pdata)
			{
				uint8_t len = pdata[0];
				
				if(!IsInitialized() || IsFull(len))
					return false;

				memcpy(&psend_g->packet.pdata[ind_state_g], pdata, len);
				ind_state_g += len;
				
				return true;
			}
			
			static bool Send(DeviceID *psrcid, DeviceID *pdstid)
			{
				if(!IsInitialized())
					return false;
				
				psend_g->packet.srcId.raw = psrcid->raw;
				psend_g->packet.srcId.InternalAddr = 0;
				psend_g->packet.destId.raw = pdstid->raw;
				psend_g->packet.devID.raw = psrcid->raw;
				
				if(!IsFull(0))
					psend_g->packet.pdata[ind_state_g] = 0x00; // null termination if not fully packed
				
				psend_g->is_locked = false;
				
				psend_g = NULL;
				return true;
			}
	};
	
	spi_send_object* PacketPacker::psend_g;
	uint8_t PacketPacker::ind_state_g;
}



#endif /* PACKPACKET_H_ */