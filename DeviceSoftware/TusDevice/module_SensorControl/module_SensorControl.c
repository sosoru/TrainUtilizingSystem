/*
 * module_SensorControl.c
 *
 * Created: 2012/02/06 2:11:41
 *  Author: root
 */ 

#include <avr/io.h>

typedef struct tag_TrSensPort
{
	TUPLE_PORT portLed;
	TUPLE_PORT portTrans;
	TUPLE_PORT portChain;
} TrSensPort;

typedef TrSensPort TrSensDir;

#define SENS_LED_OFF(sens) cbi_p((sens).portLed)
#define SENS_LED_ON(sens) sbi_p((sens).portLed)

#define SENS_PORT_A {{&PORTA, 0}, {&PORTA, 1}, {&PORTA, 2}}
#define SENS_DIR_A  {{&DDRA, 0}, {&DDRA, 1}, {&DDRA, 2}}

TrSensPort g_SensPorts[] = {SENS_PORT_A};
TrSensDir g_SensDirs[] = {SENS_DIR_A};
	
static TrSensPort cur_sens = &g_SensPorts[0];

static uint8_t cur_buffer = 0;

ISR(TIMER1_COMPA_vect)
{
	
}

void sens_init(TrSensPort * psens, TrSensDir * pdir)
{
	cur_sens = psens;
	
	sbi_p(pdir->portLed); //led port : output
	sbi_p(pdir->portChain); //chain port : output
	cbi_p(pdir->portTrans); //trans port : input
}	

void sens_numbering()
{
	
}	

void sens_send(uint8_t data)
{
	//timer start
	
	
}	
	
int main(void)
{
    while(1)
    {
        //TODO:: Please write your application code 
		
		
    }
}