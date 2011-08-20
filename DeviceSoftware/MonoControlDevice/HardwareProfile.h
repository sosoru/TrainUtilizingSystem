
#ifndef HARDWARE_PROFILE_H
#define HARDWARE_PROFILE_H

#define VERSION_REV2
#include <p18cxxx.h>

#define self_powered
#define self_power          1

//#define USE_USB_BUS_SENSE_IO
//#define tris_usb_bus_sense TRISDbits.TRISD2
#define USB_BUS_SENSE       1//PORTDbits.RD2

#define CLOCK_FREQ 48000000
#define GetSystemClock() CLOCK_FREQ   

#endif  //HARDWARE_PROFILE_H
