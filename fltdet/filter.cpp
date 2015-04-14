#include "filter.h"


extern void *memcpy_kaetemi_sse2(void *dst, void *src, int nBytes);

// Put out the name of a function and instance on the debugger.
// Invoke this at the start of functions to allow a trace.
//#define DbgFunc(a) DbgLog(( LOG_TRACE                        \
//                          , 2                                \
//                          , TEXT("CMDFilter(Instance %d)::%s") \
//                          , m_nThisInstance                  \
//                          , TEXT(a)                          \
//                         ));


// Self-registration data structures

const AMOVIESETUP_MEDIATYPE
sudPinTypes =   { &MEDIATYPE_Video        // clsMajorType
                , &MEDIASUBTYPE_NULL };   // clsMinorType

const AMOVIESETUP_PIN
psudPins[] = { { L"In"				// strName
               , FALSE               // bRendered
               , FALSE               // bOutput
               , FALSE               // bZero
               , FALSE               // bMany
               , &CLSID_NULL         // clsConnectsToFilter
               , L"Output"           // strConnectsToPin
               , 1                   // nTypes
               , &sudPinTypes        // lpTypes
               }
             , { L"Out"				// strName
               , FALSE               // bRendered
               , TRUE                // bOutput
               , FALSE               // bZero
               , FALSE               // bMany
               , &CLSID_NULL         // clsConnectsToFilter
               , L"Input"            // strConnectsToPin
               , 1                   // nTypes
               , &sudPinTypes        // lpTypes
               }
             };

const AMOVIESETUP_FILTER
sudMDFilter = { &CLSID_MDFilter                 // class id
            , L"FltDet"						    // strName
            , MERIT_DO_NOT_USE                // dwMerit
            , 2                               // nPins
            , psudPins                        // lpPin
            };

// Needed for the CreateInstance mechanism
CFactoryTemplate g_Templates[1]= { { L"Motion Detection Filter"
                                   , &CLSID_MDFilter
                                   , CMDFilter::CreateInstance
                                   , NULL
                                   , &sudMDFilter
                                   }
                                 };

int g_cTemplates = sizeof(g_Templates)/sizeof(g_Templates[0]);


CMDFilter::CMDFilter(TCHAR *tszName, LPUNKNOWN punk, HRESULT *phr)
    : CTransformFilter (tszName, punk, CLSID_MDFilter)

{
	memset(&m_vii,0,sizeof(VIDEOINFO));
	memset(&m_vio,0,sizeof(VIDEOINFO));
	m_mode = 1;
	m_logfile = "";
	m_logpath = "";
} 


CUnknown * WINAPI CMDFilter::CreateInstance(LPUNKNOWN punk, HRESULT *phr)
{
    CMDFilter *pNewObject = new CMDFilter(NAME("FltDet"), punk, phr);
    if (pNewObject == NULL) {
        if (phr)
            *phr = E_OUTOFMEMORY;
    }
    return pNewObject;
} 

STDMETHODIMP CMDFilter::NonDelegatingQueryInterface(REFIID riid, void **ppv)
{
    CheckPointer(ppv,E_POINTER);

    if (riid == IID_IMDFilter) {
        return GetInterface((IMDFilter *) this, ppv);

    }  else {
        // Pass the buck
        return CTransformFilter::NonDelegatingQueryInterface(riid, ppv);
    }
} 

 
HRESULT CMDFilter::CheckInputType(const CMediaType *mtIn)
{
	if (mtIn->majortype == MEDIATYPE_Video)
	{
		if (mtIn->subtype == MEDIASUBTYPE_NV12)
		//if (mtIn->subtype == MEDIASUBTYPE_RGB24)
		{
			if (mtIn->formattype == FORMAT_VideoInfo)
			{
				VIDEOINFO *pvi = (VIDEOINFO *)mtIn->Format();
				if (pvi->bmiHeader.biHeight < 0)
					return VFW_E_TYPE_NOT_ACCEPTED;

				m_det.Init(pvi->bmiHeader.biWidth, pvi->bmiHeader.biHeight, pvi->bmiHeader.biSizeImage, m_filename);
				m_pnt.Init(pvi->bmiHeader.biWidth, pvi->bmiHeader.biHeight, pvi->bmiHeader.biSizeImage);
				_sshooter.Init(pvi->bmiHeader.biWidth, pvi->bmiHeader.biHeight, pvi->bmiHeader.biSizeImage);
				return S_OK;
			}
		}
	}
    return VFW_E_TYPE_NOT_ACCEPTED;
} 

HRESULT CMDFilter::CheckTransform(const CMediaType *mtIn, const CMediaType *mtOut)
{
	if (mtIn->majortype != MEDIATYPE_Video || 
		mtIn->subtype != MEDIASUBTYPE_NV12 ||
		//mtIn->subtype != MEDIASUBTYPE_RGB24 ||
		mtIn->formattype != FORMAT_VideoInfo)
	{
		return VFW_E_TYPE_NOT_ACCEPTED;
	}
	
    BITMAPINFOHEADER *bmii = HEADER(mtIn);
    BITMAPINFOHEADER *bmio = HEADER(mtOut);

    if ((bmii->biWidth <= bmio->biWidth) &&
        //(bmii->biHeight == abs(bmio->biHeight))
		(bmii->biHeight == bmio->biHeight)
		)
    {
       return S_OK;
    }
    return VFW_E_TYPE_NOT_ACCEPTED;
}

HRESULT CMDFilter::GetMediaType(int iPosition, CMediaType *pMediaType)
{
    if (iPosition < 0)
    {
        return E_INVALIDARG;
    }
    else if (iPosition == 0)
    {  
        return m_pInput->ConnectionMediaType(pMediaType);
    }
    return VFW_S_NO_MORE_ITEMS;
}

HRESULT CMDFilter::DecideBufferSize(IMemAllocator *pAlloc, ALLOCATOR_PROPERTIES *pProp)
{
    if (!m_pInput->IsConnected()) 
    {
        return E_UNEXPECTED;
    }

    ALLOCATOR_PROPERTIES InputProps;
    IMemAllocator *pAllocInput = 0;
    HRESULT hr = m_pInput->GetAllocator(&pAllocInput);

    if (FAILED(hr))
    {
        return hr;
    }
    hr = pAllocInput->GetProperties(&InputProps);
    pAllocInput->Release();
    if (FAILED(hr)) 
    {
        return hr;
    }
    if (pProp->cbAlign == 0)
    {
        pProp->cbAlign = 1;
    }
    if (pProp->cbBuffer == 0)
    {
        pProp->cBuffers = 1;
    }
    pProp->cbBuffer = max(InputProps.cbBuffer, pProp->cbBuffer);
    ALLOCATOR_PROPERTIES Actual;
    hr = pAlloc->SetProperties(pProp, &Actual);
    if (FAILED(hr)) 
    {
		 return hr;
    }  
    if (InputProps.cbBuffer > Actual.cbBuffer) 
    {
        return E_FAIL;
    }    
    return S_OK;
}

HRESULT CMDFilter::StartStreaming()
{
	HRESULT hr = CTransformFilter::StartStreaming();
	if (FAILED(hr))
		return hr;

	m_framenum = 0;
	if (m_mode == 1)
		m_pnt.SetLogFile(m_logfile);
	m_det.Clear();
	return S_OK;
}

HRESULT CMDFilter::StopStreaming()
{
	HRESULT hr = CTransformFilter::StopStreaming();
	if (FAILED(hr))
		return hr;

	if (m_mode == 1)
		m_pnt.Reset();
	return S_OK;
}

HRESULT CMDFilter::NewSegment(REFERENCE_TIME tStart, REFERENCE_TIME tStop, double dRate)
{
	HRESULT hr = CTransformFilter::NewSegment(tStart, tStop, dRate);
	if (FAILED(hr))
		return hr;

	if (m_mode == 1)
		m_framenum = m_pnt.Seek(tStart, tStop);

	return hr;
}

HRESULT CMDFilter::Receive(IMediaSample *pSample)
{
      AM_SAMPLE2_PROPERTIES * const pProps = m_pInput->SampleProps();
    if (pProps->dwStreamId != AM_STREAM_MEDIA) {
        //return m_pOutput->m_pInputPin->Receive(pSample);
		return m_pOutput->Deliver(pSample);
    }
    HRESULT hr;
    ASSERT(pSample);
    IMediaSample * pOutSample;

    // If no output to deliver to then no point sending us data

    ASSERT (m_pOutput != NULL) ;

    // Set up the output sample
    hr = InitializeOutputSample(pSample, &pOutSample);

    if (FAILED(hr)) {
        return hr;
    }

    // Start timing the transform (if PERF is defined)
    MSR_START(m_idTransform);

    // have the derived class transform the data

    hr = Transform(pSample, pOutSample);

    // Stop the clock and log it (if PERF is defined)
    MSR_STOP(m_idTransform);

    if (FAILED(hr)) {
	DbgLog((LOG_TRACE,1,TEXT("Error from transform")));
    } else {
        // the Transform() function can return S_FALSE to indicate that the
        // sample should not be delivered; we only deliver the sample if it's
        // really S_OK (same as NOERROR, of course.)
        if (hr == NOERROR) {
//    	    hr = m_pOutput->m_pInputPin->Receive(pOutSample);
			hr = m_pOutput->Deliver(pOutSample);
            m_bSampleSkipped = FALSE;	// last thing no longer dropped

			if (m_mode ==  1)
			{
				if ( hr == S_OK)
					m_framenum++;
			}
		//	else
		//		m_framenum++;
			
        } else {
            // S_FALSE returned from Transform is a PRIVATE agreement
            // We should return NOERROR from Receive() in this cause because returning S_FALSE
            // from Receive() means that this is the end of the stream and no more data should
            // be sent.
            if (S_FALSE == hr) {

                //  Release the sample before calling notify to avoid
                //  deadlocks if the sample holds a lock on the system
                //  such as DirectDraw buffers do
                pOutSample->Release();
                m_bSampleSkipped = TRUE;
                if (!m_bQualityChanged) {
                    NotifyEvent(EC_QUALITY_CHANGE,0,0);
                    m_bQualityChanged = TRUE;
                }
                return NOERROR;
            }
        }
    }

    // release the output buffer. If the connected pin still needs it,
    // it will have addrefed it itself.
    pOutSample->Release();

    return hr;
}


HRESULT CMDFilter::Transform(IMediaSample *pSource, IMediaSample *pDest)
{
	HRESULT hr = S_OK;
    CMediaType *pmt = 0;
    if (S_OK == pDest->GetMediaType((AM_MEDIA_TYPE**)&pmt) && pmt)
    {
        m_pOutput->SetMediaType(pmt);
        DeleteMediaType(pmt);
    }
    BYTE *pin, *pout;
	int sizein, sizeout;

	REFERENCE_TIME rtStart, rtEnd;
	hr = pSource->GetTime(&rtStart, &rtEnd);

    hr = pSource->GetPointer(&pin);
	sizein = pSource->GetActualDataLength();
    hr = pDest->GetPointer(&pout);
	sizeout = pDest->GetActualDataLength();

	hr = ProcessFrame(pin, sizein, pout, sizeout, rtStart);
   // pDest->SetActualDataLength(cbByte);
    return hr;
}


HRESULT CMDFilter::ProcessFrame(unsigned char *pin, int sizein, unsigned char *pout, int sizeout, REFERENCE_TIME smpltime)
{
	// Make processing first
	//m_det.Process(pin,sizein);		
	//

	//// Then, copy to output ( RGB24 format )
	//int h = abs(m_vii.bmiHeader.biHeight);
	//
	//int stridei = (int)(sizein / h);
	//int strideo = (int)(sizeout / h);
	//unsigned char *pi = pin;
	////unsigned char *pi = pin + (h-1) * stridei;
	//unsigned char *po = pout ;
	////unsigned char *po = pout +  (h-1) * strideo;
	//for ( int i = 0; i < h; i++)
	//{
	//	memcpy(po, pi,stridei);
	//	//memcpy_kaetemi_sse2(po, pi,stridei);
	//	//pi -= stridei;
	//	pi += stridei;
	//
	//	//po -= strideo;
	//	po += strideo;
	//}

	/*int high = 146;
	int low = 100;
	int avg = (high + low)/2;
	for ( int i = 0; i < sizein/1.5; i++)
		if (pin[i] > low && pin[i] < high) 
			pin[i] = 0; 
		else 
			pin[i] = 255;*/

	////m_det.Process(pout,sizeout);	
	m_eff.Process(pin,(int)(sizein/1.5));
	if (m_mode == 1)
		m_pnt.Process(pin, smpltime, m_framenum);
	else if ( m_mode == 3 || m_mode == 0 )
	{
		if (!m_framenum)
			m_det.WriteHdr(m_eff.GetBrightness(), m_eff.GetContrast());
		m_det.Process(pin,(int)(sizein/1.5), smpltime, m_framenum);		
	}

	if (m_mode != 3) // Preview or display log
		_sshooter.Copy(pin);

	// Then, copy to output ( NV12 format )
	unsigned char *pi = NULL;
	unsigned char *po = NULL;
	int h = abs(m_vii.bmiHeader.biHeight);
	
	// copy luma
	int si = (int)(sizein / 1.5);
	int so = (int)(sizeout / 1.5);
	int stridei = (int)(si / h);
	int strideo = (int)(so / h);

	pi = pin;
	po = pout;
	for ( int i = 0; i < h; i++)
	{
		memcpy(po, pi,stridei);
		//memcpy_kaetemi_sse2(po, pi,stridei);
		pi += stridei;
		po += strideo;
	}

	// copy chroma
	pi = pin + si;
	po = pout + so;

	si = sizein - (int)(sizein / 1.5);
	so = sizeout - (int)(sizeout / 1.5);
	stridei = (si / h) * 2;
	strideo = (so / h) * 2;

	//for ( int i = 0; i < h/2; i++)
	//{
	//	memcpy(po, pi,stridei);
	//	//memcpy_kaetemi_sse2(po, pi,stridei); 
	//	pi += stridei;
	//	po += strideo;
	//}

	
	if (m_mode != 1)
		m_framenum++;


	return S_OK;
}


HRESULT CMDFilter::SetMediaType(PIN_DIRECTION direction,const CMediaType *pmt)
{
	if (pmt->formattype != FORMAT_VideoInfo /*|| pmt->subtype != MEDIASUBTYPE_RGB24*/ )
		return VFW_E_TYPE_NOT_ACCEPTED;

	if (direction == PINDIR_INPUT)
		memcpy(&m_vii, pmt->pbFormat, sizeof(VIDEOINFO));
	else if (direction == PINDIR_OUTPUT)
		memcpy(&m_vio, pmt->pbFormat, sizeof(VIDEOINFO));
   return S_OK;
} 

CBasePin *CMDFilter::GetPin(int n)
{
	HRESULT hr = S_OK;

	// Create an input pin if necessary

	if (m_pInput == NULL) {

		m_pInput = new CTransformInputPin(NAME("Transform input pin"),
			this,              // Owner filter
			&hr,               // Result code
			L"In");      // Pin name


		//  Can't fail
		ASSERT(SUCCEEDED(hr));
		if (m_pInput == NULL) {
			return NULL;
		}
		m_pOutput = (CTransformOutputPin *)
			new CTransformOutputPin(NAME("Transform output pin"),
			this,            // Owner filter
			&hr,             // Result code
			L"Out");   // Pin name


		// Can't fail
		ASSERT(SUCCEEDED(hr));
		if (m_pOutput == NULL) {
			delete m_pInput;
			m_pInput = NULL;
		}
	}

	// Return the appropriate pin

	if (n == 0) {
		return m_pInput;
	} else
		if (n == 1) {
			return m_pOutput;
		} else {
			return NULL;
		}
}

STDMETHODIMP CMDFilter::get_Threshold(unsigned char *threshold)
{
	*threshold = m_det.GetThreshold();
	return S_OK;
}

STDMETHODIMP  CMDFilter::put_Threshold(unsigned char threshold)
{
	m_det.SetThreshold(threshold);
	return S_OK;
}

STDMETHODIMP  CMDFilter::get_Brightness(unsigned char *brightness)
{
	*brightness  = m_eff.GetBrightness();
	return S_OK;
}

STDMETHODIMP  CMDFilter::put_Brightness(unsigned char brightness)
{
	m_eff.SetBrightness(brightness);
	return S_OK;
}

STDMETHODIMP  CMDFilter::get_Contrast(unsigned char *contrast)
{
	*contrast  = m_eff.GetContrast();
	return S_OK;
}

STDMETHODIMP  CMDFilter::put_Contrast(unsigned char contrast)
{
	m_eff.SetContrast(contrast);
	return S_OK;
}

STDMETHODIMP CMDFilter::get_Mode(int *mode)
{
	*mode = m_mode;
	return S_OK;
}

STDMETHODIMP CMDFilter::put_Mode(int mode)
{
	m_mode = mode;
	m_det.SetMode(m_mode);
	return S_OK;
}

STDMETHODIMP CMDFilter::get_Criteria(int *criteria)
{
	*criteria = m_det.GetCriteria();
	return S_OK;
}
STDMETHODIMP CMDFilter::put_Criteria(int criteria)
{
	m_det.SetCriteria(criteria);
	return S_OK;
}


STDMETHODIMP CMDFilter::get_LogFile(char *logfile)
{
	if (m_mode == 3)
		m_logfile = m_det.GetLogFile();

	lstrcpy(logfile,m_logfile.c_str());
	return S_OK;
}

STDMETHODIMP CMDFilter::put_LogFile(char *logfile)
{
	m_logfile = logfile ? logfile : "";
	if (m_mode == 1)
		m_pnt.SetLogFile(m_logfile);

	if (m_mode == 3)
		m_det.SetLogFile(m_logfile);
	return S_OK;
}

STDMETHODIMP CMDFilter::put_Filename(char *filename)
{
	m_det.SetFilename(filename);
	return S_OK;
}

STDMETHODIMP  CMDFilter::get_FramesCount(int *count)
{
	*count = 0;
	if (m_mode == 1)
		*count = m_pnt.GetFramesCount();
	return S_OK;
}

STDMETHODIMP CMDFilter::get_MotionIndexData(unsigned char **midata)
{
	if (m_mode == 1)
		*midata = m_pnt.GetMotionIndexData();
	return S_OK;
}

STDMETHODIMP CMDFilter::AddExcRect(RECT r)
{
	m_det.AddExcRect(r);
	return S_OK;
}

STDMETHODIMP CMDFilter::ClearExcRects()
{
	m_det.ClearExcRects();
	return S_OK;
}

STDMETHODIMP CMDFilter::TakeSShot(char *path)
{
	_sshooter.Save(path);
	return S_OK;
}

STDAPI DllRegisterServer()
{
  return AMovieDllRegisterServer2( TRUE );
}


STDAPI DllUnregisterServer()
{
  return AMovieDllRegisterServer2( FALSE );
}


extern "C" BOOL WINAPI DllEntryPoint(HINSTANCE, ULONG, LPVOID);

BOOL APIENTRY DllMain(HANDLE hModule, 
                      DWORD  dwReason, 
                      LPVOID lpReserved)
{
	return DllEntryPoint((HINSTANCE)(hModule), dwReason, lpReserved);
}

#pragma warning(disable: 4514) // "unreferenced inline function has been removed"



