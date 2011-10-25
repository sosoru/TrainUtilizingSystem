#ifndef BLOCK_TRAINCOTROLLER
#define BLOCK_TRAINCOTROLLER

#include "../Headers/ModuleBase.h"

#define TRAIN_CONTROLLER_MODULE_TYPE 0x03

#define DIRECTION_TRAINCONTROLLER_POSITIVE 0
#define DIRECTION_TRAINCONTROLLER_NEGATIVE 1

#define MODE_TRAINCONTROLLER_DUTY 0
#define MODE_TRAINCONTROLLER_FOLLOWING 1
#define MODE_TRAINCONTROLLER_ONDEVICE 2

HRESULT GetFuncTableTrainController(DeviceID* pid, ModuleFuncTable* table);
HRESULT InitTrainController(DeviceID* pid);
HRESULT CreateTrainControllerState(DeviceID* pid, PMODULE_DATA data);
HRESULT StoreTrainControllerState(DeviceID* pid, PMODULE_DATA data);
void InterruptTrainController(DeviceID* pid); 

void TrainControllerTimerInterrupt();

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
		
		BYTE mode;
		unsigned int voltage;
		BYTE voltageEnabledBits;
		unsigned int meisuredvoltage;
		
		BYTE paramp;
		BYTE parami;
		BYTE paramd;
		unsigned int meisuredvoltage2;
		
	};
	BYTE data[SIZE_DATA];
} TrainControllerState;

#endif