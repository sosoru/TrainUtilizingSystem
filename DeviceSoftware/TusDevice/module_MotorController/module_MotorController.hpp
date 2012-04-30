/*
 * module_MotorController.h
 *
 * Created: 2012/04/06 22:57:57
 *  Author: Administrator
 */ 


#ifndef MODULE_MOTORCONTROLLER_H_
#define MODULE_MOTORCONTROLLER_H_

#include "avr_base.hpp"
#include "MotorProcess.hpp"
#include "Pulse.hpp"

#include <Timer.h>

namespace MotorController
{

	typedef PulseGenerator<TimerCounter2, B> PulseA;
	typedef PulseGenerator<TimerCounter1, B> PulseB;
	typedef PulseGenerator<TimerCounter2, A> PulseC;
	typedef PulseGenerator<TimerCounter1, A> PulseD;


	typedef MotorProcess<OutputPin4<PortA>, OutputPin5<PortA>, OutputPin6<PortA>, InputPin7<PortA>, 2, PulseA> MotorControllerA;
	typedef MotorProcess<OutputPin3<PortB>, OutputPin2<PortB>, OutputPin1<PortB>, InputPin0<PortB>, 3, PulseB> MotorControllerB;
	typedef MotorProcess<OutputPin3<PortD>, OutputPin2<PortD>, OutputPin1<PortD>, InputPin0<PortD>, 1, PulseC> MotorControllerC;
	typedef MotorProcess<OutputPin0<PortC>, OutputPin7<PortC>, OutputPin6<PortC>, InputPin1<PortC>, 0, PulseD> MotorControllerD;	
}

#endif /* MODULE_MOTORCONTROLLER_H_ */