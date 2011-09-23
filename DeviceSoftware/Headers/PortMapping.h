#ifndef BLOCK_PORT_MAPPING
#define BLOCK_PORT_MAPPING

#include "../Headers/CommonDefs.h"

#ifdef VERSION_REV1
#include "../Headers/PortMappingRev1.h"
#elif defined VERSION_REV2
#include "../Headers/PortMappingRev2.h"
#endif

extern near BYTE* ModuleTris[];
extern near BYTE* ModulePort[];
extern near BYTE* ModuleLat[];
extern BYTE ModuleShift[];

#define BIT_MASK(bit_no) (1 << (bit_no))
#define GetMaskedValue(value, bit_no) (((value) & BIT_MASK((bit_no))) >> (bit_no))
#define SetMaskedValue(value, bit_no, set) ((value) = (( value & (~BIT_MASK(bit_no))) | (((set)&1) << (bit_no))))

#define getTris(no) (GetMaskedValue(*ModuleTris[(no)], ModuleShift[(no)]))
#define setTris(no, val) (SetMaskedValue(*ModuleTris[(no)], ModuleShift[(no)], val))
#define getPort(no) (GetMaskedValue(*ModulePort[(no)], ModuleShift[(no)]))
#define setPort(no, val) (SetMaskedValue(*ModulePort[(no)], ModuleShift[(no)], val))
#define getLat(no) (GetMaskedValue(*ModuleLat[(no)], ModuleShift[(no)]))
#define setLat(no, val) (SetMaskedValue(*ModuleLat[(no)], ModuleShift[(no)], val))


#endif