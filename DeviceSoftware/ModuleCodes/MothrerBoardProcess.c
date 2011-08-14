#include "HardwareProfile.h"
#include "../Headers/MotherBoardModule.h"
#include "../Headers/ModuleFuncDefs.h"
#include <stdlib.h>

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
	memcpy(g_mbState.ModuleType, saved.ModuleType, (size_t)COUNT_MBSTATE_MODULETYPE);
	g_mbState.Timer = 0;
	
}

HRESULT CreateMotherBoardState(DeviceID* pid, PMODULE_DATA data)
{
	MotherBoardState* pmbdata = (MotherBoardState*)data;
	
	g_mbState.Timer = ReadTimer0();
	memcpy(pmbdata, &g_mbState, sizeof(MotherBoardState));
	
	return S_OK | REPEAT_TERMINATE;
}


HRESULT StoreMotherBoardSavedState(DeviceID* pid, PMODULE_DATA buf)
{
	BYTE i;
	MotherBoardState* pcurrent = (MotherBoardState*)buf;
			
	g_mbState.ParentId = pcurrent->ParentId;
	
	for(i=0; i< COUNT_MBSTATE_MODULETYPE; i++)
	{
		BYTE curtype, beftype;
		curtype = READ_MBSTATE_MODULETYPE(*pcurrent, i);
		beftype = READ_MBSTATE_MODULETYPE(g_mbState, i);
		if(beftype != curtype)
		{
			//GET_FUNC_TABLE(i)->fnclose(i);			
			InitializeTable(i, curtype, GET_FUNC_TABLE(i));
			WRITE_MBSTATE_MODULETYPE(g_mbState, i, curtype);
		}
	}
	WriteMotherBoardSavedState(0, &g_mbState);
	
	return S_OK;
}

