#include "module_MotorController.h"
#include "motor_adc.h"
#include <avr/io.h>
#include <avr/interrupt.h>

void adc_init()
{
	sbi(ACSR, ACD);		// disable analog comparator
	
	cbi(ADMUX, REFS1);	// internal Vref 
	sbi(ADMUX, REFS0);
	
	sbi(ADMUX, ADLAR);	// left adjust
	
	//sbi(ADCSRA, ADIE); // enable A/D converter interrupt
	
	sbi(ADCSRA, ADEN);	// enable A/D converter
	
	sbi(ADCSRA, ADPS2);	
	sbi(ADCSRA, ADPS1);
	sbi(ADCSRA, ADPS0);	// ADC sampling frequency = f_osc / 128

}

#define ADC_MASK (0b00000111)
inline void adc_change(uint8_t channel)
{
	ADMUX &= ~ADC_MASK; //clear
	ADMUX |= ADC_MASK & channel;
	
	DIDR0 = 0;			// disable digital input
	DIDR0 = BV(channel);
}

inline void adc_start()
{
	cbi(ADCSRA, ADIF); //clear interrupt flag
	sbi(ADCSRA, ADSC); //start conversion
}

inline void adc_wait()
{
	while(!(ADCSRA & BV(ADIF)));
}

inline uint8_t adc_result()
{
	return ADCH;
}

