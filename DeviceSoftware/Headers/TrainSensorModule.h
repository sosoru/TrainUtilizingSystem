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

HRESULT GetFuncTableTrainSensor(DeviceID* pid, ModuleFuncTable* table);
HRESULT InitTrainSensor(DeviceID* pid);
HRESULT CreateTrainSensorState(DeviceID* pid, PMODULE_DATA data);
HRESULT StoreTrainSensorSavedState(DeviceID* pid, PEEPROM_DATA buf);
void InterruptTrainSensor(DeviceID* pid);

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
