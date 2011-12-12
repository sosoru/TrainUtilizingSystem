/*
 * sc2004.h
 *
 * Created: 2011/12/12 8:42:58
 *  Author: root
 */ 


#ifndef SC2004_H_
#define SC2004_H_

#include <avr/io.h>

typedef struct rec_sc2004_port
{
	*volatile uint8_t pport_data;
	*volatile uint8_t ppin_data;
	*volatile uint8_t pddr_data;
	
	*volatile uint8_t pport_rs;
	*volatile uint8_t ppin_rs;
	*volatile uint8_t pddr_rs;
	
	*volatile uint8_t pport_rw;
	*volatile uint8_t ppin_rw;
	*volatile uint8_t pddr_rw;
	
	*volatile uint8_t pport_enable;
	*volatile uint8_t ppin_enable;
	*volatile uint8_t pddr_enable;
	
	uint8_t portno_rs;
	uint8_t portno_rw;
	uint8_t portno_enable;
	
} sc2004_port;

#define sbi(PORT,BIT) (PORT) |= _BV(BIT) //set high
#define cbi(PORT,BIT) (PORT) &=~_BV(BIT) //set low

#define SET_REC_RS(rec, val) ((val) ? sbi(*(rec)->pport_rs, (rec)->portno_rs) : cbi(*(rec)->pport_rs, (rec)->portno_rs) )
#define SET_REC_RW(rec, val) ((val) ? sbi(*(rec)->pport_rw, (rec)->portno_rw) : cbi(*(rec)->pport_rw, (rec)->portno_rw) )
#define SET_REC_ENABLE(rec, val) ((val) ? sbi(*(rec)->pport_enable, (rec)->portno_enable) : cbi(*(rec)->pport_enable, (rec)->portno_enable) )

#define GET_REC_DATA(rec) (*((rec)->ppin_data))
#define SET_REC_DATA(rec, val) (*((rec)->pport_data) = (val) )

#define SET_REC_DDR_DATA(rec, dir) (*((rec)->pddr_data) = (val))

#define ENABLE_NOP asm volatile ("nop\n nop\n nop\n nop\n") // for 16Mhz

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

#endif /* SC2004_H_ */