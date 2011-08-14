#include <spi.h>
#include "../Headers/SpiTransmit.h"
#include <stdlib.h>

HRESULT SendSpiPacket(SpiPacket * ppack)
{
	BYTE i;
	BYTE * data = (BYTE *)ppack;
	
	for(i=0; i<sizeof(*ppack); ++i)
	{
		if(WriteSPI(data[i]))
			return E_FAIL;
	}
	return S_OK;
}

HRESULT ReceiveSpiPacket(SpiPacket * ppack)
{
	BYTE received[sizeof(SpiPacket)];
	BYTE i;
	
	for(i=0; i<sizeof(received); ++i)
	{
		received[i] = ReadSPI();
	}
	
	for(i=0; i<sizeof(received); ++i)
	{
		memcpy((BYTE*)ppack, received + i, sizeof(received) -i);
		memcpy((BYTE*)(ppack + i), received, i);
		
		//offset received data if mismatching crc value
		if(SUCCEEDED(ChkSpiPacketCrc(ppack->Crc, ppack)))
		{
			return S_OK;
		}
		else
		{
			received[i] = ReadSPI();
		}
	}
	return E_FAIL;
}

unsigned int CalcCrc(BYTE * data, BYTE len)
{
	BYTE i;
	unsigned int crc = 0x00;
	
	for(i=0; i<len; ++i)
	{
		// Update the CRC for transmitted and received data using
		 // the CCITT 16bit algorithm (X^16 + X^12 + X^5 + 1).
		 unsigned char ser_data = data[i];
		 crc = (unsigned char)(crc >> 8) | (crc << 8); 
		 crc ^= ser_data;
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
