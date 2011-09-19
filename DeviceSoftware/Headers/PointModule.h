#ifndef BLOCK_POINT_MODULE
#define BLOCK_POINT_MODULE

#include "../Headers/ModuleBase.h"
#include "../Headers/PointInfo.h"

#define POINT_MODULE_MODULE_TYPE 0x02

#define POINT_COUNT 8
#define SIZE_POINTMODULESTATE_DIRECTIONS (POINT_COUNT * sizeof(BYTE))

HRESULT GetFuncTablePointModule(DeviceID* pid, ModuleFuncTable* table);
HRESULT InitPointModule(DeviceID* pid);
HRESULT CreatePointModuleState(DeviceID* pid, PMODULE_DATA data);
HRESULT StorePointModuleState(DeviceID* pid, PMODULE_DATA data);
void InterruptPointModule(DeviceID* pid); 

typedef union tag_PointModuleState
{
	struct
	{
		BYTE directions[POINT_COUNT];
	};
	BYTE data[SIZE_DATA];
} PointModuleState;

typedef union tag_PointModuleSavedState
{
	struct
	{
		BYTE directions[POINT_COUNT];
	};
	BYTE data[SIZE_EEPROM_MODULE_ALLOCATED];
} PointModuleSavedState;


#endif