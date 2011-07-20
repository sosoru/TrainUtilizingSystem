#ifndef BLOCK_TRAINCOTROLLER
#define BLOCK_TRAINCOTROLLER

#include "../Headers/ModuleBase.h"

#define TRAIN_CONTROLLER_MODULE_TYPE 0x03

#define DIRECTION_TRAINCONTROLLER_POSITIVE 0
#define DIRECTION_TRAINCONTROLLER_NEGATIVE 1

HRESULT GetFuncTableTrainController(BYTE module, ModuleFuncTable* table);
HRESULT InitTrainController(BYTE module);
HRESULT CreateTrainControllerState(BYTE module, PMODULE_DATA data);
HRESULT StoreTrainControllerState(BYTE module, PMODULE_DATA data);
void InterruptTrainController(BYTE module); 

typedef union tag_TrainControllerState
{
	struct
	{
		unsigned int duty;
		BYTE dutyEnabledBits;
		
		BYTE period;
		BYTE prescale;
		BYTE frequency; //Million
		
		BYTE direction;
	};
	BYTE data[SIZE_DATA];
} TrainControllerState;

#endif