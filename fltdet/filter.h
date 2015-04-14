#pragma once
#include <streams.h>
#include <initguid.h>
#include "guids.h"
#include "interface.h"
#include "detect.h"
#include "effects.h"
#include "painter.h"
#include "sshoot.h"
#include <string>
#include <list>
using namespace std;

class CMDFilter : public CTransformFilter,
	//public ISpecifyPropertyPages ,
	public IMDFilter                
{

public:
    static CUnknown * WINAPI CreateInstance(LPUNKNOWN punk, HRESULT *phr);

    DECLARE_IUNKNOWN;

    // Basic COM - used here to reveal our property interface.
    STDMETHODIMP NonDelegatingQueryInterface(REFIID riid, void ** ppv);

	// CTransformFilter overrides
    HRESULT CheckInputType(const CMediaType *mtIn);
    HRESULT CheckTransform(const CMediaType *mtIn, const CMediaType *mtOut);
    HRESULT DecideBufferSize(IMemAllocator *pAlloc, ALLOCATOR_PROPERTIES *pProp);
    HRESULT GetMediaType(int iPosition, CMediaType *pMediaType);
    HRESULT Transform(IMediaSample *pIn, IMediaSample *pOut);
    HRESULT StartStreaming();
	HRESULT StopStreaming();
	HRESULT NewSegment(REFERENCE_TIME tStart, REFERENCE_TIME tStop, double dRate);
	HRESULT Receive(IMediaSample *pSample);

	// CBaseFilter overrides
	CBasePin *GetPin(int n);
	
 private:

    // Constructor
    CMDFilter(TCHAR *tszName, LPUNKNOWN punk, HRESULT *phr);
	HRESULT ProcessFrame(unsigned char *pin, int sizein, unsigned char *pout, int sizeout, REFERENCE_TIME smpltime);
    virtual HRESULT SetMediaType(PIN_DIRECTION direction, const CMediaType *pmt);

	CDetector m_det;
	CEffectApplier m_eff;
	CPainter m_pnt;
		
	VIDEOINFO m_vii;
	VIDEOINFO m_vio;

	int m_mode;
	int m_framenum;
	string m_logfile;
	string m_logpath;
	string m_filename;

	CSShooter _sshooter;

public:
	STDMETHODIMP get_Threshold(unsigned char *threshold);
	STDMETHODIMP put_Threshold(unsigned char threshold);
	STDMETHODIMP get_Brightness(unsigned char *brightness);
	STDMETHODIMP put_Brightness(unsigned char brightness);
	STDMETHODIMP get_Contrast(unsigned char *contrast);
	STDMETHODIMP put_Contrast(unsigned char contrast);
	STDMETHODIMP get_Mode(int *mode);
	STDMETHODIMP put_Mode(int mode);
	STDMETHODIMP get_Criteria(int *criteria);
	STDMETHODIMP put_Criteria(int criteria);
	STDMETHODIMP put_Filename(char *filename);
	STDMETHODIMP get_LogFile(char *logfile);
	STDMETHODIMP put_LogFile(char *logfile);
	STDMETHODIMP get_FramesCount(int *count);
	STDMETHODIMP get_MotionIndexData(unsigned char **midata);
	STDMETHODIMP AddExcRect(RECT r);
	STDMETHODIMP ClearExcRects();
	STDMETHODIMP TakeSShot(char *path);
}; 

