#include "logparser.h"


CLogParser::CLogParser() : 
	m_logfile(""),
	m_length(0),
	m_duration(0),
	m_seektime(0),
	m_evSeek(true),
	m_index(NULL),
	m_idxsize(0)

{
}


CLogParser::~CLogParser()
{
	DeleteIndex();
}

void CLogParser::SetLogFile(string logfile)
{
	m_logfile = logfile;
	string idxfile = m_logfile;
	if (idxfile.length() != 0)
	{
		idxfile.replace(idxfile.end()-4, idxfile.end(), ".midx");
		// read whole idx file into memory
		ReadIndex(idxfile);
	}
	
	m_length = 0;
	m_duration = 0;
}

void CLogParser::ReadIndex(string idxfile)
{
	DeleteIndex();

	ifstream idx(idxfile, ios_base::binary);
	idx.seekg(0, ios::end);
	m_idxsize = (int)idx.tellg();
	idx.seekg(0);
	if (m_idxsize != -1)
	{
		m_index = new unsigned char[m_idxsize];
		idx.read((char *)m_index,(streamoff)m_idxsize);
	}
	idx.close();
}

int CLogParser::GetFramesCount()
{
	return  m_idxsize / 16;
}

unsigned char *CLogParser::GetMotionIndexData()
{
	return m_index;
}

void CLogParser::DeleteIndex()
{
	if (m_index)
	{
		delete m_index;
		m_index  = NULL;
		m_idxsize = 0;
	}
}

void  CLogParser::Reset()
{ 
	m_instrm.close();
}

void  CLogParser::OpenIfNeeded()
{
	if ( !m_instrm.is_open())
	{
		m_instrm.open(m_logfile, ios_base::binary);
		m_instrm.seekg (0, ios::end);
		m_length = (int)m_instrm.tellg();
	    m_instrm.seekg (0, ios::beg);
	}
}

bool  CLogParser::MoveTo(int framenum)
{
	OpenIfNeeded();

	IndexPortion *idx = (IndexPortion *)m_index;
	if ( idx && idx[framenum].filepos >= 0)
	{
		m_instrm.clear();
		m_instrm.seekg (idx[framenum].filepos, ios::beg);
		return true;
	}
	return false;
}


bool CLogParser::GetLine(string &line)
{
	line = "";
	char c;
	for(;;)
	{
		if (!m_instrm.read(&c,1))
			return false;

		if (c == '\n')
			break;
		line += c;
	}
	return true;
}

void CLogParser::SetDuration(REFERENCE_TIME duration)
{
	m_duration = duration;
}

int CLogParser::Seek(REFERENCE_TIME seektime)
{
	if ( m_instrm.is_open()) 
	{
		int n = SearchIndex(seektime);
		IndexPortion *idx = (IndexPortion *)m_index;

//	char s[256]; 
//	sprintf_s(s,256,"n=%d, pos=%d, line=%s\n",n,idx[n].filepos, line.c_str());
//	OutputDebugString(s);

		return idx[n].number;
	} 
	return 0;
}

int CLogParser::SearchIndex(REFERENCE_TIME time)
{
	IndexPortion *idx = (IndexPortion *)m_index;

	int count = m_idxsize / 16;
	int b = 0;
	int e = count-1;

	//if (time < idx[b].time)
	//	return b;

	//if (time > idx[e].time)
	//	return -1;

	for(;;)
	{
		if (b == e)
			return b;

		if (b == e-1)
			return e;

		int m = (e + b) / 2;
		if (idx[m].time > time)
		{
			e = m;
		}
		else 
		{
			b = m;
		}
	}
	return -1;
}