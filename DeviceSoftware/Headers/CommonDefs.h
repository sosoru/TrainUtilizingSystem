#ifndef BLOCK_COMMONDEFS_INCUDE
#define BLOCK_COMMONDEFS_INCUDE

#include <GenericTypeDefs.h>

#define PGMSTR(str) ((rom far char *)str)
#define PGMCSTR(str) ((const rom far char *)str)

typedef char HRESULT;

#define S_OK ((HRESULT)0)
#define E_FAIL ((HRESULT)-1)

#define SUCCEEDED(Status) (((HRESULT)(Status)) >= S_OK)
#define FAILED(Status) (!SUCCEEDED(Status))

#define INPUT_PIN 1
#define OUTPUT_PIN 0

#define TRUE 1
#define FALSE 0

#endif