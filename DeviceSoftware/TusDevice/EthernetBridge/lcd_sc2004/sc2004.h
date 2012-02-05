/*
 * sc2004.h
 *
 * Created: 2011/12/12 8:42:58
 *  Author: root
 */ 


#ifndef SC2004_H_
#define SC2004_H_

#include "../global.h"
#include <avr/io.h>
#include <util/delay.h>

typedef struct rec_sc2004_port
{
	volatile uint8_t *pport_data;
	volatile uint8_t *ppin_data;
	volatile uint8_t *pddr_data;
	
	volatile uint8_t *pport_rs;
	volatile uint8_t *ppin_rs;
	volatile uint8_t *pddr_rs;
	
	volatile uint8_t *pport_rw;
	volatile uint8_t *ppin_rw;
	volatile uint8_t *pddr_rw;
	
	volatile uint8_t *pport_enable;
	volatile uint8_t *ppin_enable;
	volatile uint8_t *pddr_enable;
	
	uint8_t portno_rs;
	uint8_t portno_rw;
	uint8_t portno_enable;
	
} sc2004_port;

#define SET_REC_RS(rec, val) ((val) ? sbi(*(rec)->pport_rs, (rec)->portno_rs) : cbi(*(rec)->pport_rs, (rec)->portno_rs))
#define SET_REC_RW(rec, val) ((val) ? sbi(*(rec)->pport_rw, (rec)->portno_rw) : cbi(*(rec)->pport_rw, (rec)->portno_rw) )
#define SET_REC_ENABLE(rec, val) ((val) ? sbi(*(rec)->pport_enable, (rec)->portno_enable) : cbi(*(rec)->pport_enable, (rec)->portno_enable) )

#define GET_REC_DATA(rec) (*((rec)->ppin_data))
#define SET_REC_DATA(rec, val) (*((rec)->pport_data) = (val) )

#define SET_REC_DDR_DATA(rec, dir) (*((rec)->pddr_data) = (dir))

#define ENABLE_NOP _delay_us(1) // asm volatile ("nop\n nop\n nop\n nop\n") // for 16Mhz

#define DB0 0
#define DB1 1
#define DB2 2
#define DB3 3
#define DB4 4
#define DB5 5
#define DB6 6
#define DB7 7

#define LCD_WIDTH 20
#define LCD_HEIGHT 4

 void sc2004_setPort(sc2004_port * rec);
 void sc2004_PulseEnable();
 void sc2004_ClearDisplay(void);
 void sc2004_ReturnHome(void);
 void sc2004_EntryMode(uint8_t increment_direction, uint8_t display_shift);
 void sc2004_DisplayMode(uint8_t display, uint8_t cursor, uint8_t cur_blink);
 void sc2004_CursorDisplayShift(uint8_t shift_display_or_cursor, uint8_t direction_right);
 void sc2004_FunctionSet(uint8_t length_is_8bit, uint8_t double_line, uint8_t wide_font);
 void sc2004_SetAddr_CGRAM(uint8_t addr);
 void sc2004_SetAddr_DDRAM(uint8_t addr);
 uint8_t sc2004_ReadBusyAndAddress(uint8_t * addr);
 void sc2004_WriteData(uint8_t data);
 uint8_t sc2004_ReadData();
 void sc2004_init();

#endif /* SC2004_H_ */