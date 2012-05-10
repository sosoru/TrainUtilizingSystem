/*
 * Pulse.hpp
 *
 * Created: 2012/04/07 4:27:02
 *  Author: Administrator
 */ 


#ifndef PULSE_H_
#define PULSE_H_

#include "avr_base.hpp"
#include <Timer.h>

namespace MotorController
{
	using namespace AVRCpp;
	using namespace Timer;
	
	enum Channel
	{
		A,
		B
	};
	
	template<class Timer>
	inline void SetupTimer()
	{
	}		
		
	template<>
	inline void SetupTimer<TimerCounter1>()
	{
		TimerCounter1::ChannelAPin::InitOutput();
		TimerCounter1::ChannelBPin::InitOutput();
		
		TimerCounter1::SetUp(NoPrescaleB, FastPWM16BitsCount8, ClearA, ClearB, Off, Fall);
		TimerCounter1::CompareMatchAInterrupt::IsEnabled();
			
	}
	
	template<>
	inline void SetupTimer<TimerCounter2>()
	{
		TimerCounter2::Synchronous::ChannelAPin::InitOutput();
		TimerCounter2::Synchronous::ChannelBPin::InitOutput();
		
		TimerCounter2::Synchronous::SetUp(NoPrescaleA, FastPWM8, ClearA, ClearB);
	}
	
	template<class Timer, Channel dir>
	inline void SetDuty(uint8_t duty)
	{
	}
	
	template<class Timer, Channel dir>
	inline uint8_t GetDuty()
	{
		return 0;
	}
	
	template<>
	inline void SetDuty<TimerCounter1, A>(uint8_t duty)
	{
		TimerCounter1::OutputCompareA::Set(duty);			
	}
	
	template<>
	inline uint8_t GetDuty<TimerCounter1, A>()
	{
		return TimerCounter1::OutputCompareA::Get();
	}
	
	template<>
	inline void SetDuty<TimerCounter1, B>(uint8_t duty)
	{
		TimerCounter1::OutputCompareB::Set(duty);
	}
	
	template<>
	inline uint8_t GetDuty<TimerCounter1, B>()
	{
		return TimerCounter1::OutputCompareB::Get();
	}
	
	template<>
	inline void SetDuty<TimerCounter2, A>(uint8_t duty)
	{
		TimerCounter2::Synchronous::OutputCompareA reg;
		reg.Set(duty);
	}
	
	template<>
	inline uint8_t GetDuty<TimerCounter2, A>()
	{
		TimerCounter2::Synchronous::OutputCompareA reg;
		return reg.Get();
	}

	template<>
	inline void SetDuty<TimerCounter2, B>(uint8_t duty)
	{
		TimerCounter2::Synchronous::OutputCompareB reg;
		reg.Set(duty);
	}
	
	template<>
	inline uint8_t GetDuty<TimerCounter2, B>()
	{
		TimerCounter2::Synchronous::OutputCompareB reg;
		return reg.Get();
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
			MotorController::SetDuty<TimerCounter, chn>(duty);
		}
		
		static inline uint8_t GetDuty()
		{
			return MotorController::GetDuty<TimerCounter, chn>();
		}
	};
}



#endif /* PULSE_H_ */