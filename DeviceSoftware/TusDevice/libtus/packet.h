/*
 * packet.h
 *
 * Created: 2011/12/17 7:43:39
 *  Author: root
 */ 


#ifndef PACKET_H_
#define PACKET_H_

#include <avr/io.h>
#include "net.h"

#define BYTE uint8_t

#define ETHCMD_REPLY 1
#define ETHCMD_MEMORY 2

#define ETH_DATA_LEN 26 

#define MODULETYPE_KERNEL 0x11
#define MODULETYPE_MOTOR 0x12

typedef union tag_DeviceID
{
	struct
	{
		uint16_t SubnetAddr;
		BYTE InternalAddr;
		BYTE ModuleAddr;
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
	union{
		struct {
			DeviceID srcId; 
			DeviceID destId; 
					
			DeviceID devID;
	
			BYTE pdata[ETH_DATA_LEN]; 
		};
		
		//todo : how to read the size of unnamed structs or unions 
		BYTE raw_array[40];
	};
} EthPacket;

typedef struct tag_BaseState
{
	struct{
		BYTE DataLength;
		BYTE InternalAddr;
		BYTE ModuleType;
	};
} BaseState;

typedef struct tag_KernelState
{
	union{
		struct{
			BaseState Base;
			BYTE KernelCommand;
			BYTE pdata [4];
			};
		};
		
		BYTE raw_array[8];
} KernalState;	

typedef struct tag_MemoryState
{
	struct{
		BYTE CurerntMemory;
		BYTE MemoryLimit;
	};
} MemoryState;

#endif /* PACKET_H_ */