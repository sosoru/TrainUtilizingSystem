/*
 * tus_mstspi.h
 *
 * Created: 2012/01/30 15:18:39
 *  Author: root
 */ 


#ifndef TUS_MSTSPI_H_
#define TUS_MSTSPI_H_

#include "../packet.h"
#include "tus_mstcfg.h"


void tus_mstspi_slave_init(SPI_SLAVE_PORT *pport, SPI_SLAVE_DIRECTION *pdir);
void tus_mstspi_trans_init(SPI_TRANS_PORT *pport, SPI_TRANS_DIRECTION *pdir);
void tus_mstspi_trans(EthPacket *ppack, EthPacket *preceived);


#endif /* TUS_MSTSPI_H_ */