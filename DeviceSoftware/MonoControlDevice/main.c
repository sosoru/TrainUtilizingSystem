
#include "MonoDevice.h"

#include <timers.h>
#include <adc.h>
#include <stdlib.h>
#include <string.h>

 /** Configuration Bits *******************************************/
 #pragma config PLLDIV = 5           //Configure for 20Mhz crystal, 20/5 = 4Mhz (required for USB)
 #pragma config CPUDIV = OSC1_PLL2   //Use 48MHz clock for system clock = 96MHz/2 = 48MHz
 #pragma config USBDIV = 2           //USB Clock source from the 96 MHz PLL divided by 2
 #pragma config FOSC = HSPLL_HS      //Oscillator Selection bits - HS oscillator, PLL enabled, HS used by USB
 #pragma config FCMEN = OFF          //Fail-Safe Clock Monitor Enable bit: Disable
 #pragma config IESO = OFF           //Internal/External Oscillator Switchover bit: Disable
 #pragma config PWRT = ON            //Power Up timer enabled
 #pragma config BOR = SOFT           //Brown-out Reset enabled and controlled by software (SBOREN is enabled)
 #pragma config BORV = 2             //Brown-out voltage = 2.7V
 #pragma config VREGEN = ON          //USB Voltage regulator enabled
 #pragma config WDT = OFF            //Watchdog timer HW Disabled - SW Controlled
 #pragma config WDTPS = 128          //Watchdog timer = 512ms (128 x 4ms)
 #pragma config MCLRE = ON           //Master clear enabled
 #pragma config LPT1OSC = OFF        //Disabled
 #pragma config PBADEN = OFF         //Port B0-4 are digital inputs on reset
 #pragma config CCP2MX = ON          //RC1
 #pragma config STVREN = ON         //Stack overflow reset enable
 #pragma config LVP = OFF            //Low voltage programming disabled
 #pragma config ICPRT = OFF          //Dedicated In-Circuit Debug/Programming Port (ICPORT) disabled
 #pragma config XINST = OFF          //Extended mode off
 //#pragma config DEBUG = OFF          //Debug mode off
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
 #pragma config WRTB = OFF            //Table write protect bootloader code 
 #pragma config WRTC = OFF 
 #pragma config WRTD = OFF 
 #pragma config EBTR0 = OFF 
 #pragma config EBTR1 = OFF 
 #pragma config EBTR2 = OFF 
 #pragma config EBTR3 = OFF 
 #pragma config EBTRB = OFF
 
unsigned long Timer0OverflowCount=0L;
unsigned long TimerOccupied=0L;
BYTE lightCounter = 0;
BYTE USBTasksCounter = 0;

#if defined(__18F14K50) || defined(__18F13K50) || defined(__18LF14K50) || defined(__18LF13K50) 
    #pragma udata usbram2
#elif defined(__18F2455) || defined(__18F2550) || defined(__18F4455) || defined(__18F4550)\
    || defined(__18F2458) || defined(__18F2453) || defined(__18F4558) || defined(__18F4553)
    #pragma udata USB_VARIABLES=0x500
#elif defined(__18F4450) || defined(__18F2450)
    #pragma udata USB_VARIABLES=0x480
#else
    #pragma udata
#endif
unsigned char OUTPacket[64];	//User application buffer for receiving and holding OUT packets sent from the host
unsigned char INPacket[64];		//User application buffer for sending IN packets to the host
#pragma code
USB_HANDLE USBGenericOutHandle = 0;
USB_HANDLE USBGenericInHandle = 0;

void main();
void DeviceInit();
void ModuleInit();
void Process();
void low_isr();
void high_isr();

#pragma interrupt high_isr //save = PROD
#pragma interruptlow low_isr save = WREG, BSR, STATUS//, PROD
#pragma code LOW_VECTOR = 0x18
void low_interrupt()
{ _asm GOTO low_isr _endasm}
#pragma code

#pragma code HIGH_VECTOR = 0x08
void high_interrupt()
{ _asm GOTO high_isr _endasm}
#pragma code

BYTE isr_i;
DeviceID isr_id;

void low_isr()
{

	if(PIR2bits.TMR3IF)
	{
		PIE2bits.TMR3IE = 0;
		PIR2bits.TMR3IF = 0;
		
		isr_id.ParentPart = g_mbState.ParentId;
		for(isr_i = 0; isr_i < MODULE_COUNT; ++isr_i)
		{
			isr_id.ModuleAddr = isr_i;
			
			GET_FUNC_TABLE(isr_i)->fninterrupt(&isr_id);
		}
		
		TMR3H |= ~TMR3H;
		//TMR3L = 0;
		PIE2bits.TMR3IE = 1;
	}

	//remoted packets receiving for RemoteModule
	if(PIR1bits.SSPIF)
	{
		SpiPacket packet;
		DeviceID devid;
		MODULE_DATA data[SIZE_DATA];
		BYTE received; 
		
		PIR1bits.SSPIF = 0;
		received = SSPBUF;
		SSPBUF = 0x00;
		
		ReceiveByte(received);
		
		if( SUCCEEDED(PacketReady(&packet))
			&& SUCCEEDED(CreateMessageFromReceived(&packet, &devid, data)))
		{
			BYTE addr = devid.ModuleAddr;
			BYTE type = READ_MBSTATE_MODULETYPE(g_mbState, addr);
			
			AddPacketUSB(&devid, type, data);
		}
		
	}

}

void high_isr()
{

	if(INTCONbits.T0IF)
	{
		INTCONbits.T0IF = 0;
		
        if(TimerOccupied == 0L)
        {
                Timer0OverflowCount = 0L;
        }
        else
        {
                Timer0OverflowCount++;
        }        
	}
	
    if(PIR1bits.TMR1IF)
	{
		PIR1bits.TMR1IF = 0;
		
		TMR1H |= ~TMR1H;
					
		USBDeviceTasks();	
	}
	
//	if(PIR1bits.SSPIF)
//		Port_SurfaceLedA = 1;
//	else
//		Port_SurfaceLedA = 0;
//	
}



void ModuleInit()
{
	BYTE i;
	DeviceID id;
	
	SetFuncTable();
	
	id.ParentPart = g_mbState.ParentId;
	for(i=0; i<MODULE_COUNT; i++)
	{
		id.ModuleAddr = i;
		if(FAILED(GET_FUNC_TABLE(i)->fninit(&id)))
		{
			// do nothing
		}
	}
}

void DeviceInit()
{
//	ADCON1bits.VCFG = 0; // voltage references are Vdd, Vss
//	ADCON1bits.PCFG = 0b0000;	// all analog configuration
//	ADCON2bits.ADFM = 1; // right justified format
//	ADCON2bits.ADCS = 0b110; // A/D conversion clock is Fosc / 64
	
	ADCON1 = 0x0F;
	CMCON = 0x07; //disable comparators
	
	Tris_SurfaceLedA = 0;
	Tris_SurfaceLedB = 0;
	
	//TRISB = 0xFC;	// RB0,1 output
	//TRISDbits.RD0 = INPUT_PIN;
	
	//PORTB = 0;

//    SSPADD = 0x02;
//    OpenI2C(SLAVE_7, SLEW_OFF);
//    SSPCON2bits.SEN = 0;
//
	//i2c
	//PIE1bits.SSPIE = 1;
	//IPR1bits.SSPIP=1;	
	//TRISCbits.RC3 = 1;
	//TRISCbits.RC4 = 1;

    #if defined(USE_USB_BUS_SENSE_IO)
    tris_usb_bus_sense = INPUT_PIN; // See HardwareProfile.h
    #endif

    #if defined(USE_SELF_POWER_SENSE_IO)
    tris_self_power = INPUT_PIN;	// See HardwareProfile.h
    #endif
    
 	USBDeviceInit(); //check the endpoint initialization written in a callback method
 	
    INTCONbits.GIE = 1;
    INTCONbits.PEIE = 1;   
    RCONbits.IPEN = 1;
    PIE1bits.SSPIE = 1;

    INTCON2bits.TMR0IP = 1; // tmr0 = high interrupt
    IPR1bits.TMR1IP = 1; //tmr1 = high interrupt
    IPR1bits.SSPIP = 0; // ssp = low interrupt
    IPR2bits.TMR3IP = 0; //tmr3 = low interrupt

	OpenTimer0(TIMER_INT_ON 
				& T0_16BIT 
		    	& T0_SOURCE_INT 
		  		& T0_PS_1_256 );
		  		
	
	
	//for USB tasks
	OpenTimer1(TIMER_INT_ON & T1_8BIT_RW & T1_SOURCE_INT & T1_PS_1_8 & T1_OSC1EN_OFF & T1_SYNC_EXT_OFF); 
	OpenTimer3(TIMER_INT_ON & T3_8BIT_RW & T3_SOURCE_INT & T3_PS_1_8 & T3_SYNC_EXT_OFF);
}

void Process()
{
	BYTE i, j;
	DeviceID id;
	MODULE_DATA data[SIZE_DATA];
	
    if(!USB_TRANSFAR_AVAILABLE)
	{
		Port_SurfaceLedB = 0;
	    return;
    }
	Port_SurfaceLedB = 1;
	
	id.ParentPart = g_mbState.ParentId;
	    	
	for(i = 0; i < MODULE_COUNT; i++)
	{
		BYTE type;
		
		ReceivingProcessUSB();
		type = READ_MBSTATE_MODULETYPE(g_mbState, i);
		
		if(type != UNKNOWN_MODULE_TYPE)
		{
			//memset((void*)data, 0x00, (size_t)SIZE_DATA);
			for(j=0; j<INTERNAL_MODULE_COUNT; ++j)
			{		
				HRESULT res;
				BYTE k, remoting = (type == REMOTE_MODULE_MODULE_TYPE);
				

				id.ModuleAddr = i;
				id.InternalAddr = j;
				
				for(k=0; k<=remoting; ++k)
				{
					id.RemoteBit = k;
					
					res = GET_FUNC_TABLE(i)->fncreate(&id, data);
					if(SUCCEEDED(res))
					{
						AddPacketUSB(&id, type, data);
						//memset((void*)data, 0x00, (size_t)SIZE_DATA);
						
						SendPacketUSB();
					}
				}
				
				if(TERMINATED(res))
				{
					break;
				}
			}
		}
	}
	SendPacketUSB();
	
}

void main()
{ 
	ModuleInit();
	DeviceInit();
	
	Port_SurfaceLedA = 1;
	
//	LATBbits.LATB0 = 1;
//	Delay10KTCYx(200);
//	LATBbits.LATB0 = 0;
//	
//	LATBbits.LATB0 = 1;
	USBDeviceAttach();

	while(1)
	{
		//Port_SurfaceLedA = (USBDeviceState == CONFIGURED_STATE);
		
		Process();	
	}
}
