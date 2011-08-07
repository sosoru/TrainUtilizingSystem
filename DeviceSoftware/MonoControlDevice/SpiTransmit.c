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

void CalcSpiPacketCrc(SpiPacket * ppack)
{
	
}

HRESULT ChkSpiPacketCrc(unsigned int crc, SpiPacket * ppack)
{

}
