#include "stdafx.h"
#include <ctime>
#include <sstream>
using namespace std;
EXTERN_C const CLSID CLSID_NullRenderer;

CDetector::CDetector() 
	: m_nr(0),
	  m_mf(0)
{
}

CDetector::~CDetector()
{
}

HRESULT CDetector::Load(char *file, char *logpath)
{
	m_file = file;
	m_logpath = logpath;

	HRESULT hr = Init();
	if (FAILED(hr))
		return hr;

	Configure();

	// Reset clock
	hr = m_mf->SetSyncSource(NULL);
	return hr;
}

void CDetector::Reset()
{
	m_mf.Release();
	m_md.Release();
	m_nr.Release();
	m_det.Release();

	CGraph::Reset();
}


HRESULT CDetector::AddFilters()
{
	HRESULT hr = CGraph::AddFilters();
	if(FAILED(hr))
		return hr;

	// Add motion detector filter
	hr = AddFilter(CLSID_MDFilter, L"Motion detector", m_det); 
	if (FAILED(hr))
		return hr;

	// Add null renderer
	hr = AddFilter(CLSID_NullRenderer, L"Null Renderer", m_nr); 
	if (FAILED(hr))
		return hr;
	
	return hr;
}

HRESULT CDetector::QueryInterfaces()
{
	HRESULT hr = CGraph::QueryInterfaces();
	if (FAILED(hr))
		return hr;

	hr = m_gb.QueryInterface(&m_mf);
	if (FAILED(hr))
		return hr;

	hr = m_det->QueryInterface(IID_IMDFilter,(void **)&m_md);
	if (FAILED(hr))
		return hr;


	//m_ms.Release();
	//hr = m_nr.QueryInterface(&m_ms);

	/*CComPtr<IEnumPins>en;
	m_src->EnumPins(&en);


	CComPtr<IPin>pin;
	DWORD f;
	en->Next(1,&pin,&f);

	CComPtr<IPin>conp;
	pin->ConnectedTo(&conp);

	PIN_INFO pi;
	conp->QueryPinInfo(&pi);

	m_ms.Release();
	hr = pi.pFilter->QueryInterface(&m_ms);


	pi.pFilter->Release();

	conp.Release();
	pin.Release();*/

	return hr;
}

HRESULT CDetector::ConnectFilters()
{
	HRESULT hr = CGraph::ConnectFilters();
	if (FAILED(hr))
		return hr;

	hr = Connect(m_ffdshow, m_det);
	if (FAILED(hr))
		return hr;

	hr = Connect(m_det, m_nr);
	if (FAILED(hr))
		return hr;


	return hr;
}

LONGLONG CDetector::GetPosition()
{
	if (!m_ms)
		return 0;

	LONGLONG position;
	LONGLONG stop;

	HRESULT hr = m_ms->GetPositions(&position, &stop);
	if (FAILED(hr))
		return 0;
	return position;
}




void CDetector::Configure()
{
	char drive[3];
	char dir[MAX_PATH];
	char name[MAX_PATH];
	char ext[10];

	_splitpath(m_file.data(),drive,dir,name,ext);

	time_t  t;
	time(&t);
	struct tm  tstruct;
    char       buf[80];
    tstruct = *localtime(&t);
    strftime(buf, sizeof(buf), "%Y-%m-%d_%H-%M-%S", &tstruct);

	stringstream ss;
	if (m_logpath == "")
		ss << drive << dir;
	else
		ss << m_logpath << "\\";
	ss << name << "_" << buf << ".log";

	if (m_md)
	{
		m_md->put_Mode(3); // full processing
		m_md->put_LogFile((char *)ss.str().c_str());	
		m_md->put_Filename((char *)m_file.c_str());
	}		
}

void CDetector::GetRenderer(CComPtr<IBaseFilter>&renderer)
{
	renderer = m_nr;
}

void CDetector::SetThreshold(unsigned char threshold)
{
	if (m_md)
	{
		m_md->put_Threshold(threshold);
	}
}

unsigned char CDetector::GetThreshold()
{
	unsigned char result = 0;
	if (m_md)
	{
		m_md->get_Threshold(&result);
	}
	return result;
}

void  CDetector::SetContrast(unsigned char contrast)
{
	if (m_md)
	{
		m_md->put_Contrast(contrast);
	}
}

unsigned char  CDetector::GetContrast()
{
	unsigned char result = 0;
	if (m_md)
	{
		m_md->get_Contrast(&result);
	}
	return result;
}

void  CDetector::SetBrightness(unsigned char brightness)
{
	if (m_md)
	{
		m_md->put_Brightness(brightness);
	}
}

unsigned char  CDetector::GetBrightness()
{
	unsigned char result = 0;
	if (m_md)
	{
		m_md->get_Brightness(&result);
	}
	return result;
}


