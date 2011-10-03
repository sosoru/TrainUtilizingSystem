#include <p18cxxx.h>
#include "MonoDevice.h"
#include "USB/usb.h"
#include "USB/usb_function_generic.h"
#include "../Headers/UsbPacket.h"
#include <stdlib.h>
#include <string.h>

DevicePacket g_PacketBuffer[BUFFER_MAX];
BYTE g_PacketBufferPos = 0;
BYTE g_PacketTransfarPos = 0;

HRESULT AddPacketUSB(DeviceID* pid, BYTE moduleType, char * data)
{
	DevicePacket * ppack;
	BYTE intCache = INTCON;
	
	if(USBDeviceState < CONFIGURED_STATE)
		return E_FAIL;
	
	INTCONbits.PEIE = 0; // disable low-priority interrupts

	ppack = &g_PacketBuffer[g_PacketBufferPos];
		
	ppack->ReadMark = 0xFF;
	memcpy((void*)&(ppack->ID), (void*)pid, (size_t)sizeof(DeviceID));
	ppack->ModuleType = moduleType;
	memcpy((void*)ppack->Data, (void*)data, (size_t)SIZE_DATA);
	
	//memcpy(g_PacketBuffer + g_PacketBufferPos, ppack, sizeof(DevicePacket));
	g_PacketBufferPos ++;
	if(g_PacketBufferPos >= BUFFER_MAX)
		g_PacketBufferPos = 0;
	
	INTCON = intCache; // resume interrupts	
	return S_OK;
}

HRESULT SendPacketUSB()
{
	BYTE i;
	BYTE savedStatus; 
	BYTE distanceBufPos;
	BYTE intCache = INTCON;
		
	if(USBHandleBusy(USBGenericInHandle) && (USBDeviceState < CONFIGURED_STATE))
		return E_FAIL;

	if(g_PacketBufferPos >= g_PacketTransfarPos)
		distanceBufPos = g_PacketBufferPos - g_PacketTransfarPos;
	else
		distanceBufPos = g_PacketBufferPos + ((BYTE)COUNT_PACKET_BUFFER - g_PacketTransfarPos);
	
	if(distanceBufPos < COUNT_ONE_TRANSFAR_PACKETS)
	{
		return E_FAIL;
	}
	
	INTCONbits.GIE = 0; //disable high-priority interrupts
	INTCONbits.PEIE = 0; // disable low-priority interrupts
	
	Port_SurfaceLedB = 0;

	//INPacket should be specified because of usb ram memory allocation
	memcpy((void*)INPacket, (void*)&g_PacketBuffer[g_PacketTransfarPos], (size_t)SIZE_ONE_TRANSFAR_PACKETS);
	USBGenericInHandle = USBGenWrite(USBGEN_EP_NUM, INPacket, SIZE_ONE_TRANSFAR_PACKETS);
	if(USBGenericInHandle == 0)
		return E_FAIL;
	
	g_PacketTransfarPos += COUNT_ONE_TRANSFAR_PACKETS;		
	if(g_PacketTransfarPos >= BUFFER_MAX)
		g_PacketTransfarPos = 0;
	
	INTCON = intCache; // resume interrupts	
	
	return S_OK;
}

HRESULT ReceivingProcessUSB()
{
	BYTE i, j;
	BYTE intCache = INTCON;

    //OUTPacket contains data the host sent
    if(USBHandleBusy(USBGenericOutHandle) && (USBDeviceState < CONFIGURED_STATE))	//Check if the endpoint has received any data from the host.
    	return E_FAIL;
	
	INTCONbits.GIE = 0; //disable high-priority interrupts
	INTCONbits.PEIE = 0; // disable low-priority interrupts

    for(i = 0 ; i < USBGEN_EP_SIZE; i+= sizeof(DevicePacket))
    {
    	DevicePacket* pack = (DevicePacket*)&OUTPacket[i];
    	
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
    	
    	pack->ReadMark = 0x00;
    }
    //Port_SurfaceLedA = 0;
   	
   	//memset((void*)OUTPacket, 0x00, (size_t)sizeof(OUTPacket));
	USBGenericOutHandle = USBGenRead(USBGEN_EP_NUM,(BYTE*)&OUTPacket,USBGEN_EP_SIZE);
	
	INTCON = intCache; // resume interrupts	

	return S_OK;
}