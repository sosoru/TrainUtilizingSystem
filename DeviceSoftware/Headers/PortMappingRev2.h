#ifndef BLOCK_PORT_MAPPING_REV2
#define BLOCK_PORT_MAPPING_REV2

#define PORT_PIN_COUNT 6

#define Tris_SurfaceLedA TRISCbits.TRISC0
#define Tris_SurfaceLedB TRISDbits.TRISD0
#define Port_SurfaceLedA LATCbits.LATC0
#define Port_SurfaceLedB LATDbits.LATD0

#define PORT_PORTA_A PORTAbits.RA2
#define PORT_PORTA_B PORTAbits.RA3
#define PORT_PORTA_C PORTAbits.RA5
#define PORT_PORTA_D PORTEbits.RE0
#define PORT_PORTA_E PORTEbits.RE1
#define PORT_PORTA_F PORTEbits.RE2

#define PORT_PORTB_A PORTBbits.RB1
#define PORT_PORTB_B PORTBbits.RB2
#define PORT_PORTB_C PORTAbits.RA1
#define PORT_PORTB_D PORTBbits.RB3
#define PORT_PORTB_E PORTAbits.RA0
#define PORT_PORTB_F PORTBbits.RB4

#define PORT_PORTC_A PORTCbits.RC6
#define PORT_PORTC_B PORTCbits.RC7
#define PORT_PORTC_C PORTDbits.RD4
#define PORT_PORTC_D PORTDbits.RD5
#define PORT_PORTC_E PORTDbits.RD6
#define PORT_PORTC_F PORTDbits.RD7

#endif