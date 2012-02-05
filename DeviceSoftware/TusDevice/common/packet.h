/*
 * packet.h
 *
 * Created: 2011/12/17 7:43:39
 *  Author: root
 */ 


#ifndef PACKET_H_
#define PACKET_H_

#include "eth/net.h"

#define BYTE uint8_t

#define ETHCMD_REPLY 0

#define ETH_MSG_LEN 16
#define ETH_DATA_LEN 128

typedef union tag_DeviceID
{
	struct
	{
		uint16_t SubnetAddr;
		BYTE ModuleAddr;
		BYTE InternalAddr;
	};
	struct
	{
		uint16_t ParentPart;
		uint16_t ModulePart;
	};
	uint32_t raw;
} DeviceID;

typedef struct tag_EthPacket // 154 bytes
{
	DeviceID srcId; 
	DeviceID destId; 
		
	BYTE command; 
	BYTE error; 
	
	char message[ETH_MSG_LEN]; 
	BYTE pdata[ETH_DATA_LEN]; 

} EthPacket;

#endif /* PACKET_H_ */