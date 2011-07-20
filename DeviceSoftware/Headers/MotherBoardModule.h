#ifndef BLOCK_MOTHERBOARD
#define BLOCK_MOTHERBOARD

#include "../Headers/ModuleBase.h"

#define MOTHER_BOARD_MODULE_TYPE 0x00

#define ReadModuleSavedState(module, pbuf) EEPROMcpy((unsigned char *)(pbuf), (unsigned char)(ADDRESS_EEPROM_STARTS(module)), (unsigned char)(SIZE_EEPROM_MODULE_ALLOCATED));
#define WriteModuleSavedState(module, pbuf) EEPROMset((unsigned char)ADDRESS_EEPROM_STARTS(module), (unsigned char *)(pbuf), (unsigned char)(SIZE_EEPROM_MODULE_ALLOCATED));
#define ReadMotherBoardSavedState(pbuf) EEPROMcpy((unsigned char * )(pbuf), (unsigned char)0, (unsigned char)(SIZE_EEPROM_MOTHERBOARD_ALLOCATED));
#define WriteMotherBoardSavedState(pbuf) EEPROMset((unsigned char)0, (unsigned char * )(pbuf), (unsigned char)(SIZE_EEPROM_MOTHERBOARD_ALLOCATED))

#define COUNT_MBSTATE_MODULETYPE (MODULE_COUNT / 2)
#define READ_MBSTATE_MODULETYPE(state, n) (((state).ModuleType[(n)/2] &(0b1111 << (((n)%2)*4))) >> (((n)%2)*4))
#define WRITE_MBSTATE_MODULETYPE(state, n, value) ((state).ModuleType[(n)/2] = ((state).ModuleType[(n)/2] & (~(0b1111 << (((n)%2)*4)))) | ((value) << (((n)%2)*4)))

HRESULT GetFuncTableMotherBoard(BYTE module, ModuleFuncTable* table);
HRESULT InitMotherBoard(BYTE module);
HRESULT CreateMotherBoardState(BYTE module, PMODULE_DATA data);
HRESULT StoreMotherBoardSavedState(BYTE module, PEEPROM_DATA buf);

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