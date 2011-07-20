#ifndef MONO_DEVICE_INCLUDE
#define MONO_DEVICE_INCLUDE

#include "HardwareProfile.h"


#include "USB/usb.h"
#include "USB/usb_function_generic.h"
#include "../Headers/UsbPacket.h"

#include "../Headers/CommonDefs.h"
#include "../Headers/MonoModules.h"

#include "../Headers/ModuleFuncDefs.h"
#include "../Headers/PortMapping.h"


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


#define USB_TRANSFAR_AVAILABLE (!((USBDeviceState < CONFIGURED_STATE)||(USBSuspendControl==1)))


#endif