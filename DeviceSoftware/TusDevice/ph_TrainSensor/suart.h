#ifndef SUART
#define SUART

#include <avr/io.h>

void xmit(uint8_t);
void xmit_parent(uint8_t);
uint8_t rcvr();


#endif	/* SUART */
