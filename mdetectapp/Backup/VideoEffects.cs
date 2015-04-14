using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;


namespace MotionDetector
{
    public class VideoEffects
    {



        public static Bitmap ApplyBrightness(Bitmap srcBmp, float brightnessPercent)
        {
            float brightness = brightnessPercent / 100f;
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            float[][] ptsArray ={   new float[] {1f, 0, 0, 0, 0},
								    new float[] {0, 1f, 0, 0, 0},
									new float[] {0, 0, 1f, 0, 0},
									new float[] {0, 0, 0, 1, 0}, 
									new float[] {brightness, brightness, brightness, 0, 1}};

            ColorMatrix clrMatrix = new ColorMatrix(ptsArray);
            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(clrMatrix);
            Graphics g = Graphics.FromImage(destBmp);
            g.DrawImage(srcBmp, new Rectangle(0, 0, destBmp.Width, destBmp.Height), 0, 0, srcBmp.Width, srcBmp.Height, GraphicsUnit.Pixel, imgAttributes);
            g.Dispose();
            return destBmp;
        }



        public static Bitmap ApplyContrast(Bitmap srcBmp, float contrastPercent)
        {
            float contrast = contrastPercent / 100f;
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            float trans = -0.5f * contrast + 0.5f;
            float[][] ptsArray ={   new float[] {contrast, 0, 0, 0, 0},
								    new float[] {0, contrast, 0, 0, 0},
									new float[] {0, 0, contrast, 0, 0},
									new float[] {0, 0, 0, 1, 0}, 
									new float[] {trans, trans, trans, 0, 1}};

            ColorMatrix clrMatrix = new ColorMatrix(ptsArray);
            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(clrMatrix);
            Graphics g = Graphics.FromImage(destBmp);
            g.DrawImage(srcBmp, new Rectangle(0, 0, destBmp.Width, destBmp.Height), 0, 0, srcBmp.Width, srcBmp.Height, GraphicsUnit.Pixel, imgAttributes);
            g.Dispose();
            return destBmp;
        }


        private static int ColorValue(double value)
        {
            int intVal = (int)value;// Math.Round(value);
            if (intVal < 0) intVal = 0;
            if (intVal > 255) intVal = 255;
            return intVal;
        }


        private static int RoundColorValue(int value)
        {
            if (value < 0) value = 0;
            if (value > 255) value = 255;
            return value;
        }



        public unsafe static void ApplyBrightnessInPlaceUnsafe(Bitmap srcBmp, float brightnessPercent)
        {
            float brightness = brightnessPercent / 100f;
            int transf = (int)(brightness * 255);

            BitmapData bmData = srcBmp.LockBits(new Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            byte* pBmp = (byte*)bmData.Scan0;

            int width = srcBmp.Width;
            int height = srcBmp.Height;
            int diffWidth = bmData.Stride - width * 3;


            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = (x + y * width) * 3 + diffWidth * y;
                    int pixB = (int)pBmp[index];
                    int pixG = (int)pBmp[index + 1];
                    int pixR = (int)pBmp[index + 2];


                    

                    int r = RoundColorValue(pixR + transf);
                    int g = RoundColorValue(pixG + transf);
                    int b = RoundColorValue(pixB + transf);

                    pBmp[index] = (byte)b;
                    pBmp[index + 1] = (byte)g;
                    pBmp[index + 2] = (byte)r;


                }
            }

            srcBmp.UnlockBits(bmData);

        }



        public unsafe static void ApplyContrastInPlaceUnsafe(Bitmap srcBmp, float contrastPercent)
        {
            float contrast = contrastPercent / 100f;
            int transf = (int)(-128f * contrast + 128f);

            BitmapData bmData = srcBmp.LockBits(new Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            byte* pBmp = (byte*)bmData.Scan0;

            int width = srcBmp.Width;
            int height = srcBmp.Height;
            int diffWidth = bmData.Stride - width * 3;


            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = (x + y * width) * 3 + diffWidth * y;
                    int pixB = (int)pBmp[index];
                    int pixG = (int)pBmp[index + 1];
                    int pixR = (int)pBmp[index + 2];



                    int r = RoundColorValue((int)(pixR * contrast) + transf);
                    int g = RoundColorValue((int)(pixG * contrast) + transf);
                    int b = RoundColorValue((int)(pixB * contrast) + transf);

                    pBmp[index] = (byte)b;
                    pBmp[index + 1] = (byte)g;
                    pBmp[index + 2] = (byte)r;


                }
            }

            srcBmp.UnlockBits(bmData);

        }


        public static void ApplyBrightnessInPlace(Bitmap srcBmp, float brightnessPercent)
        {
            float brightness = brightnessPercent / 100f;
            float transf = brightness * 255;

            FastBitmap fastBitmap = new FastBitmap(srcBmp);
            fastBitmap.LockImage();

            int width = srcBmp.Width;
            int height = srcBmp.Height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = fastBitmap.GetPixel(x, y);

                    int r = ColorValue(pixel.R + transf);
                    int g = ColorValue(pixel.G + transf);
                    int b = ColorValue(pixel.B + transf);

                    Color pixelProcessed = Color.FromArgb(r, g, b);
                    fastBitmap.SetPixel(x, y, pixelProcessed);
                }
            }

            fastBitmap.UnlockImage();
        }



        public static void ApplyContrastInPlace(Bitmap srcBmp, float contrastPercent)
        {
            float contrast = contrastPercent / 100f;
            float transf = -128f * contrast + 128f;

            FastBitmap fastBitmap = new FastBitmap(srcBmp);
            fastBitmap.LockImage();

            int width = srcBmp.Width;
            int height = srcBmp.Height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = fastBitmap.GetPixel(x, y);

                    int r = ColorValue(pixel.R * contrast + transf);
                    int g = ColorValue(pixel.G * contrast + transf);
                    int b = ColorValue(pixel.B * contrast + transf);

                    Color pixelProcessed = Color.FromArgb(r, g, b);
                    fastBitmap.SetPixel(x, y, pixelProcessed);
                }
            }

            fastBitmap.UnlockImage();


        }






        public unsafe static void ApplyBrightnessFast(Bitmap srcBmp, float brightnessPercent)
        {
            float brightness = brightnessPercent / 100f;
            float transf = brightness * 255;


            int width = srcBmp.Width;
            int height = srcBmp.Height;

            BitmapData imageBmData = srcBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte* pImage = (byte*)imageBmData.Scan0;
            int diffImage = imageBmData.Stride - width * 3;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int pIndex = (x + y * width) * 3 + diffImage * y;
                    int pixB = (int)pImage[pIndex];
                    int pixG = (int)pImage[pIndex + 1];
                    int pixR = (int)pImage[pIndex + 2];


                    int r = ColorValue(pixR + transf);
                    int g = ColorValue(pixG + transf);
                    int b = ColorValue(pixB + transf);

                    pImage[pIndex] = (byte)b;
                    pImage[pIndex + 1] = (byte)g;
                    pImage[pIndex + 2] = (byte)r;
                }
            }

            srcBmp.UnlockBits(imageBmData);
        }


        public unsafe static void ApplyContrastFast(Bitmap srcBmp, float contrastPercent)
        {
            float contrast = contrastPercent / 100f;
            float transf = -128f * contrast + 128f;


            int width = srcBmp.Width;
            int height = srcBmp.Height;

            BitmapData imageBmData = srcBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte* pImage = (byte*)imageBmData.Scan0;
            int diffImage = imageBmData.Stride - width * 3;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int pIndex = (x + y * width) * 3 + diffImage * y;
                    int pixB = (int)pImage[pIndex];
                    int pixG = (int)pImage[pIndex + 1];
                    int pixR = (int)pImage[pIndex + 2];


                    int r = ColorValue(pixR * contrast + transf);
                    int g = ColorValue(pixG * contrast + transf);
                    int b = ColorValue(pixB * contrast + transf);

                    pImage[pIndex] = (byte)b;
                    pImage[pIndex + 1] = (byte)g;
                    pImage[pIndex + 2] = (byte)r;
                }
            }

            srcBmp.UnlockBits(imageBmData);
        }



        public static Bitmap ApplyContrastFast1(Bitmap srcBmp, float contrastPercent)
        {
            float contrast = contrastPercent / 100f;
            float transf = -128f * contrast + 128f;

            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            FastBitmap fastBitmapDest = new FastBitmap(destBmp);
            fastBitmapDest.LockImage();

            FastBitmap fastBitmap = new FastBitmap(srcBmp);
            fastBitmap.LockImage();

            int width = srcBmp.Width;
            int height = srcBmp.Height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = fastBitmap.GetPixel(x, y);

                    int r = ColorValue(pixel.R * contrast + transf);
                    int g = ColorValue(pixel.G * contrast + transf);
                    int b = ColorValue(pixel.B * contrast + transf);

                    Color pixelProcessed = Color.FromArgb(r, g, b);
                    //fastBitmap.SetPixel(x, y, pixelProcessed);
                    fastBitmapDest.SetPixel(x, y, pixelProcessed);
                }
            }

            fastBitmap.UnlockImage();
            fastBitmapDest.UnlockImage();

            return destBmp;
        }



    }
}
