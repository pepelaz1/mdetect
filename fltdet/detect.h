#pragma once
#include <streams.h>
#include <list>
#include <fstream>
#include <string>
#include <list>
using namespace std;

class CDetector
{
private:
	int m_width;
	int m_height;
	int m_imagesize;
	unsigned char m_threshold;
	int m_mode;
	string m_logfile;
	string m_idxfile;
	REFERENCE_TIME m_curtime;
	int m_currframe;
	int m_currpos;
	list<RECT>m_excRects;

	string _filename;
	int _brightness;
	int _contrast;
	int _criteria; // 0 - MaxMin, 1- Avg

	
	unsigned char *m_motionBuff;

	unsigned char *m_backgroundFrame/*, *m_backgroundFrame2*/;
	unsigned char *m_currentFrame;
	unsigned char *m_currentFrameDilatated;
	int	m_counter/*, m_counter3*/;
	int	m_pixelsChanged;
	void PreprocessInputImageMaxMin( unsigned char *data, unsigned char  *buff );
	void PreprocessInputImageAvg( unsigned char *data, unsigned char  *buff );
	void PreprocessInputImage( unsigned char *data, unsigned char  *buff );
	void PostprocessInputImage( unsigned char *data,  unsigned char  *buff);
	void SaveMotion();
	void SaveIndex();
	void IdentifyObject(int i, int objno);
	void SaveObject(list<POINT>&points, int objno);

	inline bool InExcRects(int x, int y);
public:
	CDetector();
	~CDetector();
	void Clear();
	void Init(int width, int height, int imagesize, string filename);
	void Process(unsigned char *p, int size, REFERENCE_TIME time, int framenum);
	void WriteHdr(int brightness, int contrast);

	unsigned char GetThreshold();
	void SetThreshold(unsigned char threshold);
	void SetMode(int mode);
	string GetLogFile();
	void SetLogFile(string logfile);
	void SetFilename(string filename);
	void SetCriteria(int criteria);
	int GetCriteria();

	void AddExcRect(RECT &r);
	void ClearExcRects();
};

