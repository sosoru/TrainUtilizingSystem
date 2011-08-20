#include "HardwareProfile.h"
#include "../Headers/RemoteModule.h"
#include "../Headers/MotherBoardModule.h"
#include "../Headers/SpiTransmit.h"
#include <stdlib.h>
#include <string.h>
#include <spi.h>

#include "../Headers/PortMappingRev2.h"

#define PENDING_COUNT 7

typedef struct tag_PendingCnt
{
	SpiPacket Packet;
	DeviceID InternalID;
} PendingCnt;

BOOL g_SendingCompletion = TRUE;
DeviceID g_RemotingPendingIntDevID;
BOOL PacketSetting = FALSE;

PendingCnt pendingList[PENDING_COUNT];
#define PENDING_SIZE (sizeof(PendingCnt)*PENDING_COUNT)
BYTE pendingFirstInd = 0;
BYTE pendingLastInd = 0;

HRESULT EnqueueRemotingPending(SpiPacket ** ppacket, DeviceID * pid);
HRESULT DequeueRemotingPending(SpiPacket ** ppacket, DeviceID ** pid);
HRESULT SendRemoteDevice(DeviceID * pid, BYTE mode, PMODULE_DATA data);

#define IS_PENDING_FULL ((pendingFirstInd <= pendingLastInd) \
							? pendingLastInd - pendingFirstInd >= (PENDING_COUNT-1) \
							: (pendingLastInd + PENDING_COUNT) - pendingFirstInd >= (PENDING_COUNT-1))
							
HRESULT EnqueueRemotingPending(SpiPacket ** ppacket, DeviceID * pid)
{
	PendingCnt * pcnt;
	
	if(IS_PENDING_FULL)
		return E_FAIL;
		
	pcnt = &pendingList[pendingLastInd++];
	*ppacket = &(pcnt->Packet);
	memcpy((void*)&(pcnt->InternalID), (void*)pid, sizeof(DeviceID));
	
	if(pendingLastInd >= PENDING_COUNT)
		pendingLastInd = 0;
	
	return S_OK;
}

HRESULT DequeueRemotingPending(SpiPacket ** ppacket, DeviceID ** pid)
{
	PendingCnt * pcnt;
	
	if(pendingFirstInd == pendingLastInd)
		return E_FAIL; //pendingList is empty
	
	pcnt = &pendingList[pendingFirstInd++];
	*ppacket = &(pcnt->Packet);
	*pid = &(pcnt->InternalID);
	if(pendingFirstInd >= PENDING_COUNT)
		pendingFirstInd = 0;
	
	return S_OK;
}


HRESULT GetFuncTableRemoteModule(DeviceID * pid, ModuleFuncTable* table)
{
	table->fncreate = CreateRemoteModuleState;
	table->fnstore = StoreRemoteModuleState;
	table->fninit = InitRemoteModule;
	table->fninterrupt = InterruptRemoteModule;
	
	return S_OK;
}

HRESULT InitRemoteModule(DeviceID * pid)
{
	#if defined VERSION_REV2
		OpenSPI(SPI_FOSC_16, MODE_11, SMPEND);
	#elif defined CONTROLLER_DEIVCE_REV1
		OpenSPI(SLV_SSOFF, MODE_11, SMPEND);
	#endif
	
	memset((void*)pendingList, 0x00, (size_t)PENDING_SIZE);
}

HRESULT SendRemoteDevice(DeviceID * pid, BYTE mode, PMODULE_DATA data)
{
	SpiPacket * packet;
	RemoteModuleSavedState saved;
	
	ReadInnerRemoteModuleSavedState(pid->ModulePart, pid->InternalAddr, &saved);
	
	if(saved.remid.ParentPart == g_mbState.ParentId)
		return E_FAIL;
	
	if(SUCCEEDED(EnqueueRemotingPending(&packet, pid)))
	{
		memcpy((void*)&(packet->devid), (void*)&(saved.remid), (size_t)sizeof(DeviceID));
		packet->mode = mode;
		if(mode == MODE_STORE)
		{
			memcpy((void*)packet->data, (void*)data, (size_t)SPI_DATASIZE);
		}
		else 
		{
			memset((void*)packet->data, 0x00, (size_t)SPI_DATASIZE);
		}
		CalcSpiPacketCrc(packet);
	}
	
	
	return E_FAIL;// cannot create a packet only this device. it will wait associated device by interrupting method. 
}

void InterruptRemoteModule(DeviceID * pid)
{	
	if(g_SendingCompletion && !PacketSetting)
	{
		SpiPacket * ppacket;
		DeviceID * pidbuf;
		if(SUCCEEDED(DequeueRemotingPending(&ppacket, &pidbuf)))
		{
			Port_SurfaceLedA = 0;
			SendSpiPacket(ppacket);
			memcpy((void*)&g_RemotingPendingIntDevID, (void*)pidbuf, (size_t)sizeof(DeviceID));
			
			g_SendingCompletion = (ppacket->mode != MODE_CREATE); // replies a message from destination
		}	
	}
}

HRESULT CreateMessageFromReceived(SpiPacket * ppacket, DeviceID * pid, PMODULE_DATA data)
{
	if(g_SendingCompletion)
		return E_FAIL; //invalid calling
		
	memcpy((void*)pid, (void*)&g_RemotingPendingIntDevID, (size_t)sizeof(DeviceID));
	memcpy((void*)data, (void*)ppacket->data, (size_t)sizeof(MODULE_DATA));
	
	return S_OK;
}

HRESULT CreateRemoteModuleState(DeviceID * pid, PMODULE_DATA data)
{
	HRESULT res = (pid->InternalAddr >= REMOTE_MODULE_INNERCOUNT) ? REPEAT_TERMINATE : 0;
	
	PacketSetting = TRUE;	
	
	if(pid->RemoteBit)
	{
		RemoteModuleState * casted = (RemoteModuleState*)data;
		RemoteModuleSavedState saved;
		ReadInnerRemoteModuleSavedState(pid->ModuleAddr, pid->InternalAddr, &saved);
		
		memcpy((void*)&(casted->remid), (void*)&(saved.remid), (size_t)sizeof(DeviceID));
		res |= S_OK;
	}
	else
		res |= SendRemoteDevice(pid, MODE_CREATE, data);
		
	PacketSetting = FALSE;
	return res;
}

HRESULT StoreRemoteModuleState(DeviceID * pid, PMODULE_DATA data)
{
	HRESULT res = 0;
	
	PacketSetting = TRUE;
	
	if(pid->RemoteBit)
	{
		RemoteModuleState * pstate = (RemoteModuleState * )data;
		RemoteModuleSavedState saved;
		
		memcpy((void*)&(saved.remid), (void*)&(pstate->remid), (size_t)sizeof(DeviceID));
		WriteInnerRemoteModuleSavedState(pid->ModuleAddr, pid->InternalAddr, &saved);
		
		res |= S_OK;		
	}
	else
	{
		res |= SendRemoteDevice(pid, MODE_STORE, data);	
	}
	
	PacketSetting = FALSE;
	return res;
	
}
