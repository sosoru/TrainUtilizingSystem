/*
 * arp_table.c
 *
 * Created: 2011/12/21 22:38:51
 *  Author: root
 */ 

#include "../global.h"
#include "arp_table.h"
#include <stdlib.h>
#include <string.h>

#define isempty_arprecord(prec) ((prec)->ipAddr[0] == 0x00)
arp_record g_arpTable[ARPTABLE_LEN];

uint8_t ipequals_arptable(arp_record *pA, arp_record *pB)
{
	uint8_t size = 4;
	uint8_t i;
	
	for(i=0; i<size; ++i)
	{
		if( pA->ipAddr[i] != pB->ipAddr[i])
			return FALSE;
	}
	return TRUE;
}

void set_arptable(arp_record *psrc, arp_record *pdst)
{
	memcpy((void * )pdst, (void *)psrc, sizeof(*psrc));
}

void eth_init_arptable()
{
	memset(g_arpTable, 0x00, sizeof(g_arpTable));
}

void eth_clear_arptable()
{
	memset(g_arpTable, 0x00, sizeof(g_arpTable));
}

void eth_arptable_add(arp_record *prec)
{
	uint8_t i;
	
	for(i=0; i<ARPTABLE_LEN; ++i)
	{
		arp_record * pcur = &g_arpTable[i];;
		
		if(isempty_arprecord(pcur) || ipequals_arptable(prec, pcur))
		{
			set_arptable(prec, pcur);
			return;
		}
	}
	
	set_arptable(prec, &g_arpTable[ARPTABLE_LEN-1]); //if the table is full, add the argument record to tail of the table	
}

uint8_t eth_arptable_get(arp_record *prec)
{
	uint8_t i;
	
	for(i=0; i<ARPTABLE_LEN; ++i)
	{
		arp_record * pcur = &g_arpTable[i];
		
		if(ipequals_arptable(prec, pcur))
		{
			memcpy(prec->macAddr, pcur->macAddr, sizeof(prec->macAddr));
			return 1;
		}
	}
	
	return 0;
}