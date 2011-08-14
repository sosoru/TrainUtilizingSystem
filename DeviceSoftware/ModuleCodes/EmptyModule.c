#include "../Headers/EmptyModule.h"

HRESULT CreateEmptyState(DeviceID* pid, PMODULE_DATA data)
{
	// do nothing
	return E_FAIL;
}

HRESULT StoreEmptySavedState(DeviceID* pid, PEEPROM_DATA buf)
{
	// do nothing
	return E_FAIL;
}

HRESULT InitEmpty(DeviceID* pid)
{
	//do nothing
	return E_FAIL;
}

void CloseEmpty(DeviceID* pid)
{
	//do nothing
}

void InterruptEmpty(DeviceID* pid)
{
	//do nothing
}

