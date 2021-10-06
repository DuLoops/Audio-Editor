#ifndef main
#define main
#include <windows.h>
#include <mmsystem.h>
#include <strsafe.h>

#include "Header.h"

typedef struct _data {
	int count;
	TCHAR* msg;
}DATA, * PDATA;

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
	PSTR szCMLine, int iCmdShow);

#endif


