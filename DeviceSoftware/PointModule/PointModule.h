#ifndef INCLUDE_POINT_MODULE
#define INCLUDE_POINT_MODULE

#include <p18f2420.h>
#include <GenericTypeDefs.h>
#include "../Headers/eeprom.h"
#include "../Headers/PointInfo.h"

#define INPUT_PIN           1
#define OUTPUT_PIN          0

#define PORT_RECEIVING PORTA
#define TRIS_RECEIVING TRISA

#define PORT_ACK_HOST PORTAbits.RA5
#define PORT_ACK_DEVICE PORTAbits.RA0
#define TRIS_ACK_HOST TRISAbits.TRISA5
#define TRIS_ACK_DEVICE TRISAbits.TRISA0

#define PORT_OUTPUT_UPPER PORTC
#define PORT_OUTPUT_LOWER PORTB
#define TRIS_OUTPUT_UPPER TRISC
#define TRIS_OUTPUT_LOWER TRISB

#define GetBitValue(value, b) ((value) & (1 << (b)))
#define ClearBitValue(dest, b) ((dest) &= (~(1<<(b))))
#define SetBitValue(dest, b, src) ((dest) |= ((src)<<(b)))

#endif