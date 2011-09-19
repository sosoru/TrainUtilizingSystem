#ifndef BLOCK_POINTINFO
#define BLOCK_POINTINFO

typedef union tag_PointInfo
{
	struct
	{
		unsigned PointDirection : 1;
		unsigned PointValue : 3;
		unsigned :4;
	};
	BYTE data;
} PointInfo;

#endif