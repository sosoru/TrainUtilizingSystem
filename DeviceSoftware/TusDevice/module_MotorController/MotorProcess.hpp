/*
 * MotorProcess.hpp
 *
 * Created: 2012/04/07 4:57:22
 *  Author: Administrator
 */ 


#ifndef MOTORPROCESS_H_
#define MOTORPROCESS_H_

#include <avr/io.h>
#include "mtrPacket.hpp"
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
					 uint8_t AdcNum,
					 class Pulse
				>
	class MotorProcess
	{
		private :
			
			typedef Pulse PulseGenerator;
			typedef Motor<StandByPort, 
						 Out1Port, 
						 Out2Port, 
						 AlertPort
						> AssociatedMotor;
		
			float fb_bef2;
			float fb_bef;
			float fb_cur;
			float internal_duty;
		
			ControlMode m_mode;			
			Direction m_dir;
			uint8_t m_voltage;
			
		public :
						
			MotorProcess()
				: fb_bef(0.0f), fb_bef2(0.0f), fb_cur(0.0f), internal_duty(0.0f), m_voltage(0), m_mode(DutySpecifiedMode), m_dir(Standby)
			{
			}
			
			void set_Packet(const MtrControllerPacket *ppacket)
			{
				switch(ppacket->DirectionValue)
				{
					case Positive:
						AssociatedMotor::SetPositive();
						break;
					case Negative:
						AssociatedMotor::SetNegative();
						break;
					case Standby:
					default:
						AssociatedMotor::SetStandby();
						break;	
				}
				
				this->m_dir = ppacket->DirectionValue;
				this->m_voltage = ppacket->VoltageValue;
				this->m_mode = ppacket->ControlModeValue;
				Pulse::SetDuty(ppacket->DutyValue);
			}				
			
			void get_Packet(MtrControllerPacket *ppacket)
			{
				ppacket->ControlModeValue = this->m_mode;
				ppacket->DirectionValue = this->m_dir;
				ppacket->VoltageValue = this->m_voltage;
				ppacket->DutyValue = Pulse::GetDuty();
			}
			
			void Init()
			{		
				PulseGenerator::InitGenerator();
				AssociatedMotor::SetStandby();		
			}
		
			void Process()
			{								
				if(this->m_mode != CurrentFeedBackMode)
					return;
					
				{
					float result;
					
					InputPins<PortA, AdcNum>::InitDefaultInput();
					
					AnalogToDigital::ControlSetUp(ADCEnable, StartLater, FreeRunStopped, InterruptDisable, Div128);
					AnalogToDigital::SelectionSetUp(AVCC, AlignLeft, (AnalogChannel)AdcNum);		
					AnalogToDigital::StartConversion();
					AnalogToDigital::WaitWhileConverting();
					
					result = (float) ADCH;
					
					//fb_bef2 = fb_bef;
					fb_bef = fb_cur;
					fb_cur = (float)(m_voltage) - result;
		
					internal_duty +=  ((fb_cur - fb_bef) * 0.1f)  + (fb_cur * 0.3f);
					
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