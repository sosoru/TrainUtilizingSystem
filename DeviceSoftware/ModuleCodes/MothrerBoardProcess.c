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
			
	g_mbState.ParentId = pcurrent->ParentId;
	memcpy((void*)&mid, (void*)pid, sizeof(DeviceID));
	mid.ParentPart = g_mbState.ParentId;
	
	for(i=0; i< COUNT_MBSTATE_MODULETYPE; i++)
	{
		BYTE curtype, beftype;
		curtype = READ_MBSTATE_MODULETYPE(*pcurrent, i);
		beftype = READ_MBSTATE_MODULETYPE(g_mbState, i);
		if(beftype != curtype)
		{
			//GET_FUNC_TABLE(i)->fnclose(i);			
			mid.ModulePart = i;
			InitializeTable(&mid, curtype, GET_FUNC_TABLE(i));
			WRITE_MBSTATE_MODULETYPE(g_mbState, i, curtype);
		}
	}
	WriteMotherBoardSavedState(0, &g_mbState);
	
	return S_OK;
}

