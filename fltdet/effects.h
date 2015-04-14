#pragma once
class CEffectApplier
{
private:
	unsigned char m_brightness;
	unsigned char m_contrast;
public:
	CEffectApplier();
	~CEffectApplier();
	void Process(unsigned char *p, int size);
	void SetContrast(unsigned char contrast);
	void SetBrightness(unsigned char brightness);
	unsigned char GetContrast();
	unsigned char GetBrightness();
};

