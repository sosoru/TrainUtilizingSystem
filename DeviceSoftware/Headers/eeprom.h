#ifndef BLOCK_EEPROM_HEADER
#define BLOCK_EEPROM_HEADER

unsigned char ReadEEPROM(unsigned char address);
void WriteEEPROM(unsigned char address,unsigned char data);
void EEPROMcpy(unsigned char * dest, unsigned char srcaddr, unsigned char len );
void EEPROMset(unsigned char destaddr, unsigned char* src, unsigned char len);


#endif