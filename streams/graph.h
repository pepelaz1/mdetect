#pragma once


class CGraph
{
protected:
	string m_file;
	string m_logfile;
	DWORD m_reg;
	
	REFERENCE_TIME m_fps;

	RECT m_VideoRect;
	int m_video_width;
	int m_video_height;

	// Filters
	CComPtr<IBaseFilter>m_src;
	CComPtr<IBaseFilter>m_splt;
	CComPtr<IBaseFilter>m_det;
	CComPtr<IBaseFilter>m_ffdshow;

	// Interfaces
	CComPtr<IGraphBuilder> m_gb;
	CComPtr<IMediaControl>m_mc;
	CComPtr<IMediaSeeking>m_ms;
	CComPtr<IVideoFrameStep>m_fs;
	CComPtr<IMDFilter>m_md;
		
	virtual HRESULT AddFilters();
	virtual HRESULT ConnectFilters();
	virtual HRESULT QueryInterfaces();
	
	HRESULT AddFilter(const GUID &guid, LPWSTR name, CComPtr<IBaseFilter>&filter);
	HRESULT Connect(CComPtr<IBaseFilter>&sflt, CComPtr<IBaseFilter>&dflt);

	HRESULT AddToRot(IUnknown *graph, DWORD *reg);
	void RemoveFromRot(DWORD reg);

	void GetVideoParams();
	virtual void GetRenderer(CComPtr<IBaseFilter>&renderer) = 0;

public:
	CGraph();
	~CGraph();

	virtual HRESULT Init();
	virtual void Reset();
	virtual void Run();
	virtual void Pause();
	virtual void Stop();
	virtual int GetState();
	LONGLONG GetDuration();
	virtual LONGLONG GetPosition();
	void SetPosition(LONGLONG pos);

	virtual void SetThreshold(unsigned char threshold) = 0;
	virtual unsigned char GetThreshold() = 0;
	virtual void SetContrast(unsigned char contrast) = 0;
	virtual unsigned char GetContrast() = 0;
	virtual void SetBrightness(unsigned char brightness) = 0;
	virtual unsigned char GetBrightness() = 0;
	void SetLogFile(char *logfile);
	const char *GetLogFile();
	void AddExcRect(float left, float top, float right, float bottom);
	void ClearExcRects();
	int GetCriteria();
	void SetCriteria(int criteria);
};

