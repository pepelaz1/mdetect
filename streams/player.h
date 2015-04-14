#pragma once

interface IMDFilter;
class CPlayer :	public CGraph
{
protected:
	string m_sshotspath;

	// Filters
	CComPtr<IBaseFilter>m_evr;
		
	HWND m_owner;
	int _mode;
	unsigned char *m_motion_data;

	// Interfaces
	CComPtr<IMFGetService> m_gs;
	CComPtr<IMFVideoDisplayControl> m_vdc;

	virtual HRESULT AddFilters();
	virtual HRESULT ConnectFilters();
	virtual HRESULT QueryInterfaces();

	void TuneVideoWindow();
	void UpdateVWPos();
	virtual void GetRenderer(CComPtr<IBaseFilter>&renderer);
public:
	CPlayer();
	~CPlayer();
	HRESULT Load(char *file, HWND owner, int mode);
	virtual void Run();
	virtual void Reset();
	void UpdateVideoSize();
	void StepForward(int nframes);
	void StepBackward(int nframes);
	
	virtual void SetThreshold(unsigned char threshold);
	virtual unsigned char GetThreshold();
	virtual void SetContrast(unsigned char contrast);
	virtual unsigned char GetContrast();
	virtual void SetBrightness(unsigned char brightness);
	virtual unsigned char GetBrightness();
	void SetMode(int mode);
	int GetMode();
	int GetFramesCount();
	unsigned char *GetMotionIndexData();
	RECT GetVideoRect();
	void SetScreenshootsPath(char *path);
	void TakeSShot();
};

