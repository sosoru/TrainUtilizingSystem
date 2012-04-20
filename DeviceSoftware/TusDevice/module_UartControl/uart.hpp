/*
 * uart.hpp
 *
 * Created: 2012/03/23 5:09:55
 *  Author: Administrator
 */ 


#ifndef UART_HPP_
#define UART_HPP_

#include <avrlibdefs.h>
#include <avr/io.h>

#define PTR_REG volatile uint8_t* 

template<PTR_REG pport, uint8_t num>
struct Pin
{
	bool read()			{ return *pport & (1 << num); }
	
	bool is_high()		{ return read(); }
	bool is_low()		{ return !is_high(); }
		
	bool set_high()		{ *pport |= (1 << num); }
	bool set_low()		{ *pport &= ~(1 << num); }

};		

#define TEMP_ARG_UART_UDR pUDRn
#define TEMP_ARG_UART_UCSRNA pUCSRnA
#define TEMP_ARG_UART_UCSRNB pUCSRnB
#define TEMP_ARG_UART_UCSRNC pUCSRnC
#define TEMP_ARG_UART_UBRRNL pUBRRnL
#define TEMP_ARG_UART_UBRRNH pUBRRnH

#define TEMPLATE_PARAM_UART PTR_REG TEMP_ARG_UART_UDR, \
							PTR_REG TEMP_ARG_UART_UCSRNA, \
							PTR_REG TEMP_ARG_UART_UCSRNB, \
							PTR_REG TEMP_ARG_UART_UCSRNC, \
							PTR_REG TEMP_ARG_UART_UBRRNL, \
							PTR_REG TEMP_ARG_UART_UBRRNH 

#define TEMPLATE_PARAM_DEFINE_UART   TEMP_ARG_UART_UDR, \
									 TEMP_ARG_UART_UCSRNA, \
									 TEMP_ARG_UART_UCSRNB, \
									 TEMP_ARG_UART_UCSRNC, \
									 TEMP_ARG_UART_UBRRNL, \
									 TEMP_ARG_UART_UBRRNH 
									 
#define def_member_uart Uart<TEMPLATE_PARAM_DEFINE_UART>

template<TEMPLATE_PARAM_UART>
class Uart
{
	public :
		
	Uart(uint16_t baud)
	{
		/* Set baud rate */
		*pUBRRnH = (unsigned char)(baud >> 8);
		*pUBRRnL = (unsigned char)baud;
		/* Enable receiver and transmitter */
		*pUCSRnB = (1<<RXEN0)|(1<<TXEN0);
		/* Set frame format: 8data, 2stop bit */
		*pUCSRnC = (1<<USBS0)|(3<<UCSZ00);
	}
	
	void send(uint8_t data)
	{
		while(!is_register_empty());
		*pUDRn = data;
	}
	
	uint8_t receive()
	{
		while(!is_receive_completed());
		return *pUDRn;
	}
	
	bool is_register_empty()		{ return *pUCSRnA & BV(UDRE0); }
	bool is_receive_completed()		{ return *pUCSRnA & BV(RXC0); }	
	bool is_transmit_completed()	{ return *pUCSRnA & BV(TXC0); }
	bool is_frame_error()			{ return *pUCSRnA & BV(FE0); }
	
	bool is_receiver_enabled()		{ return *pUCSRnB & BV(RXEN0); }
	bool is_transmitter_enabled()	{ return *pUCSRnB & BV(TXEN0); }
};



#endif /* UART_HPP_ */