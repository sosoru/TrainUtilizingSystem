#if !defined BLOCK_USB_PACKET
#define BLOCK_USB_PACKET

#include "../Headers/ModuleBase.h"

typedef struct tag_DevicePacket
{
	BYTE ReadMark;
	DeviceID ID;
	BYTE ModuleType;
	MODULE_DATA Data[SIZE_DATA]; 
} DevicePacket;

#define COUNT_ONE_TRANSFAR_PACKETS ((BYTE)(USBGEN_EP_SIZE / sizeof(DevicePacket)))
#define SIZE_ONE_TRANSFAR_PACKETS USBGEN_EP_SIZE
#define COUNT_PACKET_BUFFER (COUNT_ONE_TRANSFAR_PACKETS * 4)

#define BYTESIZE_PACKET_BUFFER (COUNT_PACKET_BUFFER * sizeof(DevicePacket))
#define BUFFER_MAX COUNT_PACKET_BUFFER

HRESULT AddPacketUSB(DeviceID* pid, BYTE moduleType, char* data);
HRESULT SendPacketUSB(void);
HRESULT ReceivingProcessUSB(void);

#endif