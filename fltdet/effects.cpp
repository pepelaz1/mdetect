#include "effects.h"


CEffectApplier::CEffectApplier() :
	m_brightness(128),
	m_contrast(128)
{
}


CEffectApplier::~CEffectApplier()
{
}


void CEffectApplier::SetContrast(unsigned char contrast)
{
	m_contrast = contrast;
}

void CEffectApplier::SetBrightness(unsigned char brightness)
{
	m_brightness = brightness;
}

unsigned char CEffectApplier::GetContrast()
{
	return m_contrast;
}
unsigned char CEffectApplier::GetBrightness()
{
	return m_brightness;
}

void CEffectApplier::Process(unsigned char *p, int size)
{
    double C = (double)m_contrast / 127.0;
	int B = m_brightness;
	for ( int i = 0; i < size; i++)
	{
		short v = p[i];
		v = (short)((v - 16) * C) ;
		if ( v > 255 ) v = 255;
		if ( v < 0)  v = 0;
		v = v + (B-127) + 16;
		if ( v > 255 ) v = 255;
		if ( v < 0)  v = 0;
		p[i] = (unsigned char)v;
	}
}