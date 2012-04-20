/*
 * MotorProcess.hpp
 *
 * Created: 2012/04/07 4:57:22
 *  Author: Administrator
 */ 


#ifndef MOTORPROCESS_H_
#define MOTORPROCESS_H_

#include <avr/io.h>
#include "module_MotorController.hpp"
#include "Pulse.hpp"
#include "Motor.hpp"

namespace MotorController
{
	using namespace AVRCpp;
	using namespace AnalogToDigital;
	
	template	<class StandByPort,	
					 class Out1Port,	
					 class Out2Port,	
					 class AlertPort,	
					 class AdcNum,
					 class Pulse
				>
	class MotorProcess
	{
		private :
			
			typedef Motor<StandByPort, 
						 Out1Port, 
						 Out2Port, 
						 AlertPort,
						> AssociatedMotor;
		
			float fb_bef2;
			float fb_bef;
			float fb_cur;
			float internal_duty;
			
		public :
			
			uint8_t duty;
			MotorProcess()
				: fb_bef(0.0f), fb_bef2(0.0f), fb_cur(0.0f), internal_duty(0.0f), duty(0)
			{
			}
			
			void Init()
			{		
				PulseGenerator<Timer>::InitGenerator();
				AssociatedMotor::SetStandby();		
			}
		
			void Process()
			{				
				
				{
					float result;
					
					AnalogToDigital::ControlSetUp(ADCEnable, StartLater, FreeRunStopped, InterruptDisable, Div128);
					AnalogToDigital::SelectionSetUp(AVCC, AlignLeft, AdcNum);		
					AnalogToDigital::StartConversion();
					AnalogToDigital::WaitWhileConverting();
					
					result = (float) ADCH;
					
					//fb_bef2 = fb_bef;
					fb_bef = fb_cur;
					fb_cur = (float)(duty) - result;
		
					internal_duty +=  ((fb_cur - fb_bef) * 0.5f)  + (fb_cur * 0.1f);
					
					if(internal_duty > 150.0f)
						internal_duty = 150.0f;
					else if(internal_duty < -150.0f)
						internal_duty = -150.0f;
							
					if(internal_duty<0.0f)
						Pulse::SetDuty(0);
					else
						Pulse::SetDuty((uint16_t)internal_duty);

				}
				
			}
		
		
	};	
}


#endif /* MOTORPROCESS_H_ */