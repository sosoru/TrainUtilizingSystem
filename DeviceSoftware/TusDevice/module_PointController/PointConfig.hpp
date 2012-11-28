/*
 * PointConfig.h
 *
 * Created: 2012/05/01 7:38:54
 *  Author: Administrator
 */ 


#ifndef POINTCONFIG_H_
#define POINTCONFIG_H_

#include "module_PointController.hpp"
#include "PointModule.hpp"

namespace module_PointController
{
	namespace Config
	{
		using namespace AVRCpp;
		
		typedef PointModule<OutputPin0<PortA>, OutputPin1<PortA>, A> PointA;
		typedef PointModule<OutputPin2<PortA>, OutputPin3<PortA>, B> PointB;
		typedef PointModule<OutputPin4<PortA>, OutputPin5<PortA>, C> PointC;
		typedef PointModule<OutputPin6<PortA>, OutputPin7<PortA>, D> PointD;
		typedef PointModule<OutputPin0<PortD>, OutputPin1<PortD>, E> PointE;
		typedef PointModule<OutputPin2<PortD>, OutputPin3<PortD>, F> PointF;
		typedef PointModule<OutputPin4<PortD>, OutputPin5<PortD>, G> PointG;
		typedef PointModule<OutputPin6<PortD>, OutputPin7<PortD>, H> PointH;
	}	
}


#endif /* POINTCONFIG_H_ */