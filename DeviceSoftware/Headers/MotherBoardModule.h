#ifndef BLOCK_MOTHERBOARD
#define BLOCK_MOTHERBOARD

#include "HardwareProfile.h"
#include "../Headers/ModuleBase.h"

#ifdef VERSION_REV1

	#define MODULE_COUNT 6

#elif VERSION_REV2

	#define MODULE_COUNT 4

#else
	
	#define MODULE_COUNT 8
	
#endif

#define MOTHER_BOARD_MODULE_TYPE 0x00

#define ReadModuleSavedState(module, pbuf) EEPROMcpy((unsigned char *)(pbuf), (unsigned char)(ADDRESS_EEPROM_STARTS(module)), (unsigned char)(SIZE_EEPROM_MODULE_ALLOCATED))
#define WriteModuleSavedState(module, pbuf) EEPROMset((unsigned char)ADDRESS_EEPROM_STARTS(module), (unsigned char *)(pbuf), (unsigned char)(SIZE_EEPROM_MODULE_ALLOCATED))
#define ReadMotherBoardSavedState(pbuf) EEPROMcpy((unsigned char * )(pbuf), (unsigned char)0, (unsigned char)(SIZE_EEPROM_MOTHERBOARD_ALLOCATED))
#define WriteMotherBoardSavedState(pbuf) EEPROMset((unsigned char)0, (unsigned char * )(pbuf), (unsigned char)(SIZE_EEPROM_MOTHERBOARD_ALLOCATED))

#define COUNT_MBSTATE_MODULETYPE (MODULE_COUNT / 2)
#define READ_MBSTATE_MODULETYPE(state, n) (((state).ModuleType[(n)/2] &(0b1111 << (((n)%2)*4))) >> (((n)%2)*4))
#define WRITE_MBSTATE_MODULETYPE(state, n, value) ((state).ModuleType[(n)/2] = ((state).ModuleType[(n)/2] & (~(0b1111 << (((n)%2)*4)))) | ((value) << (((n)%2)*4)))

#define IS_THIS_BOARD(pid) (pid->ParentId == g_mbState.ParentId)
#define IS_BOARDING_MODULE(state, pid, type) (READ_MBSTATE_MODULETYPE((state), (pid)->ModulePart) == (type))

HRESULT GetFuncTableMotherBoard(DeviceID * pid, ModuleFuncTable* table);
HRESULT InitMotherBoard(DeviceID* pid);
HRESULT CreateMotherBoardState(DeviceID* pid, PMODULE_DATA data);
HRESULT StoreMotherBoardSavedState(DeviceID* pid, PEEPROM_DATA buf);

typedef union tag_MotherBoardState
{
	struct
	{
		BYTE ParentId;
		BYTE ModuleType[COUNT_MBSTATE_MODULETYPE];
		unsigned int Timer;
	};
	BYTE data[SIZE_DATA];
} MotherBoardState;

typedef union tag_MotherBoardSavedState
{
	struct
	{
		BYTE ParentId;
		BYTE ModuleType[MODULE_COUNT];
	};
	BYTE data[SIZE_EEPROM_MOTHERBOARD_ALLOCATED];
} MotherBoardSavedState;

extern MotherBoardState g_mbState;

#endif