#ifndef BLOCK_TRAIN_SENSOR
#define BLOCK_TRAIN_SENSOR

#include "../Headers/ModuleBase.h"

#define TRAIN_SENSOR_MODULE_TYPE 0x01

#define MODE_TRAINSENSOR_MEISURING 0x01
#define MODE_TRAINSENSOR_DETECTING 0x02

extern unsigned long Timer0OverflowCount;
extern unsigned long TimerOccupied;

#define SET_TIMER_OCCUPIED(pos, val) (TimerOccupied = (TimerOccupied & (~(((unsigned long)1) << ((unsigned long)(pos)))) | (((unsigned long)val) << ((unsigned long)(pos)))))
#define GET_TIMER_OCCUPIED(pos) ((TimerOccupied & (((unsigned long)0x00000001) << ((unsigned long)(pos)))) > ((unsigned long)0))

HRESULT GetFuncTableTrainSensor(BYTE module, ModuleFuncTable* table);
HRESULT InitTrainSensor(BYTE module);
HRESULT CreateTrainSensorState(BYTE module, PMODULE_DATA data);
HRESULT StoreTrainSensorSavedState(BYTE module, PEEPROM_DATA buf);
void InterruptTrainSensor(BYTE module);

typedef union tag_TrainSensorState
{
	struct
	{
		BYTE Mode;
		unsigned int Timer;
		unsigned int OverflowedCount;
		BYTE ReferenceVoltageMinus;
		BYTE ReferenceVoltagePlus;
		BYTE VoltageResolution;
		unsigned int ThresholdVoltage;
		unsigned int CurrentVoltage;
		BYTE IsDetected;
	};
	BYTE data[SIZE_DATA];
} TrainSensorState;

typedef union tag_TrainSensorSavedState
{
	struct 
	{
		BYTE Mode;
		unsigned int ThresholdVoltage;
	};
	BYTE data[SIZE_EEPROM_MODULE_ALLOCATED];
} TrainSensorSavedState;


#endif
