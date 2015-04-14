// streams.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"

CPlayer g_player;
CDetector g_detector;
vector<CDetector *>g_detectors;

//
// Player
//
extern "C" __declspec( dllexport ) void __cdecl _OpenP(char *file, HWND owner, int mode)
{
	g_player.Load(file, owner, mode);
}

extern "C" __declspec( dllexport ) void __cdecl _ResetP()
{
	g_player.Reset();
}

extern "C" __declspec( dllexport )  void __cdecl _RunP()
{
	g_player.Run();
}

extern "C" __declspec( dllexport )  void __cdecl _PauseP()
{
	g_player.Pause();
}

extern "C" __declspec( dllexport )  void __cdecl _StopP()
{
	g_player.Stop();
}

extern "C" __declspec( dllexport ) void __cdecl _UpdateVideoSizeP()
{
	g_player.UpdateVideoSize();
}

extern "C" __declspec( dllexport ) __int64 __cdecl _GetDurationP()
{
	return g_player.GetDuration();
}
extern "C" __declspec( dllexport ) __int64 __cdecl _GetPositionP()
{
	return g_player.GetPosition();
}
extern "C" __declspec( dllexport ) void __cdecl _SetPositionP(__int64 pos)
{
	g_player.SetPosition(pos);
}

extern "C"__declspec( dllexport ) void __cdecl _StepForwardP(int nframes)
{
	g_player.StepForward(nframes);
}

extern "C"__declspec( dllexport ) void __cdecl _StepBackwardP(int nframes)
{
	g_player.StepBackward(nframes);
}

extern "C" __declspec( dllexport ) unsigned char __cdecl _GetThresholdP()
{
	return g_player.GetThreshold();
}

extern "C" __declspec( dllexport ) void __cdecl _SetThresholdP(unsigned char threshold)
{
	g_player.SetThreshold(threshold);
}

extern "C" __declspec( dllexport ) void __cdecl _SetBrightnessP(unsigned char brightness)
{
	g_player.SetBrightness(brightness);
}

extern "C" __declspec( dllexport ) unsigned char __cdecl _GetBrightnessP()
{
	return g_player.GetBrightness();
}

extern "C" __declspec( dllexport ) void __cdecl _SetContrastP(unsigned char contrast)
{
	g_player.SetContrast(contrast);
}

extern "C" __declspec( dllexport ) unsigned char __cdecl _GetContrastP()
{
	return g_player.GetContrast();
}

extern "C" __declspec( dllexport ) void __cdecl _SetLogFileNameP(char *logfile)
{
	g_player.SetLogFile(logfile);
}

extern "C" __declspec( dllexport ) const char *__cdecl _GetLogFileNameP()
{
	return g_player.GetLogFile();
}

extern "C" __declspec( dllexport ) void __cdecl _SetModeP(int mode)
{
	g_player.SetMode(mode);
}

extern "C" __declspec( dllexport ) int __cdecl _GetModeP()
{
	return g_player.GetMode();
}

extern "C"	__declspec( dllexport ) int __cdecl _GetFramesCountP()
{
	return g_player.GetFramesCount();
}

extern "C"	__declspec( dllexport ) unsigned char *__cdecl _GetMotionIndexDataP()
{
	return g_player.GetMotionIndexData();
}


extern "C" __declspec( dllexport ) RECT __cdecl _GetVideoRectP()
{
	return g_player.GetVideoRect();
}

extern "C" __declspec( dllexport ) void __cdecl _AddExcRectP(float left, float top, float right, float bottom)
{
	g_player.AddExcRect(left, top, right, bottom);
}

extern "C" __declspec( dllexport ) void __cdecl _ClearExcRectsP()
{
	g_player.ClearExcRects();
}

extern "C" __declspec( dllexport ) void __cdecl _SetScreenshootsPathP(char *path)
{
	g_player.SetScreenshootsPath(path);
}

extern "C" __declspec( dllexport ) void __cdecl _TakeSShotP()
{
	g_player.TakeSShot();
}

extern "C" __declspec( dllexport ) void __cdecl _SetCriteriaP(int criteria)
{
	g_player.SetCriteria(criteria);
}

extern "C" __declspec( dllexport ) int __cdecl _GetCriteriaP()
{
	return g_player.GetCriteria();
}


//
// Detector
//
extern "C" __declspec( dllexport ) int __cdecl _CreateD()
{
	CDetector *d = new CDetector();
	int ret = (int)g_detectors.size();
	g_detectors.push_back(d);
	return ret;
}

extern "C"  __declspec( dllexport ) void ClearAllD()
{
	for ( int i = 0; i < (int)g_detectors.size(); i++)
	{
		CDetector *d = g_detectors[i];
		d->Reset();
		delete d;
	} 
	g_detectors.clear();
}

extern "C" __declspec( dllexport ) void __cdecl _OpenD(int id, char *file,  char *logpath)
{
	CDetector *d = g_detectors[id];
	if (d)
		d->Load(file, logpath);
}

extern "C"__declspec( dllexport ) void __cdecl _ResetD(int id)
{
	CDetector *d = g_detectors[id];
	if (d)
		d->Reset();
}

extern "C"__declspec( dllexport ) void __cdecl _RunD(int id)
{
	CDetector *d = g_detectors[id];
	if (d)
		d->Run();
}

extern "C"__declspec( dllexport ) void __cdecl _PauseD(int id)
{
	CDetector *d = g_detectors[id];
	if (d)
		d->Pause();
}

extern "C"__declspec( dllexport ) void __cdecl _StopD(int id)
{
	CDetector *d = g_detectors[id];
	if (d)
		d->Stop();
}

extern "C" __declspec( dllexport ) int __cdecl _GetStateD(int id)
{
	CDetector *d = g_detectors[id];
	if (d)
		return d->GetState();
	return -1;
}

extern "C" __declspec( dllexport ) __int64 __cdecl _GetDurationD(int id)
{
	CDetector *d = g_detectors[id];
	if (d)
		return d->GetDuration();
	return -1;
}

extern "C" __declspec( dllexport ) __int64 __cdecl _GetPositionD(int id)
{
	CDetector *d = g_detectors[id];
	if (d)
		return d->GetPosition();
	return -1;
}

extern "C" __declspec( dllexport ) void __cdecl _SetBrightnessD(int id, unsigned char brightness)
{
	CDetector *d = g_detectors[id];
	if (d)
		d->SetBrightness(brightness);
}

extern "C" __declspec( dllexport ) unsigned char __cdecl _GetBrightnessD(int id)
{
	CDetector *d = g_detectors[id];
	if (d)
		return d->GetBrightness();
	return -1;
}

extern "C" __declspec( dllexport ) void __cdecl _SetContrastD(int id, unsigned char contrast)
{
	CDetector *d = g_detectors[id];
	if (d)
		d->SetContrast(contrast);
}

extern "C" __declspec( dllexport ) unsigned char __cdecl _GetContrastD(int id)
{
	CDetector *d = g_detectors[id];
	if (d)
		return d->GetContrast();
	return -1;
}

extern "C" __declspec( dllexport ) unsigned char __cdecl _GetThresholdD(int id)
{
	CDetector *d = g_detectors[id];
	if (d)
		return d->GetThreshold();
	return -1;
}

extern "C" __declspec( dllexport ) void __cdecl _SetThresholdD(int id, unsigned char threshold)
{
	CDetector *d = g_detectors[id];
	if (d)
		d->SetThreshold(threshold);
}

extern "C" __declspec( dllexport ) const char * __cdecl _GetLogFileNameD(int id)
{
	CDetector *d = g_detectors[id];
	if (d)
		return d->GetLogFile();
	return "";
}

extern "C" __declspec( dllexport ) void __cdecl _AddExcRectD(int id, float left, float top, float right, float bottom)
{
	CDetector *d = g_detectors[id];
	if (d)
		d->AddExcRect(left, top, right, bottom);
}

extern "C" __declspec( dllexport ) void __cdecl _ClearExcRectsD(int id)
{
	CDetector *d = g_detectors[id];
	if (d)
		d->ClearExcRects();
}

extern "C" __declspec( dllexport ) void __cdecl _SetCriteriaD(int id, int criteria)
{
	CDetector *d = g_detectors[id];
	if (d)
		d->SetCriteria(criteria);
}

extern "C" __declspec( dllexport ) int __cdecl _GetCriteriaD(int id)
{
	CDetector *d = g_detectors[id];
	if (d)
		return d->GetCriteria();
	return 0;
}