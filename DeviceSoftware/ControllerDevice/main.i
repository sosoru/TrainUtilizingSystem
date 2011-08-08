#line 1 "main.c"
#line 1 "main.c"
#line 1 "./ControllerDevice.h"
#line 2 "./ControllerDevice.h"
#line 3 "./ControllerDevice.h"

#line 5 "./ControllerDevice.h"

#line 1 "./ControllerModule.h"
#line 2 "./ControllerModule.h"
#line 3 "./ControllerModule.h"

#line 1 "./ControllerDevice.h"
#line 2 "./ControllerDevice.h"
#line 4 "./ControllerModule.h"
#line 5 "./ControllerModule.h"

#line 1 "./../Headers/ModuleBase.h"

#line 3 "./../Headers/ModuleBase.h"

#line 1 "./../Headers/CommonDefs.h"

#line 3 "./../Headers/CommonDefs.h"

#line 1 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"

#line 45 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
 


#line 49 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"

 
#line 52 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
#line 54 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
#line 55 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
#line 56 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"

#line 58 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
#line 59 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
#line 60 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"

 
#line 1 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stddef.h"
 

#line 4 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stddef.h"

typedef unsigned char wchar_t;


#line 10 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stddef.h"
 
typedef signed short int ptrdiff_t;
typedef signed short int ptrdiffram_t;
typedef signed short long int ptrdiffrom_t;


#line 20 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stddef.h"
 
typedef unsigned short int size_t;
typedef unsigned short int sizeram_t;
typedef unsigned short long int sizerom_t;


#line 34 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stddef.h"
 
#line 36 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stddef.h"


#line 41 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stddef.h"
 
#line 43 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stddef.h"

#line 45 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stddef.h"
#line 62 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
 

typedef enum _BOOL { FALSE = 0, TRUE } BOOL;     
typedef enum _BIT { CLEAR = 0, SET } BIT;

#line 68 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
#line 69 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
#line 70 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"

 
typedef signed int          INT;
typedef signed char         INT8;
typedef signed short int    INT16;
typedef signed long int     INT32;

 
#line 79 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
#line 81 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"

 
typedef unsigned int        UINT;
typedef unsigned char       UINT8;
typedef unsigned short int  UINT16;
 
#line 88 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
typedef unsigned short long UINT24;
#line 90 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
typedef unsigned long int   UINT32;      
 
#line 93 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
#line 95 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"

typedef union
{
    UINT8 Val;
    struct
    {
         UINT8 b0:1;
         UINT8 b1:1;
         UINT8 b2:1;
         UINT8 b3:1;
         UINT8 b4:1;
         UINT8 b5:1;
         UINT8 b6:1;
         UINT8 b7:1;
    } bits;
} UINT8_VAL, UINT8_BITS;

typedef union 
{
    UINT16 Val;
    UINT8 v[2] ;
    struct 
    {
        UINT8 LB;
        UINT8 HB;
    } byte;
    struct 
    {
         UINT8 b0:1;
         UINT8 b1:1;
         UINT8 b2:1;
         UINT8 b3:1;
         UINT8 b4:1;
         UINT8 b5:1;
         UINT8 b6:1;
         UINT8 b7:1;
         UINT8 b8:1;
         UINT8 b9:1;
         UINT8 b10:1;
         UINT8 b11:1;
         UINT8 b12:1;
         UINT8 b13:1;
         UINT8 b14:1;
         UINT8 b15:1;
    } bits;
} UINT16_VAL, UINT16_BITS;

 
#line 144 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
typedef union
{
    UINT24 Val;
    UINT8 v[3] ;
    struct 
    {
        UINT8 LB;
        UINT8 HB;
        UINT8 UB;
    } byte;
    struct 
    {
         UINT8 b0:1;
         UINT8 b1:1;
         UINT8 b2:1;
         UINT8 b3:1;
         UINT8 b4:1;
         UINT8 b5:1;
         UINT8 b6:1;
         UINT8 b7:1;
         UINT8 b8:1;
         UINT8 b9:1;
         UINT8 b10:1;
         UINT8 b11:1;
         UINT8 b12:1;
         UINT8 b13:1;
         UINT8 b14:1;
         UINT8 b15:1;
         UINT8 b16:1;
         UINT8 b17:1;
         UINT8 b18:1;
         UINT8 b19:1;
         UINT8 b20:1;
         UINT8 b21:1;
         UINT8 b22:1;
         UINT8 b23:1;
    } bits;
} UINT24_VAL, UINT24_BITS;
#line 183 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"

typedef union
{
    UINT32 Val;
    UINT16 w[2] ;
    UINT8  v[4] ;
    struct 
    {
        UINT16 LW;
        UINT16 HW;
    } word;
    struct 
    {
        UINT8 LB;
        UINT8 HB;
        UINT8 UB;
        UINT8 MB;
    } byte;
    struct 
    {
        UINT16_VAL low;
        UINT16_VAL high;
    }wordUnion;
    struct 
    {
         UINT8 b0:1;
         UINT8 b1:1;
         UINT8 b2:1;
         UINT8 b3:1;
         UINT8 b4:1;
         UINT8 b5:1;
         UINT8 b6:1;
         UINT8 b7:1;
         UINT8 b8:1;
         UINT8 b9:1;
         UINT8 b10:1;
         UINT8 b11:1;
         UINT8 b12:1;
         UINT8 b13:1;
         UINT8 b14:1;
         UINT8 b15:1;
         UINT8 b16:1;
         UINT8 b17:1;
         UINT8 b18:1;
         UINT8 b19:1;
         UINT8 b20:1;
         UINT8 b21:1;
         UINT8 b22:1;
         UINT8 b23:1;
         UINT8 b24:1;
         UINT8 b25:1;
         UINT8 b26:1;
         UINT8 b27:1;
         UINT8 b28:1;
         UINT8 b29:1;
         UINT8 b30:1;
         UINT8 b31:1;
    } bits;
} UINT32_VAL;

 
#line 245 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
#line 332 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"

 

 
typedef void                    VOID;

typedef char                    CHAR8;
typedef unsigned char           UCHAR8;

typedef unsigned char           BYTE;                            
typedef unsigned short int      WORD;                            
typedef unsigned long           DWORD;                           
 

typedef unsigned long long      QWORD;                           
typedef signed char             CHAR;                            
typedef signed short int        SHORT;                           
typedef signed long             LONG;                            
 

typedef signed long long        LONGLONG;                        
typedef union
{
    BYTE Val;
    struct 
    {
         BYTE b0:1;
         BYTE b1:1;
         BYTE b2:1;
         BYTE b3:1;
         BYTE b4:1;
         BYTE b5:1;
         BYTE b6:1;
         BYTE b7:1;
    } bits;
} BYTE_VAL, BYTE_BITS;

typedef union
{
    WORD Val;
    BYTE v[2] ;
    struct 
    {
        BYTE LB;
        BYTE HB;
    } byte;
    struct 
    {
         BYTE b0:1;
         BYTE b1:1;
         BYTE b2:1;
         BYTE b3:1;
         BYTE b4:1;
         BYTE b5:1;
         BYTE b6:1;
         BYTE b7:1;
         BYTE b8:1;
         BYTE b9:1;
         BYTE b10:1;
         BYTE b11:1;
         BYTE b12:1;
         BYTE b13:1;
         BYTE b14:1;
         BYTE b15:1;
    } bits;
} WORD_VAL, WORD_BITS;

typedef union
{
    DWORD Val;
    WORD w[2] ;
    BYTE v[4] ;
    struct 
    {
        WORD LW;
        WORD HW;
    } word;
    struct 
    {
        BYTE LB;
        BYTE HB;
        BYTE UB;
        BYTE MB;
    } byte;
    struct 
    {
        WORD_VAL low;
        WORD_VAL high;
    }wordUnion;
    struct 
    {
         BYTE b0:1;
         BYTE b1:1;
         BYTE b2:1;
         BYTE b3:1;
         BYTE b4:1;
         BYTE b5:1;
         BYTE b6:1;
         BYTE b7:1;
         BYTE b8:1;
         BYTE b9:1;
         BYTE b10:1;
         BYTE b11:1;
         BYTE b12:1;
         BYTE b13:1;
         BYTE b14:1;
         BYTE b15:1;
         BYTE b16:1;
         BYTE b17:1;
         BYTE b18:1;
         BYTE b19:1;
         BYTE b20:1;
         BYTE b21:1;
         BYTE b22:1;
         BYTE b23:1;
         BYTE b24:1;
         BYTE b25:1;
         BYTE b26:1;
         BYTE b27:1;
         BYTE b28:1;
         BYTE b29:1;
         BYTE b30:1;
         BYTE b31:1;
    } bits;
} DWORD_VAL;

 
typedef union
{
    QWORD Val;
    DWORD d[2] ;
    WORD w[4] ;
    BYTE v[8] ;
    struct 
    {
        DWORD LD;
        DWORD HD;
    } dword;
    struct 
    {
        WORD LW;
        WORD HW;
        WORD UW;
        WORD MW;
    } word;
    struct 
    {
         BYTE b0:1;
         BYTE b1:1;
         BYTE b2:1;
         BYTE b3:1;
         BYTE b4:1;
         BYTE b5:1;
         BYTE b6:1;
         BYTE b7:1;
         BYTE b8:1;
         BYTE b9:1;
         BYTE b10:1;
         BYTE b11:1;
         BYTE b12:1;
         BYTE b13:1;
         BYTE b14:1;
         BYTE b15:1;
         BYTE b16:1;
         BYTE b17:1;
         BYTE b18:1;
         BYTE b19:1;
         BYTE b20:1;
         BYTE b21:1;
         BYTE b22:1;
         BYTE b23:1;
         BYTE b24:1;
         BYTE b25:1;
         BYTE b26:1;
         BYTE b27:1;
         BYTE b28:1;
         BYTE b29:1;
         BYTE b30:1;
         BYTE b31:1;
         BYTE b32:1;
         BYTE b33:1;
         BYTE b34:1;
         BYTE b35:1;
         BYTE b36:1;
         BYTE b37:1;
         BYTE b38:1;
         BYTE b39:1;
         BYTE b40:1;
         BYTE b41:1;
         BYTE b42:1;
         BYTE b43:1;
         BYTE b44:1;
         BYTE b45:1;
         BYTE b46:1;
         BYTE b47:1;
         BYTE b48:1;
         BYTE b49:1;
         BYTE b50:1;
         BYTE b51:1;
         BYTE b52:1;
         BYTE b53:1;
         BYTE b54:1;
         BYTE b55:1;
         BYTE b56:1;
         BYTE b57:1;
         BYTE b58:1;
         BYTE b59:1;
         BYTE b60:1;
         BYTE b61:1;
         BYTE b62:1;
         BYTE b63:1;
    } bits;
} QWORD_VAL;

#line 547 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"

#line 549 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/GenericTypeDefs.h"
#line 4 "./../Headers/CommonDefs.h"


#line 7 "./../Headers/CommonDefs.h"
#line 8 "./../Headers/CommonDefs.h"

typedef signed char HRESULT;

#line 12 "./../Headers/CommonDefs.h"
#line 13 "./../Headers/CommonDefs.h"

#line 15 "./../Headers/CommonDefs.h"
#line 16 "./../Headers/CommonDefs.h"

#line 18 "./../Headers/CommonDefs.h"
#line 19 "./../Headers/CommonDefs.h"

#line 21 "./../Headers/CommonDefs.h"
#line 22 "./../Headers/CommonDefs.h"

#line 4 "./../Headers/ModuleBase.h"
#line 5 "./../Headers/ModuleBase.h"
#line 1 "./../Headers/eeprom.h"

#line 3 "./../Headers/eeprom.h"

unsigned char ReadEEPROM(unsigned char address);
void WriteEEPROM(unsigned char address,unsigned char data);
void EEPROMcpy(unsigned char * dest, unsigned char srcaddr, unsigned char len );
void EEPROMset(unsigned char destaddr, unsigned char* src, unsigned char len);


#line 5 "./../Headers/ModuleBase.h"
#line 6 "./../Headers/ModuleBase.h"
#line 1 "./../Headers/PortMapping.h"

#line 3 "./../Headers/PortMapping.h"

#line 1 "./../Headers/CommonDefs.h"
#line 4 "./../Headers/PortMapping.h"
#line 5 "./../Headers/PortMapping.h"

#line 9 "./../Headers/PortMapping.h"

extern near BYTE* ModuleTris[];
extern near BYTE* ModulePort[];
extern near BYTE* ModuleLat[];
extern BYTE ModuleShift[];

#line 16 "./../Headers/PortMapping.h"
#line 17 "./../Headers/PortMapping.h"
#line 18 "./../Headers/PortMapping.h"

#line 20 "./../Headers/PortMapping.h"
#line 21 "./../Headers/PortMapping.h"
#line 22 "./../Headers/PortMapping.h"
#line 23 "./../Headers/PortMapping.h"
#line 24 "./../Headers/PortMapping.h"
#line 25 "./../Headers/PortMapping.h"


#line 6 "./../Headers/ModuleBase.h"
#line 7 "./../Headers/ModuleBase.h"

#line 9 "./../Headers/ModuleBase.h"

typedef struct tag_DeviceID
{
	BYTE ParentPart;
	BYTE ModulePart;
} DeviceID;

#line 17 "./../Headers/ModuleBase.h"
#line 18 "./../Headers/ModuleBase.h"
#line 19 "./../Headers/ModuleBase.h"

#line 21 "./../Headers/ModuleBase.h"

extern BYTE g_usingAdc;

typedef char MODULE_DATA;
typedef MODULE_DATA* PMODULE_DATA;
typedef char EEPROM_DATA;
typedef EEPROM_DATA* PEEPROM_DATA;

typedef HRESULT (*FUNC_CREATE_STATE) (DeviceID*, PMODULE_DATA);
typedef HRESULT (*FUNC_STORE_STATE) (DeviceID*, PMODULE_DATA);
typedef HRESULT (*FUNC_INIT_STATE) (DeviceID*);
typedef void (*FUNC_CLOSE_STATE) (DeviceID*);
typedef void (*FUNC_INTERRUPT_STATE) (DeviceID*);

typedef struct tag_ModuleFuncTable
{
	FUNC_CREATE_STATE fncreate;
	FUNC_STORE_STATE fnstore;
	FUNC_INIT_STATE fninit;

	FUNC_INTERRUPT_STATE fninterrupt;
	
} ModuleFuncTable;

#line 1 "./../Headers/EmptyModule.h"

#line 3 "./../Headers/EmptyModule.h"

#line 1 "./../Headers/ModuleBase.h"
#line 4 "./../Headers/EmptyModule.h"
#line 5 "./../Headers/EmptyModule.h"

HRESULT InitEmpty(DeviceID* pid);
HRESULT CreateEmptyState(DeviceID* pid, PMODULE_DATA data);
HRESULT StoreEmptySavedState(DeviceID* pid, PEEPROM_DATA buf);
void InterruptEmpty(DeviceID* pid);


#line 45 "./../Headers/ModuleBase.h"
#line 46 "./../Headers/ModuleBase.h"

#line 6 "./ControllerModule.h"
#line 7 "./ControllerModule.h"
#line 1 "./../Headers/MotherBoardModule.h"

#line 3 "./../Headers/MotherBoardModule.h"


#line 1 "./../Headers/ModuleBase.h"
#line 5 "./../Headers/MotherBoardModule.h"
#line 6 "./../Headers/MotherBoardModule.h"

#line 12 "./../Headers/MotherBoardModule.h"
#line 16 "./../Headers/MotherBoardModule.h"
	
#line 18 "./../Headers/MotherBoardModule.h"
	
#line 20 "./../Headers/MotherBoardModule.h"

#line 22 "./../Headers/MotherBoardModule.h"

#line 24 "./../Headers/MotherBoardModule.h"
#line 25 "./../Headers/MotherBoardModule.h"
#line 26 "./../Headers/MotherBoardModule.h"
#line 27 "./../Headers/MotherBoardModule.h"

#line 29 "./../Headers/MotherBoardModule.h"
#line 30 "./../Headers/MotherBoardModule.h"
#line 31 "./../Headers/MotherBoardModule.h"

#line 33 "./../Headers/MotherBoardModule.h"
#line 34 "./../Headers/MotherBoardModule.h"

HRESULT GetFuncTableMotherBoard(DeviceID * pid, ModuleFuncTable* table);
HRESULT InitMotherBoard(DeviceID* pid);
HRESULT CreateMotherBoardState(DeviceID* pid, PMODULE_DATA data);
HRESULT StoreMotherBoardSavedState(DeviceID* pid, PEEPROM_DATA buf);

typedef union tag_MotherBoardState
{
	struct
	{
		BYTE ParentId;
		BYTE ModuleType[(16  / 2) ];
		unsigned int Timer;
	};
	BYTE data[28 ];
} MotherBoardState;

typedef union tag_MotherBoardSavedState
{
	struct
	{
		BYTE ParentId;
		BYTE ModuleType[16 ];
	};
	BYTE data[40 ];
} MotherBoardSavedState;

extern MotherBoardState g_mbState;

#line 7 "./ControllerModule.h"
#line 8 "./ControllerModule.h"
#line 1 "./../Headers/RemoteModule.h"
#line 2 "./../Headers/RemoteModule.h"
#line 3 "./../Headers/RemoteModule.h"

#line 1 "./../Headers/ModuleBase.h"
#line 4 "./../Headers/RemoteModule.h"
#line 5 "./../Headers/RemoteModule.h"
#line 1 "./../Headers/TrainController.h"

#line 3 "./../Headers/TrainController.h"

#line 1 "./../Headers/ModuleBase.h"
#line 4 "./../Headers/TrainController.h"
#line 5 "./../Headers/TrainController.h"

#line 7 "./../Headers/TrainController.h"

#line 9 "./../Headers/TrainController.h"
#line 10 "./../Headers/TrainController.h"

#line 12 "./../Headers/TrainController.h"
#line 13 "./../Headers/TrainController.h"

HRESULT GetFuncTableTrainController(DeviceID* pid, ModuleFuncTable* table);
HRESULT InitTrainController(DeviceID* pid);
HRESULT CreateTrainControllerState(DeviceID* pid, PMODULE_DATA data);
HRESULT StoreTrainControllerState(DeviceID* pid, PMODULE_DATA data);
void InterruptTrainController(DeviceID* pid); 

typedef union tag_TrainControllerState
{
	struct
	{
		unsigned int duty;
		BYTE dutyEnabledBits;
		
		BYTE period;
		BYTE prescale;
		BYTE frequency; 
		
		BYTE direction;
		
		BYTE mode;
		unsigned int voltage;
		BYTE voltageEnabledBits;
		unsigned int meisuredvoltage;
		
		float paramp;
		float parami;
		float paramd;
		
	};
	BYTE data[28 ];
} TrainControllerState;

#line 5 "./../Headers/RemoteModule.h"
#line 6 "./../Headers/RemoteModule.h"

#line 8 "./../Headers/RemoteModule.h"

HRESULT GetFuncTableRemoteModule(DeviceID * pid, ModuleFuncTable* table);
HRESULT InitRemoteModule(DeviceID * pid);
HRESULT CreateRemoteModuleState(DeviceID * pid, PMODULE_DATA data);
HRESULT StoreRemoteModuleState(DeviceID * pid, PMODULE_DATA data);
void InterruptRemoteModule(DeviceID * pid); 

typedef union tag_RemoteModuleState
{
	struct
	{
		DeviceID remid;
	};
	BYTE data[28 ];
} RemoteModuleState;

typedef union tag_RemoteModuleSavedState
{
	struct
	{
		DeviceID remid;
	};
	BYTE data[8 ];
} RemoteModuleSavedState;

#line 8 "./ControllerModule.h"
#line 9 "./ControllerModule.h"
#line 1 "./../Headers/TrainController.h"
#line 9 "./ControllerModule.h"
#line 10 "./ControllerModule.h"


#line 13 "./ControllerModule.h"
#line 6 "./ControllerDevice.h"


#line 1 "main.c"
#line 2 "main.c"

  
 #pragma config PLLDIV = 5           
 #pragma config CPUDIV = OSC1_PLL2   
 #pragma config USBDIV = 2           
 #pragma config FOSC = HSPLL_HS      
 #pragma config FCMEN = OFF          
 #pragma config IESO = OFF           
 #pragma config PWRT = ON            
 #pragma config BOR = SOFT           
 #pragma config BORV = 2             
 #pragma config VREGEN = ON          
 #pragma config WDT = OFF            
 #pragma config WDTPS = 128          
 #pragma config MCLRE = ON           
 #pragma config LPT1OSC = OFF        
 #pragma config PBADEN = OFF         
 #pragma config CCP2MX = ON          
 #pragma config STVREN = ON          
 #pragma config LVP = OFF            
 #pragma config ICPRT = OFF          
 #pragma config XINST = OFF          
 #pragma config DEBUG = OFF          
 #pragma config CP0 = OFF 
 #pragma config CP1 = OFF 
 #pragma config CP2 = OFF 
 #pragma config CP3 = OFF 
 #pragma config CPB = OFF 
 #pragma config CPD = OFF 
 #pragma config WRT0 = OFF 
 #pragma config WRT1 = OFF 
 #pragma config WRT2 = OFF 
 #pragma config WRT3 = OFF 
 #pragma config WRTB = ON            
 #pragma config WRTC = OFF 
 #pragma config WRTD = OFF 
 #pragma config EBTR0 = OFF 
 #pragma config EBTR1 = OFF 
 #pragma config EBTR2 = OFF 
 #pragma config EBTR3 = OFF 
 #pragma config EBTRB = OFF
 
#pragma code HIGH_VECTOR = 0x08
void high_interrupt()
{ _asm GOTO high_isr _endasm}
#pragma code

#pragma interrupt high_isr
void high_isr()
{
	BYTE i;
	DeviceID id;
	
	if(PIR2bits.TMR3IF)
	{
		PIE2bits.TMR3IE = 0;
		PIR2bits.TMR3IF = 0;
		
		TMR1H |= ~TMR1H;
		id.ParentPart = g_mbState.ParentId;
		for(i = 0; i < 16 ; ++i)
		{
			id.ModulePart = i;
			
			GET_FUNC_TABLE(i)->fninterrupt(&id);
		}
		PIE2bits.TMR3IE = 1;
	}

}
#pragma code 


void ModuleInit()
{
	BYTE i;
	SetFuncTable(16 );
	
	for(i=0; i<16 ; i++)
	{
		if(((HRESULT)(GET_FUNC_TABLE(i)->fninit(i))<0) )
		{
			
		}
	}
}

void DeviceInit()
{
	INTCONbits.GIE = 1;
    INTCONbits.PEIE = 1;   
	
	OpenTimer3(TIMER_INT_ON & T3_8BIT_RW & T3_SOURCE_INT & T3_PS_1_8 & T3_SYNC_EXT_OFF);
}

void main()
{
	ModuleInit();
	DeviceInit();
	
	while(1);
}