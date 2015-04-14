#include "painter.h"


CPainter::CPainter() :
	m_width(0),
	m_height(0),
	m_imagesize(0)
{
}


CPainter::~CPainter()
{
}
//
void CPainter::SetLogFile(string logfile)
{
	m_lp.SetLogFile(logfile);
}

void CPainter::Init(int width, int height, int imagesize)
{
	m_width = width;
	m_height = height;
	m_imagesize = imagesize;
}


void CPainter::Reset()
{
	m_lp.Reset();
}

struct tokens: ctype<char> 
{
    tokens(string delims): ctype<char>(get_table(delims)) {}

    static ctype_base::mask const* get_table(string delims)
    {
        typedef ctype<char> cctype;
        static const cctype::mask *const_rc= cctype::classic_table();

        static cctype::mask rc[cctype::table_size];
        memcpy(rc, const_rc, cctype::table_size * sizeof(cctype::mask));

		for (size_t i = 0; i < delims.length(); i++)
			rc[delims[i]] = ctype_base::space; 
        return &rc[0];
    }
};

vector<string>  split(string &s, string delims = " ")
{
	stringstream ss(s);
	ss.imbue(std::locale(std::locale(), new tokens(delims)));
	istream_iterator<string> begin(ss);
	istream_iterator<string> end;
	vector<string> vstrings(begin, end);
	copy(vstrings.begin(), vstrings.end(), ostream_iterator<string>(cout, "\n"));
	return vstrings;
}


void  CPainter::Process(unsigned char *p, REFERENCE_TIME time, int framenum)
{
	CAutoLock lck(&m_cs);
	if (m_lp.MoveTo(framenum))
	{
		string line = "";
		vector<string> v;

		m_lp.GetLine(line);
		v = split(line, " ,");
		if (v.size() > 1)
		{
			while (m_lp.GetLine(line) && line != "" && line != "\r")
				Draw(p,line);
		}
	}
}

void CPainter::Draw(unsigned char *p, string &line)
{
	vector<string> v = split(line, " ;:");
	vector<string> ptc_s = split(v[2]," (),");
	vector<string> ptc1;
	vector<string> ptc2;

	for( size_t i = 2; i < v.size()-1; i++)
	{
		ptc1 = split(v[i]," (),");
		ptc2 = split(v[i+1]," (),");
		DrawLine(p, atoi(ptc1[0].c_str()),atoi(ptc1[1].c_str()),atoi(ptc2[0].c_str()), atoi(ptc2[1].c_str()));
	}	
	if (ptc2.size() > 0 )
		DrawLine(p, atoi(ptc2[0].c_str()),atoi(ptc2[1].c_str()),atoi(ptc_s[0].c_str()), atoi(ptc_s[1].c_str()));
}

void CPainter::DrawLine(unsigned char *p, int x1, int y1, int x2, int y2)
{
	if ( x1 == x2)
	{
		if ( y2 >= y1)
		{
			for ( int i = y1; i <= y2; i++)
				p[i*m_width+x1] = 0xFF;
		}
		else
		{
			for ( int i = y2; i <= y1; i++)
				p[i*m_width+x1] = 0xFF;
		}
	}
	else if ( y1 == y2)
	{
		if ( x2 >= x1 )
		{
			for (int i = x1; i <= x2; i++)
				p[y1*m_width+i] = 0xFF;
		}
		else
		{
			for (int i = x2; i <= x1; i++)
				p[y1*m_width+i] = 0xFF;
		}
	}
}

int  CPainter::Seek(REFERENCE_TIME start, REFERENCE_TIME end)
{
	CAutoLock lck(&m_cs);
	if (!start)
		m_lp.SetDuration(end);
	return m_lp.Seek(start); 
}

int  CPainter::GetFramesCount()
{
	return m_lp.GetFramesCount();
}

unsigned char *CPainter::GetMotionIndexData()
{
	return m_lp.GetMotionIndexData();
}