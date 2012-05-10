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

struct TrainSensorPacket_rcev
{
	union{
		struct{
			uint8_t number;
			uint8_t adc;
			uint8_t checksum;
		};
		uint8_t rawdata[3];
	};
};	
		
struct TrainSensorPacket_xmit
{
	uint8_t number;
	uint8_t prescale;
	uint8_t checksum;
};
		
#ifdef __cplusplus
};
#endif

#endif /* UARTPACKET_H_ */