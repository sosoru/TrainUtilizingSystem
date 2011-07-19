#ifndef INCLUDE_POINT_MODULE
#define INCLUDE_POINT_MODULE

#include <p18f4520.h>
#include <GenericTypeDefs.h>
#include "../Headers/PointInfo.h"

#define INPUT_PIN           1
#define OUTPUT_PIN          0

#define PARENT_VALUE LATA
#define PARENT_TRIS TRISA
#define PARENT_ACK_TRIS TRISAbits.TRISA5
#define PARENT_ACK_PORT LATAbits.LATA5

#define PORT_POINT_A PORTB
#define PORT_POINT_B PORTC
#define PORT_POINT_C PORTD
#define PORT_POINT_DIRECTION PORTEbits.RE0
#define PORT_DEFAULT PORT_POINT_A

#define TRIS_POINT_A TRISB
#define TRIS_POINT_B TRISC
#define TRIS_POINT_C TRISD
#define TRIS_POINT_DIRECTION TRISEbits.TRISE0

#define GetBitValue(value, b) ((value) & (1 << (b)))
#define ClearBitValue(dest, b) ((dest) &= (~(1<<(b))))
#define SetBitValue(dest, b, src) ((dest) |= ((src)<<(b)))

#endif