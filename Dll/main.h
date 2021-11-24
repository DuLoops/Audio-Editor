#pragma once
#ifdef __c
#define EXPORT extern "C" __declspec (dllexport)
#else
#define EXPORT __declspec (dllexport)
#endif

EXPORT int CALLBACK record();

EXPORT PBYTE getBuffer();

EXPORT DWORD getBufferLen();

EXPORT void setBuffer(PBYTE newBuffer);