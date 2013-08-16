/*
* MotorProcess.hpp
*
* Created: 2012/04/07 4:57:22
*  Author: Administrator
*/


#ifndef MOTORPROCESS_H_
#define MOTORPROCESS_H_

#include <avr/io.h>
#include <avr/delay.h>
#include <mtr_packet.h>
#include <PackPacket.hpp>
#include "Pulse.hpp"
#include "Motor.hpp"
#include "module_MotorController.h"

#define MTRPROCESS_MEM_CAPACITY 8

namespace MotorController
{
	using namespace AVRCpp;
	using namespace AnalogToDigital;
	
	template	<	 uint8_t t_internal_id,
	class StandByPort,
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
		Direction m_before_dir;
		
		MtrControllerPacket m_buffer_packet[MTRPROCESS_MEM_CAPACITY];
		uint8_t m_buffer_index;
		public :
		
		MotorProcess()
		: fb_bef(0.0f), fb_bef2(0.0f), fb_cur(0.0f), internal_duty(0.0f)
		, m_voltage(0), m_buffer_index(0), m_before_dir(Standby)
		{
			MtrRunningState *pstate = (MtrRunningState*)CurrentPacket()->State;
			
			CurrentPacket()->Base.ModuleType = MODULETYPE_MOTOR;
			CurrentPacket()->Base.DataLength = sizeof(MtrControllerPacket);
			CurrentPacket()->Base.InternalAddr = t_internal_id;
			CurrentPacket()->ControlModeValue = DutySpecifiedMode;
			CurrentPacket()->CurrentMemory = 0;
			pstate->DirectionValue = Standby;
			pstate->DutyValue = 0;
			pstate->CurrentValue = 0;
			
		}
		
		inline MtrControllerPacket* CurrentPacket() { return &m_buffer_packet[m_buffer_index]; }
		
		void ApplyPacket(const MtrControllerPacket *ppacket)
		{
			MtrRunningState *pstate = (MtrRunningState*)ppacket->State;
			
			switch(ppacket->ControlModeValue)
			{
				case DutySpecifiedMode:
				ApplyDutyMode(pstate);
				break;
				case CurrentFeedBackMode:
				ApplyCurrentMode(pstate)	;
				break;
				case WaitingPulseMode:
				ApplyWaiting(pstate);
				break;
			}
		}
		
		void ApplyDirection(Direction dir)
		{			
			switch(dir)
			{
				case Positive:
				AssociatedMotor::SetPositive();
				break;
				case Negative:
				AssociatedMotor::SetNegative();
				break;
				case Standby:
				AssociatedMotor::SetStandby();
				break;
				default:
					return;
				break;
			}
			
			if(dir != m_before_dir && (dir == Standby || m_before_dir == Standby))
			{
				_delay_ms(25);
			}
			m_before_dir = dir;
		}
		
		uint8_t MeisureCurrent()
		{
			InputPins<PortA, AdcNum>::InitDefaultInput();
			
			AnalogToDigital::ControlSetUp(ADCEnable, StartLater, FreeRunStopped, InterruptDisable, Div128);
			AnalogToDigital::SelectionSetUp(AVCC, AlignLeft, (AnalogChannel)AdcNum);
			AnalogToDigital::StartConversion();
			AnalogToDigital::WaitWhileConverting();
			
			return ADCH;
		}
		
		void ApplyDutyMode(MtrRunningState *pstate)
		{
			pstate->CurrentValue = MeisureCurrent();
			
			ApplyDirection(pstate->DirectionValue);
			//if(pstate ->Accelation == 0) // if acceleration is enabled set duty immediately
			//{
				Pulse::SetDuty(pstate->DutyValue);
			//}
		}
		
		//void AccelControl()
		//{
		//const MtrRunningState* pstate = (MtrRunningState*)CurrentPacket()->State;
		//uint8_t newvalue;
		//
		//if(pstate->Accelation == 0)
		//return;
		//
		//if(Pulse::GetDuty() > pstate->DutyValue)
		//{
		//newvalue = Pulse::GetDuty() - pstate->Accelation;
		//if(newvalue < pstate->DutyValue)
		//{
		//newvalue = pstate->DutyValue;
		//}
		//
		//}else if(Pulse::GetDuty() < pstate -> DutyValue)
		//{
		//newvalue = Pulse::GetDuty() + pstate->Accelation;
		//if(newvalue > pstate -> DutyValue)
		//{
		//newvalue = pstate->DutyValue;
		//}
		//}else //equals
		//{
		//newvalue = pstate->DutyValue;
		//}
		//Pulse::SetDuty(newvalue);
		//}
		//
		void ApplyCurrentMode(MtrRunningState* pstate)
		{
			ApplyDirection(pstate->DirectionValue);
			
			float result;
			
			result = (float) MeisureCurrent();
			
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
		
		void ApplyWaiting(MtrRunningState *pstate)
		{
			uint8_t curr;
			
			ApplyDirection(Positive);
			Pulse::SetDuty(0);
			
			curr = MeisureCurrent();
			
			if(curr > 15 /*pstate->ThresholdValue*/) // entered
			{
				KernalState kstate;
				MemoryState *pmstate = (MemoryState*)kstate.pdata;
				
				kstate.Base.DataLength = 8;
				kstate.Base.InternalAddr = pstate->DestinationID.InternalAddr;
				kstate.Base.ModuleType = MODULETYPE_KERNEL;
				kstate.KernelCommand = ETHCMD_MEMORY;
				pmstate->CurerntMemory = pstate->DestinationMemory;
				
				if( (pstate->DestinationID.raw & 0x000000FF) == (g_myDeviceID.raw & 0x000000FF))
				{
					ProcessKernelPacket(&kstate, &g_myDeviceID, &pstate->DestinationID);
				}
				else{
					if(!g_packer.IsInitialized())
					{
						g_packer.Init();
						
						g_packer.Pack((uint8_t*)&kstate);
						g_packer.Send(&g_myDeviceID, &pstate->DestinationID);
					}
				}
				
				if(pstate->MemoryAfterEntered < MTRPROCESS_MEM_CAPACITY )
					m_buffer_index = pstate->MemoryAfterEntered;
			}
		}
		
		void MemoryProcess(const MemoryState *pstate)
		{
			if(pstate->CurerntMemory < MTRPROCESS_MEM_CAPACITY)
			{
				m_buffer_index = pstate->CurerntMemory;
			}
		}
		
		void set_Packet(const MtrControllerPacket *ppacket)
		{
			MtrRunningState* pstate = (MtrRunningState*)ppacket->State;
			uint8_t targetmem = ppacket->CurrentMemory;
			
			if(targetmem >= MTRPROCESS_MEM_CAPACITY) // if targetmem is over the capacity
			{
				return; // do nothing
			}
			
			memcpy(&m_buffer_packet[targetmem], ppacket, sizeof(*ppacket));
		}
		
		void get_KernelState(KernalState *pstate)
		{
			pstate->Base.InternalAddr = t_internal_id;
			pstate->Base.ModuleType = MODULETYPE_KERNEL;
			pstate->Base.DataLength = sizeof(KernalState);
			pstate->KernelCommand = ETHCMD_MEMORY;
			
			MemoryState *pmem = (MemoryState*)pstate->pdata;
			pmem->CurerntMemory = m_buffer_index;
			pmem->MemoryLimit = MTRPROCESS_MEM_CAPACITY;
		}
		
		void get_Packet(MtrControllerPacket *ppacket)
		{
			memcpy(ppacket, CurrentPacket(), sizeof(*ppacket));
		}
		
		void PackPacket(Tus::PacketPacker *ppack)
		{
			MtrControllerPacket *ppacket = CurrentPacket();
			MtrRunningState* pstate = (MtrRunningState*)ppacket->State;
		
			ppacket->Base.ModuleType = MODULETYPE_MOTOR;
			ppacket->Base.DataLength = sizeof(MtrControllerPacket);
			ppacket->Base.InternalAddr = t_internal_id;
			ppacket->CurrentMemory = m_buffer_index;

			ppack->Pack((uint8_t*)ppacket);
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