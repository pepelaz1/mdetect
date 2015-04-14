extern "C"
{
	// Modes

	// 0 - Detection preview
	// 1 - Displaying log
	// 2 - Simple playback
	// 3 - Processing


	__declspec( dllexport ) void __cdecl _OpenP(char *file, HWND owner, int mode);
	__declspec( dllexport ) void __cdecl _ResetP();
	__declspec( dllexport ) void __cdecl _RunP();
	__declspec( dllexport ) void __cdecl _PauseP();
	__declspec( dllexport ) void __cdecl _StopP();
	__declspec( dllexport ) void __cdecl _UpdateVideoSizeP();
	__declspec( dllexport ) __int64 __cdecl _GetDurationP();
	__declspec( dllexport ) __int64 __cdecl _GetPositionP();
	__declspec( dllexport ) void __cdecl _SetPositionP(__int64 pos);
	__declspec( dllexport ) void __cdecl _StepForwardP(int nframes);
	__declspec( dllexport ) void __cdecl _StepBackwardP(int nframes);
	__declspec( dllexport ) unsigned char __cdecl _GetThresholdP();
	__declspec( dllexport ) void __cdecl _SetThresholdP(unsigned char threshold);
	__declspec( dllexport ) void __cdecl _SetBrightnessP(unsigned char brightness);
	__declspec( dllexport ) unsigned char __cdecl _GetBrightnessP();
	__declspec( dllexport ) void __cdecl _SetContrastP(unsigned char contrast);
	__declspec( dllexport ) unsigned char __cdecl _GetContrastP();
	__declspec( dllexport ) void __cdecl _SetLogFileNameP(char *logfile);
	__declspec( dllexport ) void __cdecl _GetLogFileNameP(char *logfile);
	__declspec( dllexport ) void __cdecl _SetModeP(int mode);
	__declspec( dllexport ) int __cdecl _GetModeP();
	__declspec( dllexport ) int __cdecl _GetFramesCountP();
	__declspec( dllexport ) unsigned char *__cdecl _GetMotionIndexDataP();
	__declspec( dllexport ) RECT __cdecl _GetVideoRectP();
	__declspec( dllexport ) void __cdecl _AddExcRectP(float left, float top, float right, float bottom);
	__declspec( dllexport ) void __cdecl _ClearExcRectsP();
	__declspec( dllexport ) void __cdecl _SetScreenshootsPathP(char *path);
	__declspec( dllexport ) void __cdecl _TakeSShotP();
	__declspec( dllexport ) void __cdecl _SetCriteriaP(int criteria);
	__declspec( dllexport ) int __cdecl _GetCriteriaP();

	__declspec( dllexport ) int CreateD();
	__declspec( dllexport ) void ClearAllD();
	__declspec( dllexport ) void __cdecl _OpenD(int id, char *file, char *logpath);
	__declspec( dllexport ) void __cdecl _ResetD(int id);
	__declspec( dllexport ) void __cdecl _RunD(int id);
	__declspec( dllexport ) void __cdecl _PauseD(int id);
	__declspec( dllexport ) void __cdecl _StopD(int id);
	__declspec( dllexport ) int __cdecl _GetStateD(int id);
	__declspec( dllexport ) __int64 __cdecl _GetDurationD(int id);
	__declspec( dllexport ) __int64 __cdecl _GetPositionD(int id);	
	__declspec( dllexport ) unsigned char __cdecl _GetThresholdD(int id);
	__declspec( dllexport ) void __cdecl _SetThresholdD(int id, unsigned char threshold);
	__declspec( dllexport ) void __cdecl _SetBrightnessD(int id, unsigned char brightness);
	__declspec( dllexport ) int __cdecl _GetBrightnessD(int id);
	__declspec( dllexport ) void __cdecl _SetContrastD(int id, unsigned char contrast);
	__declspec( dllexport ) int __cdecl _GetContrastD(int id);
	__declspec( dllexport ) char *__cdecl _GetLogFileNameD(int id);
	__declspec( dllexport ) void __cdecl _AddExcRectD(int id, float left, float top, float right, float bottom);
	__declspec( dllexport ) void __cdecl _ClearExcRectsD(int id);
	__declspec( dllexport ) void __cdecl _SetCriteriaD(int id, int criteria);
	__declspec( dllexport ) int __cdecl _GetCriteriaD(int id);

};

