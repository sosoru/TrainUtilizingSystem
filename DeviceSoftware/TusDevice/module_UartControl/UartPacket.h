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

typedef union tag_TrainSensorPacket_xmit
{
	struct{
		uint8_t number[3];
	};
	uint8_t rawdata[3];
} TrainSensorPacket_xmit;	
		
typedef union tag_TrainSensorPacket_rcev
{
	struct{
		uint8_t number;
		uint8_t result;
		uint8_t checksum;			
	};
	uint8_t rawdata[3];
} TrainSensorPacket_rcev;
		
#ifdef __cplusplus
};
#endif

#endif /* UARTPACKET_H_ */