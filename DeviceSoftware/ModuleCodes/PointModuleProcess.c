#include "HardwareProfile.h"
#include "../Headers/ModuleBase.h"
#include "../Headers/MotherBoardModule.h"
#include "../Headers/PointModule.h"
#include "../Headers/PortMapping.h"
#include <stdlib.h>
#include <string.h>

BYTE IsStoring = FALSE;
BYTE SendingPointInd = 0xFF;
PointModuleSavedState cachePointSaved;

void ReadPointModuleSavedState(DeviceID * pid, PointModuleSavedState * psaved);
void WritePointModuleSavedState(DeviceID * pid, PointModuleSavedState * psaved);

void StateToSaved(PointModuleState* pstate, PointModuleSavedState* psaved);
void SavedToState(PointModuleSavedState* psaved, PointModuleState* pstate);

void StateToSaved(PointModuleState* pstate, PointModuleSavedState* psaved)
{
	memcpy((void*)psaved->directions, (void*)pstate->directions, SIZE_POINTMODULESTATE_DIRECTIONS);
}

void SavedToState(PointModuleSavedState* psaved, PointModuleState* pstate)
{
	memcpy((void*)pstate->directions, (void*)psaved->directions, SIZE_POINTMODULESTATE_DIRECTIONS);
}

void ReadPointModuleSavedState(DeviceID * pid, PointModuleSavedState * psaved)
{
	BYTE module = pid->ModuleAddr;
	
	ReadModuleSavedState(module, psaved);
}

void WritePointModuleSavedState(DeviceID * pid, PointModuleSavedState* psaved)
{
	BYTE module = pid->ModuleAddr;
	
	WriteModuleSavedState(module, psaved);
}


HRESULT GetFuncTablePointModule(DeviceID * pid, ModuleFuncTable* table)
{
	table->fncreate = CreatePointModuleState;
	table->fnstore = StorePointModuleState;
	table->fninit = InitPointModule;
	
	//table->fnclose = CloseEmpty;
	table->fninterrupt = InterruptPointModule;
	
	return S_OK;
}

HRESULT CreatePointModuleState(DeviceID * pid, PMODULE_DATA data)
{
	PointModuleState * pstate = (PointModuleState * ) data;
		
	//memset((void*)pstate, 0x00, (size_t)sizeof(PointModuleState));
	SavedToState(&cachePointSaved, pstate);
	
	return S_OK | REPEAT_TERMINATE;	
	
}

HRESULT StorePointModuleState(DeviceID * pid, PMODULE_DATA data)
{
	PointModuleState* pstate = (PointModuleState*)data;
	
	IsStoring = TRUE;
	StateToSaved(pstate, &cachePointSaved);
	WritePointModuleSavedState(pid, &cachePointSaved);
	ReadPointModuleSavedState(pid, &cachePointSaved);
	
	IsStoring = FALSE;
	
	return S_OK;
}

HRESULT InitPointModule(DeviceID * pid)
{
	BYTE module = (pid->ModuleAddr-1) * PORT_PIN_COUNT + 1;
	
	setTris(module, OUTPUT_PIN);
	setTris(module+1, OUTPUT_PIN);
	setTris(module+2, OUTPUT_PIN);
	setTris(module+3, OUTPUT_PIN);
	setTris(module+4, OUTPUT_PIN);
	setTris(module+5, INPUT_PIN);

	ReadPointModuleSavedState(pid, &cachePointSaved);

	return S_OK;
}

void InterruptPointModule(DeviceID * pid)
{
	BYTE module = (pid->ModuleAddr-1) * PORT_PIN_COUNT + 1;;
	
	if(IsStoring)
		return;
		
	if(!getPort(module+5)) // device ack false
		return;

	if(++SendingPointInd >= POINT_COUNT)
		SendingPointInd = 0;
		
	setLat(module+4, 0); // host ack disable
	
	setLat(module, cachePointSaved.directions[SendingPointInd]&1);
	setLat(module+1, (SendingPointInd&1));
	setLat(module+2, (SendingPointInd&2)>>1);
	setLat(module+3, (SendingPointInd&4)>>2);
	
	setLat(module+4, 1); // host ack enable		

//	LATAbits.LATA4 = 0;
//	
//	LATA = 0b00001111 & (((SendingPointInd & 7)<<1) | (cachePointSaved.directions[SendingPointInd]&1));
//	
//	LATAbits.LATA4 = 1;
}