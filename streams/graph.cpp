#include "stdafx.h"
#include "graph.h"
#include "logger.h"
#include <strsafe.h>

CLogger g_slog;


CGraph::CGraph(void) :
	m_file(""),
	m_gb(0),
	m_mc(0),
	m_ms(0),
	m_fs(0),
	m_src(0),
	m_splt(0),
	m_ffdshow(0),
	m_reg(0),
	m_md(0),
	m_fps(0),
	m_video_width(0),
	m_video_height(0)
{
	CoInitialize(NULL);
}


CGraph::~CGraph(void)
{
	Reset();
}

HRESULT CGraph::Init()
{
	HRESULT hr = S_OK;
	Reset();

	hr = m_gb.CoCreateInstance(CLSID_FilterGraph);
	if (FAILED(hr))
		return hr;

	// Add filters to graph
	hr = AddFilters();
	if (FAILED(hr))
		return hr;

	// Connect filters
	hr = ConnectFilters();
	if (FAILED(hr))
		return hr;
	
	// Query interfaces
	hr = QueryInterfaces();
	if (FAILED(hr))
		return hr;
	
	AddToRot(m_gb,&m_reg);
	GetVideoParams();
	return hr;
}

HRESULT CGraph::AddFilters()
{
	HRESULT hr = S_OK;
	
	// Add source filter and put input file to it
	hr = AddFilter(CLSID_AsyncReader, L"File source", m_src);
	if (FAILED(hr))
		return hr;

	CComPtr<IFileSourceFilter>fsf;
	hr = m_src.QueryInterface(&fsf);
	if (FAILED(hr))
		return hr;

	USES_CONVERSION;
	LPCOLESTR lpo = A2COLE(m_file.c_str());

	hr = fsf->Load(lpo, NULL);
	if (FAILED(hr))
		return hr;

	fsf.Release();

	// Add haali media splitter
	hr = AddFilter(CLSID_HaaliSplitter, L"Haali Splitter", m_splt);
	if (FAILED(hr))
		return hr;
	
	// Add ffdshow video decoder filter
	hr = AddFilter(CLSID_FFDShow, L"FFDShow Video Decoder", m_ffdshow);
	if (FAILED(hr))
		return hr;


	return hr;
}

HRESULT CGraph:: ConnectFilters()
{
	HRESULT hr = S_OK;
	
	// Connect source filter and haali media splitter 
	hr = Connect(m_src, m_splt);
	if (FAILED(hr))
		return hr;

	//// Connect haali media splitter and ffdshow video decoder 
	hr = Connect(m_splt, m_ffdshow);
	if (FAILED(hr))
		return hr;

	// Connect source filter and ffdshow video decoder
	//hr = Connect(m_src, m_ffdshow);
	//if (FAILED(hr))
	//	return hr;

	return hr;
}


HRESULT CGraph::QueryInterfaces()
{
	HRESULT hr = S_OK;
	
	hr = m_gb.QueryInterface(&m_mc);
	if (FAILED(hr))
		return hr;

	hr = m_gb.QueryInterface(&m_ms);
	if (FAILED(hr))
		return hr;

	hr = m_gb.QueryInterface(&m_fs);
	if (FAILED(hr))
		return hr;
	return hr;
}

void CGraph::Reset()
{
	RemoveFromRot(m_reg);

	m_splt.Release();
	m_ffdshow.Release();
	m_src.Release();
	m_det.Release();
	
	m_mc.Release();
	m_ms.Release();
	m_fs.Release();
	m_gb.Release();
	m_md.Release();
}

void CGraph::Run()
{
	if (m_mc)
		 m_mc->Run();	
}

void CGraph::Pause()
{
	if (m_mc)
		m_mc->Pause();
}

void CGraph::Stop()
{
	if (m_mc)
		 m_mc->Stop();
}

int CGraph::GetState()
{
	if (!m_mc)
		return -1;

	OAFilterState fs;
	m_mc->GetState(100, &fs);
	return (int)fs;
}

HRESULT CGraph::AddFilter(const GUID &guid, LPWSTR name, CComPtr<IBaseFilter> &filter)
{
	HRESULT hr = S_OK;
	CComPtr<IBaseFilter> flt;
	hr = flt.CoCreateInstance(guid);
	if (FAILED(hr))
		return hr;

	filter = flt;

	hr = m_gb->AddFilter(filter, name);
	if (FAILED(hr))
		return hr;	
	
	return hr;
}


HRESULT CGraph::Connect(CComPtr<IBaseFilter>&sflt, CComPtr<IBaseFilter>&dflt)
{
	HRESULT hr = S_OK;
	CComPtr<IEnumPins>senm;
	sflt->EnumPins(&senm);

	CComPtr<IEnumPins>denm;
	dflt->EnumPins(&denm);
	
	CComPtr<IPin>opin;
	ULONG ftch1 = 0;
	while(senm->Next(1,&opin,&ftch1) == S_OK)
	{
		PIN_DIRECTION spd;
		opin->QueryDirection(&spd);
		if (spd == PINDIR_OUTPUT)
		{
			CComPtr<IPin>ipin;
			ULONG ftch2 = 0;
			while(denm->Next(1,&ipin,&ftch2) == S_OK)
			{
				PIN_DIRECTION dpd;
				ipin->QueryDirection(&dpd);
				if (dpd == PINDIR_INPUT)
				{
					hr = m_gb->Connect(opin,ipin);
					if (SUCCEEDED(hr))
						return hr;
				}
				ipin.Release();
			}
		}
		opin.Release();
	}
	return hr;
}

LONGLONG CGraph::GetDuration()
{
	if (!m_ms)
		return 0;
	LONGLONG duration;

	m_ms->GetDuration(&duration);
	return duration;
}

LONGLONG CGraph::GetPosition()
{
	if (!m_ms)
		return 0;
	LONGLONG position;
	HRESULT hr = m_ms->GetCurrentPosition(&position);
	if (FAILED(hr))
		return 0;
	return position;
}

void CGraph::SetPosition(LONGLONG pos)
{
	if (!m_ms)
		return;

	LONGLONG start = pos;
	LONGLONG stop = 0;
	m_ms->SetPositions(&start, AM_SEEKING_AbsolutePositioning ,&stop,0);
}

HRESULT CGraph::AddToRot(IUnknown *graph, DWORD *reg) 
{
    CComPtr<IMoniker> moniker = NULL;
    CComPtr<IRunningObjectTable>rot = NULL;

    if (FAILED(GetRunningObjectTable(0, &rot))) 
    {
        return E_FAIL;
    }
    
    const size_t STRING_LENGTH = 256;

    WCHAR wsz[STRING_LENGTH];
    StringCchPrintfW(wsz, STRING_LENGTH, L"FilterGraph %08x pod %08x", (DWORD_PTR)graph, GetCurrentProcessId());
    

    HRESULT hr = CreateItemMoniker(L"!", wsz, &moniker);
    if (SUCCEEDED(hr)) 
    {
        hr = rot->Register(ROTFLAGS_REGISTRATIONKEEPSALIVE, 
							graph, 
							moniker, 
							reg);
    }
    return hr;
}  

void CGraph::RemoveFromRot(DWORD reg)
{
    IRunningObjectTable *pROT;
    if (SUCCEEDED(GetRunningObjectTable(0, &pROT)))
	{
        pROT->Revoke(reg);
        pROT->Release();
    }
}

void  CGraph::SetLogFile(char *logfile)
{
	if (m_md)
		m_md->put_LogFile((char *)logfile);
}

const char *CGraph::GetLogFile()
{
	char lf[256];
	if (m_md)
		m_md->get_LogFile(lf);
	m_logfile = lf;
	return m_logfile.c_str();
}

void CGraph::GetVideoParams()
{
	CComPtr<IBaseFilter> rend;
	GetRenderer(rend);

	CComPtr<IEnumPins>penum;
	rend->EnumPins(&penum);
	CComPtr<IPin>pin;
	DWORD f;
	penum->Next(1,&pin,&f);
	AM_MEDIA_TYPE amt;
	pin->ConnectionMediaType(&amt);
	VIDEOINFO *pvi = (VIDEOINFO *)amt.pbFormat;
	if (pvi)
	{
		m_fps = pvi->AvgTimePerFrame;
		m_video_width = pvi->bmiHeader.biWidth;
		m_video_height = pvi->bmiHeader.biHeight; 
	}
	pin.Release();
	penum.Release();
}

void CGraph::AddExcRect(float left, float top, float right, float bottom)
{
	if(!m_md)
		return;

	RECT r;
	r.left = (int)(m_video_width * left);
	r.top = (int)(m_video_height * top);
	r.right = (int)(m_video_width * right);
	r.bottom = (int)(m_video_height * bottom);

	m_md->AddExcRect(r);
}

void CGraph::ClearExcRects()
{
	if(!m_md)
		return;

	m_md->ClearExcRects();
}

int  CGraph::GetCriteria()
{
	if (!m_md)
		return 0;
	int criteria;
	m_md->get_Criteria(&criteria);
	return criteria;
}

void CGraph::SetCriteria(int criteria)
{
	if (m_md)
		m_md->put_Criteria(criteria);
}