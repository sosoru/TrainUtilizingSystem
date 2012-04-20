/*
 * module_SensorControl.c
 *
 * Created: 2012/02/06 2:11:41
 *  Author: root
 */ 

#include <avr/io.h>
#include <avr/pgmspace.h>
#include <inttypes.h>
#include <avr/delay.h>
#include "parent_suart.h"

//typedef struct tag_TrSensPort
//{
	//TUPLE_PORT portLed;
	//TUPLE_PORT portTrans;
	//TUPLE_PORT portChain;
//} TrSensPort;
//
//typedef TrSensPort TrSensDir;
//
//#define SENS_LED_OFF(sens) cbi_p((sens).portLed)
//#define SENS_LED_ON(sens) sbi_p((sens).portLed)
//
//#define SENS_PORT_A {{&PORTA, 0}, {&PORTA, 1}, {&PORTA, 2}}
//#define SENS_DIR_A  {{&DDRA, 0}, {&DDRA, 1}, {&DDRA, 2}}
//
//TrSensPort g_SensPorts[] = {SENS_PORT_A};
//TrSensDir g_SensDirs[] = {SENS_DIR_A};
	//
//static TrSensPort cur_sens = &g_SensPorts[0];
//
//static uint8_t cur_buffer = 0;
//
//ISR(TIMER1_COMPA_vect)
//{
	//
//}
//
//void sens_init(TrSensPort * psens, TrSensDir * pdir)
//{
	//cur_sens = psens;
	//
	//sbi_p(pdir->portLed); //led port : output
	//sbi_p(pdir->portChain); //chain port : output
	//cbi_p(pdir->portTrans); //trans port : input
//}	
//
//void sens_numbering()
//{
	//
//}	

//
//void sens_send(uint8_t data)
//{
	////timer start
	//
	//
//}	
	//
int main(void)
{
    while(1)
    {
        //TODO:: Please write your application code 
		
	const prog_char *s;
	char c = ' ';
	uint8_t i;

	/* Initialize I/O ports */
	DDRB  = 0b01000000;
	PORTB = 0b10111111;	/* PB6:Inv-Tx, PB5:Inv-Rx */
	//PORTB = 0b01111111;	/* Pull-up Port D */
	

	/* Send message */
	//_delay_ms(10);
	for (i=0; i<255; ++i) {
		
		c++;
		if(c=='z')
		{
			c = ' ';
			_delay_ms(10);
		}			
		
		xmit(c);		
		
	}

	/* Receives data and echos it in incremented data */
	for(;;) {
		c = rcvr();
		c++;
		xmit(c);
	}

    }
}