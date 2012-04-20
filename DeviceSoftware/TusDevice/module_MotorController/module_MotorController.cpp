/*
 * module_MotorController.cpp
 *
 * Created: 2012/04/06 22:57:36
 *  Author: Administrator
 */ 

#include "module_MotorController.hpp"
#include "Motor.hpp"
#include "MotorProcess.hpp"
#include <util/delay.h>
#include <stdlib.h>
#include <avr/interrupt.h>
#define PI 3.14159265358979323846

using namespace AVRCpp;
using namespace MotorController;
using namespace Timer;


MotorControllerA cnta;

template <uint16_t ms>
inline void rand_wait()
{
	uint8_t rnd = rand() % 100;
	uint8_t i;
	
	for(i=0; i<rnd; ++i)
	{
		_delay_ms(ms);
	}
	
}

int16_t ar1,yz1,yz2;
inline void sin_init(float freq, float sps, float phi) {
	yz1 = sin(phi) * 256.0f;
	yz2 = sin(phi+2*PI*freq/sps) * 256.0f;
	ar1 = 2.0f*cos(2*PI*freq/sps) * 256.0f;
}

inline int16_t sin_get() {
	int16_t g;
	g   = yz1;
	yz1 = yz2;
	yz2 = ((ar1>>8)*(yz1>>8)) - g;
	return g;
}

uint16_t duty = 100;

ISR(TIMER1_OVF_vect)
{
	TimerCounter1::OutputCompareA::Set(duty + (((duty<<8) * sin_get()) >> 9) );
	//TimerCounter1::OutputCompareA::Set(duty );
}

#define SAMPLING_FREQ  19531.25f
float KQ1000[] = {0.0f, 0.0f, 0.0f, 0.0f, 300.0f, 350.0f, 392.0f, 440.0f, 466.0f, 523.0f, 587.0f, 622.0f, 698.0f, 783.0f, 900.0f, 900.0f, 900.0f, 900.0f, 900.0f, 900.0f, 900.0f, 900.0f, 900.0f, 900.0f, 900.0f, 900.0f, 900.0f};
int main(void)
{	
	MCUCR = 0b01100000; //todo: turn off bods
	MCUSR = 0; // Do not omit to clear this resistor, otherwise suffer a terrible reseting cause.
	
	{
		int8_t inc = 1, dir=1;
		
		sin_init(300.0f, SAMPLING_FREQ, 0.0f);	
		MotorControllerA::Init		

		TimerCounter1::OverflowInterrupt::Enable();
		sei();
		
		while(1)
		{			
			_delay_ms(100);
			cli();
				sin_init(KQ1000[duty / 25 ], SAMPLING_FREQ, 0.0f);			
			sei();

			if(dir > 0)
			{
				if(duty <= 400)
				{
					inc = 4;
				}
				else
				{
					inc = 2;
				}	
			}
			else
			{
				if(duty > 200)
				{
					inc = 4;	
				}
				else
				{
					inc = 2;
				}
			}		
			
			
			
			if(duty <=80)
			{
				_delay_ms(3000);
				rand_wait<300>();
				dir = 1;
				
				switch(rand() % 5)
				{
					case 1:
						MotorA::SetPositive();
						break;
					case 2:
						MotorA::SetNegative();
				}
			}
			else if(duty >= 500)
			{
				_delay_ms(5000);
				rand_wait<200>();
				dir = -1;
			}		
					
			duty += inc * dir;
		}		
    }
}