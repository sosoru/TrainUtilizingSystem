/*
 * Motor.hpp
 *
 * Created: 2012/04/07 1:30:53
 *  Author: Administrator
 */ 


#ifndef MOTOR_H_
#define MOTOR_H_

#include "avr_base.hpp"

namespace MotorController
{
	using namespace AVRCpp;
		
	template	<class PortStandBy,	
				 class PortOut1,	
				 class PortOut2,	
				 class PortAlert>
	class Motor
	{
		public:
			
			static inline void SetStandby()
			{
				PortStandBy::InitOutput();
				PortOut1::InitOutput();
				PortOut2::InitOutput();
				PortAlert::InitInput();
				
				PortStandBy::SetTo(false);
			}
			
			static inline void SetPositive()
			{
				PortOut1::SetTo(true);
				PortOut2::SetTo(false);
				PortStandBy::SetTo(true);
			}
			
			static inline void SetNegative()
			{
				PortOut1::SetTo(false);
				PortOut2::SetTo(true);
				PortStandBy::SetTo(true);
			}
			
			static inline void SetStop()
			{
				PortOut1::SetTo(false);
				PortOut2::SetTo(false);
				PortStandBy::SetTo(true);
			}
			
			static inline void SetBrake()
			{
				PortOut1::SetTo(true);
				PortOut2::SetTo(true);
				PortStandBy::SetTo(true);
			}
			
			static inline bool IsAlerted()
			{
				return PortAlert::IsSet();
			}
	};
}





#endif /* MOTOR_H_ */