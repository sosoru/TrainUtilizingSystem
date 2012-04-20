/*
 * tus_mstspi.h
 *
 * Created: 2012/01/30 15:18:39
 *  Author: root
 */ 


#ifndef TUS_MSTSPI_H_
#define TUS_MSTSPI_H_

#include "../../libtus/packet.h"
#include "tus_mstcfg.h"


void tus_mstspi_slave_init(uint8_t id);
void tus_mstspi_trans_init();
void tus_mstspi_trans(EthPacket *ppack, EthPacket *preceived);


#endif /* TUS_MSTSPI_H_ */