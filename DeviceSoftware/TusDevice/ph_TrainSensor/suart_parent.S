;---------------------------------------------------------------------------;
; Software implemented UART module                                          ;
; (C)ChaN, 2005 (http://elm-chan.org/)                                      ;
;---------------------------------------------------------------------------;
; Bit rate settings:
;
;            1MHz  2MHz  4MHz  6MHz  8MHz  10MHz  12MHz  16MHz  20MHz
;   2.4kbps   138     -     -     -     -      -      -      -      -
;   4.8kbps    68   138     -     -     -      -      -      -      -
;   9.6kbps    33    68   138   208     -      -      -      -      -
;  19.2kbps     -    33    68   102   138    173    208      -      -
;  38.4kbps     -     -    33    50    68     85    102    138    172
;  57.6kbps     -     -    21    33    44     56     68     91    114
; 115.2kbps     -     -     -     -    21     27     33     44     56

.nolist
#include <avr/io.h>
.list

#define	BPS	68	/* Bit delay. (see above table) */
#define	BIDIR	0	/* 0:Separated Tx/Rx, 1:Shared Tx/Rx */

#define	OUT_1		sbi _SFR_IO_ADDR(PORTB), 3	/* Output 1 */
#define	OUT_0		cbi _SFR_IO_ADDR(PORTB), 3	/* Output 0 */



#ifdef SPM_PAGESIZE
.macro	_LPMI	reg
	lpm	\reg, Z+
.endm
.macro	_MOVW	dh,dl, sh,sl
	movw	\dl, \sl
.endm
#else
.macro	_LPMI	reg
	lpm
	mov	\reg, r0
	adiw	ZL, 1
.endm
.macro	_MOVW	dh,dl, sh,sl
	mov	\dl, \sl
	mov	\dh, \sh
.endm
#endif



;---------------------------------------------------------------------------;
; Transmit a byte in serial format of N81
;
;Prototype: void xmit_parent (uint8_t data);
;Size: 16 words

.global xmit_parent
.func xmit_parent
xmit_parent:
#if BIDIR
	ldi	r23, BPS-1	;Pre-idle time for bidirectional data line
5:	dec	r23     	;
	brne	5b		;/
#endif
	in	r17, _SFR_IO_ADDR(SREG)	;Save flags

	com	r24		;C = start bit
	ldi	r25, 10		;Bit counter
	cli			;Start critical section

1:	ldi	r23, BPS-1	;----- Bit transferring loop 
2:	dec	r23     	;Wait for a bit time
	brne	2b		;/
	brcs	3f		;MISO = bit to be sent
	OUT_1			;
3:	brcc	4f		;
	OUT_0			;/
4:	lsr	r24     	;Get next bit into C
	dec	r25     	;All bits sent?
	brne	1b	     	;  no, coutinue

	out	_SFR_IO_ADDR(SREG), r17	;End of critical section
	ret
.endfunc


