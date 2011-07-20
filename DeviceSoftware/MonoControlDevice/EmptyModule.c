#include "../Headers/EmptyModule.h"

HRESULT CreateEmptyState(BYTE module, PMODULE_DATA data)
{
	// do nothing
	return E_FAIL;
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

