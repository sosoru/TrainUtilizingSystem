#ifndef BLOCK_MODULE_BASE
#define BLOCK_MODULE_BASE

#include "../Headers/CommonDefs.h"
#include "../Headers/eeprom.h"
#include "../Headers/PortMapping.h"

typedef union tag_DeviceID
{
	struct
	{
		unsigned GlobalAddr:5;
		unsigned InDeviceAddr:3;
		unsigned RemoteBit:1;
		unsigned ModuleAddr:3;
		unsigned InternalAddr:4;
	};
	struct
	{
		BYTE ParentPart;
		BYTE ModulePart;
	};
} DeviceID;

#define INTERNAL_MODULE_COUNT (1<<(3+1))

#define REPEAT_TERMINATE ((HRESULT)0x04)
#define TERMINATED(Status) ((HRESULT)((Status) & REPEAT_TERMINATE)!=0)

#define SIZE_DATA 28
#define SIZE_EEPROM_MODULE_ALLOCATED 27
#define SIZE_EEPROM_MOTHERBOARD_ALLOCATED 40

#define ADDRESS_EEPROM_STARTS(module) (module * SIZE_EEPROM_MODULE_ALLOCATED + SIZE_EEPROM_MOTHERBOARD_ALLOCATED)

extern BYTE g_usingAdc;

typedef char MODULE_DATA;
typedef MODULE_DATA* PMODULE_DATA;
typedef char EEPROM_DATA;
typedef EEPROM_DATA* PEEPROM_DATA;

typedef HRESULT (*FUNC_CREATE_STATE) (DeviceID*, PMODULE_DATA);
typedef HRESULT (*FUNC_STORE_STATE) (DeviceID*, PMODULE_DATA);
typedef HRESULT (*FUNC_INIT_STATE) (DeviceID*);
typedef void (*FUNC_CLOSE_STATE) (DeviceID*);
typedef void (*FUNC_INTERRUPT_STATE) (DeviceID*);

typedef struct tag_ModuleFuncTable
{
	FUNC_CREATE_STATE fncreate;
	FUNC_STORE_STATE fnstore;
	FUNC_INIT_STATE fninit;
//	FUNC_CLOSE_STATE fnclose;
	FUNC_INTERRUPT_STATE fninterrupt;
	
} ModuleFuncTable;

#include "../Headers/EmptyModule.h"

#endif