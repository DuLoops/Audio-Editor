#pragma once
#ifdef __c
#define EXPORT extern "C" __declspec (dllexport)
#else
#define EXPORT __declspec (dllexport)
#endif

EXPORT int CALLBACK start();

EXPORT PBYTE getBuffer();

EXPORT DWORD getBufferLen();

EXPORT void setSaveBuffer(PBYTE newbuffer, long dLen, long _nSamplesPerSec, long _nAvgBytesPerSec, long _nBlockAlign, long _wBitsPerSample);

EXPORT void play();

EXPORT void record();

EXPORT void end();

EXPORT void clipBuffer(PBYTE newbuffer, DWORD dLen);

EXPORT void pause();

EXPORT DWORD getSampleRate();

EXPORT void setHeader(DWORD _nSamplesPerSec, DWORD _nAvgBytesPerSec, DWORD _nBlockAlign, DWORD _wBitsPerSample);
