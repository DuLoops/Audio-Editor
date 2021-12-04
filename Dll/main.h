#pragma once
#ifdef __c
#define EXPORT extern "C" __declspec (dllexport)
#else
#define EXPORT __declspec (dllexport)
#endif

// Start The Program (Create Dialog Box)
EXPORT int CALLBACK start();

// Get Buffer Pointer
EXPORT PBYTE getBuffer();

// Get Buffer Length
EXPORT DWORD getBufferLen();

// Set Buffer
EXPORT void setSaveBuffer(PBYTE newbuffer, long dLen, long _nSamplesPerSec, long _nAvgBytesPerSec, long _nBlockAlign, long _wBitsPerSample);

// Play Sound Saved (In Buffer)
EXPORT void play();

// Record Sound (Begin Recording Sound)
EXPORT void record();

// End Record (Save Sound in Buffer)
EXPORT void end();

// Clip Buffer
EXPORT void clipBuffer(PBYTE newbuffer, DWORD dLen);

// Pause Record
EXPORT void pause();

// Get Sample Rate
EXPORT DWORD getSampleRate();

// Set Header (Sound File Information)
EXPORT void setHeader(DWORD _nSamplesPerSec, DWORD _nAvgBytesPerSec, DWORD _nBlockAlign, DWORD _wBitsPerSample);
