#line 1 "C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\ModuleCodes\UsbTransmit.c"
#line 1 "C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\ModuleCodes\UsbTransmit.c"




#line 1 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 

#line 4 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"

#line 6 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"

#line 9 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
 

#line 16 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
double atof (const auto char *s);

#line 28 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
signed char atob (const auto char *s);


#line 39 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
int atoi (const auto char *s);

#line 47 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
long atol (const auto char *s);

#line 58 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
unsigned long atoul (const auto char *s);


#line 71 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
char *btoa (auto signed char value, auto char *s);

#line 83 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
char *itoa (auto int value, auto char *s);

#line 95 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
char *ltoa (auto long value, auto char *s);

#line 107 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
char *ultoa (auto unsigned long value, auto char *s);
 


#line 112 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
 

#line 116 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
#line 118 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"


#line 124 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
int rand (void);

#line 136 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
 
void srand (auto unsigned int seed);
 
#line 140 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
#line 149 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"

#line 151 "C:/Program Files (x86)/Microchip/mplabc18/v3.39/h/stdlib.h"
#line 5 "C:\Users\root\Documents\TrainUtilizingSystem\DeviceSoftware\ModuleCodes\UsbTransmit.c"


DevicePacket g_PacketBuffer[BUFFER_MAX];
BYTE g_PacketBufferPos = 0;
BYTE g_PacketTransfarPos = 0;

HRESULT AddPacketUSB(DeviceID* pid, BYTE moduleType, char * data)
{
	BYTE savedStatus;
	DevicePacket * ppack;
	
	savedStatus = PIE2bits.TMR3IE;
	PIE2bits.TMR3IE = 0; 

	ppack = &g_PacketBuffer[g_PacketBufferPos];
		
	ppack->ReadMark = 0xFF;
	memcpy(&(ppack->ID), pid, sizeof(*pid));
	ppack->ModuleType = moduleType;
	memcpy(ppack->Data, data, (size_t)SIZE_DATA);
	
	
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
	
    
    if(USBHandleBusy(USBGenericOutHandle))		
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
    
   	
   	memset(OUTPacket, 0x00, (size_t)sizeof(OUTPacket));
	USBGenericOutHandle = USBGenRead(USBGEN_EP_NUM,(BYTE*)&OUTPacket,USBGEN_EP_SIZE);
	return S_OK;
}