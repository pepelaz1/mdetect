#pragma once
#include <streams.h>
#include <string>
#include <fstream>
#include <iostream>
using namespace std;

struct IndexPortion
{
	REFERENCE_TIME time;
	int number;
	int filepos;
};

class CLogParser
{
private:
	string m_logfile;
	ifstream m_instrm;
	int m_length;
	REFERENCE_TIME m_duration;
	
	//int m_prevframe;
	//int m_curpos;
	//int m_prevpos;
	
	CAMEvent m_evSeek;
	REFERENCE_TIME m_seektime;


	unsigned char *m_index;
	int m_idxsize;
	void ReadIndex(string file);
	void DeleteIndex();
	int SearchIndex(REFERENCE_TIME time);
public:
	void SetLogFile(string logfile);
	void Reset();
	bool MoveTo(int framenum);
	void OpenIfNeeded();
	bool GetLine(string &line);
	void SetDuration(REFERENCE_TIME duration);
	int Seek(REFERENCE_TIME time);
	CLogParser();
	~CLogParser();
	int GetFramesCount();
	unsigned char *GetMotionIndexData();
};

