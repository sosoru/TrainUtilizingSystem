#include "../Headers/MotherBoardModule.h"
#include "../Headers/PointModule.h"


#define IS_POINTMODULE_REPRESENT(module) ((module) % 4 == 1)

void ReadPointModuleSavedState(DeviceID * pid, PointModuleState * pstate);
void WritePointModuleSavedState(DeviceID * pid, PointModuleState* pstate);

BYTE PointChangingCurrent[MODULE_COUNT / 4];

HRESULT GetFuncTablePointModule(DeviceID * pid, ModuleFuncTable* table)
{
	if(!IS_POINTMODULE_REPRESENT(pid->ModulePart))
		return E_FAIL;

	table->fncreate = CreatePointModuleState;
	table->fnstore = StorePointModuleState;
	table->fninit = InitPointModule;
	
	//table->fnclose = CloseEmpty;
	table->fninterrupt = InterruptPointModule;
	
	return S_OK;
}

HRESULT CreatePointModuleState(DeviceID * pid, PMODULE_DATA data)
{
	PointModuleSavedState saved;
	PointModuleState * pstate = (PointModuleState * ) data;
		
	memset(pstate, 0x00, sizeof(*pstate));
	ReadPointModuleSavedState(pid->ModulePart, pstate);
	
	return S_OK;	
	
}

HRESULT StorePointModuleState(DeviceID * pid, PMODULE_DATA data)
{
	PointModuleState* pstate = (PointModuleState*)data;
				
	WritePointModuleSavedState(pid->ModulePart, pstate);
	
	return S_OK;
}

void ReadPointModuleSavedState(DeviceID * pid, PointModuleState * pstate)
{
	PointModuleSavedState saved;
	BYTE module = pid->ModulePart;
	
	ReadModuleSavedState(module, &saved);
	memcpy(pstate->directions, saved.directions, SIZE_POINTMODULESTATE_DIRECTIONS);
}

void WritePointModuleSavedState(DeviceID * pid, PointModuleState* pstate)
{
	PointModuleSavedState saved;
	BYTE module = pid->ModulePart;
	
	memset(&saved, 0x00, (size_t)sizeof(PointModuleSavedState));
	memcpy(saved.directions, pstate->directions, SIZE_POINTMODULESTATE_DIRECTIONS);
	WriteModuleSavedState(module, &saved);
}

HRESULT InitPointModule(DeviceID * pid)
{
	BYTE module = pid->ModulePart;
	
	setTris(module, OUTPUT_PIN);
	setTris(module+1, OUTPUT_PIN);
	setTris(module+2, OUTPUT_PIN);
	setTris(module+3, OUTPUT_PIN);

	return S_OK;
}

void InterruptPointModule(DeviceID * pid)
{
	BYTE i, pack, arindex, module = pid->ModulePart;
	PointModuleSavedState saved;
	
	ReadPointModuleSavedState(module, &saved);
	
	//load and increment it
	arindex = module / 4;
	pack = PointChangingCurrent[arindex];
	PointChangingCurrent[arindex] +=2;
	
	if(saved.directions[0] & (1 << pack) != 0)
		++pack;
		
	//currently available port count == 4
	//back changing no
	if(pack > 8)
		PointChangingCurrent[arindex] = 0;
		
	//valid lower 4 bits
	setPort(module, pack & 0b0001);
	setPort(module+1, pack & 0b0010);
	setPort(module+2, pack & 0b0100);
	setPort(module+3, pack & 0b1000);

}