#if !defined BLOCK_REMOTE_MODULE
#define BLOCK_REMOTE_MODULE

#include "../Headers/ModuleBase.h"
#include "../Headers/SpiTransmit.h"

#define REMOTE_MODULE_MODULE_TYPE 0x04

#define REMOTE_MODULE_INNERCOUNT 8

#define ReadInnerRemoteModuleSavedState(module, inner, pbuf) EEPROMcpy((unsigned char *)(pbuf), (unsigned char)(ADDRESS_EEPROM_STARTS(module) + sizeof(RemoteModuleSavedState)*(inner)), (unsigned char)(sizeof(RemoteModuleSavedState)))
#define WriteInnerRemoteModuleSavedState(module, inner, pbuf) EEPROMset((unsigned char)(ADDRESS_EEPROM_STARTS(module) + sizeof(RemoteModuleSavedState)*(inner)), (unsigned char *)(pbuf), (unsigned char)(sizeof(RemoteModuleSavedState)))

extern BOOL g_SendingCompletion;
extern DeviceID g_RemotingPendingIntDevID;

HRESULT CreateMessageFromReceived(SpiPacket * ppacket, DeviceID * pid, PMODULE_DATA data);

HRESULT GetFuncTableRemoteModule(DeviceID * pid, ModuleFuncTable* table);
HRESULT InitRemoteModule(DeviceID * pid);
HRESULT CreateRemoteModuleState(DeviceID * pid, PMODULE_DATA data);
HRESULT StoreRemoteModuleState(DeviceID * pid, PMODULE_DATA data);
void InterruptRemoteModule(DeviceID * pid); 

typedef union tag_RemoteModuleState
{
	struct
	{
		DeviceID remid;
	};
	BYTE data[SIZE_DATA];
} RemoteModuleState;

typedef union tag_RemoteModuleSavedState
{
	struct
	{
		DeviceID remid;
	};
	BYTE data[sizeof(DeviceID)];
} RemoteModuleSavedState;

#endif