#ifndef SENSOR_MODULE_INCUDE
#define SENSOR_MODULE_INCUDE

#include <GenericTypeDefs.h>

#define PGMSTR(str) ((rom far char *)str)
#define PGMCSTR(str) ((const rom far char *)str)

typedef signed char HRESULT;
typedef char MODULE_DATA;
typedef MODULE_DATA* PMODULE_DATA;
typedef char EEPROM_DATA;
typedef EEPROM_DATA* PEEPROM_DATA;

#define S_OK ((HRESULT)0)
#define E_FAIL ((HRESULT)-1)

#define SUCCEEDED(Status) ((HRESULT)(Status) >= 0)
#define FAILED(Status) ((HRESULT)(Status)<0)

#define SIZE_DATA 28
#define SIZE_EEPROM_MODULE_ALLOCATED 8
#define SIZE_EEPROM_MOTHERBOARD_ALLOCATED 40
#define ADDRESS_EEPROM_STARTS(module) (module * SIZE_EEPROM_MODULE_ALLOCATED + SIZE_EEPROM_MOTHERBOARD_ALLOCATED)

#define MODULE_COUNT 32

#define MODE_TRAINSENSOR_MEISURING 0x01
#define MODE_TRAINSENSOR_DETECTING 0x02

#define MOTHER_BOARD_MODULE_TYPE 0x00
#define TRAIN_SENSOR_MODULE_TYPE 0x01
#define POINT_MODULE_MODULE_TYPE 0x02
#define TRAIN_CONTROLLER_MODULE_TYPE 0x03
#define UNKNOWN_MODULE_TYPE 0x0F

#define COUNT_ONE_TRANSFAR_PACKETS ((BYTE)(USBGEN_EP_SIZE / sizeof(DevicePacket)))
#define SIZE_ONE_TRANSFAR_PACKETS USBGEN_EP_SIZE
#define COUNT_PACKET_BUFFER (COUNT_ONE_TRANSFAR_PACKETS * 4)

#define BYTESIZE_PACKET_BUFFER (COUNT_PACKET_BUFFER * sizeof(DevicePacket))
#define BUFFER_MAX COUNT_PACKET_BUFFER

unsigned char ReadEEPROM(unsigned char address);
void WriteEEPROM(unsigned char address,unsigned char data);
void EEPROMcpy(unsigned char * dest, unsigned char srcaddr, unsigned char len );
void EEPROMset(unsigned char destaddr, unsigned char* src, unsigned char len);

#define ReadModuleSavedState(module, pbuf) EEPROMcpy((unsigned char *)(pbuf), (unsigned char)(ADDRESS_EEPROM_STARTS(module)), (unsigned char)(SIZE_EEPROM_MODULE_ALLOCATED));
#define WriteModuleSavedState(module, pbuf) EEPROMset((unsigned char)ADDRESS_EEPROM_STARTS(module), (unsigned char *)(pbuf), (unsigned char)(SIZE_EEPROM_MODULE_ALLOCATED));
#define ReadMotherBoardSavedState(pbuf) EEPROMcpy((unsigned char * )(pbuf), (unsigned char)0, (unsigned char)(SIZE_EEPROM_MOTHERBOARD_ALLOCATED));
#define WriteMotherBoardSavedState(pbuf) EEPROMset((unsigned char)0, (unsigned char * )(pbuf), (unsigned char)(SIZE_EEPROM_MOTHERBOARD_ALLOCATED))

typedef struct tag_DeviceIdType
{
	BYTE ParentPart;
	BYTE ModulePart;
} DeviceIdType;

typedef struct tag_DevicePacket
{
	BYTE ReadMark;
	DeviceIdType DeviceID;
	BYTE ModuleType;
	MODULE_DATA Data[SIZE_DATA]; 
} DevicePacket;

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

#define COUNT_MBSTATE_MODULETYPE (MODULE_COUNT / 2)
#define READ_MBSTATE_MODULETYPE(state, n) (((state).ModuleType[(n)/2] &(0b1111 << (((n)%2)*4))) >> (((n)%2)*4))
#define WRITE_MBSTATE_MODULETYPE(state, n, value) ((state).ModuleType[(n)/2] = ((state).ModuleType[(n)/2] & (~(0b1111 << (((n)%2)*4)))) | ((value) << (((n)%2)*4)))

typedef union tag_MotherBoardState
{
	struct
	{
		BYTE ParentId;
		BYTE ModuleType[COUNT_MBSTATE_MODULETYPE];
		unsigned int Timer;
	};
	BYTE data[SIZE_DATA];
} MotherBoardState;


typedef union tag_MotherBoardSavedState
{
	struct
	{
		BYTE ParentId;
		BYTE ModuleType[MODULE_COUNT];
	};
	BYTE data[SIZE_EEPROM_MOTHERBOARD_ALLOCATED];
} MotherBoardSavedState;

typedef union tag_PointModuleState
{
	struct
	{
		BYTE directions[4];
	};
	BYTE data[SIZE_DATA];
} PointModuleState;

typedef union tag_PointModuleSavedState
{
	struct
	{
		BYTE directions[4];
	};
	BYTE data[SIZE_EEPROM_MODULE_ALLOCATED];
} PointModuleSavedState;

#define SIZE_POINTMODULESTATE_DIRECTIONS (4 * sizeof(BYTE))

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

#define DIRECTION_TRAINCONTROLLER_POSITIVE 0
#define DIRECTION_TRAINCONTROLLER_NEGATIVE 1

typedef HRESULT (*FUNC_CREATE_STATE) (BYTE, PMODULE_DATA);
typedef HRESULT (*FUNC_STORE_STATE) (BYTE, PMODULE_DATA);
typedef HRESULT (*FUNC_INIT_STATE) (BYTE);
typedef void (*FUNC_CLOSE_STATE) (BYTE);
typedef void (*FUNC_INTERRUPT_STATE) (BYTE);

typedef struct tag_ModuleFuncTable
{
	FUNC_CREATE_STATE fncreate;
	FUNC_STORE_STATE fnstore;
	FUNC_INIT_STATE fninit;
//	FUNC_CLOSE_STATE fnclose;
	FUNC_INTERRUPT_STATE fninterrupt;
	
} ModuleFuncTable;

extern ModuleFuncTable g_ModuleFuncLower[];
extern ModuleFuncTable g_ModuleFuncHigher[];
#define SIZE_SPLITED_FUNCTABLE (MODULE_COUNT/2)
#define GET_FUNC_TABLE(i) (((i) >= SIZE_SPLITED_FUNCTABLE) ? &g_ModuleFuncHigher[(i)-SIZE_SPLITED_FUNCTABLE] : &g_ModuleFuncLower[(i)])

void SetFuncTable();

HRESULT SendPacketUSB();
HRESULT ReceivingProcessUSB();

HRESULT GetFuncTableMotherBoard(BYTE module, ModuleFuncTable* table);
HRESULT InitMotherBoard(BYTE module);
HRESULT CreateMotherBoardState(BYTE module, PMODULE_DATA data);
HRESULT StoreMotherBoardSavedState(BYTE module, PEEPROM_DATA buf);

HRESULT InitEmpty(BYTE module);
HRESULT CreateEmptyState(BYTE module, PMODULE_DATA data);
HRESULT StoreEmptySavedState(BYTE module, PEEPROM_DATA buf);
void InterruptEmpty(BYTE module);

HRESULT GetFuncTableTrainSensor(BYTE module, ModuleFuncTable* table);
HRESULT InitTrainSensor(BYTE module);
HRESULT CreateTrainSensorState(BYTE module, PMODULE_DATA data);
HRESULT StoreTrainSensorSavedState(BYTE module, PEEPROM_DATA buf);
void InterruptTrainSensor(BYTE module);

HRESULT GetFuncTablePointModule(BYTE module, ModuleFuncTable* table);
HRESULT InitPointModule(BYTE module);
HRESULT CreatePointModuleState(BYTE module, PMODULE_DATA data);
HRESULT StorePointModuleState(BYTE module, PMODULE_DATA data);
void InterruptPointModule(BYTE module); 

HRESULT GetFuncTableTrainController(BYTE module, ModuleFuncTable* table);
HRESULT InitTrainController(BYTE module);
HRESULT CreateTrainControllerState(BYTE module, PMODULE_DATA data);
HRESULT StoreTrainControllerState(BYTE module, PMODULE_DATA data);
void InterruptTrainController(BYTE module); 

#endif