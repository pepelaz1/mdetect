#pragma once
#include "time.h"
#include <stdio.h>
#include <stdlib.h>
#include <string>
#include <sstream>
#include <fstream>
#include <streams.h>
using namespace std;

class CSShooter
{
private:
	CCritSec _cs;
	int _width;
	int _height;
	int _diff;
	int _size;
	unsigned char *_buff;
	
	void Clear();
public:
	CSShooter();
	~CSShooter();

	void Init(int width, int height, int size);
	void Copy(unsigned char *p);
	void Save(string path);
};