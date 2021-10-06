/*******************************************************************
Purpose: Basic windows program. Creates a window with "Hello World"
		 in it.
*******************************************************************/

#include "main.h"

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
	PSTR szCMLine, int iCmdShow) {
	static TCHAR szAppName1[] = TEXT("app1");//name of app
	static TCHAR szAppName2[] = TEXT("app2");//name of app
	HWND	hwnd;//holds handle to the main window
	MSG		msg;//holds any message retrieved from the msg queue
	WNDCLASS wndclass;//wnd class for 
	PDATA pdata, pdatacls;

	//Class1
	wndclass.style = CS_HREDRAW | CS_VREDRAW;//redraw on refresh both directions
	wndclass.lpfnWndProc = HelloWndProc;//wnd proc to handle windows msgs/commands
	wndclass.cbClsExtra = sizeof(PDATA);//class space for expansion/info carrying
	wndclass.cbWndExtra = sizeof(PDATA);//wnd space for info carrying
	wndclass.hInstance = hInstance;//application instance handle
	wndclass.hIcon = LoadIcon(NULL, IDI_APPLICATION);//set icon for window
	wndclass.hCursor = LoadCursor(NULL, IDC_ARROW);//set cursor for window
	wndclass.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);//set background
	wndclass.lpszMenuName = NULL;//set menu
	wndclass.lpszClassName = szAppName1;//set application name

	//register wndclass to O/S so approp. wnd msg are sent to application
	if (!RegisterClass(&wndclass)) {
		MessageBox(NULL, TEXT("This program requires Windows 95/98/NT"),
			szAppName1, MB_ICONERROR);//if unable to be registered
		return 0;
	}


	pdata = (PDATA)malloc(sizeof(DATA));
	pdata->count = 0;
	pdata->msg = (TCHAR*)malloc(sizeof(TCHAR) * 10);
	pdata->msg = TEXT("a1 Message");

	pdatacls = (PDATA)malloc(sizeof(DATA));
	pdatacls->count = 0;
	pdatacls->msg = (TCHAR*)malloc(sizeof(TCHAR) * 10);
	pdatacls->msg = TEXT("class Message");


	//Class1 Wnd1
	hwnd = CreateWindow(szAppName1,		//window class name
		TEXT("a1"), // window caption
		WS_OVERLAPPEDWINDOW,	//window style
		CW_USEDEFAULT,		//initial x position
		CW_USEDEFAULT,		//initial y position
		CW_USEDEFAULT,		//initial x size
		CW_USEDEFAULT,		//initial y size
		NULL,				//parent window handle
		NULL,				//window menu handle
		hInstance,			//program instance handle
		(LPVOID)pdatacls);				//creation parameters
	SetWindowLongPtr(hwnd, 0, (LONG)pdata);
	ShowWindow(hwnd, iCmdShow);//set window to be shown
	UpdateWindow(hwnd);//force an update so window is drawn


	//messgae loop
	while (GetMessage(&msg, NULL, 0, 0)) {//get message from queue
		TranslateMessage(&msg);//for keystroke translation
		DispatchMessage(&msg);//pass msg back to windows for processing
		//note that this is to put windows o/s in control, rather than this app
	}

	return msg.wParam;
}

