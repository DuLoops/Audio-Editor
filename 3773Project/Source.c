
/**
Purpose: To handle windows messages for specific cases including when
		 the window is first created, refreshing (painting), and closing
		 the window.

Returns: Long - any error message (see Win32 API for details of possible error messages)
Notes:	 CALLBACK is defined as __stdcall which defines a calling
		 convention for assembly (stack parameter passing)
**/

#include "Header.h"

LRESULT CALLBACK HelloWndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam) {
	HDC		hdc;
	PAINTSTRUCT ps;
	RECT	rect;
	PDATA pdata;

	switch (message) {
	case WM_CREATE://additional things to do when window is created
	{
		LPCREATESTRUCT creation = (LPCREATESTRUCT)lParam;
		pdata = (PDATA)creation->lpCreateParams;

		if (pdata != NULL) {
			SetClassLongPtr(hwnd, 0, (LONG)pdata);
		}
		pdata = (PDATA)GetClassLongPtr(hwnd, 0);
		pdata->count++;
		return 0;
	}


	case WM_PAINT://what to do when a paint msg occurs
		hdc = BeginPaint(hwnd, &ps);//get a handle to a device context for drawing
		GetClientRect(hwnd, &rect);//define drawing area for clipping
		pdata = (PDATA)GetWindowLongPtr(hwnd, 0);
		DrawText(hdc, pdata->msg, -1, &rect,
			DT_SINGLELINE | DT_CENTER | DT_VCENTER);//write text to the context

		pdata = (PDATA)GetClassLongPtr(hwnd, 0);
		char outString[256]; //buffer to put result string in
		wsprintf(outString, TEXT("Window #%d"), pdata->count);
		DrawText(hdc, outString, -1, &rect,
			DT_SINGLELINE);//write text to the context

		EndPaint(hwnd, &ps);//release the device context
		return 0;

	case WM_DESTROY://how to handle a destroy (close window app) msg
		pdata = (PDATA)GetClassLongPtr(hwnd, 0);
		if (pdata->count > 1) {
			ShowWindow(hwnd, SW_HIDE);
			(pdata->count)--;
		}
		else
			PostQuitMessage(0);

		return 0;
	}
	//return the message to windows for further processing
	return DefWindowProc(hwnd, message, wParam, lParam);
}


