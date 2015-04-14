#include "detect.h"

extern void *memcpy_kaetemi_sse2(void *dst, void *src, int nBytes);

CDetector::CDetector()
{
	m_threshold = 15;
	m_backgroundFrame = NULL;
	//m_backgroundFrame2 = NULL;
	m_currentFrame = NULL;
	m_currentFrameDilatated = NULL;
	m_counter = 0;
	//m_counter3 = 0;
	m_pixelsChanged = 0;
	m_mode = 0;
	m_motionBuff = NULL;
	m_currframe = 0;
	m_logfile = "";
	m_idxfile = "";
	m_currpos = -1;
	_filename = "";
	_brightness = 128;
	_contrast = 128;
	_criteria = 0;
}


CDetector::~CDetector()
{
	Clear();

	if (m_motionBuff)
	{
		delete [] m_motionBuff;
		m_motionBuff = NULL;
	}
}

void CDetector::Clear()
{
	if (m_backgroundFrame)
	{
		delete [] m_backgroundFrame;
		m_backgroundFrame = NULL;
	}
	//if (m_backgroundFrame2)
	//{
	//	delete [] m_backgroundFrame2;
	//	m_backgroundFrame2 = NULL;
	//}
	if (m_currentFrame)
	{
		delete [] m_currentFrame;
		m_currentFrame = NULL;
	}
	if (m_currentFrameDilatated)
	{
		delete [] m_currentFrameDilatated;
		m_currentFrameDilatated = NULL;
	}
}

void CDetector::Init(int width, int height, int imagesize, string filename)
{
	m_width = width;
	m_height = height;
	m_imagesize = imagesize;	
	_filename = filename;

	if (m_motionBuff)
	{
		delete [] m_motionBuff;
		m_motionBuff =  NULL;
	}
	m_motionBuff = new unsigned char[m_width * m_height /*+ (m_width+1)*/];
	m_curtime = 0;
}

unsigned char CDetector::GetThreshold()
{
	return m_threshold;
}
void CDetector::SetThreshold(unsigned char threshold)
{
	m_threshold = threshold;
}

void CDetector::SetMode(int mode)
{
	m_mode = mode;
}

string CDetector::GetLogFile() 
{
	return m_logfile;
}

void CDetector::SetLogFile(string logfile)
{
	m_logfile = logfile;
	m_idxfile = m_logfile;
	m_idxfile.replace(m_idxfile.end()-4, m_idxfile.end(), ".midx");
}

void CDetector::SetFilename(string filename)
{
	_filename = filename;
}

void CDetector::SetCriteria(int criteria)
{
	_criteria = criteria;
}

int  CDetector::GetCriteria()
{
	return _criteria;
}


void  CDetector::AddExcRect(RECT &r)
{
	m_excRects.push_back(r);
}

void  CDetector::ClearExcRects()
{
	m_excRects.clear();
}

void CDetector::Process(unsigned char *p, int size, REFERENCE_TIME smpltime, int framenum)
{
	m_curtime = smpltime;
	m_currframe = framenum;

	if ( m_mode != 1) // if not plotter mode
	{
		int fW = ( ( ( m_width - 1 ) / 8 ) + 1 );
		int fH = ( ( ( m_height - 1 ) / 8 ) + 1 );
		//int fW = m_width / 8;
		//int fH =  m_height / 8;
		int len = fW * fH;


		if (!m_backgroundFrame)
		{
			// alloc memory for a backgound image and for current image
			m_backgroundFrame = new unsigned char[len];
			memset(m_backgroundFrame,0,len);
			//m_backgroundFrame2 = new unsigned char[len];
			//memset(m_backgroundFrame2,0,len);
			m_currentFrame = new unsigned char[len];
			memset(m_currentFrame,0,len);
			m_currentFrameDilatated = new unsigned char [len];
			memset(m_currentFrameDilatated,0,len);

			// create initial background images
			PreprocessInputImage(p, m_backgroundFrame);
		//	PreprocessInputImage(p, m_backgroundFrame2);

			if (m_mode == 3) // full processing
				SaveMotion();

			return;
		}

		// preprocess input image
		PreprocessInputImage( p,  m_currentFrame );
		 
		if ( ++m_counter == 2 )
		{
			m_counter = 0;

			// move background towards current frame
			for ( int i = 0; i < len; i++ )
			{
				int t = m_currentFrame[i] - m_backgroundFrame[i];
				if ( t > 0 )
					m_backgroundFrame[i]++;
				else if ( t < 0 )
					m_backgroundFrame[i]--;
			}
		}

		//if ( ++m_counter3 == 4 )
		//{
		//	m_counter3 = 0;

		////	 move background2 towards current frame
		//	for ( int i = 0; i < len; i++ )
		//	{
		//		int t = m_backgroundFrame[i] - m_backgroundFrame2[i];
		//		if ( t > 0 )
		//			m_backgroundFrame2[i]++;
		//		else if ( t < 0 )
		//			m_backgroundFrame2[i]--;
		//	}
		//}


		// difference and thresholding
		m_pixelsChanged = 0;
		for ( int i = 0; i < len; i++ )
		{
			//int t = m_currentFrame[i] - m_backgroundFrame[i];
			//if ( t < 0 )
			//	t = -t;
			int t1 = abs(m_currentFrame[i] - m_backgroundFrame[i]);
			//int t2 = abs(m_currentFrame[i]  - m_backgroundFrame2[i]);


			if ( t1 >= m_threshold/* && t2 >= 50*/)
			{
				m_pixelsChanged++;
				m_currentFrame[i] = 255;
			}
			else
			{
				m_currentFrame[i] =  0;
			}
		}

		//if ( calculateMotionLevel )
		//	pixelsChanged *= 64;
		//else
		//   pixelsChanged = 0;

		// dilatation analogue for borders extending
		// it can be skipped
		for ( int i = 0; i < fH; i++ )
		{
			for ( int j = 0; j < fW; j++ )
			{
				int k = i * fW + j;
				int v = m_currentFrame[k];

				// left pixels
				if ( j > 0 )
				{
					v += m_currentFrame[k - 1];

					if ( i > 0 )
					{
						v += m_currentFrame[k - fW - 1];
					}
					if ( i < fH - 1 )
					{
						v += m_currentFrame[k + fW - 1];
					}
				}
				// right pixels
				if ( j < fW - 1 )
				{
					v += m_currentFrame[k + 1];

					if ( i > 0 )
					{
						v += m_currentFrame[k - fW + 1];
					}
					if ( i < fH - 1 )
					{
						v += m_currentFrame[k + fW + 1];
					}
				}
				// top pixel
				if ( i > 0 )
				{
					v += m_currentFrame[k - fW];
				}
				// right pixel
				if ( i < fH - 1 )
				{
					v += m_currentFrame[k + fW];
				}
				m_currentFrameDilatated[k] = (v != 0) ?  255 : 0;
			}
			// postprocess the input image
		}

		PostprocessInputImage( p, m_currentFrameDilatated );
	
		if (m_mode == 3) // full processing
			SaveMotion();

	
		
	///	for ( int i = 0; i < len; i++ )
		///	 m_backgroundFrame2[i] = m_currentFrame[i];
		
	}
}

//
// Preprocess input image
//
void CDetector::PreprocessInputImage( unsigned char *data, unsigned char  *buff )
{
	if (!_criteria)
		PreprocessInputImageMaxMin(data,buff);
	else
		PreprocessInputImageAvg(data,buff);
}
//
// Preprocess input image (avg criteria)
//
void CDetector::PreprocessInputImageAvg( unsigned char *data, unsigned char  *buff )
{
	int stride = m_width;
	int offset = 0;
	int len = (int)( ( m_width - 1 ) / 8 ) + 1;
	int rem = ( ( m_width - 1 ) % 8 ) + 1;
	int *tmp = new int[len];
	int i, j, t1, t2, k = 0;

	unsigned char *src =  data;
	unsigned char *m = m_motionBuff;

	for ( int y = 0; y < m_height; )
	{
		// collect pixels
		memset(tmp,0,len*sizeof(int));

		// calculate
		for ( i = 0; ( i < 8 ) && ( y < m_height ); i++, y++ )
		{
			// for each pixel
			for ( int x = 0; x < m_width; x++, src++, m++ )
			{
				// grayscale value using BT709
				//tmp[(int) ( x / 8 )] += (int)( 0.2125f * src[2] + 0.7154f * src[1] + 0.0721f * src[0] );
				tmp[(int) ( x / 8 )] += (int) *src;
				*m = 0;
			}
			src += offset;
		}

		// get average values
		t1 = i * 8;
		t2 = i * rem;
		
		for ( j = 0; j < len - 1; j++, k++ )
			buff[k] = (unsigned char )( tmp[j] / t1 );
		buff[k++] = (unsigned char )( tmp[j] / t2 );
	}
	delete [] tmp;
}

//
// Preprocess input image (MaxMin criteria)
//
void CDetector::PreprocessInputImageMaxMin( unsigned char *data, unsigned char  *buff )
{
	int stride = m_width;
	int offset = 0;
	int len = (int)( ( m_width - 1 ) / 8 ) + 1;
	int rem = ( ( m_width - 1 ) % 8 ) + 1;
	int i, j,k = 0, idx;

	unsigned char *src =  data;
	unsigned char *m = m_motionBuff;
		
	unsigned char *min = new unsigned char[len];
	unsigned char *max = new unsigned char[len];

	for ( int y = 0; y < m_height; )
	{
		// collect pixels
		memset(max,0,len);
		memset(min,255,len);

		// calculate
		for ( i = 0; ( i < 8 ) && ( y < m_height ); i++, y++ )
		{
			// for each pixel
			for ( int x = 0; x < m_width; x++, src++, m++ )
			{
				*m = 0;
				idx = (int) ( x / 8);
				if ( *src > max[idx] ) 
					max[idx] = *src;
				if ( *src <= min[idx] ) 
					min[idx]  = *src;
			}
			src += offset;
		}
		for ( j = 0; j < len; j++, k++ )
			buff[k] = max[j] - min[j];
	}
	delete [] min;
	delete [] max;
}

inline bool CDetector::InExcRects(int x, int y)
{
	//POINT pt;
	//pt.x = x;
	//pt.y = y;
	//	 
	list<RECT>::iterator it = m_excRects.begin();
	for(;it != m_excRects.end(); ++it)
	{
		if ((*it).left <= x && (*it).top <= y && (*it).right >= x && (*it).bottom >= y)
			return true;
	}
	return false;
}

// Postprocess input image
void CDetector::PostprocessInputImage( unsigned char *data, unsigned char *buff )
{
	//int stride = m_imagesize / m_height;
	//int offset = stride - m_width * 3;
	int stride = m_width;
	int offset = 0;

	int len = (int)( ( m_width - 1 ) / 8 ) + 1;
	int lenWM1 = len - 1;
	int lenHM1 = (int)( ( m_height - 1 ) / 8) ;
	//int rem = ( ( m_width - 1 ) % 8 ) + 1;

	int i, j, k;
	
	unsigned char *src = data;
	unsigned char *m = m_motionBuff;

	// for each line
	for ( int y = 0; y < m_height; y++ )
	{
		i = (y / 8);

		// for each pixel
		//for ( int x = 0; x < m_width; x++, src += 3 )
		for ( int x = 0; x < m_width; x++, src++, m++ )
		{
			//if (!InExcRects(x,y))
		//	{
				j = x / 8;	
				k = i * len + j;

				// check if we need to highlight moving object
				if (buff[k] == 255)
				{
					if (!InExcRects(x,y))
					{
						if (((x % 8 == 0) && ((j == 0) || (buff[k -	1] == 0)))	
							|| ((y % 8 == 0) && ((i == 0) || (buff[k - len] == 0))))
						{
							*src = 255;
							*m = 1;
						}

						if ((x % 8 == 7) && ((j == lenWM1) || (buff[k + 1] == 0)))
						{
							if ( x < m_width - 1)
							{
								*(src+1) = 255;
								*(m+1) = 1;
							}
							else
							{
								*src = 255;
								*m = 1;
							}
						}

						if ((y % 8 == 7) && ((i == lenHM1) || (buff[k + len] == 0)))
						{
							if ( y < m_height - 1)
							{
								*(src + m_width) = 255;
								*(m + m_width) = 1;
								if (x % 8 == 7 &&  x < m_width - 1 )
								{
									*(src+m_width+1) = 255;
									*(m+m_width+1) = 1;
								}
							}
							else
							{
								*src = 255;
								*m = 1;
							}
						}
					}
				}


			//}
		}
		src += offset;
	}
}

void CDetector::SaveIndex()
{
	ofstream out(m_logfile,ios_base::app | ios_base::ate)  ;

	//char s[256];
	//sprintf_s(s,256,"m_curtime=%I64d, m_currframe=%d, currpos=%d\n",m_curtime, m_currframe, m_currpos);
	//OutputDebugString(s);

	ofstream idx(m_idxfile,ios_base::app | ios_base::binary);
	idx.write((const char *)&m_curtime, sizeof(REFERENCE_TIME));
	idx.write((const char *)&m_currframe, sizeof(int));
	idx.write((const char *)&m_currpos, sizeof(int));
	idx.close();
	out.close();
}

void CDetector::SaveMotion()
{
	m_currpos = -1;

	int size = m_width * m_height;
	unsigned char *m = m_motionBuff;
	int objno = 0;
	bool b = false;
	for (int i = 0; i < size; i++)
	{
		if (m[i] != 2)
		{
			if (m[i] == 1)
			{
				IdentifyObject(i, objno++);
				b = true;
			}
			m[i] = 2;
		}
	}

	SaveIndex();
	
	if (b)
	{
		ofstream out(m_logfile,ios_base::app);
		out << "\n";
		out.close();
	}
}

void CDetector::IdentifyObject(int i, int objno)
{
	list<POINT>objpts;
	unsigned char *m = m_motionBuff;
	int w = m_width;
	int h = m_height;

	int org = i;
	bool b = true;
	int cur = 0;
	int prev = -1;

	int pi = i;

	for(;;)
	{
		m[i] = 2; // processed

		if ( i == org  && !b )
		{
			//OutputDebugString("\nend\n\n");
			break;
		}

		b = false;
		if (cur != prev)
		{
			POINT p;
			p.y = pi / w;
			p.x = pi - p.y*w;
			objpts.push_back(p);

			prev = cur;
		}

		// check right
		if((i+1  <h*w && m[i+1] == 1) || (i+1 == org))
		{
			//char s[256];
			//sprintf_s(s,256,"right, i = %d, i+1 = %d\n", i, i+1);
			//OutputDebugString(s);

			pi = i;
			i++;
			cur = 0;
		}
		// check right-bottom
		//else if((i+w+1 <h*w && m[i+w+1] == 1) || (i+w+1 == org))
		//{
		//	i+=w+1;
		//}
		// check bottom
		else if ((i+w < h*w && m[i+w] == 1) || (i+w == org))
		{
			//char s[256];
			//sprintf_s(s,256,"bottom, i = %d, i+w = %d\n", i, i+w);
			//OutputDebugString(s);

			pi = i;
			i+=w;
			cur = 1;
		}
		// check bottom-left
		//else if ((i+w-1 < h*w && m[i+w-1] == 1) || (i+w+1 == org))
		//{
		//	i+=w-1;
		//}
		// check left
		else if ((i-1 > 0 && m[i-1] == 1) || (i-1 == org))
		{
			/*char s[256];
			sprintf_s(s,256,"left, i = %d, i-1 = %d\n", i, i-1);
			OutputDebugString(s);*/

			pi = i;
			i--;
			cur = 2;			
		}
		// check left-up
		//else if ((i-w-1 > 0 && m[i-w-1] == 1) || (i-w-1 == org))
		//{
		//	i-=w-1;
		//}
		// check up
		else if ((i-w > 0 && m[i-w] == 1) || (i-w == org))
		{
			//char s[256];
			//sprintf_s(s,256,"up, i = %d, i-w = %d\n", i, i-w);
			//OutputDebugString(s);

			pi = i;
			i-=w;
			cur = 3;
		}
		// check up-right
		//else if ((i-w+1 > 0 && m[i-w+1] == 1) || (i+w+1 == org))
		//{
		//	i-=w+1;
		//}
		else
		{
			//notf = true;
			break; // for border cases
		}
	}
	SaveObject(objpts, objno);	
}

void CDetector::WriteHdr(int brightness, int contrast)
{
	_brightness = brightness;
	_contrast = contrast;

	ofstream out(m_logfile,ios_base::app | ios_base::ate);
	out << "Filename: " << _filename << "\n";
	out << "Threshold: " << (int)m_threshold << "\n";
	out << "Contrast: " << contrast << "\n";
	out << "Brightness: " << brightness << "\n\n";	
	out.close();
}


void  CDetector::SaveObject(list<POINT>&points, int objno)
{
	ofstream out(m_logfile,ios_base::app | ios_base::ate)  ;

	if (!objno)
	{
		CRefTime rt = m_curtime;
		// first object
		char buf[256];
		int mseconds =  rt.Millisecs();
		int hours = mseconds / 3600000;
		mseconds -= hours * 3600000;
		int minutes = mseconds / 60000;
		mseconds -=  minutes * 60000; 
		int seconds = mseconds / 1000;
		mseconds -= seconds * 1000;

		// Save index portion
		m_currpos = (int)out.tellp();

	////char s[256];
	////sprintf_s(s,256,"m_curtime=%I64d, m_currframe=%d, currpos=%d\n",m_curtime, m_currframe, currpos);
	////OutputDebugString(s);

	//	ofstream idx(m_idxfile,ios_base::app | ios_base::binary);
	//	idx.write((const char *)&m_curtime, sizeof(REFERENCE_TIME));
	//	idx.write((const char *)&m_currframe, sizeof(int));
	//	idx.write((const char *)&currpos, sizeof(int));
	//	idx.close();

		sprintf_s(buf,256,"%0.2d:%0.2d:%0.2d.%0.2d",hours, minutes, seconds, mseconds/10);
		out << "Frame: " << m_currframe << ", time: " << buf << "\n";
		//currpos = (int)out.tellp();
	}
	
	out << "Object " << objno << ": " ;
	list<POINT>::iterator it = points.begin();
	for(;it != points.end(); ++it)
		out << "(" << it->x << "," << it->y << ");";
	out << "\n";
	out.close();
}