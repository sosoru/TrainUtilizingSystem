
#ifndef HARDWARE_PROFILE_H
#define HARDWARE_PROFILE_H

#define VERSION_REV2
#include <p18cxxx.h>

#define self_powered
#define self_power          1

#define USE_USB_BUS_SENSE_IO
#define tris_usb_bus_sense TRISCbits.TRISC6
#define USB_BUS_SENSE       PORTCbits.RC6

#define AD_PORT 0b00001001

#define CLOCK_FREQ 48000000
#define GetSystemClock() CLOCK_FREQ   

#endif  //HARDWARE_PROFILE_H
