#include "../Headers/ModuleFuncDefs.h"
#include "../Headers/MonoModules.h"

void InitializeTable(DeviceID* pid, BYTE moduletype, ModuleFuncTable* table);

void SetFuncTable()
{
	MotherBoardSavedState state;
	DeviceID id;
	BYTE i;
	
	ReadMotherBoardSavedState(&state);
	if(READ_MBSTATE_MODULETYPE(state, 0) != MOTHER_BOARD_MODULE_TYPE)
	{
		memset(&state, 0xFF, sizeof(MotherBoardSavedState));
		WRITE_MBSTATE_MODULETYPE(state,0, MOTHER_BOARD_MODULE_TYPE);
			
		WriteMotherBoardSavedState(&state);
	}
	
	id.ParentPart = state.ParentId;
	for(i = 0; i < MODULE_COUNT; i++)
	{
		id.ModulePart = i;
		InitializeTable(&id, READ_MBSTATE_MODULETYPE(state, i), GET_FUNC_TABLE(i));
	}
}

void InitializeTable(DeviceID* pid, BYTE moduletype, ModuleFuncTable* ptable)
{
	BYTE empty = 0;
	HRESULT res = 0;
	switch(moduletype)
	{
		case TRAIN_SENSOR_MODULE_TYPE:
			if(FAILED(GetFuncTableTrainSensor(pid, ptable)))
			{
				empty = 1;
			}
		break;
		case MOTHER_BOARD_MODULE_TYPE:
			if(FAILED(GetFuncTableMotherBoard(pid, ptable)))
			{
				empty = 1;
			}
		break;
		case POINT_MODULE_MODULE_TYPE:
			if(FAILED(GetFuncTablePointModule(pid, ptable)))
			{
				empty = 1;
			}
		break;
		case TRAIN_CONTROLLER_MODULE_TYPE:
			if(FAILED(GetFuncTableTrainController(pid, ptable)))
			{
				empty = 1;
			}
		break;
		case REMOTE_MODULE_MODULE_TYPE:
			if(FAILED(GetFuncTableRemoteModule(pid, ptable)))
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
