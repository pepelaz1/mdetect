#include "stdafx.h"


CPlayer::CPlayer() :
	m_evr(0),
	m_owner(0),
	_mode(0),
	m_motion_data(NULL),
	m_sshotspath("")
{	
}

CPlayer::~CPlayer()
{
}

HRESULT CPlayer::Load(char *file, HWND owner, int mode)
{
	m_file = file;
	m_owner = owner;
	_mode = mode;

	HRESULT hr = Init();
	if (FAILED(hr))
		return hr;

	hr = m_gs->GetService(MR_VIDEO_RENDER_SERVICE, IID_IMFVideoDisplayControl
		, (void **)&m_vdc);
	if (FAILED(hr))
		return hr;


	TuneVideoWindow();
	GetVideoParams();

	if (m_md)
		m_md->put_Mode(_mode);

	return hr;
}

void CPlayer::Reset()
{
	m_gs.Release();
	m_vdc.Release();
	m_evr.Release();

	CGraph::Reset();
}

HRESULT CPlayer::AddFilters()
{
	HRESULT hr = CGraph::AddFilters();
	if (FAILED(hr))
		return hr;

	hr = AddFilter(CLSID_MDFilter, L"Motion detector", m_det); 
	if (FAILED(hr))
		return hr;
	
	// Add video renderer
	hr = AddFilter(CLSID_EnhancedVideoRenderer, L"Enhanced Video Renderer", m_evr); 
//	hr = AddFilter(CLSID_VideoMixingRenderer, L"Video Renderer", m_evr); 
	if (FAILED(hr))
		return hr;

	return hr;
}

HRESULT CPlayer::ConnectFilters()
{
	HRESULT hr = CGraph::ConnectFilters();
	if (FAILED(hr))
		return hr;

	hr = Connect(m_ffdshow, m_det);
	if (FAILED(hr))
		return hr;

	hr = Connect(m_det, m_evr);
	if (FAILED(hr))
		return hr;
	return hr;
}

HRESULT CPlayer::QueryInterfaces()
{
	HRESULT hr = CGraph::QueryInterfaces();
	if (FAILED(hr))
		return hr;

	hr = m_evr.QueryInterface(&m_gs);
	if (FAILED(hr))
		return hr;

	hr = m_det->QueryInterface(IID_IMDFilter,(void **)&m_md);
	if (FAILED(hr))
		return hr;

	return hr;
}

void CPlayer::TuneVideoWindow()
{
	m_vdc->SetVideoWindow(m_owner);
	UpdateVideoSize();
}

void CPlayer::GetRenderer(CComPtr<IBaseFilter>&renderer)
{
	renderer = m_evr;
}

void CPlayer::UpdateVideoSize()
{
	if (m_vdc)
	{	
		RECT r;
		GetClientRect(m_owner, &r);
		m_vdc->SetVideoPosition(NULL,&r);
	}
}

void  CPlayer::Run()
{
	SetMode(_mode);
	CGraph::Run();
}

void CPlayer::StepForward(int nframes)
{
	if (nframes < 0)
		return ;

	Pause();
	
	LONGLONG pos = GetPosition();
	pos += m_fps * nframes;
	LONGLONG duration = GetDuration();
	if (pos > duration) pos = duration;
	SetPosition(pos);
}

void CPlayer::StepBackward(int nframes)
{
	if (nframes < 0)
		return;

	Pause();

	LONGLONG pos = GetPosition();
	pos -= m_fps * nframes;
	LONGLONG duration = GetDuration();
	if (pos > duration) pos = duration;
	SetPosition(pos);
}

unsigned char CPlayer::GetThreshold()
{
	unsigned char result = 0;
	if (m_md)
	{
		m_md->get_Threshold(&result);
	}
	return result;
}

void CPlayer::SetThreshold(unsigned char threshold)
{
	if (m_md)
	{
		m_md->put_Threshold(threshold);
	}
}
	
void  CPlayer::SetContrast(unsigned char contrast)
{
	if (m_md)
	{
		m_md->put_Contrast(contrast);
	}
}

unsigned char  CPlayer::GetContrast()
{
	unsigned char result = 0;
	if (m_md)
	{
		m_md->get_Contrast(&result);
	}
	return result;
}

void  CPlayer::SetBrightness(unsigned char brightness)
{
	if (m_md)
	{
		m_md->put_Brightness(brightness);
	}
}

unsigned char  CPlayer::GetBrightness()
{
	unsigned char result = 0;
	if (m_md)
	{
		m_md->get_Brightness(&result);
	}
	return result;
}


void CPlayer::SetMode(int mode)
{
	_mode = mode;
	if (m_md)
	{
		m_md->put_Mode(_mode);
	}
}

int CPlayer::GetMode()
{
	if (m_md)
	{
		int mode = 0;
		m_md->get_Mode(&mode);
		_mode = mode;
	}
	return _mode;
}

int CPlayer::GetFramesCount()
{
	if (!m_md)
		return 0;

	int count;
	m_md->get_FramesCount(&count);
	return count;
}

unsigned char *CPlayer::GetMotionIndexData()
{
	if (!m_md)
		return NULL;

	m_md->get_MotionIndexData(&m_motion_data);
	return m_motion_data;
}

RECT CPlayer::GetVideoRect()
{
	RECT rcParent;
	GetClientRect(m_owner, &rcParent);

	m_VideoRect.left = 0;
	m_VideoRect.top = 0;
	m_VideoRect.right = m_video_width;
	m_VideoRect.bottom = m_video_height;

	float k1 = (float)m_VideoRect.right/(float)m_VideoRect.bottom;
	float k2 = (float)rcParent.right/(float)rcParent.bottom;
	int n = 0;

	if (k1 > k2)
	{
		m_VideoRect.right = rcParent.right;
		m_VideoRect.bottom = (int)(m_VideoRect.right / k1);
		n = (rcParent.bottom - m_VideoRect.bottom)/2;
		m_VideoRect.top += n;
	}
	else
	{
		m_VideoRect.bottom = rcParent.bottom;
		m_VideoRect.right = (int)(m_VideoRect.bottom * k1);
		n = (rcParent.right - m_VideoRect.right)/2;
		m_VideoRect.left += n;
	}		 

	return m_VideoRect;
}

void CPlayer::SetScreenshootsPath(char *path)
{
	m_sshotspath = path;
}

void CPlayer::TakeSShot()
{
	if (m_md)
		m_md->TakeSShot((char *)m_sshotspath.data());
}