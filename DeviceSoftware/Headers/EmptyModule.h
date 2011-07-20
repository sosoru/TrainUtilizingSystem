#ifndef BLOCK_EMPTY_MODULE
#define BLOCK_EMPTY_MODULE

#include "../Headers/ModuleBase.h"

HRESULT InitEmpty(BYTE module);
HRESULT CreateEmptyState(BYTE module, PMODULE_DATA data);
HRESULT StoreEmptySavedState(BYTE module, PEEPROM_DATA buf);
void InterruptEmpty(BYTE module);


#endif