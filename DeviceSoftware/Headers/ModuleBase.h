#ifndef BLOCK_MODULE_BASE
#define BLOCK_MODULE_BASE

#include "../Headers/CommonDefs.h"
#include "../Headers/eeprom.h"
#include "../Headers/PortMapping.h"


#define SIZE_DATA 28
#define SIZE_EEPROM_MODULE_ALLOCATED 8
#define SIZE_EEPROM_MOTHERBOARD_ALLOCATED 40

#define ADDRESS_EEPROM_STARTS(module) (module * SIZE_EEPROM_MODULE_ALLOCATED + SIZE_EEPROM_MOTHERBOARD_ALLOCATED)

extern BYTE g_usingAdc;

typedef char MODULE_DATA;
typedef MODULE_DATA* PMODULE_DATA;
typedef char EEPROM_DATA;
typedef EEPROM_DATA* PEEPROM_DATA;

typedef HRESULT (*FUNC_CREATE_STATE) (BYTE, PMODULE_DATA);
typedef HRESULT (*FUNC_STORE_STATE) (BYTE, PMODULE_DATA);
typedef HRESULT (*FUNC_INIT_STATE) (BYTE);
typedef void (*FUNC_CLOSE_STATE) (BYTE);
typedef void (*FUNC_INTERRUPT_STATE) (BYTE);

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