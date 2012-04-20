/*
 * Pulse.hpp
 *
 * Created: 2012/04/07 4:27:02
 *  Author: Administrator
 */ 


#ifndef PULSE_H_
#define PULSE_H_

#include "module_MotorController.hpp"

namespace MotorController
{
	using namespace AVRCpp;
	using namespace Timer;
	
	enum Channel
	{
		A,
		B
	};
	
	template<>
	inline void SetupTimer<TimerCounter1>()
	{
		TimerCounter1::ChannelAPin::InitOutput();
		TimerCounter1::ChannelBPin::InitOutput();
		
		TimerCounter1::SetUp(NoPrescaleB, FastPWM16BitsCount8, ClearA, ClearB ,Off, Fall);
		
	}
	
	template<>
	inline void SetUpTimer<TimerCounter2>()
	{
		TimerCounter2::Synchronous::ChannelAPin::InitOutput();
		TimerCounter2::Synchronous::ChannelBPin::InitOutput();
		
		TimerCounter2::Synchronous::SetUp(NoPrescaleA, FastPWM8, ClearA, ClearA);
	}
	
	template<>
	inline void SetDuty<TimerCounter1, A>(uint8_t duty)
	{
		TimerCounter1::OutputCompareA::Set(duty);			
	}
	
	template<>
	inline void SetDuty<TimerCounter1, B>(uint8_t duty)
	{
		TimerCounter1::OutputCompareB::Set(duty);
	}
	
	template<>
	inline void SetDuty<TimerCounter2, A>(uint8_t duty)
	{
		TimerCounter2::Synchronous::OutputCompareA::Set(duty);
	}
	
	template<>
	inline void SetDuty<TimerCounter2, B>(uint8_t duty)
	{
		TimerCounter2::Synchronous::OutputCompareB::Set(duty);
	}
	
	template<class TimerCounter, Channel chn>
	class PulseGenerator
	{	
		public:
			
		static inline void InitGenerator()
		{
			SetupTimer<TimerCounter>();
		}
		
		static inline void SetDuty(uint8_t duty)
		{
			SetDuty(duty);
		}
	};
}



#endif /* PULSE_H_ */