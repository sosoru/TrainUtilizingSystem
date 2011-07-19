#include "MonoDevice.h"

void InitializeTable(BYTE module, BYTE moduletype, ModuleFuncTable* table);

MotherBoardState g_mbState;

void SetFuncTable()
{
	MotherBoardSavedState state;
	BYTE i;
	
	ReadMotherBoardSavedState(&state);
	if(READ_MBSTATE_MODULETYPE(state, 0) != MOTHER_BOARD_MODULE_TYPE)
	{
		memset(&state, 0xFF, sizeof(MotherBoardSavedState));
		WRITE_MBSTATE_MODULETYPE(state,0, MOTHER_BOARD_MODULE_TYPE);
			
		WriteMotherBoardSavedState(&state);
	}
	
	for(i = 0; i < MODULE_COUNT; i++)
	{
		InitializeTable(i, READ_MBSTATE_MODULETYPE(state, i), GET_FUNC_TABLE(i));
	}
}

void InitializeTable(BYTE module, BYTE moduletype, ModuleFuncTable* ptable)
{
	BYTE empty = 0;
	HRESULT res = 0;
	switch(moduletype)
	{
		case TRAIN_SENSOR_MODULE_TYPE:
			if(FAILED(GetFuncTableTrainSensor(module, ptable)))
			{
				empty = 1;
			}
		break;
		case MOTHER_BOARD_MODULE_TYPE:
			if(FAILED(GetFuncTableMotherBoard(module, ptable)))
			{
				empty = 1;
			}
		break;
		case POINT_MODULE_MODULE_TYPE:
			if(FAILED(GetFuncTablePointModule(module, ptable)))
			{
				empty = 1;
			}
		break;
		case TRAIN_CONTROLLER_MODULE_TYPE:
			if(FAILED(GetFuncTableTrainController(module, ptable)))
			{
				empty = 1;
			}
		break;
		default:
			empty = 1;
		break;
	}
	
	if(empty)
	{
		ptable->fncreate = CreateEmptyState;
		ptable->fnstore = StoreEmptySavedState;
		ptable->fninit = InitEmpty;
		//ptable->fnclose = CloseEmpty;
		ptable->fninterrupt = InterruptEmpty;
	}
}

HRESULT GetFuncTableMotherBoard(BYTE module, ModuleFuncTable* table)
{
	table->fncreate = CreateMotherBoardState;
	table->fnstore = StoreMotherBoardSavedState;
	table->fninit = InitMotherBoard;
	//table->fnclose = CloseEmpty;
	table->fninterrupt = InterruptEmpty;
	
	return S_OK;
}

HRESULT InitMotherBoard(BYTE module)
{
	ReadMotherBoardSavedState((MotherBoardSavedState*) &g_mbState);
}

HRESULT CreateMotherBoardState(BYTE module, PMODULE_DATA data)
{
	MotherBoardState* pmbdata = (MotherBoardState*)data;
	
	g_mbState.Timer = ReadTimer0();
	memcpy(pmbdata, &g_mbState, sizeof(MotherBoardState));
	
	return S_OK;
}

HRESULT CreateEmptyState(BYTE module, PMODULE_DATA data)
{
	// do nothing
	return E_FAIL;
}

HRESULT StoreMotherBoardSavedState(BYTE module, PMODULE_DATA buf)
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

HRESULT StoreEmptySavedState(BYTE module, PEEPROM_DATA buf)
{
	// do nothing
	return E_FAIL;
}

HRESULT InitEmpty(BYTE module)
{
	//do nothing
	return E_FAIL;
}

void CloseEmpty(BYTE module)
{
	//do nothing
}

void InterruptEmpty(BYTE module)
{
	//do nothing
}
