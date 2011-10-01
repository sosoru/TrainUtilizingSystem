#include "HardwareProfile.h"
#include "../Headers/ModuleFuncDefs.h"
#include "../Headers/MotherBoardModule.h"
#include <stdlib.h>
#include <string.h>
#include <timers.h>

MotherBoardState g_mbState;
BYTE g_usingAdc = FALSE;


HRESULT GetFuncTableMotherBoard(DeviceID* pid, ModuleFuncTable* table)
{
	table->fncreate = CreateMotherBoardState;
	table->fnstore = StoreMotherBoardSavedState;
	table->fninit = InitMotherBoard;
	//table->fnclose = CloseEmpty;
	table->fninterrupt = InterruptEmpty;
	
	return S_OK;
}

HRESULT InitMotherBoard(DeviceID* pid)
{
	MotherBoardSavedState saved;
	
	ReadMotherBoardSavedState(&saved);
	
	g_mbState.ParentId = saved.ParentId;
	memcpy((void*)g_mbState.ModuleType, (void*)saved.ModuleType, (size_t)COUNT_MBSTATE_MODULETYPE);
	g_mbState.Timer = 0;
	
}

HRESULT CreateMotherBoardState(DeviceID* pid, PMODULE_DATA data)
{
	MotherBoardState* pmbdata = (MotherBoardState*)data;
	
	g_mbState.Timer = ReadTimer0();
	memcpy((void*)pmbdata, (void*)&g_mbState, (size_t)sizeof(MotherBoardState));
	
	return S_OK | REPEAT_TERMINATE;
}


HRESULT StoreMotherBoardSavedState(DeviceID* pid, PMODULE_DATA buf)
{
	BYTE i;
	DeviceID mid;
	MotherBoardState* pcurrent = (MotherBoardState*)buf;
	MotherBoardSavedState saved;
			
	g_mbState.ParentId = pcurrent->ParentId;
	memcpy((void*)&mid, (void*)pid, sizeof(DeviceID));
	mid.ParentPart = g_mbState.ParentId;
	
	for(i=0; i< COUNT_MBSTATE_MODULETYPE; ++i)
	{
		BYTE curtype, beftype;
		curtype = READ_MBSTATE_MODULETYPE(*pcurrent, i);
		beftype = READ_MBSTATE_MODULETYPE(g_mbState, i);
		if(beftype != curtype)
		{
			BYTE dummy[SIZE_EEPROM_MOTHERBOARD_ALLOCATED];
			
			//GET_FUNC_TABLE(i)->fnclose(i);			
			mid.ModuleAddr = i;
			InitializeTable(&mid, curtype, GET_FUNC_TABLE(i));
			WRITE_MBSTATE_MODULETYPE(g_mbState, i, curtype);
			
			//clear eeprom
			memset((void*)dummy, 0xff, sizeof(dummy));
			WriteModuleSavedState(i, dummy);			
			
			GET_FUNC_TABLE(i)->fninit(&mid);
		}
	}
	
	saved.ParentId = pcurrent->ParentId;
	memcpy((void*)saved.ModuleType, (void*)pcurrent->ModuleType, COUNT_MBSTATE_MODULETYPE);
	WriteMotherBoardSavedState(&saved);
	
	memcpy((void*)&g_mbState, (void*)pcurrent, sizeof(g_mbState));
	
	return S_OK;
}

