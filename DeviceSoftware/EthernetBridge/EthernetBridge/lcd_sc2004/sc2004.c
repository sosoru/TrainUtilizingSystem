/*
 * sc2004.c
 *
 * Created: 2011/12/12 8:42:14
 *  Author: root
 */ 

#include "sc2004.h"
#include <avr/io.h>

#define SET_RS(val) (SET_REC_RS(pcurrent_port, val))
#define SET_RW(val) (SET_REC_RW(pcurrent_port, val))
#define SET_ENABLE(val) (SET_REC_ENABLE(pcurrent_port, val))

#define SET_DATA(val) (SET_REC_DATA(pcurrent_port, val))
#define GET_DATA (GET_REC_DATA(pcurrent_port))

#define SET_DDR_DATA_OUT (SET_DDR_DATA(pcurrent_port, 0x00))
#define SET_DDR_DATA_IN (SET_DDR_DATA(pcurrent_port, 0xff))

*rec_sc2004_port pcurrent_port;

inline void sc2004_setPort(rec_sc2004_port * rec)
{
	cbi(*rec->pddr_rs, rec->portno_rs); //init port direction
	cbi(*rec->pddr_rw, rec->portno_rw);
	cbi(*rec->pddr_enable, rec->portno_enable);
	
	SET_DDR_DATA_OUT;
	SET_RS(0);
	SET_RW(0);
	SET_DATA(0);
	SET_ENABLE(0);
	
	pcurrent_port = rec;
}

inline uint8_t sc2004_PulseEnable()
{
	SET_ENABLE(0);
	ENABLE_NOP;
	
	SET_ENABLE(1);
	ENABLE_NOP;

	SET_ENABLE(0);
	ENABLE_NOP;	
}


inline void sc2004_ClearDisplay(void)
{
	SET_RS(0);
	SET_RW(0);
	SET_DATA(1<<DB0);
	
	sc2004_PulseEnable();
}

inline void sc2004_ReturnHome(void)
{	
	SET_RS(0);
	SET_RW(0);
	SET_DATA(1<<DB1);
	
	sc2004_PulseEnable();
}

inline void sc2004_EntryMode(uint8_t increment_direction, uint8_t display_shift)
{	
	SET_RS(0);
	SET_RW(0);
	SET_DATA( (1<<DB2)
			| (increment_direction<<DB1)
			| (display_shift<<DB0)
			);

	sc2004_PulseEnable();
} 

inline void sc2004_DisplayMode(uint8_t display, uint8_t cursor, uint8_t, cur_blink)
{
	SET_RS(0);
	SET_RW(0);
	SET_DATA( (1<<DB3)
			| (display<<DB2)
			| (cursor <<DB1)
			| (cur_blink<<DB0)
			);
	
	sc2004_PulseEnable();
}

inline void sc2004_CursorDisplayShift(uint8_t shift_display_or_cursor, uint8_t direction_right)
{
	SET_RS(0);
	SET_RW(0);
	SET_DATA( (1<<DB4)
			| (shift_display_or_cursor<<DB3)
			| (direction_right<<DB2)
			);
	
	sc2004_PulseEnable();
}	

inline void sc2004_FunctionSet(uint8_t length_is_8bit, uint8_t double_line, uint8_t wide_font)
{
	SET_RS(0);
	SET_RW(0);
	SET_DATA( (1<<DB5)
			| (length_is_8bit<<DB4)
			| (double_line << DB3)
			| (wide_font << DB2)
			);
	
	sc2004_PulseEnable();
}

inline void sc2004_SetAddr_CGRAM(uint8_t addr)
{
	SET_RS(0);
	SET_RW(0);
	SET_DATA( (1<<DB6)
			| (addr & 0b00111111)
			);
			
	sc2004_PulseEnable();
}

inline void sc2004_SetAddr_DDRAM(uint8_t addr)
{
	SET_RS(0);
	SET_RW(0);
	SET_DATA( (1 << DB7)
			| (addr & 0b01111111)
			);
			
	sc2004_PulseEnable();
}

inline uint8_t sc2004_ReadBusyAndAddress(uint8_t * addr)
{
	uint8_t data;
	
	SET_RS(0);
	SET_RW(1);
	
	sc2004_PulseEnable();
	
	data = GET_DATA;
	if(addr)
		*addr = data & 0b01111111;
		
	return data & DB7;
};

inline void sc2004_WriteData(uint8_t data)
{
	SET_RS(1);
	SET_RW(0);
	
	SET_DATA(data);
	sc2004_PulseEnable();
	
}

inline uint8_t sc2004_ReadData()
{
	SET_RS(1);
	SET_RS(1);
	
	sc2004_PulseEnable();
	
	return GET_DATA;
}

inline uint8_t sc2004_init()
{
	// busy flag cannot be read before initialization completed.
	
	delay_ms(15);
	sc2004_FunctionSet(1, 1, 1);
	delay_ms(5);
	sc2004_FunctionSet(1, 1, 1);
	delay_us(100);
	sc2004_FunctionSet(1, 1, 1);
	delay_us(37);

	sc2004_FunctionSet(1, 1, 1);
	delay_us(37);
	
	sc2004_DisplayMode(0, 0, 0);
	delay_us(37);

	sc2004_ClearDisplay();
	delay_ms(2);

	sc2004_EntryMode(1, 0);
	delay_us(37);

}
