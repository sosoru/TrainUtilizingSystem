#ifndef BLOCK_PORT_MAPPING_REV2
#define BLOCK_PORT_MAPPING_REV2

#define PORT_PIN_COUNT 6

#define Tris_SurfaceLedA TRISCbits.TRISC0
#define Tris_SurfaceLedB TRISDbits.TRISD0
#define Port_SurfaceLedA LATCbits.LATC0
#define Port_SurfaceLedB LATDbits.LATD0

#define PORT_PORTA_A PORTAbits.RA0
#define PORT_PORTA_B PORTAbits.RA1
#define PORT_PORTA_C PORTAbits.RA2
#define PORT_PORTA_D PORTEbits.RA3
#define PORT_PORTA_E PORTEbits.RA4
#define PORT_PORTA_F PORTEbits.RA5

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

#define TRIS_PORTA_A TRISAbits.TRISA0
#define TRIS_PORTA_B TRISAbits.TRISA1
#define TRIS_PORTA_C TRISAbits.TRISA2
#define TRIS_PORTA_D TRISEbits.TRISA3
#define TRIS_PORTA_E TRISEbits.TRISA4
#define TRIS_PORTA_F TRISEbits.TRISA5

#endif