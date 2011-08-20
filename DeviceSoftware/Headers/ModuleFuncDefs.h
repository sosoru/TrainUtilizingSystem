#if !defined BLOCK_MODULEFUNCDEFS
#define BLOCK_MODULEFUNCDEFS

#include "../Headers/ModuleBase.h"

//extern ModuleFuncTable g_ModuleFuncLower[];
//extern ModuleFuncTable g_ModuleFuncHigher[];
//#define SIZE_SPLITED_FUNCTABLE (MODULE_COUNT/2)
//#define GET_FUNC_TABLE(i) (((i) >= SIZE_SPLITED_FUNCTABLE) ? &g_ModuleFuncHigher[(i)-SIZE_SPLITED_FUNCTABLE] : &g_ModuleFuncLower[(i)])

extern ModuleFuncTable g_ModuleFunc[];
#define GET_FUNC_TABLE(i) (&g_ModuleFunc[(i)])

void InitializeTable(DeviceID* pid, BYTE moduletype, ModuleFuncTable* table);
void SetFuncTable(void);

#endif