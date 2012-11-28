/*
 * MotorProcess.hpp
 *
 * Created: 2012/04/07 4:57:22
 *  Author: Administrator
 */ 


#ifndef MOTORPROCESS_H_
#define MOTORPROCESS_H_

#include <avr/io.h>
#include <mtr_packet.h>
#include <PackPacket.hpp>
#include "Pulse.hpp"
#include "Motor.hpp"
#include "module_MotorController.h"

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
			
			MtrControllerPacket buffer_packet[8];
			uint8_t buffer_index;
		public :
						
			MotorProcess()
				: fb_bef(0.0f), fb_bef2(0.0f), fb_cur(0.0f), internal_duty(0.0f)
				, m_voltage(0), buffer_index(0)
			{
				MtrRunningState *pstate = (MtrRunningState*)CurrentPacket()->State;							
				
				CurrentPacket()->Base.ModuleType = MODULETYPE_MOTOR;
				CurrentPacket()->Base.DataLength = 7;
				CurrentPacket()->Base.InternalAddr = t_internal_id;
				CurrentPacket()->ControlModeValue = DutySpecifiedMode;
				pstate->DirectionValue = Standby;
				pstate->DutyValue = 0;
				pstate->VoltageValue = 0;
			}
			
			inline MtrControllerPacket* CurrentPacket() { return &buffer_packet[buffer_index]; }
			
			void ApplyPacket(const MtrControllerPacket *ppacket)
			{				
				switch(ppacket->ControlModeValue)
				{
					case DutySpecifiedMode:
						ApplyDutyMode((MtrRunningState*)ppacket->State);
					break;
					case CurrentFeedBackMode:
						ApplyCurrentMode((MtrRunningState*) ppacket->State)	;
					break;
					case WaitingPulseMode:
						ApplyWaiting((MtrRunningState*)ppacket->State);
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
					default:
					AssociatedMotor::SetStandby();
					break;
				}				
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
				pstate->VoltageValue = MeisureCurrent();
				
				ApplyDirection(pstate->DirectionValue);
				Pulse::SetDuty(pstate->DutyValue);
			}
			
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
				
				if(curr > pstate->ThresholdValue) // entered
				{			
					KernalState kstate;		
					MemoryState *pmstate = (MemoryState*)kstate.pdata;
					
					kstate.Base.DataLength = 8;
					kstate.Base.InternalAddr = pstate->DestinationID.InternalAddr;
					kstate.Base.ModuleType = MODULETYPE_KERNEL;
					kstate.KernelCommand = ETHCMD_MEMORY;
					pmstate->CurerntMemory = pstate->DestinationMemory;
					
					if(!g_packer.IsInitialized())
					{
						g_packer.Init();
						
						g_packer.Pack((uint8_t*)&kstate);
						g_packer.Send(&g_myDeviceID, &pstate->DestinationID);
					}						
					
					buffer_index = pstate->MemoryAfterEntered;
				}
			}
			
			void MemoryProcess(const MemoryState *pstate)
			{
				//pstate->MemoryLimit = 8;
				
				//if(pstate->CurerntMemory <= pstate->MemoryLimit)
				//{
					buffer_index = pstate->CurerntMemory;
				//} 
			}
			
			void set_Packet(const MtrControllerPacket *ppacket)
			{				
				memcpy(CurrentPacket(), ppacket, sizeof(*ppacket));

			}				
			
			void get_KernelState(KernalState *pstate)
			{
				pstate->Base.InternalAddr = t_internal_id;
				pstate->Base.ModuleType = MODULETYPE_KERNEL;
				pstate->Base.DataLength = sizeof(KernalState);
				pstate->KernelCommand = ETHCMD_MEMORY;
				
				MemoryState *pmem = (MemoryState*)pstate->pdata;
				pmem->CurerntMemory = buffer_index;
				pmem->MemoryLimit = 8;
			}				
						
			void get_Packet(MtrControllerPacket *ppacket)
			{
				memcpy(ppacket, CurrentPacket(), sizeof(*ppacket));
			}
			
			void PackPacket(Tus::PacketPacker *ppack)
			{
				MtrControllerPacket *ppacket = CurrentPacket();
				
				ppacket->Base.ModuleType = MODULETYPE_MOTOR;
				ppacket->Base.DataLength = 7;
				ppacket->Base.InternalAddr = t_internal_id;

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