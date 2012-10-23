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
		
			uint8_t m_voltage;
			
			MtrControllerPacket buffer_packet[8];
			uint8_t buffer_index;
		public :
						
			MotorProcess()
				: fb_bef(0.0f), fb_bef2(0.0f), fb_cur(0.0f), internal_duty(0.0f)
				, m_voltage(0), buffer_index(0)
			{
				memset(buffer_packet, 0x00, sizeof(buffer_packet));
				
				buffer_packet[buffer_index].DirectionValue = Standby;
				buffer_packet[buffer_index].ControlModeValue = DutySpecifiedMode;
				buffer_packet[buffer_index].DutyValue = 0;
			}
			
			inline MtrControllerPacket* CurrentPacket() { return &buffer_packet[buffer_index]; }
			
			void ApplyPacket(const MtrControllerPacket *ppacket)
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
				
				if(ppacket->ControlModeValue != CurrentFeedBackMode)
				{
					Pulse::SetDuty(ppacket->DutyValue);
				}
				else			
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
			
			void MemoryProcess(const MemoryState *pstate)
			{
				//pstate->MemoryLimit = 8;
				
				if(pstate->CurerntMemory <= pstate->MemoryLimit)
				{
					buffer_index = pstate->CurerntMemory;
				} 
			}
			
			void set_Packet(const MtrControllerPacket *ppacket)
			{				
				CurrentPacket()->ControlModeValue = ppacket->ControlModeValue;
				CurrentPacket()->DirectionValue = ppacket->DirectionValue;
				CurrentPacket()->VoltageValue = ppacket->VoltageValue;
				CurrentPacket()->DutyValue = ppacket->DutyValue;
			}				
						
			void get_Packet(MtrControllerPacket *ppacket)
			{
				ppacket->ControlModeValue = CurrentPacket()->ControlModeValue;
				ppacket->DirectionValue = CurrentPacket()->DirectionValue;
				ppacket->VoltageValue = CurrentPacket()->VoltageValue;
				ppacket->DutyValue = Pulse::GetDuty();
			}
			
			void Init()
			{		
				PulseGenerator::InitGenerator();
				AssociatedMotor::SetStandby();		
			}
		
			void Process()
			{								
				ApplyPacket(CurrentPacket());				
			}
		
		
	};	
}


#endif /* MOTORPROCESS_H_ */