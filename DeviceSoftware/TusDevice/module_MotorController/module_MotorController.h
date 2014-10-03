/*
 * module_MotorController.h
 *
 * Created: 2012/04/06 22:57:57
 *  Author: Administrator
 */ 


#ifndef MODULE_MOTORCONTROLLER_H_
#define MODULE_MOTORCONTROLLER_H_

#include "avr_base.hpp"
#include <PackPacket.hpp>
#include <Timer.h>

extern Tus::PacketPacker g_packer;
extern DeviceID g_myDeviceID;

extern bool ProcessKernelPacket(KernalState *pstate, DeviceID* psrcid, DeviceID* pdstid);

extern char g_dbgmsg[256];
extern uint8_t g_dbgmsgind;


#endif /* MODULE_MOTORCONTROLLER_H_ */