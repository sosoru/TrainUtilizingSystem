#if !defined BLOCK_SPI_TRANS
#define BLOCK_SPI_TRANS

#include "../Headers/ModuleBase.h"

#define SPI_DATASIZE SIZE_DATA
#define SPI_PACKETSIZE (SPI_DATASIZE + 5)

#define MODE_CREATE 0x01
#define MODE_STORE 0x02

typedef struct tag_SpiPacket
{
	DeviceID devid;
	BYTE mode;
	unsigned int Crc;
	BYTE data [SPI_DATASIZE];
} SpiPacket;

HRESULT SendSpiPacket(SpiPacket * ppack);
void ReceiveByte(BYTE newdata);
HRESULT PacketReady(SpiPacket * ppacket);

void CalcSpiPacketCrc(SpiPacket * ppack);
HRESULT ChkSpiPacketCrc(unsigned int crc, SpiPacket * ppack);

#endif

