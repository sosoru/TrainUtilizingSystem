/*
 * IdTable.h
 *
 * Created: 2011/12/19 16:05:29
 *  Author: root
 */ 


#ifndef IDTABLE_H_
#define IDTABLE_H_

#include "packet.h"

typedef struct tag_idAssociation
{
	DeviceID id;
	BYTE ModuleType;
} idAssociation;

extern idAssociation g_idTable;


#endif /* IDTABLE_H_ */