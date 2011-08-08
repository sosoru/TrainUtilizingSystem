#include "HardwareProfile.h"
#include "../Headers/RemoteModule.h"
#include "../Headers/MotherBoardModule.h"
#include "../Headers/SpiTransmit.h"
#include <stdlib.h>

HRESULT SendRemoteDevice(DeviceID * pid, BYTE mode, PMODULE_DATA data);

HRESULT GetFuncTableRemoteModule(DeviceID * pid, ModuleFuncTable* table)
{
	table->fncreate = CreateRemoteModuleState;
	table->fnstore = StoreRemoteModuleState;
	table->fninit = InitRemoteModule;
	table->fninterrupt = InterruptEmpty;
	
	return S_OK;
}

HRESULT InitRemoteModule(DeviceID * pid)
{
	#if defined VERSION_REV2
		OpenSPI(SPI_FOSC_4, MODE_01, SMPEND);
	#elif defined TRAIN_CONTROLLER_REV1
		OpenSPI(SLV_SSOFF, MODE_01, SMPEND);
	#endif
}

HRESULT SendRemoteDevice(DeviceID * pid, BYTE mode, PMODULE_DATA data)
{
	SpiPacket packet;
	RemoteModuleSavedState saved;
	
	ReadModuleSavedState(pid->ModulePart, &saved);
	
	memcpy(&packet.devid, &saved.remid, sizeof(*pid));
	packet.mode = mode;
	memcpy(packet.data, data, sizeof(SPI_DATASIZE));
	CalcSpiPacketCrc(&packet);
	
	if(FAILED(SendSpiPacket(&packet)))
		return E_FAIL;
	
	if(FAILED(ReceiveSpiPacket(&packet)))
		return E_FAIL;
	
	memcpy(data, &(packet.data), sizeof(packet.data));
	return S_OK;

}

HRESULT CreateRemoteModuleState(DeviceID * pid, PMODULE_DATA data)
{
	return SendRemoteDevice(pid, MODE_CREATE, data);
}

HRESULT StoreRemoteModuleState(DeviceID * pid, PMODULE_DATA data)
{
	if(pid->RemoteBit)
	{
		RemoteModuleState * pstate = (RemoteModuleState * )data;
		RemoteModuleSavedState saved;
		
		memcpy(&(saved.remid), &(pstate->remid), sizeof(saved.remid));
		WriteModuleSavedState(pid->ModulePart, &saved);
		
		return S_OK;		
	}
	else
	{
		return SendRemoteDevice(pid, MODE_STORE, data);	
	}
	
}