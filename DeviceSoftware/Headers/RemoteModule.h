#if !defined BLOCK_REMOTE_MODULE
#define BLOCK_REMOTE_MODULE

#include "../Headers/ModuleBase.h"
#include "../Headers/TrainController.h"

#define REMOTE_MODULE_MODULE_TYPE 0x04

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
	BYTE data[SIZE_EEPROM_MODULE_ALLOCATED];
} RemoteModuleSavedState;

#endif