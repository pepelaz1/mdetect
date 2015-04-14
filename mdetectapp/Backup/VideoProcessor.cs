using System;
using System.Collections.Generic;
using System.Text;
using DirectShowLib;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using OpenCvSharp;
using System.IO;





namespace MotionDetector
{
    public class VideoProcessor : ISampleGrabberCB
    {

        private Size _videoSize;
        private double _currentTime = 0;
        private double _duration = 0;
        private bool _stopProcessing = false;
        private Bitmap _previousFrame = null;
        private double _previousFrameTime = 0;
        private long _frameCount = 0;


        private List<RectangleF> _exclusionRectangles = new List<RectangleF>();

        private List<MotionFrame> _motionFramesList;


        private IGraphBuilder _graphBuilder;
        private IMediaControl _mediaControl;
        private IMediaPosition _mediaPosition;


        public event EventHandler ProcessCompleted;


        //public float Brightness = 0;
        //public float Contrast = 100;
        public int FrameSkip = 1;



        private FFDShowAPI.FFDShowAPI _ffdshowApi = null;

        private float _contrast = 128;
        private float _brightness = 0;

        public float Brightness
        {
            set
            {
                _brightness = value;
                try
                {
                    _ffdshowApi.PictureBrightness = (int)value;
                }
                catch { }
            }
            get
            {
                return _brightness;
            }
        }
        public float Contrast
        {
            set
            {
                _contrast = value;
                try
                {
                    _ffdshowApi.PictureContrast = (int)value;
                }
                catch { }
            }
            get
            {
                return _contrast;
            }
        }





        public VideoProcessor()
        {
        }



        private Size GetVideoSize(string filename)
        {
            int hr = 0;
            int videoWidth = 0;
            int videoHeight = 0;
            IGraphBuilder graphBuilder = null;
            try
            {
                graphBuilder = (IGraphBuilder)new FilterGraph();
                hr = graphBuilder.RenderFile(filename, filename);
                DsError.ThrowExceptionForHR(hr);

                IBasicVideo basicVideo = (IBasicVideo)graphBuilder;
                hr = basicVideo.GetVideoSize(out videoWidth, out videoHeight);
                DsError.ThrowExceptionForHR(hr);
            }
            finally
            {
                if (graphBuilder != null)
                {
                    Marshal.ReleaseComObject(graphBuilder);
                }
            }

            return new Size(videoWidth, videoHeight);
        }



        public static IBaseFilter GetFFDShow()
        {
            try
            {
                Type cscType = Type.GetTypeFromCLSID(FFDShowAPI.FFDShowAPI.FFDShowVideoGuid);
                IBaseFilter cscFilter = (IBaseFilter)Activator.CreateInstance(cscType);
                return cscFilter;
            }
            catch
            {
                return null;
            }
        }


        public void OpenFile(string filename)
        {
            int hr = 0;

            ReleaseInterfaces();

            _videoSize = GetVideoSize(filename);


            _graphBuilder = (IGraphBuilder)new FilterGraph();

            ICaptureGraphBuilder2 captureGraphBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
            captureGraphBuilder.SetFiltergraph(_graphBuilder);

            ISampleGrabber sampleGrabber = (ISampleGrabber)new SampleGrabber();
            ConfigSampleGrabber(sampleGrabber);

            IBaseFilter sampleGrabberFilter = (IBaseFilter)sampleGrabber;
            hr = _graphBuilder.AddFilter(sampleGrabberFilter, "Sample Grabber");
            DsError.ThrowExceptionForHR(hr);

            IBaseFilter sourceFilter;
            hr = _graphBuilder.AddSourceFilter(filename, filename, out sourceFilter);
            DsError.ThrowExceptionForHR(hr);

            IBaseFilter nullRenderer = (IBaseFilter) new NullRenderer();
            hr = _graphBuilder.AddFilter(nullRenderer, "Null Renderer");
            DsError.ThrowExceptionForHR(hr);

            _ffdshowApi = null;
            IBaseFilter ffdshowFilter = GetFFDShow();
            if (ffdshowFilter != null)
            {
                hr = _graphBuilder.AddFilter(ffdshowFilter, "FFDShow");
                DsError.ThrowExceptionForHR(hr);
                _ffdshowApi = new FFDShowAPI.FFDShowAPI(ffdshowFilter);
            }



            hr = captureGraphBuilder.RenderStream(null, null, sourceFilter, ffdshowFilter, sampleGrabberFilter);
            DsError.ThrowExceptionForHR(hr);

            hr = captureGraphBuilder.RenderStream(null, null, sampleGrabberFilter, null, nullRenderer);
            DsError.ThrowExceptionForHR(hr);



            IMediaFilter mediaFilter = (IMediaFilter)_graphBuilder;
            hr = mediaFilter.SetSyncSource(null);
            DsError.ThrowExceptionForHR(hr);

            _mediaControl = (IMediaControl)_graphBuilder;

            _mediaPosition = (IMediaPosition)_graphBuilder;

            hr = _mediaPosition.get_Duration(out _duration);
            DsError.ThrowExceptionForHR(hr);


            try
            {
                _ffdshowApi.PictureBrightness = (int)_brightness;
            }
            catch { }

            try
            {
                _ffdshowApi.PictureContrast = (int)_contrast;
            }
            catch { }

        }




        //public List<MotionFrame> MotionFrames;


        private void GenerateRandomData()
        {
            //MotionFrames = new List<MotionFrame>();

            int nrMotionFrames = 100000;
            double frameInterval = _duration / nrMotionFrames;

            Random random = new Random();

            for (int i = 0; i < nrMotionFrames; i++)
            {

                List<MotionVector> motionVectors = new List<MotionVector>();

                
            for (int v = 0; v < 50; v++)
            {

                PointF point1 = new PointF(random.Next(0, _videoSize.Width), random.Next(0, _videoSize.Height));
                PointF point2 = new PointF(random.Next(0, _videoSize.Width), random.Next(0, _videoSize.Height));



                    MotionVector vector = new MotionVector(point1, point2);



                    motionVectors.Add(vector);

                
            }
                
                // motion frame - starts when new points are calculated, until the next FrameSkip frames
                double prevTime = frameInterval * i;
                double currTime = prevTime + frameInterval;

                _currentTime = prevTime;

                MotionFrame frame = new MotionFrame(prevTime, currTime, motionVectors);

                // todo: save frames to disk instead of keeping in memory
                _motionFramesList.Add(frame);
                


                Application.DoEvents();
            }


            if (!_stopProcessing && ProcessCompleted != null)
            {
                ProcessCompleted(this, EventArgs.Empty);
            }
        }



        public void Start()
        {
            int hr = 0;

            _previousFrame = null;
            _previousFrameTime = 0;

            _motionFramesList = new List<MotionFrame>();
            _frameCount = 0;

            _stopProcessing = false;


            //GenerateRandomData();

            //return;




            
            hr = _mediaControl.Run();
            DsError.ThrowExceptionForHR(hr);

            

            IMediaEvent mediaEvent = (IMediaEvent)_graphBuilder;

            EventCode eventCode;
            do
            {
                Application.DoEvents();
                hr = mediaEvent.WaitForCompletion(100, out eventCode);
            }
            while (eventCode != EventCode.Complete && !_stopProcessing);


            if (!_stopProcessing && ProcessCompleted != null)
            {
                ProcessCompleted(this, EventArgs.Empty);
            }

        }


        public void Stop()
        {
            try
            {
                _stopProcessing = true;
                _mediaControl.Stop();
            }
            catch { }
        }



        public double GetCurrentTime()
        {
            return _currentTime;
        }


        public double GetDuration()
        {
            return _duration;
        }

        
        public List<MotionFrame> GetMotion()
        {
            return _motionFramesList;
        }

        public void SetMotion(List<MotionFrame> motionFrames)
        {
            _motionFramesList = motionFrames;
        }





        public List<RectangleF> GetExclusionRectangles()
        {
            return _exclusionRectangles;
        }


        public bool FilterNoise = false;
        public double FrameReductionFactor = 1;

        public void ReleaseInterfaces()
        {
            Stop();

            if (_graphBuilder != null)
            {
                Marshal.ReleaseComObject(_graphBuilder);
                _graphBuilder = null;
                _mediaControl = null;
            }

            _previousFrame = null;
            _exclusionRectangles = new List<RectangleF>();

            _motionFramesList = new List<MotionFrame>();
        }


        

        private void ConfigSampleGrabber(ISampleGrabber sampleGrabber)
        {
            sampleGrabber.SetCallback(this, 1);

            AMMediaType sgMediaType = new AMMediaType();
            sgMediaType.majorType = MediaType.Video;
            sgMediaType.subType = MediaSubType.RGB24;
            sgMediaType.formatType = FormatType.VideoInfo;
            sampleGrabber.SetMediaType(sgMediaType);
        }

        


        public int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            _currentTime = SampleTime;


            if (true) //_frameCount++ % 1 == 0)
            {
                Bitmap bitmap = new Bitmap(_videoSize.Width, _videoSize.Height, _videoSize.Width * 3, PixelFormat.Format24bppRgb, pBuffer);

                Bitmap resizedBitmap = bitmap;
                if (FrameReductionFactor > 1)
                {
                    resizedBitmap = new Bitmap((int)Math.Ceiling(_videoSize.Width / FrameReductionFactor), (int)Math.Ceiling(_videoSize.Height / FrameReductionFactor));
                    Graphics gResize = Graphics.FromImage(resizedBitmap);
                    gResize.SmoothingMode = SmoothingMode.None;
                    gResize.InterpolationMode = InterpolationMode.NearestNeighbor;
                    gResize.DrawImage(bitmap, 0, 0, resizedBitmap.Width, resizedBitmap.Height);
                    gResize.Dispose();
                }
                else
                {
                    resizedBitmap = new Bitmap(bitmap);
                }

                /*
                Bitmap contrastProcessed = resizedBitmap;
                if (this.Contrast != 100)
                {
                    contrastProcessed = VideoEffects.ApplyContrast(resizedBitmap, this.Contrast);
                    if (resizedBitmap != bitmap) resizedBitmap.Dispose();
                    resizedBitmap = null;
                }
                

                Bitmap brightnessProcessed = contrastProcessed;
                if (this.Brightness != 0)
                {
                    brightnessProcessed = VideoEffects.ApplyBrightness(contrastProcessed, this.Brightness);
                    if (contrastProcessed != bitmap) contrastProcessed.Dispose();
                    contrastProcessed = null;
                }
                try
                {
                    if (resizedBitmap != null) resizedBitmap.Dispose();
                    if (contrastProcessed != null) contrastProcessed.Dispose();
                }
                catch { }
                */

                /*
                Bitmap contrastProcessed = VideoEffects.ApplyContrast(resizedBitmap, this.Contrast);
                Bitmap brightnessProcessed = VideoEffects.ApplyBrightness(contrastProcessed, this.Brightness);
                
                if (resizedBitmap != bitmap) resizedBitmap.Dispose();
                contrastProcessed.Dispose();
                */

                //Graphics g = Graphics.FromImage(brightnessProcessed);
                Graphics g = Graphics.FromImage(resizedBitmap);
                foreach (RectangleF exclusionRect in _exclusionRectangles)
                {
                    Rectangle rect = new Rectangle();
                    rect.X = (int)(exclusionRect.X * _videoSize.Width);
                    rect.Y = (int)(exclusionRect.Y * _videoSize.Height);
                    rect.Width = (int)(exclusionRect.Width * _videoSize.Width);
                    rect.Height = (int)(exclusionRect.Height * _videoSize.Height);
                    rect.Y = _videoSize.Height - rect.Height - rect.Y;

                    g.FillRectangle(Brushes.Black, rect);
                }
                g.Dispose();

                Bitmap processedFrame = resizedBitmap; // brightnessProcessed;


                if (_previousFrame != null)
                {
                    // calculate new points each "FrameSkip" frames
                    bool newPoints = _frameCount++ % FrameSkip == 0;
                    if (newPoints)
                    {
                        if (_previousFrameTime > 0)
                        {

                            List<MotionVector> motionVectors = new List<MotionVector>();


                            for (int i = 0; i < MotionDetector.UpdatedPoints.Length; i++)
                            {
                                if (MotionDetector.UpdatedPoints[i] != null && MotionDetector.UpdatedPoints[i].X >= 0)
                                {
                                    PointF point1 = new PointF(MotionDetector.InitialPoints[i].X, MotionDetector.InitialPoints[i].Y);
                                    PointF point2 = new PointF(MotionDetector.UpdatedPoints[i].X, MotionDetector.UpdatedPoints[i].Y);


                                    point1.X = (point1.X * _videoSize.Width) / processedFrame.Width;
                                    point2.X = (point2.X * _videoSize.Width) / processedFrame.Width;
                                    point1.Y = (point1.Y * _videoSize.Height) / processedFrame.Height;
                                    point2.Y = (point2.Y * _videoSize.Height) / processedFrame.Height;
                                    
                                    
                                    MotionVector vector = new MotionVector(point1, point2);


                                    bool valid = MotionDetector.IsValid(vector, MotionDetector.PointVectors[i], processedFrame.Width);

                                    if ((valid || !this.FilterNoise) && vector.Norm > 3)
                                    {
                                        motionVectors.Add(vector);
                                    }

                                }
                            }

                            // motion frame - starts when new points are calculated, until the next FrameSkip frames
                            double prevTime = _previousFrameTime;
                            double currTime = SampleTime;

                            if (motionVectors.Count > 0)
                            {
                                MotionFrame frame = new MotionFrame(prevTime, currTime, motionVectors);

                                _motionFramesList.Add(frame);
                            }

                        }

                        // start time - when newPoints=true
                        _previousFrameTime = SampleTime;
                    }

                    MotionDetector.DetectMotionTrackPoints(_previousFrame, processedFrame, newPoints);

                    _previousFrame.Dispose();
                }
                _previousFrame = processedFrame;
            }


            return 0;
        }


        public int SampleCB(double SampleTime, IMediaSample pSample)
        {
            return 0;
        }




    }
}
