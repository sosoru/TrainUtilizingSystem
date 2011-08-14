//#include "Usb.h"
#include "MonoDevice.h"
#include "./USB/usb.h"
#include "./USB/usb_function_generic.h"
#include <stdlib.h>

DevicePacket g_PacketBuffer[BUFFER_MAX];
BYTE g_PacketBufferPos = 0;
BYTE g_PacketTransfarPos = 0;

HRESULT AddPacketUSB(DeviceID* pid, BYTE moduleType, char * data)
{
	BYTE savedStatus;
	DevicePacket * ppack;
	
	savedStatus = PIE2bits.TMR3IE;
	PIE2bits.TMR3IE = 0; // disable timer 3 interruption (SendPacket())

	ppack = &g_PacketBuffer[g_PacketBufferPos];
		
	ppack->ReadMark = 0xFF;
	memcpy(&(ppack->ID), pid, sizeof(*pid));
	ppack->ModuleType = moduleType;
	memcpy(ppack->Data, data, (size_t)SIZE_DATA);
	
	//memcpy(g_PacketBuffer + g_PacketBufferPos, ppack, sizeof(DevicePacket));
	g_PacketBufferPos ++;
	if(g_PacketBufferPos >= BUFFER_MAX)
		g_PacketBufferPos = 0;
	
	PIE2bits.TMR3IE = 1;
	return S_OK;
}

HRESULT SendPacketUSB()
{
	BYTE i;
	BYTE savedStatus; 
	BYTE distanceBufPos;
		
	if(USBHandleBusy(USBGenericInHandle))
		return E_FAIL;

	if(g_PacketBufferPos >= g_PacketTransfarPos)
		distanceBufPos = g_PacketBufferPos - g_PacketTransfarPos;
	else
		distanceBufPos = g_PacketBufferPos + ((BYTE)COUNT_PACKET_BUFFER - g_PacketTransfarPos);
	
	if(distanceBufPos < COUNT_ONE_TRANSFAR_PACKETS)
	{
		return E_FAIL;
	}
	
	Port_SurfaceLedB = 0;

	//INPacket should be specified because of usb ram memory allocation
	memcpy(INPacket, &g_PacketBuffer[g_PacketTransfarPos], SIZE_ONE_TRANSFAR_PACKETS);
	USBGenericInHandle = USBGenWrite(USBGEN_EP_NUM, INPacket, SIZE_ONE_TRANSFAR_PACKETS);
	
	g_PacketTransfarPos += COUNT_ONE_TRANSFAR_PACKETS;		
	if(g_PacketTransfarPos >= BUFFER_MAX)
		g_PacketTransfarPos = 0;
	
	return S_OK;
}

HRESULT ReceivingProcessUSB()
{
	BYTE i, j;
	
    //OUTPacket contains data the host sent
    if(USBHandleBusy(USBGenericOutHandle))		//Check if the endpoint has received any data from the host.
    	return E_FAIL;

    for(i = 0 ; i < USBGEN_EP_SIZE; i+= sizeof(DevicePacket))
    {
    	DevicePacket* pack = &OUTPacket[i];
    	
    	if(pack->ReadMark != (BYTE)0xFF)
    		continue;
    		
    	if(g_mbState.ParentId == pack->ID.ParentPart)
    	{
    		if( pack->ID.ModuleAddr < MODULE_COUNT
    			&& READ_MBSTATE_MODULETYPE(g_mbState, pack->ID.ModuleAddr) == pack->ModuleType)
	    	{
	   			GET_FUNC_TABLE(pack->ID.ModuleAddr)->fnstore(&(pack->ID), pack->Data);
	    	}
    	}
    	
    }
    //Port_SurfaceLedA = 0;
   	
   	memset(OUTPacket, 0x00, (size_t)sizeof(OUTPacket));
	USBGenericOutHandle = USBGenRead(USBGEN_EP_NUM,(BYTE*)&OUTPacket,USBGEN_EP_SIZE);
	return S_OK;
}