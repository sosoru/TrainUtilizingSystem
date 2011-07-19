#ifndef BLOCK_POINTINFO
#define BLOCK_POINTINFO

typedef union tag_PointInfo
{
	struct
	{
		unsigned PointDirection : 1;
		unsigned PointValue : 3;
		unsigned ModuleDefine:1;
		unsigned ACK :1;		
	};
	struct
	{
		unsigned ModuleValue :4;
		unsigned ModuleDefine:1;
		unsigned ACK :1;	
	};
	BYTE data;
} PointInfo;

#endif