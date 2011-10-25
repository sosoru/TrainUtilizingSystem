#include <p18cxxx.h>
#include "../Headers/eeprom.h"

unsigned char ReadEEPROM(unsigned char address){
    EECON1=0;                   //ensure CFGS=0 and EEPGD=0 
    EEADR = address;
    EECON1bits.RD = 1;
    return(EEDATA);
}

void EEPROMcpy(unsigned char * dest, unsigned char srcaddr, unsigned char len )
{
	unsigned char conCache = INTCON;
	unsigned char i;
	
	//INTCONbits.GIE = 0;
	//INTCONbits.PEIE = 0;
	
	if((unsigned char)dest >= 0x400 && (unsigned char)dest < 0x500 )
	{
		i++;
	}
	
	for(i = 0; i < len; ++i)
	{
		*(dest+i) = ReadEEPROM(srcaddr+i);
	}
	
	INTCON = conCache;
}

void WriteEEPROM(unsigned char address,unsigned char data){
    EECON1=0;                   //ensure CFGS=0 and EEPGD=0
    EECON1bits.WREN = 1;        //enable write to EEPROM
    EEADR = address;            //setup Address
    EEDATA = data;              //and data
    EECON2 = 0x55;              //required sequence #1
    EECON2 = 0xaa;              //#2
    EECON1bits.WR = 1;          //#3 = actual write
    while(EECON1bits.WR == 1);      //wait until finished
    EECON1bits.WREN = 0;        //disable write to EEPROM
}

void EEPROMset(unsigned char destaddr, unsigned char* src, unsigned char len)
{
	unsigned char i;
  	unsigned char SaveInt=INTCON;             //save interrupt status
	//INTCONbits.GIE=0;           //No interrupts
    //INTCONbits.PEIE=0;
	for(i = 0; i < len; i++)
	{
		WriteEEPROM(destaddr + i, *(src + i));
	}
    INTCON=SaveInt;             //restore interrupts
}