//#include <spi.h>
#include <p18cxxx.h>
#include "../Headers/SpiTransmit.h"
#include <stdlib.h>
#include <string.h>

//#include "../Headers/PortMappingRev2.h"

BYTE SpiReceived[sizeof(SpiPacket)];
#define RECEIVED_SIZE (sizeof(BYTE)*sizeof(SpiPacket))
BYTE SpiReceivedInd = 0;

BYTE WriteSPI(BYTE data);
BYTE ReadSPI();

HRESULT SendSpiPacket(SpiPacket * ppack)
{
	BYTE i;
	BYTE * data = (BYTE *)ppack;
	BYTE interrupt = PIE1bits.SSPIE;
	
	PIE1bits.SSPIE = 0;
	for(i=0; i<sizeof(*ppack); ++i)
	{
		if(WriteSPI(data[i]))
			break; //failed
	}
	PIE1bits.SSPIE = interrupt;
	
	return (i<sizeof(*ppack)) ? E_FAIL : S_OK;
}

void ReceiveByte(BYTE newdata)
{
	SpiReceived[SpiReceivedInd++] = newdata;
	if(SpiReceivedInd >= sizeof(SpiReceived))
		SpiReceivedInd = 0;
}

HRESULT PacketReady(SpiPacket * ppacket)
{	
	memcpy((void*)ppacket, (void*)(SpiReceived + SpiReceivedInd), (size_t)(RECEIVED_SIZE - SpiReceivedInd));
	memcpy((void*)(ppacket + SpiReceivedInd), (void*)SpiReceived, (size_t)SpiReceivedInd);
	
	return ChkSpiPacketCrc(ppacket->Crc, ppacket);
}

//HRESULT ReceiveSpiPacket(SpiPacket * ppack)
//{
//	BYTE received[sizeof(SpiPacket)];
//	BYTE i;
//	BYTE interrupt = PIE1bits.SSPIE;
//	
//	PIE1bits.SSPIE = 0;	
//	for(i=0; i<sizeof(received); ++i)
//	{
//		received[i] = ReadSPI();
//	}
//	
//	for(i=0; i<sizeof(received); ++i)
//	{
//		memcpy((BYTE*)ppack, received + i, (size_t)(sizeof(received) -i));
//		memcpy((BYTE*)(ppack + i), received, (size_t)i);
//		
//		//offset received data if mismatching crc value
//		if(SUCCEEDED(ChkSpiPacketCrc(ppack->Crc, ppack)))
//		{
//			break; //succeeded
//		}
//		else
//		{
//			received[i] = ReadSPI();
//		}
//	}
//	PIE1bits.SSPIE = interrupt;
//
//	return (i<sizeof(received)) ? S_OK : E_FAIL;
//}
//
unsigned int CalcCrc(BYTE * data, BYTE len)
{
	BYTE i, d;
	unsigned int crc = 0x0000;
	
	for(i=0; i<len; ++i)
	{
		d = data[i];
		// Update the CRC for transmitted and received data using
		 // the CCITT 16bit algorithm (X^16 + X^12 + X^5 + 1).
		 crc = (unsigned char)(crc >> 8) | (crc << 8); 
		 crc ^= d;
		 crc ^= (unsigned char)(crc & 0xff) >> 4;
		 crc ^= (crc << 8) << 4;
		 crc ^= ((crc & 0xff) << 4) << 1;		
	}
	
	return crc;
}

void CalcSpiPacketCrc(SpiPacket * ppack)
{
	ppack->Crc = CalcCrc(ppack->data, SPI_DATASIZE);
}

HRESULT ChkSpiPacketCrc(unsigned int crc, SpiPacket * ppack)
{
	if(crc == CalcCrc(ppack->data, SPI_DATASIZE))
		return S_OK;
	else
		return E_FAIL;
}

BYTE WriteSPI(BYTE data)
{
	PIR1bits.SSPIF = 0;
	SSPBUF = data;
	
	while(!PIR1bits.SSPIF);
	
	return FALSE;
}

BYTE ReadSPI()
{
	PIR1bits.SSPIF = 0;
	SSPBUF = 0x00;
	
	while(!PIR1bits.SSPIF);
	return SSPBUF;
}
