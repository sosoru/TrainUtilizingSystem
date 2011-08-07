#include "MonoDevice.h"
#include <timers.h>
#include <adc.h>
#include <spi.h>

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
 #pragma config STVREN = ON          //Stack overflow reset enable
 #pragma config LVP = OFF            //Low voltage programming disabled
 #pragma config ICPRT = OFF          //Dedicated In-Circuit Debug/Programming Port (ICPORT) disabled
 #pragma config XINST = OFF          //Extended mode off
 #pragma config DEBUG = OFF          //Debug mode off
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
 #pragma config WRTB = ON            //Table write protect bootloader code 
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
#pragma udata
USB_HANDLE USBGenericOutHandle = 0;
USB_HANDLE USBGenericInHandle = 0;

ModuleFuncTable g_ModuleFuncLower[SIZE_SPLITED_FUNCTABLE];
ModuleFuncTable g_ModuleFuncHigher[SIZE_SPLITED_FUNCTABLE];
BYTE g_usingAdc = FALSE;

void main();
void DeviceInit();
void ModuleInit();
void Process();
void high_isr();

void main()
{ 
	Port_SurfaceLedA = 0;
	Port_SurfaceLedB = 0;
	ModuleInit();
	DeviceInit();
	
//	LATBbits.LATB0 = 1;
//	Delay10KTCYx(200);
//	LATBbits.LATB0 = 0;
//	
	USBDeviceAttach();
//	LATBbits.LATB0 = 1;

	while(1)
	{
		Process();	
	}
}

void Process()
{
	BYTE i;
	DeviceID id;
	
	id.ParentPart = g_mbState.ParentId;
	
    if(!USB_TRANSFAR_AVAILABLE)
	{
		Port_SurfaceLedB = 0;
	    return;
    }
	Port_SurfaceLedB = 1;
	    	
	for(i = 0; i < MODULE_COUNT; i++)
	{
		BYTE type;
		MODULE_DATA data[SIZE_DATA];
		
		ReceivingProcessUSB();
		type = READ_MBSTATE_MODULETYPE(g_mbState, i);
		
		if(type != UNKNOWN_MODULE_TYPE)
		{
			memset(data, 0x00, sizeof(data));
			if(SUCCEEDED(GET_FUNC_TABLE(i)->fncreate(i, data)))
			{
				id.ModulePart = i;
				
				AddPacketUSB(&id, type, data);
				SendPacketUSB();
			}
		}
	}
	SendPacketUSB();
	
}

void ModuleInit()
{
	BYTE i;
	SetFuncTable(MODULE_COUNT);
	
	for(i=0; i<MODULE_COUNT; i++)
	{
		if(FAILED(GET_FUNC_TABLE(i)->fninit(i)))
		{
			// do nothing
		}
	}
}

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
		for(i = 0; i < MODULE_COUNT; ++i)
		{
			id.ModulePart = i;
			
			GET_FUNC_TABLE(i)->fninterrupt(&id);
		}
		PIE2bits.TMR3IE = 1;
	}

	if(INTCONbits.T0IF)
	{
		INTCONbits.T0IF = 0;
		
        if(TimerOccupied == 0L)
        {
                Timer0OverflowCount = 0L;
        }else
            {
                  Timer0OverflowCount++;
        }        
//        if(Port_SurfaceLedA == 0)
//		{
//			Port_SurfaceLedA = 1;
//	    }
//	    else
//			Port_SurfaceLedA = 0;
//
	}
    if(PIR1bits.TMR1IF)
	{
		PIR1bits.TMR1IF = 0;
		
		TMR1H |= ~TMR1H;
		//Port_SurfaceLedA = (USBDeviceState >= DEFAULT_STATE);
					
		USBDeviceTasks();	
	}
	

}
#pragma code 

void DeviceInit()
{
//	ADCON1bits.VCFG = 0; // voltage references are Vdd, Vss
//	ADCON1bits.PCFG = 0b0000;	// all analog configuration
//	ADCON2bits.ADFM = 1; // right justified format
//	ADCON2bits.ADCS = 0b110; // A/D conversion clock is Fosc / 64
	
	
	Tris_SurfaceLedA = 0;
	Tris_SurfaceLedB = 0;
	
	SSPCON1bits.SSPEN = 0;
	
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
	
    //	The USB specifications require that USB peripheral devices must never source
//	current onto the Vbus pin.  Additionally, USB peripherals should not source
//	current on D+ or D- when the host/hub is not actively powering the Vbus line.
//	When designing a self powered (as opposed to bus powered) USB peripheral
//	device, the firmware should make sure not to turn on the USB module and D+
//	or D- pull up resistor unless Vbus is actively powered.  Therefore, the
//	firmware needs some means to detect when Vbus is being powered by the host.
//	A 5V tolerant I/O pin can be connected to Vbus (through a resistor), and
// 	can be used to detect when Vbus is high (host actively powering), or low
//	(host is shut down or otherwise not supplying power).  The USB firmware
// 	can then periodically poll this I/O pin to know when it is okay to turn on
//	the USB module/D+/D- pull up resistor.  When designing a purely bus powered
//	peripheral device, it is not possible to source current on D+ or D- when the
//	host is not actively providing power on Vbus. Therefore, implementing this
//	bus sense feature is optional.  This firmware can be made to use this bus
//	sense feature by making sure "USE_USB_BUS_SENSE_IO" has been defined in the
//	HardwareProfile.h file.    
    #if defined(USE_USB_BUS_SENSE_IO)
    tris_usb_bus_sense = INPUT_PIN; // See HardwareProfile.h
    #endif
    
//	If the host PC sends a GetStatus (device) request, the firmware must respond
//	and let the host know if the USB peripheral device is currently bus powered
//	or self powered.  See chapter 9 in the official USB specifications for details
//	regarding this request.  If the peripheral device is capable of being both
//	self and bus powered, it should not return a hard coded value for this request.
//	Instead, firmware should check if it is currently self or bus powered, and
//	respond accordingly.  If the hardware has been configured like demonstrated
//	on the PICDEM FS USB Demo Board, an I/O pin can be polled to determine the
//	currently selected power source.  On the PICDEM FS USB Demo Board, "RA2" 
//	is used for	this purpose.  If using this feature, make sure "USE_SELF_POWER_SENSE_IO"
//	has been defined in HardwareProfile.h, and that an appropriate I/O pin has been mapped
//	to it in HardwareProfile.h.
    #if defined(USE_SELF_POWER_SENSE_IO)
    tris_self_power = INPUT_PIN;	// See HardwareProfile.h
    #endif
    
    INTCONbits.GIE = 1;
    INTCONbits.PEIE = 1;   

	OpenTimer0(TIMER_INT_ON 
				& T0_16BIT 
		    	& T0_SOURCE_INT 
		  		& T0_PS_1_256 );
		  		
	//for USB tasks
	OpenTimer1(TIMER_INT_ON & T1_8BIT_RW & T1_SOURCE_INT & T1_PS_1_8 & T1_OSC1EN_OFF & T1_SYNC_EXT_OFF); 
	OpenTimer3(TIMER_INT_ON & T3_8BIT_RW & T3_SOURCE_INT & T3_PS_1_8 & T3_SYNC_EXT_OFF);
	
	USBDeviceInit(); //check the endpoint initialization written in a callback method
}