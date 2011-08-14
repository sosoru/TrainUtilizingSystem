#include "HardwareProfile.h"
#include "../Headers/RemoteModule.h"
#include "../Headers/MotherBoardModule.h"
#include "../Headers/SpiTransmit.h"
#include <stdlib.h>
#include <spi.h>

#define PENDING_COUNT 8 

typedef struct tag_PendingCnt
{
	SpiPacket Packet;
	DeviceID InternalID;
} PendingCnt;

BOOL g_SendingCompletion = TRUE;
DeviceID g_RemotingPendingIntDevID;

PendingCnt pendingList[PENDING_COUNT];
BYTE pendingFirstInd = 0;
BYTE pendingLastInd = 0;

HRESULT EnqueueRemotingPending(SpiPacket * ppacket, DeviceID * pid);
HRESULT DequeueRemotingPending(SpiPacket * ppacket, DeviceID * pid);
HRESULT SendRemoteDevice(DeviceID * pid, BYTE mode, PMODULE_DATA data);

#define IS_PENDING_FULL ((pendingFirstInd < pendingLastInd) \
							? pendingLastInd - pendingFirstInd >= (PENDING_COUNT-1) \
							: (pendingLastInd + PENDING_COUNT) - pendingFirstInd >= (PENDING_COUNT-1))
							
HRESULT EnqueueRemotingPending(SpiPacket * ppacket, DeviceID * pid)
{
	PendingCnt * pcnt;
	
	if(IS_PENDING_FULL)
		return E_FAIL;
		
	pcnt = &pendingList[pendingLastInd++];
	ppacket = &(pcnt->Packet);
	pid = &(pcnt->InternalID);
	if(pendingLastInd >= PENDING_COUNT)
		pendingLastInd = 0;
	
	return S_OK;
}

HRESULT DequeueRemotingPending(SpiPacket * ppacket, DeviceID * pid)
{
	PendingCnt * pcnt;
	
	if(pendingFirstInd == pendingLastInd)
		return E_FAIL; //pendingList is empty
	
	pcnt = &pendingList[pendingFirstInd++];
	ppacket = &(pcnt->Packet);
	pid = &(pcnt->InternalID);
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
		OpenSPI(SPI_FOSC_4, MODE_01, SMPEND);
	#elif defined TRAIN_CONTROLLER_REV1
		OpenSPI(SLV_SSOFF, MODE_01, SMPEND);
	#endif
	
	memset(pendingList, 0x00, sizeof(pendingList));
}

HRESULT SendRemoteDevice(DeviceID * pid, BYTE mode, PMODULE_DATA data)
{
	SpiPacket * packet;
	RemoteModuleSavedState saved;
	HRESULT res;
	
	ReadModuleSavedState(pid->ModulePart, &saved);
	
	if(SUCCEEDED(EnqueueRemotingPending(packet, pid)))
	{
		memcpy(&packet->devid, &saved.remid, sizeof(*pid));
		packet->mode = mode;
		if(mode == MODE_STORE)
		{
			memcpy(packet->data, data, sizeof(SPI_DATASIZE));
			res = E_FAIL; // cannot create a packet only this device. it will wait associated device by interrupting method. 
		}
		else // if mode is MODE_CREATE, reply remotemodule state
		{
			RemoteModuleState * casted = (RemoteModuleState*)data;
			RemoteModuleSavedState saved;
			ReadModuleSavedState(pid->ModuleAddr, &saved);
			
			memcpy(&casted->remid, &saved.remid, sizeof(casted->remid));
			
			memset(packet->data, 0x00, sizeof(SPI_DATASIZE));
			res = S_OK;
		}
		CalcSpiPacketCrc(&packet);
	}
	
	return res;
}

void InterruptRemoteModule(DeviceID * pid)
{
	if(g_SendingCompletion)
	{
		SpiPacket * ppacket;
		DeviceID * pidbuf;
		if(SUCCEEDED(DequeueRemotingPending(ppacket, pidbuf)))
		{
			SendSpiPacket(ppacket);
			memcpy(g_RemotingPendingIntDevID, pidbuf, sizeof(*pidbuf));
			
			if(ppacket->mode == MODE_CREATE) // replies a message from destination
			{
				g_SendingCompletion = FALSE;		
			}
			else
			{
				g_SendingCompletion = TRUE;
			}
		}	
	}
}

HRESULT CreateMessageFromReceived(SpiPacket * ppacket, DeviceID * pid, PMODULE_DATA data)
{
	if(g_SendingCompletion)
		return E_FAIL; //invalid calling
		
	pid = &g_RemotingPendingIntDevID;
	memcpy(data, ppacket->data, sizeof(PMODULE_DATA));
	
	return S_OK;
}

HRESULT CreateRemoteModuleState(DeviceID * pid, PMODULE_DATA data)
{
	return SendRemoteDevice(pid, MODE_CREATE, data);
}

HRESULT StoreRemoteModuleState(DeviceID * pid, PMODULE_DATA data)
{
	if(pid->RemoteBit)
	{
		RemoteModuleState * pstate = (RemoteModuleState * )data;
		RemoteModuleSavedState saved;
		
		memcpy(&(saved.remid), &(pstate->remid), sizeof(saved.remid));
		WriteModuleSavedState(pid->ModuleAddr, &saved);
		
		return S_OK;		
	}
	else
	{
		return SendRemoteDevice(pid, MODE_STORE, data);	
	}
	
}