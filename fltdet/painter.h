#pragma once
#include <streams.h>
#include <string>
#include <fstream>
#include <iostream>
#include <vector>
#include <sstream>
#include "logparser.h"
using namespace std;

class CPainter
{
private:
	CLogParser m_lp;
	CCritSec m_cs;
	//string m_logfile;
	//ifstream m_instrm;
	//int m_cmf;
	int m_width;
	int m_height;
	int m_imagesize;

	void Draw(unsigned char *p, string &line);
	void DrawLine(unsigned char *p, int x1, int y1, int x2, int y2);
public:
	CPainter();
	~CPainter();
	void Reset();
	void SetLogFile(string logfile);
	void Init(int width, int height, int imagesize);
	void Process(unsigned char *p,  REFERENCE_TIME time, int framenum);
	int Seek(REFERENCE_TIME start , REFERENCE_TIME end);
	int GetFramesCount();
	unsigned char *GetMotionIndexData();
};

