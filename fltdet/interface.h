
#ifndef __IMDFILTER__
#define __IMDFILTER__

#ifdef __cplusplus
extern "C" {
#endif


// {1EE256A9-05B1-4D5C-83CB-425C4017CE77}
DEFINE_GUID(IID_IMDFilter, 
0x1ee256a9, 0x5b1, 0x4d5c, 0x83, 0xcb, 0x42, 0x5c, 0x40, 0x17, 0xce, 0x77);

//
// IMDFilter
//
DECLARE_INTERFACE_(IMDFilter, IUnknown) 
{
	STDMETHOD(get_Threshold)
		( THIS_
		unsigned char *threshold 
		) PURE;

	STDMETHOD(put_Threshold)
		( THIS_
		unsigned char threshold  
		) PURE;

	STDMETHOD(get_Brightness)
		( THIS_
		unsigned char *brightness 
		) PURE;

	STDMETHOD(put_Brightness)
		( THIS_
		unsigned char brightness  
		) PURE;

	STDMETHOD(get_Contrast)
		( THIS_
		unsigned char *contrast 
		) PURE;

	STDMETHOD(put_Contrast)
		( THIS_
		unsigned char contrast  
		) PURE;

	STDMETHOD(put_Mode)
		( THIS_
		int mode  
		) PURE;

	STDMETHOD(get_Mode)
		( THIS_
		int *mode  
		) PURE;

	STDMETHOD(put_Criteria)
		( THIS_
		int criteria  
		) PURE;

	STDMETHOD(get_Criteria)
		( THIS_
		int *criteria  
		) PURE;

	STDMETHOD(put_Filename)
		( THIS_
		char *filename  
		) PURE;
	
	STDMETHOD(put_LogFile)
		( THIS_
		char *logfile  
		) PURE;

	STDMETHOD(get_LogFile)
		( THIS_
		char *logfile  
		) PURE;

	STDMETHOD(get_FramesCount)
		( THIS_
		int *count  
		) PURE;
	
	STDMETHOD(get_MotionIndexData)
		( THIS_
		unsigned char **midata
		) PURE;

	STDMETHOD(AddExcRect)
		( THIS_
		RECT r  
		) PURE;
	
	STDMETHOD(ClearExcRects)
		( ) PURE;

	STDMETHOD(TakeSShot)
		( THIS_
		char *path  
		) PURE;
};


#ifdef __cplusplus
}
#endif

#endif // __IMDFILTER__
