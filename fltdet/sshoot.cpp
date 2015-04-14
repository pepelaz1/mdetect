#include "sshoot.h"
#include "jpeglib.h"

CSShooter::CSShooter() :
	 _buff(0)
{
}

CSShooter::~CSShooter()
{
	Clear();
}

void CSShooter::Init(int width, int height, int size)
{
	_width = width;
	_height = (height/16)*16;
	_diff = height - _height;

	_size = size;
	_buff = new unsigned char[_size];
}

void CSShooter::Clear()
{
	if (_buff)
	{
		delete [] _buff;
		_buff = 0;
	}
}

void CSShooter::Copy(unsigned char *p)
{
	CAutoLock lck(&_cs);
	memcpy(_buff,p,_size);
}

void CSShooter::Save(string path)
{

	if (path == "")
	{
		char dir[256];
		GetCurrentDirectory(256,dir);
		path = dir;
		MessageBox(NULL,dir,dir, MB_OK);
	}

	CAutoLock lck(&_cs);
	char  filename[256];
	SYSTEMTIME st;
	GetSystemTime(&st);
	sprintf_s(filename, 256, "%s\\%d-%02d-%02d_%02d-%02d-%02d-%03d.jpg",path.data(), st.wYear, st.wMonth, st.wDay, st.wHour, st.wMinute, st.wSecond, st.wMilliseconds);

	struct jpeg_compress_struct cinfo;
	struct jpeg_error_mgr jerr;

	FILE * outfile;		
	cinfo.err = jpeg_std_error(&jerr);
	jpeg_create_compress(&cinfo);

	outfile = fopen(filename, "wb");
	jpeg_stdio_dest(&cinfo, outfile);

	cinfo.image_width = _width; 	
	cinfo.image_height = _height;
	cinfo.input_components = 3;		
	cinfo.in_color_space = JCS_YCbCr; 	
	jpeg_set_defaults(&cinfo);

	JSAMPARRAY  pp[3];	
	JSAMPROW *rpY = (JSAMPROW*)malloc(sizeof(JSAMPROW) * _height);
	JSAMPROW *rpU = (JSAMPROW*)malloc(sizeof(JSAMPROW) * _height);
	JSAMPROW *rpV = (JSAMPROW*)malloc(sizeof(JSAMPROW) * _height);

	cinfo.comp_info[0].h_samp_factor = 2;
	cinfo.comp_info[0].v_samp_factor = 2;
	cinfo.comp_info[1].h_samp_factor = 1;
	cinfo.comp_info[1].v_samp_factor = 1;
	cinfo.comp_info[2].h_samp_factor = 1;
	cinfo.comp_info[2].v_samp_factor = 1;

	jpeg_set_quality(&cinfo, 100, TRUE);
	cinfo.raw_data_in = TRUE;  
	cinfo.dct_method = JDCT_FLOAT;

	jpeg_start_compress(&cinfo, TRUE);

	int k;
	
	unsigned char *ch = _buff +_width*(_height+_diff);
	unsigned char *u = new unsigned char [(_width*_height)/4];
	unsigned char *v = new unsigned char [(_width*_height)/4];
	for (k = 0; k < (_width * _height)/4; k++)
	{ 
		u[k] = ch[2*k];
		v[k] = ch[2*k+1];
	}
	memcpy(ch, u,(_width * _height)/4);
	memcpy(ch+(_width * _height)/4, v,(_width * _height)/4);

	free(u);
	free(v);

	for (k = 0; k < _height; k+=2) 
	{
		rpY[k]   = _buff + k*_width;
		rpY[k+1] = _buff + (k+1)*_width;
	    rpU[k/2] =  ch + (k/2)*_width/2;
        rpV[k/2] =  ch + (_width*_height)/4 + (k/2)*_width/2;
	}


	for (k = 0; k < _height; k+=2*DCTSIZE) 
	{
		pp[0] = &rpY[k];
		pp[1] = &rpU[k/2];
		pp[2] = &rpV[k/2];
		jpeg_write_raw_data(&cinfo, pp, 2*DCTSIZE);
	}
	
	jpeg_finish_compress(&cinfo);
	
	free(rpY);
	free(rpU);
	free(rpV);
	fclose(outfile);
	jpeg_destroy_compress(&cinfo);
}