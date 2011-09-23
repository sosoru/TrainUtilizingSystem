#ifndef BLOCK_PORT_MAPPING_REV2
#define BLOCK_PORT_MAPPING_REV2

#define PORT_PIN_COUNT 6

#define Tris_SurfaceLedA TRISCbits.TRISC7
#define Tris_SurfaceLedB TRISDbits.TRISD4
#define Port_SurfaceLedA LATCbits.LATC7
#define Port_SurfaceLedB LATDbits.LATD4

#define PORT_PORTA_A PORTAbits.RA1
#define PORT_PORTA_B PORTDbits.RD7
#define PORT_PORTA_C PORTBbits.RB0
#define PORT_PORTA_D PORTBbits.RB1
#define PORT_PORTA_E PORTBbits.RB2
#define PORT_PORTA_F PORTBbits.RB3

#define PORT_PORTB_A PORTAbits.RA0
#define PORT_PORTB_B PORTDbits.RD6
#define PORT_PORTB_C PORTBbits.RB4
#define PORT_PORTB_D PORTBbits.RB5
#define PORT_PORTB_E PORTBbits.RB6
#define PORT_PORTB_F PORTBbits.RB7

#define PORT_PORTC_A PORTCbits.RA2
#define PORT_PORTC_B PORTCbits.RD5
#define PORT_PORTC_C PORTDbits.RD0
#define PORT_PORTC_D PORTDbits.RD1
#define PORT_PORTC_E PORTDbits.RD2
#define PORT_PORTC_F PORTDbits.RD3



#endif