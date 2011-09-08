opt subtitle "HI-TECH Software Omniscient Code Generator (Lite mode) build 9453"

opt pagewidth 120

	opt lm

	processor	16F628A
clrc	macro
	bcf	3,0
	endm
clrz	macro
	bcf	3,2
	endm
setc	macro
	bsf	3,0
	endm
setz	macro
	bsf	3,2
	endm
skipc	macro
	btfss	3,0
	endm
skipz	macro
	btfss	3,2
	endm
skipnc	macro
	btfsc	3,0
	endm
skipnz	macro
	btfsc	3,2
	endm
indf	equ	0
indf0	equ	0
pc	equ	2
pcl	equ	2
status	equ	3
fsr	equ	4
fsr0	equ	4
c	equ	1
z	equ	0
pclath	equ	10
# 4 "C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
	psect config,class=CONFIG,delta=2 ;#
# 4 "C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
	dw 0xFFEE & 0xFFFB & 0xFFBF & 0xFFFF ;#
	FNCALL	_main,_Init
	FNCALL	_main,_ReadInput
	FNCALL	_Init,_PortClear
	FNROOT	_main
	global	_BITS_Array
	global	_PORT_Array
	global	_TRIS_Array
psect	idataBANK0,class=CODE,space=0,delta=2
global __pidataBANK0
__pidataBANK0:
	file	"C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
	line	37

;initializer for _BITS_Array
	retlw	01h
	retlw	0
	retlw	02h
	retlw	03h
	retlw	04h
	retlw	05h
	retlw	06h
	retlw	07h
	line	42

;initializer for _PORT_Array
	retlw	5&0ffh
	retlw	5&0ffh
	retlw	5&0ffh
	retlw	5&0ffh
	retlw	5&0ffh
	retlw	6&0ffh
	retlw	6&0ffh
	retlw	6&0ffh
	line	25

;initializer for _TRIS_Array
	retlw	133&0ffh
	retlw	133&0ffh
	retlw	133&0ffh
	retlw	133&0ffh
	retlw	133&0ffh
	retlw	134&0ffh
	retlw	134&0ffh
	retlw	134&0ffh
	global	_CARRY
_CARRY	set	24
	global	_GIE
_GIE	set	95
	global	_RB4
_RB4	set	52
	global	_EEADR
_EEADR	set	155
	global	_EECON1
_EECON1	set	156
	global	_EECON2
_EECON2	set	157
	global	_EEDATA
_EEDATA	set	154
	global	_RD
_RD	set	1248
	global	_TRISB4
_TRISB4	set	1076
	global	_WR
_WR	set	1249
	global	_WREN
_WREN	set	1250
	global	_PORTA
_PORTA	set	5
	global	_PORTB
_PORTB	set	6
	global	_TRISA
_TRISA	set	133
	global	_TRISB
_TRISB	set	134
	file	"TrainSensorModule.as"
	line	#
psect cinit,class=CODE,delta=2
global start_initialization
start_initialization:

psect	dataBANK0,class=BANK0,space=1
global __pdataBANK0
__pdataBANK0:
	file	"C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
	line	37
_BITS_Array:
       ds      8

psect	dataBANK0
	file	"C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
	line	42
_PORT_Array:
       ds      8

psect	dataBANK0
	file	"C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
	line	25
_TRIS_Array:
       ds      8

global btemp
psect inittext,class=CODE,delta=2
global init_fetch,btemp
;	Called with low address in FSR and high address in W
init_fetch:
	movf btemp,w
	movwf pclath
	movf btemp+1,w
	movwf pc
global init_ram
;Called with:
;	high address of idata address in btemp 
;	low address of idata address in btemp+1 
;	low address of data in FSR
;	high address + 1 of data in btemp-1
init_ram:
	fcall init_fetch
	movwf indf,f
	incf fsr,f
	movf fsr,w
	xorwf btemp-1,w
	btfsc status,2
	retlw 0
	incf btemp+1,f
	btfsc status,2
	incf btemp,f
	goto init_ram
; Initialize objects allocated to BANK0
psect cinit,class=CODE,delta=2
global init_ram, __pidataBANK0
	bcf	status, 7	;select IRP bank0
	movlw low(__pdataBANK0+24)
	movwf btemp-1,f
	movlw high(__pidataBANK0)
	movwf btemp,f
	movlw low(__pidataBANK0)
	movwf btemp+1,f
	movlw low(__pdataBANK0)
	movwf fsr,f
	fcall init_ram
psect cinit,class=CODE,delta=2
global end_of_initialization

;End of C runtime variable initialization code

end_of_initialization:
clrf status
ljmp _main	;jump to C main() function
psect	cstackCOMMON,class=COMMON,space=1
global __pcstackCOMMON
__pcstackCOMMON:
	global	?_PortClear
?_PortClear:	; 0 bytes @ 0x0
	global	??_PortClear
??_PortClear:	; 0 bytes @ 0x0
	global	?_Init
?_Init:	; 0 bytes @ 0x0
	global	??_ReadInput
??_ReadInput:	; 0 bytes @ 0x0
	global	?_main
?_main:	; 0 bytes @ 0x0
	global	?_ReadInput
?_ReadInput:	; 1 bytes @ 0x0
	ds	3
	global	PortClear@i
PortClear@i:	; 1 bytes @ 0x3
	ds	1
	global	??_Init
??_Init:	; 0 bytes @ 0x4
	ds	3
	global	Init@i
Init@i:	; 1 bytes @ 0x7
	ds	1
	global	??_main
??_main:	; 0 bytes @ 0x8
	ds	3
	global	main@tmp
main@tmp:	; 1 bytes @ 0xB
	ds	1
	global	main@cache
main@cache:	; 1 bytes @ 0xC
	ds	1
;;Data sizes: Strings 0, constant 0, data 24, bss 0, persistent 0 stack 0
;;Auto spaces:   Size  Autos    Used
;; COMMON          14     13      13
;; BANK0           80      0      24
;; BANK1           80      0       0
;; BANK2           48      0       0

;;
;; Pointer list with targets:

;; PORT_Array	PTR volatile unsigned char [8] size(1) Largest target is 1
;;		 -> PORTB(BITSFR0[1]), PORTA(BITSFR0[1]), 
;;
;; TRIS_Array	PTR volatile unsigned char [8] size(1) Largest target is 1
;;		 -> TRISB(BITSFR1[1]), TRISA(BITSFR1[1]), 
;;


;;
;; Critical Paths under _main in COMMON
;;
;;   _main->_Init
;;   _Init->_PortClear
;;
;; Critical Paths under _main in BANK0
;;
;;   None.
;;
;; Critical Paths under _main in BANK1
;;
;;   None.
;;
;; Critical Paths under _main in BANK2
;;
;;   None.

;;
;;Main: autosize = 0, tempsize = 3, incstack = 0, save=0
;;

;;
;;Call Graph Tables:
;;
;; ---------------------------------------------------------------------------------
;; (Depth) Function   	        Calls       Base Space   Used Autos Params    Refs
;; ---------------------------------------------------------------------------------
;; (0) _main                                                 5     5      0     623
;;                                              8 COMMON     5     5      0
;;                               _Init
;;                          _ReadInput
;; ---------------------------------------------------------------------------------
;; (1) _Init                                                 4     4      0     268
;;                                              4 COMMON     4     4      0
;;                          _PortClear
;; ---------------------------------------------------------------------------------
;; (1) _ReadInput                                            0     0      0       0
;; ---------------------------------------------------------------------------------
;; (2) _PortClear                                            4     4      0     134
;;                                              0 COMMON     4     4      0
;; ---------------------------------------------------------------------------------
;; Estimated maximum stack depth 2
;; ---------------------------------------------------------------------------------

;; Call Graph Graphs:

;; _main (ROOT)
;;   _Init
;;     _PortClear
;;   _ReadInput
;;

;; Address spaces:

;;Name               Size   Autos  Total    Cost      Usage
;;SFR3                 0      0       0       4        0.0%
;;BITSFR3              0      0       0       4        0.0%
;;BANK2               30      0       0       9        0.0%
;;BITBANK2            30      0       0       8        0.0%
;;SFR2                 0      0       0       5        0.0%
;;BITSFR2              0      0       0       5        0.0%
;;SFR1                 0      0       0       2        0.0%
;;BITSFR1              0      0       0       2        0.0%
;;BANK1               50      0       0       7        0.0%
;;BITBANK1            50      0       0       6        0.0%
;;CODE                 0      0       0       0        0.0%
;;DATA                 0      0      27      10        0.0%
;;ABS                  0      0      25       4        0.0%
;;NULL                 0      0       0       0        0.0%
;;STACK                0      0       2       2        0.0%
;;BANK0               50      0      18       3       30.0%
;;BITBANK0            50      0       0       5        0.0%
;;SFR0                 0      0       0       1        0.0%
;;BITSFR0              0      0       0       1        0.0%
;;COMMON               E      D       D       1       92.9%
;;BITCOMMON            E      0       0       0        0.0%
;;EEDATA              80      0       0       0        0.0%

	global	_main
psect	maintext,global,class=CODE,delta=2
global __pmaintext
__pmaintext:

;; *************** function _main *****************
;; Defined at:
;;		line 101 in file "C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
;; Parameters:    Size  Location     Type
;;		None
;; Auto vars:     Size  Location     Type
;;  tmp             1   11[COMMON] unsigned char 
;;  cache           1   12[COMMON] unsigned char 
;; Return value:  Size  Location     Type
;;		None               void
;; Registers used:
;;		wreg, fsr0l, fsr0h, status,2, status,0, pclath, cstack
;; Tracked objects:
;;		On entry : 17F/0
;;		On exit  : 0/0
;;		Unchanged: 0/0
;; Data sizes:     COMMON   BANK0   BANK1   BANK2
;;      Params:         0       0       0       0
;;      Locals:         2       0       0       0
;;      Temps:          3       0       0       0
;;      Totals:         5       0       0       0
;;Total ram usage:        5 bytes
;; Hardware stack levels required when called:    2
;; This function calls:
;;		_Init
;;		_ReadInput
;; This function is called by:
;;		Startup code after reset
;; This function uses a non-reentrant model
;;
psect	maintext
	file	"C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
	line	101
	global	__size_of_main
	__size_of_main	equ	__end_of_main-_main
	
_main:	
	opt	stack 6
; Regs used in _main: [wreg-fsr0h+status,2+status,0+pclath+cstack]
	line	102
	
l2114:	
;main.c: 102: unsigned char cache = 0x01;
	clrf	(main@cache)
	bsf	status,0
	rlf	(main@cache),f
	line	104
	
l2116:	
;main.c: 104: Init();
	fcall	_Init
	line	105
	
l2118:	
;main.c: 105: (*PORT_Array[(cache)] = ((*PORT_Array[(cache)]) & (~(1 << (cache)))) | ((1&1) << (cache)));
	movlw	(01h)
	movwf	(??_main+0)+0
	incf	(main@cache),w
	goto	u2464
u2465:
	clrc
	rlf	(??_main+0)+0,f
u2464:
	addlw	-1
	skipz
	goto	u2465
	movf	(main@cache),w
	addlw	_PORT_Array&0ffh
	movwf	fsr0
	bcf	status, 7	;select IRP bank0
	movf	indf,w
	movwf	fsr0
	movlw	(01h)
	movwf	(??_main+1)+0
	incf	(main@cache),w
	goto	u2474
u2475:
	clrc
	rlf	(??_main+1)+0,f
u2474:
	addlw	-1
	skipz
	goto	u2475
	movf	0+(??_main+1)+0,w
	xorlw	0ffh
	andwf	indf,w
	iorwf	0+(??_main+0)+0,w
	movwf	(??_main+2)+0
	movf	(main@cache),w
	addlw	_PORT_Array&0ffh
	movwf	fsr0
	movf	indf,w
	movwf	fsr0
	movf	(??_main+2)+0,w
	movwf	indf
	line	106
	
l2120:	
;main.c: 106: RB4 = 1;
	bcf	status, 5	;RP0=0, select bank0
	bcf	status, 6	;RP1=0, select bank0
	bsf	(52/8),(52)&7
	goto	l2122
	line	108
;main.c: 108: while(1)
	
l396:	
	line	110
	
l2122:	
;main.c: 109: {
;main.c: 110: unsigned char tmp = ReadInput();
	fcall	_ReadInput
	movwf	(??_main+0)+0
	movf	(??_main+0)+0,w
	movwf	(main@tmp)
	line	111
	
l2124:	
;main.c: 111: if(cache != tmp)
	movf	(main@cache),w
	xorwf	(main@tmp),w
	skipnz
	goto	u2481
	goto	u2480
u2481:
	goto	l2122
u2480:
	line	113
	
l2126:	
;main.c: 112: {
;main.c: 113: RB4 = 0;
	bcf	status, 5	;RP0=0, select bank0
	bcf	status, 6	;RP1=0, select bank0
	bcf	(52/8),(52)&7
	line	115
	
l2128:	
;main.c: 115: (*PORT_Array[(cache)] = ((*PORT_Array[(cache)]) & (~(1 << (cache)))) | ((0&1) << (cache)));
	movlw	(0)
	movwf	(??_main+0)+0
	incf	(main@cache),w
	goto	u2494
u2495:
	clrc
	rlf	(??_main+0)+0,f
u2494:
	addlw	-1
	skipz
	goto	u2495
	movf	(main@cache),w
	addlw	_PORT_Array&0ffh
	movwf	fsr0
	bcf	status, 7	;select IRP bank0
	movf	indf,w
	movwf	fsr0
	movlw	(01h)
	movwf	(??_main+1)+0
	incf	(main@cache),w
	goto	u2504
u2505:
	clrc
	rlf	(??_main+1)+0,f
u2504:
	addlw	-1
	skipz
	goto	u2505
	movf	0+(??_main+1)+0,w
	xorlw	0ffh
	andwf	indf,w
	iorwf	0+(??_main+0)+0,w
	movwf	(??_main+2)+0
	movf	(main@cache),w
	addlw	_PORT_Array&0ffh
	movwf	fsr0
	movf	indf,w
	movwf	fsr0
	movf	(??_main+2)+0,w
	movwf	indf
	line	116
	
l2130:	
;main.c: 116: if(tmp < 8)
	movlw	(08h)
	subwf	(main@tmp),w
	skipnc
	goto	u2511
	goto	u2510
u2511:
	goto	l2122
u2510:
	line	118
	
l2132:	
;main.c: 117: {
;main.c: 118: (*PORT_Array[(tmp)] = ((*PORT_Array[(tmp)]) & (~(1 << (tmp)))) | ((1&1) << (tmp)));
	movlw	(01h)
	movwf	(??_main+0)+0
	incf	(main@tmp),w
	goto	u2524
u2525:
	clrc
	rlf	(??_main+0)+0,f
u2524:
	addlw	-1
	skipz
	goto	u2525
	movf	(main@tmp),w
	addlw	_PORT_Array&0ffh
	movwf	fsr0
	movf	indf,w
	movwf	fsr0
	movlw	(01h)
	movwf	(??_main+1)+0
	incf	(main@tmp),w
	goto	u2534
u2535:
	clrc
	rlf	(??_main+1)+0,f
u2534:
	addlw	-1
	skipz
	goto	u2535
	movf	0+(??_main+1)+0,w
	xorlw	0ffh
	andwf	indf,w
	iorwf	0+(??_main+0)+0,w
	movwf	(??_main+2)+0
	movf	(main@tmp),w
	addlw	_PORT_Array&0ffh
	movwf	fsr0
	movf	indf,w
	movwf	fsr0
	movf	(??_main+2)+0,w
	movwf	indf
	line	120
	
l2134:	
;main.c: 120: RB4 = 1;
	bsf	(52/8),(52)&7
	line	121
	
l2136:	
;main.c: 121: cache = tmp;
	movf	(main@tmp),w
	movwf	(??_main+0)+0
	movf	(??_main+0)+0,w
	movwf	(main@cache)
	goto	l2122
	line	122
	
l398:	
	goto	l2122
	line	123
	
l397:	
	goto	l2122
	line	124
	
l399:	
	line	108
	goto	l2122
	
l400:	
	line	125
	
l401:	
	global	start
	ljmp	start
	opt stack 0
GLOBAL	__end_of_main
	__end_of_main:
;; =============== function _main ends ============

	signat	_main,88
	global	_Init
psect	text149,local,class=CODE,delta=2
global __ptext149
__ptext149:

;; *************** function _Init *****************
;; Defined at:
;;		line 74 in file "C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
;; Parameters:    Size  Location     Type
;;		None
;; Auto vars:     Size  Location     Type
;;  i               1    7[COMMON] unsigned char 
;; Return value:  Size  Location     Type
;;		None               void
;; Registers used:
;;		wreg, fsr0l, fsr0h, status,2, status,0, pclath, cstack
;; Tracked objects:
;;		On entry : 0/0
;;		On exit  : 0/0
;;		Unchanged: 0/0
;; Data sizes:     COMMON   BANK0   BANK1   BANK2
;;      Params:         0       0       0       0
;;      Locals:         1       0       0       0
;;      Temps:          3       0       0       0
;;      Totals:         4       0       0       0
;;Total ram usage:        4 bytes
;; Hardware stack levels used:    1
;; Hardware stack levels required when called:    1
;; This function calls:
;;		_PortClear
;; This function is called by:
;;		_main
;; This function uses a non-reentrant model
;;
psect	text149
	file	"C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
	line	74
	global	__size_of_Init
	__size_of_Init	equ	__end_of_Init-_Init
	
_Init:	
	opt	stack 6
; Regs used in _Init: [wreg-fsr0h+status,2+status,0+pclath+cstack]
	line	77
	
l2100:	
;main.c: 75: unsigned char i;
;main.c: 77: TRISA = 0xFF;
	movlw	(0FFh)
	bsf	status, 5	;RP0=1, select bank1
	bcf	status, 6	;RP1=0, select bank1
	movwf	(133)^080h	;volatile
	line	78
;main.c: 78: TRISB = 0xFF;
	movlw	(0FFh)
	movwf	(134)^080h	;volatile
	line	80
	
l2102:	
;main.c: 80: for(i=0; i<8; ++i)
	clrf	(Init@i)
	movlw	(08h)
	subwf	(Init@i),w
	skipc
	goto	u2421
	goto	u2420
u2421:
	goto	l2106
u2420:
	goto	l389
	
l2104:	
	goto	l389
	line	81
	
l388:	
	line	82
	
l2106:	
;main.c: 81: {
;main.c: 82: (*TRIS_Array[(i)] = ((*TRIS_Array[(i)]) & (~(1 << (i)))) | ((0&1) << (i)));
	movlw	(0)
	movwf	(??_Init+0)+0
	incf	(Init@i),w
	goto	u2434
u2435:
	clrc
	rlf	(??_Init+0)+0,f
u2434:
	addlw	-1
	skipz
	goto	u2435
	movf	(Init@i),w
	addlw	_TRIS_Array&0ffh
	movwf	fsr0
	bcf	status, 7	;select IRP bank0
	movf	indf,w
	movwf	fsr0
	movlw	(01h)
	movwf	(??_Init+1)+0
	incf	(Init@i),w
	goto	u2444
u2445:
	clrc
	rlf	(??_Init+1)+0,f
u2444:
	addlw	-1
	skipz
	goto	u2445
	movf	0+(??_Init+1)+0,w
	xorlw	0ffh
	andwf	indf,w
	iorwf	0+(??_Init+0)+0,w
	movwf	(??_Init+2)+0
	movf	(Init@i),w
	addlw	_TRIS_Array&0ffh
	movwf	fsr0
	movf	indf,w
	movwf	fsr0
	movf	(??_Init+2)+0,w
	movwf	indf
	line	80
	
l2108:	
	movlw	(01h)
	movwf	(??_Init+0)+0
	movf	(??_Init+0)+0,w
	addwf	(Init@i),f
	
l2110:	
	movlw	(08h)
	subwf	(Init@i),w
	skipc
	goto	u2451
	goto	u2450
u2451:
	goto	l2106
u2450:
	
l389:	
	line	85
;main.c: 83: }
;main.c: 85: TRISB4 = 0;
	bsf	status, 5	;RP0=1, select bank1
	bcf	status, 6	;RP1=0, select bank1
	bcf	(1076/8)^080h,(1076)&7
	line	87
;main.c: 87: RB4 = 0;
	bcf	status, 5	;RP0=0, select bank0
	bcf	status, 6	;RP1=0, select bank0
	bcf	(52/8),(52)&7
	line	88
	
l2112:	
;main.c: 88: PortClear();
	fcall	_PortClear
	line	89
	
l390:	
	return
	opt stack 0
GLOBAL	__end_of_Init
	__end_of_Init:
;; =============== function _Init ends ============

	signat	_Init,88
	global	_ReadInput
psect	text150,local,class=CODE,delta=2
global __ptext150
__ptext150:

;; *************** function _ReadInput *****************
;; Defined at:
;;		line 92 in file "C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
;; Parameters:    Size  Location     Type
;;		None
;; Auto vars:     Size  Location     Type
;;		None
;; Return value:  Size  Location     Type
;;                  1    wreg      unsigned char 
;; Registers used:
;;		wreg, status,2
;; Tracked objects:
;;		On entry : 0/0
;;		On exit  : 0/0
;;		Unchanged: 0/0
;; Data sizes:     COMMON   BANK0   BANK1   BANK2
;;      Params:         0       0       0       0
;;      Locals:         0       0       0       0
;;      Temps:          0       0       0       0
;;      Totals:         0       0       0       0
;;Total ram usage:        0 bytes
;; Hardware stack levels used:    1
;; This function calls:
;;		Nothing
;; This function is called by:
;;		_main
;; This function uses a non-reentrant model
;;
psect	text150
	file	"C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
	line	92
	global	__size_of_ReadInput
	__size_of_ReadInput	equ	__end_of_ReadInput-_ReadInput
	
_ReadInput:	
	opt	stack 7
; Regs used in _ReadInput: [wreg+status,2]
	line	97
	
l2096:	
;main.c: 97: return PORTB & 0b00001111;
	bcf	status, 5	;RP0=0, select bank0
	bcf	status, 6	;RP1=0, select bank0
	movf	(6),w
	andlw	0Fh
	goto	l393
	
l2098:	
	line	98
	
l393:	
	return
	opt stack 0
GLOBAL	__end_of_ReadInput
	__end_of_ReadInput:
;; =============== function _ReadInput ends ============

	signat	_ReadInput,89
	global	_PortClear
psect	text151,local,class=CODE,delta=2
global __ptext151
__ptext151:

;; *************** function _PortClear *****************
;; Defined at:
;;		line 65 in file "C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
;; Parameters:    Size  Location     Type
;;		None
;; Auto vars:     Size  Location     Type
;;  i               1    3[COMMON] unsigned char 
;; Return value:  Size  Location     Type
;;		None               void
;; Registers used:
;;		wreg, fsr0l, fsr0h, status,2, status,0
;; Tracked objects:
;;		On entry : 0/0
;;		On exit  : 0/0
;;		Unchanged: 0/0
;; Data sizes:     COMMON   BANK0   BANK1   BANK2
;;      Params:         0       0       0       0
;;      Locals:         1       0       0       0
;;      Temps:          3       0       0       0
;;      Totals:         4       0       0       0
;;Total ram usage:        4 bytes
;; Hardware stack levels used:    1
;; This function calls:
;;		Nothing
;; This function is called by:
;;		_Init
;; This function uses a non-reentrant model
;;
psect	text151
	file	"C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\TrainSensorModule\main.c"
	line	65
	global	__size_of_PortClear
	__size_of_PortClear	equ	__end_of_PortClear-_PortClear
	
_PortClear:	
	opt	stack 6
; Regs used in _PortClear: [wreg-fsr0h+status,2+status,0]
	line	67
	
l2084:	
;main.c: 66: unsigned char i;
;main.c: 67: for(i=0; i<8; ++i)
	clrf	(PortClear@i)
	
l2086:	
	movlw	(08h)
	subwf	(PortClear@i),w
	skipc
	goto	u2381
	goto	u2380
u2381:
	goto	l2090
u2380:
	goto	l385
	
l2088:	
	goto	l385
	line	68
	
l383:	
	line	69
	
l2090:	
;main.c: 68: {
;main.c: 69: (*TRIS_Array[(i)] = ((*TRIS_Array[(i)]) & (~(1 << (i)))) | ((0&1) << (i)));
	movlw	(0)
	movwf	(??_PortClear+0)+0
	incf	(PortClear@i),w
	goto	u2394
u2395:
	clrc
	rlf	(??_PortClear+0)+0,f
u2394:
	addlw	-1
	skipz
	goto	u2395
	movf	(PortClear@i),w
	addlw	_TRIS_Array&0ffh
	movwf	fsr0
	bcf	status, 7	;select IRP bank0
	movf	indf,w
	movwf	fsr0
	movlw	(01h)
	movwf	(??_PortClear+1)+0
	incf	(PortClear@i),w
	goto	u2404
u2405:
	clrc
	rlf	(??_PortClear+1)+0,f
u2404:
	addlw	-1
	skipz
	goto	u2405
	movf	0+(??_PortClear+1)+0,w
	xorlw	0ffh
	andwf	indf,w
	iorwf	0+(??_PortClear+0)+0,w
	movwf	(??_PortClear+2)+0
	movf	(PortClear@i),w
	addlw	_TRIS_Array&0ffh
	movwf	fsr0
	movf	indf,w
	movwf	fsr0
	movf	(??_PortClear+2)+0,w
	movwf	indf
	line	67
	
l2092:	
	movlw	(01h)
	movwf	(??_PortClear+0)+0
	movf	(??_PortClear+0)+0,w
	addwf	(PortClear@i),f
	
l2094:	
	movlw	(08h)
	subwf	(PortClear@i),w
	skipc
	goto	u2411
	goto	u2410
u2411:
	goto	l2090
u2410:
	goto	l385
	
l384:	
	line	71
	
l385:	
	return
	opt stack 0
GLOBAL	__end_of_PortClear
	__end_of_PortClear:
;; =============== function _PortClear ends ============

	signat	_PortClear,88
psect	text152,local,class=CODE,delta=2
global __ptext152
__ptext152:
	global	btemp
	btemp set 07Eh

	DABS	1,126,2	;btemp
	global	wtemp0
	wtemp0 set btemp
	end
