/*
 * UartPacket.hpp
 *
 * Created: 2012/05/07 19:41:36
 *  Author: Administrator
 */ 


#ifndef UARTPACKET_H_
#define UARTPACKET_H_

#ifdef __cplusplus
extern "C"{	
#endif

#define MAX_SIZE_USARTDATA 7
#define STORINGDATA_SIZE 8

typedef union tag_UsartChildPacket_Header
{
	struct{
		uint8_t type;
		uint8_t packet_size;
		uint8_t number;
		uint8_t checksum_all;			
	};
	uint8_t rawdata[4];
} UsartDevicePacket_Header;

typedef struct tag_Usart_sensor
{
	uint8_t result[STORINGDATA_SIZE];
} UsartPacket_sensor;

typedef struct tag_UsartDevicePacket
{
	UsartDevicePacket_Header header;
	uint8_t data[MAX_SIZE_USARTDATA];
} UsartPacket;
		
#ifdef __cplusplus
};
#endif

#endif /* UARTPACKET_H_ */