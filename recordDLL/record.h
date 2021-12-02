#pragma once
#ifdef __c
#define EXPORT extern "C" __declspec (dllexport)
#else
#define EXPORT __declspec (dllexport)
#endif

EXPORT int CALLBACK start();

EXPORT PBYTE RgetBuffer();

EXPORT DWORD RgetBufferLen();

EXPORT void RsetSaveBuffer(PBYTE newbuffer, long dLen, long _nSamplesPerSec, long _nAvgBytesPerSec, long _nBlockAlign, long _wBitsPerSample);

EXPORT void Rplay();

EXPORT void Rrecord();

EXPORT void Rend();

EXPORT void RclipBuffer(PBYTE newbuffer, DWORD dLen);

EXPORT void Rpause();

EXPORT DWORD RgetSampleRate();

EXPORT void RsetHeader(DWORD _nSamplesPerSec, DWORD _nAvgBytesPerSec, DWORD _nBlockAlign, DWORD _wBitsPerSample);
