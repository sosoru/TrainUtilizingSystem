/*
 * packet.h
 *
 * Created: 2011/12/17 7:43:39
 *  Author: root
 */ 


#ifndef PACKET_H_
#define PACKET_H_

//#include "../eth/net.h"
#include "global.h"

#define BYTE uint8_t

#define ETHCMD_REPLY 0
#define ETHCMD_MESSAGE 1

#define ETH_DATA_LEN 64

typedef union tag_DeviceID
{
	struct
	{
		BYTE SubnetAddr;
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

typedef struct tag_EthPacket
{
	DeviceID srcId;
	DeviceID destId;
		
	BYTE command;
	BYTE error;
	
	BYTE pdata[ETH_DATA_LEN];

} EthPacket;

#endif /* PACKET_H_ */