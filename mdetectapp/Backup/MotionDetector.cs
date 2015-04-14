using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using OpenCvSharp;



namespace MotionDetector
{








    public class MotionDetector
    {

        // TODO:
        // find closest point on the next frame
        // add multiple points within a radius and update the best feature within that radius
        // find corners every time, look for closest traking points and update, , find subcorners.

        


        // stores the points calculated by GoodFeaturesToTrack, when newPoints=true
        public static CvPoint2D32f[] InitialPoints = null;
        // stores the updated points tracked each frame, when newPoints=false. If a point lost track, it has negative values
        public static CvPoint2D32f[] UpdatedPoints;
        // for each point, stores a list of vector since its initial position until the current updated position
        public static List<MotionVector>[] PointVectors;


        public static void DetectMotionTrackPoints(Bitmap frame1, Bitmap frame2, bool newPoints)
        {

            try
            {

                using (IplImage img1 = IplImage.FromBitmap(frame1))
                using (IplImage img2 = IplImage.FromBitmap(frame2))
                {
                    int cols = img1.Width;
                    int rows = img1.Height;

                    using (IplImage eigImg = new IplImage(cols, rows, BitDepth.F32, 1))
                    using (IplImage tempImg = new IplImage(cols, rows, BitDepth.F32, 1))
                    using (IplImage gray1 = new IplImage(cols, rows, BitDepth.U8, 1))
                    using (IplImage gray2 = new IplImage(cols, rows, BitDepth.U8, 1))
                    {
                        Cv.CvtColor(img1, gray1, ColorConversion.BgrToGray);
                        Cv.CvtColor(img2, gray2, ColorConversion.BgrToGray);

                        int cornerCount = (frame1.Width * frame1.Height) / 100; //frame1.Width + frame1.Height; //250;
                        cornerCount = frame1.Width + frame1.Height;
                        cornerCount = (int)Math.Max(cornerCount, 200);

                        CvPoint2D32f[] points1;
                        int[] indexes1 = new int[cornerCount];
                        for (int i = 0; i < indexes1.Length; i++)
                        {
                            indexes1[i] = i;
                        }

                        if (newPoints)
                        {
                            Cv.GoodFeaturesToTrack(gray1, eigImg, tempImg, out points1, ref cornerCount, 0.001, 5);
                            Cv.FindCornerSubPix(gray1, points1, cornerCount, new CvSize(5, 5), new CvSize(-1, -1), new CvTermCriteria(CriteriaType.Epsilon | CriteriaType.Iteration, 20, 0.001));
                            
                            InitialPoints = points1;
                            UpdatedPoints = new CvPoint2D32f[cornerCount];
                            PointVectors = new List<MotionVector>[cornerCount];
                            for (int v = 0; v < PointVectors.Length; v++)
                            {
                                PointVectors[v] = new List<MotionVector>();
                            }
                        }
                        else
                        {
                            // updates the point that are still being tracked - not negative values
                            List<CvPoint2D32f> validPoints = new List<CvPoint2D32f>();
                            List<int> indexes = new List<int>();
                            for (int i = 0; i < UpdatedPoints.Length; i++)
                            {
                                if (UpdatedPoints[i] != null && UpdatedPoints[i].X >= 0)
                                {
                                    CvPoint2D32f point = UpdatedPoints[i];

                                    validPoints.Add(point);
                                    indexes.Add(i);
                                }
                            }
                            points1 = validPoints.ToArray();
                            indexes1 = indexes.ToArray();
                        }


                        using (CvMat velx = Cv.CreateMat(rows, cols, MatrixType.F32C1))
                        using (CvMat vely = Cv.CreateMat(rows, cols, MatrixType.F32C1))
                        {
                            Cv.SetZero(velx);
                            Cv.SetZero(vely);

                            CvPoint2D32f[] points2;
                            sbyte[] status;
                            float[] trackError;
                            // maxlevel was 10, window was 10,0
                            Cv.CalcOpticalFlowPyrLK(gray1, gray2, null, null, points1, out points2, Cv.Size(100, 100), 10, out status, out trackError, Cv.TermCriteria(CriteriaType.Iteration | CriteriaType.Epsilon, 64, 0.01), (LKFlowFlag)0);

                            for (int i = 0; i < points1.Length; i++)
                            {
                                int index = indexes1[i];

                                if (status[i] != 0)
                                {
                                    PointF point1 = new PointF(points1[i].X, points1[i].Y);
                                    PointF point2 = new PointF(points2[i].X, points2[i].Y);

                                    MotionVector vector = new MotionVector(point1, point2);


                                    PointVectors[index].Add(vector);

                                    UpdatedPoints[index] = points2[i];

                                }
                                else
                                {
                                    UpdatedPoints[index] = new CvPoint2D32f(-1, -1);
                                }

                            }


                        }

                    }


                }

            }
            catch (Exception ex)
            {
            }


        }




        public static bool IsValid(MotionVector vector, List<MotionVector> vectorParts, int width)
        {

            //List<MotionVector> vectorParts = vector.VectorParts;

            // no vector parts - when data was imported from file
            if (vectorParts.Count == 0)
            {
                return true;
            }

            double errorTolerance = 5;

            double avgNorm = vector.Norm / vectorParts.Count;
            double avgX = (vector.Point2.X - vector.Point1.X) / vectorParts.Count;
            double avgY = (vector.Point2.Y - vector.Point1.Y) / vectorParts.Count;

            float diffX = vector.Point2.X - vector.Point1.X;
            float diffY = vector.Point2.Y - vector.Point1.Y;

            double vDirX = diffX / Math.Abs(diffX);
            double vDirY = diffY / Math.Abs(diffY);


            bool valid = true;


            foreach (MotionVector vectorPart in vectorParts)
            {

                //valid &= Math.Abs(1.0 - (vectorPart.Norm / avgNorm)) < errorTolerance;
                //valid &= Math.Abs(1.0 - (vectorPart.Direction / vector.Direction)) < errorTolerance;


                // each part of the vectors should have size similar to the other parts
                valid &= Math.Abs(1.0 - ((vectorPart.Point2.X - vectorPart.Point1.X) / avgX)) <= errorTolerance;
                valid &= Math.Abs(1.0 - ((vectorPart.Point2.Y - vectorPart.Point1.Y) / avgY)) <= errorTolerance;


                float x = vectorPart.Point2.X - vectorPart.Point1.X;
                float y = vectorPart.Point2.Y - vectorPart.Point1.Y;

                double dirX = x / Math.Abs(x);
                double dirY = y / Math.Abs(y);

                //valid &= dirX == vDirX && dirY == vDirY;

                // if some vector is too big for just one frame, then it's likely noise
                //valid &= vectorPart.Norm < vectorParts.Count * width / 100.0;
                //valid &= vectorPart.Norm < width / 100.0;


            }

            return valid;
            
        }



        public static List<MotionVector> DetectMotion12(Bitmap frame1, Bitmap frame2, bool newPoints)
        {

            List<MotionVector> motionVectors = new List<MotionVector>();
            try
            {

                using (IplImage img1 = IplImage.FromBitmap(frame1))
                using (IplImage img2 = IplImage.FromBitmap(frame2))
                {
                    int cols = img1.Width;
                    int rows = img1.Height;

                    using (IplImage eigImg = new IplImage(cols, rows, BitDepth.F32, 1))
                    using (IplImage tempImg = new IplImage(cols, rows, BitDepth.F32, 1))
                    using (IplImage gray1 = new IplImage(cols, rows, BitDepth.U8, 1))
                    using (IplImage gray2 = new IplImage(cols, rows, BitDepth.U8, 1))
                    {
                        Cv.CvtColor(img1, gray1, ColorConversion.BgrToGray);
                        Cv.CvtColor(img2, gray2, ColorConversion.BgrToGray);

                        int cornerCount = 250;
                        CvPoint2D32f[] points1;
                        int[] indexes1 = new int[cornerCount];
                        for (int i = 0; i < indexes1.Length; i++) indexes1[i] = i;

                        if (newPoints)
                        {
                            Cv.GoodFeaturesToTrack(gray1, eigImg, tempImg, out points1, ref cornerCount, 0.001, 5);
                            InitialPoints = points1;
                            UpdatedPoints = new CvPoint2D32f[cornerCount];
                            PointVectors = new List<MotionVector>[cornerCount];
                            for (int v = 0; v < PointVectors.Length; v++) PointVectors[v] = new List<MotionVector>();
                        }
                        else
                        {
                            List<CvPoint2D32f> validPoints = new List<CvPoint2D32f>();
                            List<int> indexes = new List<int>();
                            for (int i = 0; i < UpdatedPoints.Length; i++)
                            {
                                if (UpdatedPoints[i] != null && UpdatedPoints[i].X >= 0)
                                {
                                    CvPoint2D32f point = UpdatedPoints[i];



                                    /*
                                    int radius = 4;
                                    CvRect rect = new CvRect((int)Math.Round(point.X) - radius, (int)Math.Round(point.Y) - radius, radius * 2, radius * 2);
                                    gray1.ResetROI();
                                    gray1.ROI = rect;
                                    CvPoint2D32f[] corners;
                                    int nrCorners = 3;
                                    Cv.GoodFeaturesToTrack(gray1, eigImg, tempImg, out corners, ref nrCorners, 0.001, 5);
                                    gray1.ResetROI();

                                    if (nrCorners > 0)
                                    {
                                        double avgX = 0;
                                        double avgY = 0;
                                        for (int c = 0; c < nrCorners; c++)
                                        {
                                            avgX += corners[c].X;
                                            avgY += corners[c].Y;
                                        }
                                        avgX /= nrCorners;
                                        avgY /= nrCorners;
                                        avgX += rect.X;
                                        avgY += rect.Y;

                                        point.X = (int)Math.Round(avgX);
                                        point.Y = (int)Math.Round(avgY);
                                    }
                                    else
                                    {
                                        //point = point;

                                    }
                                    */

                                    validPoints.Add(point);
                                    indexes.Add(i);
                                }
                            }
                            points1 = validPoints.ToArray();
                            indexes1 = indexes.ToArray();
                        }


                        using (CvMat velx = Cv.CreateMat(rows, cols, MatrixType.F32C1))
                        using (CvMat vely = Cv.CreateMat(rows, cols, MatrixType.F32C1))
                        {
                            Cv.SetZero(velx);
                            Cv.SetZero(vely);

                            CvPoint2D32f[] points2;
                            sbyte[] status;
                            float[] trackError;
                            Cv.CalcOpticalFlowPyrLK(gray1, gray2, null, null, points1, out points2, Cv.Size(10, 10), 4, out status, out trackError, Cv.TermCriteria(CriteriaType.Iteration | CriteriaType.Epsilon, 64, 0.01), (LKFlowFlag)0);

                            for (int i = 0; i < points1.Length; i++)
                            {
                                int index = indexes1[i];

                                if (status[i] != 0)
                                {
                                    PointF point1 = new PointF(points1[i].X, points1[i].Y);
                                    PointF point2 = new PointF(points2[i].X, points2[i].Y);

                                    MotionVector vector = new MotionVector(point1, point2);

                                    // TODO: set custom threshold (max diplacement per frame)
                                    if (vector.Norm < 1000)
                                    {
                                        //motionVectors.Add(vector);

                                        PointVectors[index].Add(vector);

                                        //g.DrawLine(pen, points1[i].X, points1[i].Y, points2[i].X, points2[i].Y);

                                        UpdatedPoints[index] = points2[i];
                                    }
                                    else
                                    {
                                        UpdatedPoints[index] = new CvPoint2D32f(-1, -1);
                                    }
                                }
                                else
                                {
                                    UpdatedPoints[index] = new CvPoint2D32f(-1, -1);
                                }

                            }



                            //Cv.FindCornerSubPix(gray2, _updatedPoints, _updatedPoints.Length, new CvSize(2, 2), new CvSize(-1, -1), new CvTermCriteria(CriteriaType.Epsilon | CriteriaType.Iteration, 20, 0.03));
                            //cvFindCornerSubPix(imgA, cornersA, corner_count, cvSize(win_size, win_size),cvSize(-1, -1), cvTermCriteria(CV_TERMCRIT_ITER | CV_TERMCRIT_EPS, 20, 0.03));


                        }

                    }


                }

            }
            catch (Exception ex)
            {
                //return null;
            }

            return motionVectors;


        }



        public static List<MotionVector> DetectMotion(Bitmap frame1, Bitmap frame2)
        {

            List<MotionVector> motionVectors = new List<MotionVector>();
            try
            {

                using (IplImage img1 = IplImage.FromBitmap(frame1))
                using (IplImage img2 = IplImage.FromBitmap(frame2))
                {
                    int cols = img1.Width;
                    int rows = img1.Height;

                    using (IplImage eigImg = new IplImage(cols, rows, BitDepth.F32, 1))
                    using (IplImage tempImg = new IplImage(cols, rows, BitDepth.F32, 1))
                    using (IplImage gray1 = new IplImage(cols, rows, BitDepth.U8, 1))
                    using (IplImage gray2 = new IplImage(cols, rows, BitDepth.U8, 1))
                    {
                        Cv.CvtColor(img1, gray1, ColorConversion.BgrToGray);
                        Cv.CvtColor(img2, gray2, ColorConversion.BgrToGray);

                        int cornerCount = 150;
                        CvPoint2D32f[] points1;
                        Cv.GoodFeaturesToTrack(gray1, eigImg, tempImg, out points1, ref cornerCount, 0.001, 5);

                        using (CvMat velx = Cv.CreateMat(rows, cols, MatrixType.F32C1))
                        using (CvMat vely = Cv.CreateMat(rows, cols, MatrixType.F32C1))
                        {
                            Cv.SetZero(velx);
                            Cv.SetZero(vely);

                            CvPoint2D32f[] points2;
                            sbyte[] status;
                            float[] trackError;
                            Cv.CalcOpticalFlowPyrLK(gray1, gray2, null, null, points1, out points2, Cv.Size(10, 10), 4, out status, out trackError, Cv.TermCriteria(CriteriaType.Iteration | CriteriaType.Epsilon, 64, 0.01), (LKFlowFlag)0);

                            for (int i = 0; i < cornerCount; i++)
                            {
                                if (status[i] != 0)
                                {
                                    PointF point1 = new PointF(points1[i].X, points1[i].Y);
                                    PointF point2 = new PointF(points2[i].X, points2[i].Y);

                                    motionVectors.Add(new MotionVector(point1, point2));
                                    //g.DrawLine(pen, points1[i].X, points1[i].Y, points2[i].X, points2[i].Y);
                                }

                            }


                        }

                    }


                }

            }
            catch (Exception ex)
            {
                //return null;
            }

            return motionVectors;


        }









        public static List<MotionVector> DetectMotionBM(Bitmap frame1, Bitmap frame2)
        {

            List<MotionVector> motionVectors = new List<MotionVector>();
            try
            {

                const int BlockSize = 16;
                const int ShiftSize = 20;

                CvSize block = new CvSize(BlockSize, BlockSize);
                CvSize shift = new CvSize(ShiftSize, ShiftSize);
                CvSize maxRange = new CvSize(48, 48);


                using (IplImage img1 = IplImage.FromBitmap(frame1))
                using (IplImage img2 = IplImage.FromBitmap(frame2))
                {
                    int cols = img1.Width;
                    int rows = img1.Height;

                    using (IplImage gray1 = new IplImage(cols, rows, BitDepth.U8, 1))
                    using (IplImage gray2 = new IplImage(cols, rows, BitDepth.U8, 1))
                    {
                        Cv.CvtColor(img1, gray1, ColorConversion.BgrToGray);
                        Cv.CvtColor(img2, gray2, ColorConversion.BgrToGray);


                        CvSize velSize = new CvSize();

                        velSize.Width = (frame1.Width - block.Width) / shift.Width;
                        velSize.Height = (frame1.Height - block.Height) / shift.Height;

                        using (CvMat velx = Cv.CreateMat(velSize.Height, velSize.Width, MatrixType.F32C1))
                        using (CvMat vely = Cv.CreateMat(velSize.Height, velSize.Width, MatrixType.F32C1))
                        {

                            Cv.SetZero(velx);
                            Cv.SetZero(vely);

                            Cv.CalcOpticalFlowBM(gray1, gray2, block, shift, maxRange, false, velx, vely);


                            for (int i = 0; i < velx.Height; i++)
                            {
                                for (int j = 0; j < vely.Width; j++)
                                {
                                    int dx = (int)Cv.GetReal2D(velx, j, i);
                                    int dy = (int)Cv.GetReal2D(vely, j, i);

                                    if (dx > 0 || dy > 0)
                                    {
                                        PointF point1 = new PointF(i * ShiftSize, j * ShiftSize);
                                        PointF point2 = new PointF(i * ShiftSize + dx, j * ShiftSize + dy);

                                        motionVectors.Add(new MotionVector(point1, point2));
                                    }
                                    
                                }
                            }

                        }

                    }


                }


            }
            catch (Exception ex)
            {
            }

            return motionVectors;

        }









    }
}
