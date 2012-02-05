/*
 * arp_table.h
 *
 * Created: 2011/12/21 16:15:41
 *  Author: root
 */ 


#ifndef ARP_TABLE_H_
#define ARP_TABLE_H_

#include "../global.h"
#include <avr/io.h>

typedef struct tag_arp_record
{
	uint8_t macAddr[6];
	uint8_t ipAddr[4];
} arp_record;

#define ARPTABLE_LEN 16
extern arp_record g_arpTable[];

extern void eth_init_arptable();
extern void eth_clear_arptable();
extern void eth_arptable_add(arp_record *prec);
extern uint8_t eth_arptable_get(arp_record *prec);

#endif /* ARP_TABLE_H_ */