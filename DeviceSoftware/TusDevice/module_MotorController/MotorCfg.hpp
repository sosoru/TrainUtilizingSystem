/*
 * MotorCfg.hpp
 *
 * Created: 2012/10/30 13:59:38
 *  Author: Administrator
 */ 


#ifndef MOTORCFG_H_
#define MOTORCFG_H_

#include "MotorProcess.hpp"
#include "Pulse.hpp"


namespace MotorController
{

	typedef PulseGenerator<TimerCounter2, B> PulseA;
	typedef PulseGenerator<TimerCounter1, B> PulseB;
	typedef PulseGenerator<TimerCounter2, A> PulseC;
	typedef PulseGenerator<TimerCounter1, A> PulseD;


	typedef MotorProcess<1, OutputPin4<PortA>, OutputPin5<PortA>, OutputPin6<PortA>, InputPin7<PortA>, 2, PulseA> MotorControllerA;
	typedef MotorProcess<2, OutputPin3<PortB>, OutputPin2<PortB>, OutputPin1<PortB>, InputPin0<PortB>, 3, PulseB> MotorControllerB;
	typedef MotorProcess<3, OutputPin3<PortD>, OutputPin2<PortD>, OutputPin1<PortD>, InputPin0<PortD>, 1, PulseC> MotorControllerC;
	typedef MotorProcess<4, OutputPin0<PortC>, OutputPin7<PortC>, OutputPin6<PortC>, InputPin1<PortC>, 0, PulseD> MotorControllerD;
}




#endif /* MOTORCFG_H_ */