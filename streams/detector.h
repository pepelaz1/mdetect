#pragma once

interface IMDFilter;
class CDetector : public CGraph
{
protected:
	string m_logpath;

	// Filters
	CComPtr<IBaseFilter>m_nr;
	
	// Interfaces
	CComPtr<IMediaFilter>m_mf;

	virtual HRESULT AddFilters();
	virtual HRESULT QueryInterfaces();
	virtual HRESULT ConnectFilters();

	void Configure();
	virtual void GetRenderer(CComPtr<IBaseFilter>&renderer);
public:
	CDetector();
	~CDetector();
	HRESULT Load(char *file, char *logpath);
	virtual void Reset();
	virtual LONGLONG GetPosition();

	virtual void SetThreshold(unsigned char threshold);
	virtual unsigned char GetThreshold();
	virtual void SetContrast(unsigned char contrast);
	virtual unsigned char GetContrast();
	virtual void SetBrightness(unsigned char brightness);
	virtual unsigned char GetBrightness();
};

