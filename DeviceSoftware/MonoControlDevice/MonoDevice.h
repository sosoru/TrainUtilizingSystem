#ifndef MONO_DEVICE_INCLUDE
#define MONO_DEVICE_INCLUDE

#include "../Headers/SensorModule.h"
#include "GenericTypeDefs.h"
#include "USB/usb.h"
#include "USB/usb_function_generic.h"
#include "../Headers/PointInfo.h"

#include "HardwareProfile.h"
#include <p18f4550.h>

/** VARIABLES ******************************************************/
//#if defined(__18F14K50) || defined(__18F13K50) || defined(__18LF14K50) || defined(__18LF13K50) 
//    #pragma udata usbram2
//#elif defined(__18F2455) || defined(__18F2550) || defined(__18F4455) || defined(__18F4550)\
//    || defined(__18F2458) || defined(__18F2453) || defined(__18F4558) || defined(__18F4553)
//    #pragma udata USB_VARIABLES=0x500
//#elif defined(__18F4450) || defined(__18F2450)
//    #pragma udata USB_VARIABLES=0x480
//#else
//    #pragma udata
//#endif
//
extern unsigned char OUTPacket[64];	//User application buffer for receiving and holding OUT packets sent from the host
extern unsigned char INPacket[64];		//User application buffer for sending IN packets to the host
//#pragma udata
extern USB_HANDLE USBGenericOutHandle;
extern USB_HANDLE USBGenericInHandle;
//#pragma udata

extern unsigned long Timer0OverflowCount;
extern unsigned long TimerOccupied;

extern MotherBoardState g_mbState;

#define SET_TIMER_OCCUPIED(pos, val) (TimerOccupied = (TimerOccupied & (~(((unsigned long)1) << ((unsigned long)(pos)))) | (((unsigned long)val) << ((unsigned long)(pos)))))
#define GET_TIMER_OCCUPIED(pos) ((TimerOccupied & (((unsigned long)0x00000001) << ((unsigned long)(pos)))) > ((unsigned long)0))

#define USB_TRANSFAR_AVAILABLE (!((USBDeviceState < CONFIGURED_STATE)||(USBSuspendControl==1)))

#define TRIS_PORTA_A TRISAbits.TRISA0
#define TRIS_PORTA_B TRISAbits.TRISA1
#define TRIS_PORTA_C TRISAbits.TRISA2
#define TRIS_PORTA_D TRISAbits.TRISA3

#define TRIS_PORTB_A TRISAbits.TRISA5
#define TRIS_PORTB_B TRISEbits.TRISE0
#define TRIS_PORTB_C TRISEbits.TRISE1
#define TRIS_PORTB_D TRISEbits.TRISE2

#define TRIS_PORTC_A TRISAbits.TRISA4
#define TRIS_PORTC_B TRISBbits.TRISB0
#define TRIS_PORTC_C TRISCbits.TRISC1
#define TRIS_PORTC_D TRISCbits.TRISC2

#define TRIS_PORTD_A TRISBbits.TRISB2
#define TRIS_PORTD_B TRISBbits.TRISB3
#define TRIS_PORTD_C TRISBbits.TRISB1
#define TRIS_PORTD_D TRISBbits.TRISB4

#define TRIS_PORTE_A TRISDbits.TRISD7
#define TRIS_PORTE_B TRISBbits.TRISB5
#define TRIS_PORTE_C TRISBbits.TRISB6
#define TRIS_PORTE_D TRISBbits.TRISB7

#define TRIS_PORTF_A TRISCbits.TRISC7
#define TRIS_PORTF_B TRISDbits.TRISD4
#define TRIS_PORTF_C TRISDbits.TRISD5
#define TRIS_PORTF_D TRISDbits.TRISD6

#define TRIS_PORTG_A TRISDbits.TRISD1
#define TRIS_PORTG_B TRISDbits.TRISD2
#define TRIS_PORTG_C TRISDbits.TRISD3
#define TRIS_PORTG_D TRISCbits.TRISC6

#define PORT_PORTA_A PORTAbits.RA0
#define PORT_PORTA_B PORTAbits.RA1
#define PORT_PORTA_C PORTAbits.RA2
#define PORT_PORTA_D PORTAbits.RA3

#define PORT_PORTB_A PORTAbits.RA5
#define PORT_PORTB_B PORTEbits.RE0
#define PORT_PORTB_C PORTEbits.RE1
#define PORT_PORTB_D PORTEbits.RE2

#define PORT_PORTC_A PORTAbits.RA4
#define PORT_PORTC_B PORTBbits.RB0
#define PORT_PORTC_C PORTCbits.RC1
#define PORT_PORTC_D PORTCbits.RC2

#define PORT_PORTD_A PORTBbits.RB2
#define PORT_PORTD_B PORTBbits.RB3
#define PORT_PORTD_C PORTBbits.RB1
#define PORT_PORTD_D PORTBbits.RB4

#define PORT_PORTE_A PORTDbits.RD7
#define PORT_PORTE_B PORTBbits.RB5
#define PORT_PORTE_C PORTBbits.RB6
#define PORT_PORTE_D PORTBbits.RB7

#define PORT_PORTF_A PORTCbits.RC7
#define PORT_PORTF_B PORTDbits.RD4
#define PORT_PORTF_C PORTDbits.RD5
#define PORT_PORTF_D PORTDbits.RD6

#define PORT_PORTG_A PORTDbits.RD1
#define PORT_PORTG_B PORTDbits.RD2
#define PORT_PORTG_C PORTDbits.RD3
#define PORT_PORTG_D PORTCbits.RC6

#define LAT_PORTA_A LATAbits.LATA0
#define LAT_PORTA_B LATAbits.LATA1
#define LAT_PORTA_C LATAbits.LATA2
#define LAT_PORTA_D LATAbits.LATA3

#define LAT_PORTB_A LATAbits.LATA5
#define LAT_PORTB_B LATEbits.LATE0
#define LAT_PORTB_C LATEbits.LATE1
#define LAT_PORTB_D LATEbits.LATE2

#define LAT_PORTC_A LATAbits.LATA4
#define LAT_PORTC_B LATBbits.LATB0
#define LAT_PORTC_C LATCbits.LATC1
#define LAT_PORTC_D LATCbits.LATC2

#define LAT_PORTD_A LATBbits.LATB2
#define LAT_PORTD_B LATBbits.LATB3
#define LAT_PORTD_C LATBbits.LATB1
#define LAT_PORTD_D LATBbits.LATB4

#define LAT_PORTE_A LATDbits.LATD7
#define LAT_PORTE_B LATBbits.LATB5
#define LAT_PORTE_C LATBbits.LATB6
#define LAT_PORTE_D LATBbits.LATB7

#define LAT_PORTF_A LATCbits.LATC7
#define LAT_PORTF_B LATDbits.LATD4
#define LAT_PORTF_C LATDbits.LATD5
#define LAT_PORTF_D LATDbits.LATD6

#define LAT_PORTG_A LATDbits.LATD1
#define LAT_PORTG_B LATDbits.LATD2
#define LAT_PORTG_C LATDbits.LATD3
#define LAT_PORTG_D LATCbits.LATC6

#define Tris_SurfaceLedA TRISCbits.TRISC0
#define Tris_SurfaceLedB TRISDbits.TRISD0
#define Port_SurfaceLedA LATCbits.LATC0
#define Port_SurfaceLedB LATDbits.LATD0

extern near BYTE* ModuleTris[];
extern near BYTE* ModulePort[];
extern near BYTE* ModuleLat[];
extern BYTE ModuleShift[];

#define BIT_MASK(bit_no) (1 << (bit_no))
#define GetMaskedValue(value, bit_no) ((value) & BIT_MASK((bit_no)))
#define SetMaskedValue(value, bit_no, set) ((value) |= ((BIT_MASK(bit_no)) & ((set) << (bit_no))))

#define getTris(no) (GetMaskedValue(*ModuleTris[(no)], ModuleShift[(no)]))
#define setTris(no, val) (SetMaskedValue(*ModuleTris[(no)], ModuleShift[(no)], val))
#define getPort(no) (GetMaskedValue(*ModulePort[(no)], ModuleShift[(no)]))
#define setPort(no, val) (SetMaskedValue(*ModulePort[(no)], ModuleShift[(no)], val))
#define getLat(no) (GetMaskedValue(*ModuleLat[(no)], ModuleShift[(no)]))
#define setLat(no, val) (SetMaskedValue(*ModuleLat[(no)], ModuleShift[(no)], val))

#endif