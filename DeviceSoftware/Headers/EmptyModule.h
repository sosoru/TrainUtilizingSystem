#ifndef BLOCK_EMPTY_MODULE
#define BLOCK_EMPTY_MODULE

#include "../Headers/ModuleBase.h"

HRESULT InitEmpty(DeviceID* pid);
HRESULT CreateEmptyState(DeviceID* pid, PMODULE_DATA data);
HRESULT StoreEmptySavedState(DeviceID* pid, PEEPROM_DATA buf);
void InterruptEmpty(DeviceID* pid);


#endif