using System;
using System.Collections.Generic;
using System.Text;
using DirectShowLib;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using FFDShowAPI;



namespace MotionDetector
{
    public class VideoPlayer : ISampleGrabberCB
    {

        private Panel _panelPreview;
        private Size _videoSize;

        private IGraphBuilder _graphBuilder;
        private IVideoWindow _videoWindow;
        private IMediaControl _mediaControl;
        private IMediaPosition _mediaPosition;

        private double _timePerFrame;
        private double _duration;

        private List<MotionFrame> _motionFrames = new List<MotionFrame>();



        private List<RectangleF> _exclusionRectangles = new List<RectangleF>();
        private Rectangle _videoRectangle;
        private RectangleF _selectionRectangle = RectangleF.Empty;

        private MotionVector _selectedMotion = null;

        

        //public float Brightness = 0;
        //public float Contrast = 100;
        public bool ShowMotion = false;

        public Color DetectedMotionColor;
        public Color HighlightMotionColor;
        public Color SelectedVectorMotionColor;


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
        }


        public VideoPlayer(Panel panelPreview)
        {
            _panelPreview = panelPreview;
            _panelPreview.SizeChanged += new EventHandler(_panelPreview_SizeChanged);
            _videoRectangle = new Rectangle(0, 0, _panelPreview.Width, _panelPreview.Height);
        }

        void _panelPreview_SizeChanged(object sender, EventArgs e)
        {

            try
            {
                double windowAR = (double)_panelPreview.Width / (double)_panelPreview.Height;
                double videoAR = (double)_videoSize.Width / (double)_videoSize.Height;

                int top = 0;
                int left = 0;
                int width = _panelPreview.Width;
                int height = _panelPreview.Height;

                if (videoAR > windowAR)
                {
                    height = (int)(width / videoAR);
                    top = (_panelPreview.Height - height) / 2;
                }
                else
                {
                    width = (int)(height * videoAR);
                    left = (_panelPreview.Width - width) / 2;
                }
                _videoRectangle = new Rectangle(left, top, width, height);
                _videoWindow.SetWindowPosition(left, top, width, height);

            }
            catch { }
        }





        public static IBaseFilter GetColorspaceConverter()
        {
            try
            {
                Guid cscGUID = new Guid("{1643E180-90F5-11CE-97D5-00AA0055595A}");
                Type cscType = Type.GetTypeFromCLSID(cscGUID);
                IBaseFilter cscFilter = (IBaseFilter)Activator.CreateInstance(cscType);
                return cscFilter;
            }
            catch
            {
                return null;
            }
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

            _ffdshowApi = null;
            IBaseFilter ffdshowFilter = GetFFDShow();
            if (ffdshowFilter != null)
            {
                hr = _graphBuilder.AddFilter(ffdshowFilter, "FFDShow");
                DsError.ThrowExceptionForHR(hr);
                _ffdshowApi = new FFDShowAPI.FFDShowAPI(ffdshowFilter);
            }

            IBaseFilter vmr9 = (IBaseFilter)new VideoMixingRenderer9();
            hr = _graphBuilder.AddFilter(vmr9, "VMR9");
            DsError.ThrowExceptionForHR(hr);

            IBaseFilter colorspaceConverter = GetColorspaceConverter();
            if (colorspaceConverter != null)
            {
                hr = _graphBuilder.AddFilter(colorspaceConverter, "ColorSpace");
                DsError.ThrowExceptionForHR(hr);
            }



            hr = captureGraphBuilder.RenderStream(null, null, sourceFilter, null, ffdshowFilter);
            DsError.ThrowExceptionForHR(hr);

            hr = captureGraphBuilder.RenderStream(null, null, ffdshowFilter, colorspaceConverter, sampleGrabberFilter);
            DsError.ThrowExceptionForHR(hr);

            hr = captureGraphBuilder.RenderStream(null, null, sampleGrabberFilter, null, vmr9);
            DsError.ThrowExceptionForHR(hr);

            int videoWidth = 0;
            int videoHeight = 0;
            IBasicVideo basicVideo = (IBasicVideo)_graphBuilder;
            hr = basicVideo.GetVideoSize(out videoWidth, out videoHeight);
            DsError.ThrowExceptionForHR(hr);
            _videoSize = new Size(videoWidth, videoHeight);
            
            _videoWindow = (IVideoWindow)_graphBuilder;
            _videoWindow.put_Owner(_panelPreview.Handle);
            _videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren);
            _videoWindow.put_Visible(OABool.True);
            _videoWindow.put_MessageDrain(_panelPreview.Handle);

            _panelPreview_SizeChanged(null, null);

            _mediaControl = (IMediaControl)_graphBuilder;

            _mediaPosition = (IMediaPosition)_graphBuilder;

            //IVideoFrameStep frameStep = (IVideoFrameStep)_graphBuilder;
            //frameStep.CanStep(
            _timePerFrame = 0;
            hr = basicVideo.get_AvgTimePerFrame(out _timePerFrame);
            DsError.ThrowExceptionForHR(hr);

            hr = _mediaPosition.get_Duration(out _duration);
            DsError.ThrowExceptionForHR(hr);

            _currentFrameIndex = 0;

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


        public void Play()
        {
            _mediaControl.Run();
        }

        public void Pause()
        {
            _mediaControl.Pause();
        }

        public void Stop()
        {
            _mediaControl.Stop();
        }

        public void StepForward(int nrFrames)
        {
            _mediaControl.Pause();

            double position;
            _mediaPosition.get_CurrentPosition(out position);
            position += _timePerFrame * nrFrames;
            if (position > _duration) position = _duration;
            _mediaPosition.put_CurrentPosition(position);
        }

        public void StepBackward(int nrFrames)
        {
            _mediaControl.Pause();

            double position;
            _mediaPosition.get_CurrentPosition(out position);
            position -= _timePerFrame * nrFrames;
            if (position < 0) position = 0;
            _mediaPosition.put_CurrentPosition(position);
        }



        public double Position
        {
            get
            {
                double position = 0;
                try
                {
                    _mediaPosition.get_CurrentPosition(out position);
                }
                catch { }
                return position;
            }

            set
            {
                try
                {
                    _mediaPosition.put_CurrentPosition(value);
                }
                catch { }
            }
        }

        public double Duration
        {
            get
            {
                double duration = 0;
                try
                {
                    _mediaPosition.get_Duration(out duration);
                }
                catch { }
                return duration;
            }
        }



        public void SetMotion(List<MotionFrame> motionFrames)
        {
            _motionFrames = motionFrames;
        }
        




        
        public List<RectangleF> GetExclusionRectangles()
        {
            return _exclusionRectangles;
        }

        public Rectangle GetVideoRectangle()
        {
            return _videoRectangle;
        }

      
        public void SetSelectionRectangle(RectangleF rectangle)
        {
            _selectionRectangle = rectangle;
        }

        public void SetSelectedMotion(MotionVector motionVector)
        {
            _selectedMotion = motionVector;

        }

        public void ReleaseInterfaces()
        {
            if (_mediaControl != null)
            {
                _mediaControl.Stop();
            }


            if (_graphBuilder != null)
            {
                Marshal.ReleaseComObject(_graphBuilder);
                _graphBuilder = null;
                _mediaControl = null;
            }

            _motionFrames = new List<MotionFrame>();
            //_motionTimes = new List<MotionTime>();
            _exclusionRectangles = new List<RectangleF>();
            _selectionRectangle = RectangleF.Empty;

            _selectedMotion = null;
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



        private int _currentFrameIndex = 0;

        private MotionFrame GetCurrentFrame(double currentTime)
        {
            foreach (MotionFrame frame in _motionFrames)
            {
                if (frame.Time1 <= currentTime && frame.Time2 > currentTime)
                {
                    return frame;
                }
            }
            return null;
        }

        private int GetCurrentFrameIndex(double currentTime)
        {
            for (int i = 0; i < _motionFrames.Count; i++ )
            {
                MotionFrame frame = _motionFrames[i];
                if (frame.Time1 <= currentTime && frame.Time2 > currentTime)
                {
                    return i;
                }
            }
            return -1;
        }


        private bool IsCurrentFrame(int index, double currentTime)
        {
            try
            {
                MotionFrame frame = _motionFrames[index];
                return frame.Time1 <= currentTime && frame.Time2 > currentTime;
            }
            catch { }
            return false;
        }


        private MotionFrame GetFrameFromIndex(int index)
        {
            try
            {
                return _motionFrames[index];
            }
            catch { }
            return null;
        }


        public int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {


            Bitmap bitmap = new Bitmap(_videoSize.Width, _videoSize.Height, _videoSize.Width * 3, PixelFormat.Format24bppRgb, pBuffer);
            Graphics g = Graphics.FromImage(bitmap);

            /*
            Bitmap contrastProcessed = VideoEffects.ApplyContrast(bitmap, this.Contrast);
            Bitmap brightnessProcessed = VideoEffects.ApplyBrightness(contrastProcessed, this.Brightness);
            contrastProcessed.Dispose();
            g.DrawImage(brightnessProcessed, 0, 0);
            brightnessProcessed.Dispose();
            */

            //if (this.Contrast != 100) VideoEffects.ApplyContrastInPlaceUnsafe(bitmap, this.Contrast);
            //if (this.Brightness != 0) VideoEffects.ApplyBrightnessInPlaceUnsafe(bitmap, this.Brightness);

            //if(this.Contrast != 100) VideoEffects.ApplyContrastInPlace(bitmap, this.Contrast);
            //if (this.Brightness != 0) VideoEffects.ApplyBrightnessInPlace(bitmap, this.Brightness);

            /*
            Bitmap contrastProcessed = bitmap;
            if (this.Contrast != 100)
            {
                contrastProcessed = VideoEffects.ApplyContrast(bitmap, this.Contrast);
            }

            Bitmap brightnessProcessed = contrastProcessed;
            if (this.Brightness != 0)
            {
                brightnessProcessed = VideoEffects.ApplyBrightness(contrastProcessed, this.Brightness);
            }

            if (brightnessProcessed != bitmap)
            {
                g.DrawImage(brightnessProcessed, 0, 0);
            }

            if (contrastProcessed != bitmap) contrastProcessed.Dispose();
            if (brightnessProcessed != bitmap && brightnessProcessed != contrastProcessed) brightnessProcessed.Dispose();
            */


            bool drawRect = _exclusionRectangles.Count > 0 || _selectionRectangle != RectangleF.Empty;

            if (drawRect)
            {
                Bitmap exclusionRegions = new Bitmap(_videoSize.Width, _videoSize.Height, PixelFormat.Format32bppArgb);
                Graphics gExclusion = Graphics.FromImage(exclusionRegions);
                gExclusion.CompositingMode = CompositingMode.SourceCopy;
                gExclusion.SmoothingMode = SmoothingMode.None;
                gExclusion.InterpolationMode = InterpolationMode.NearestNeighbor;
                gExclusion.Clear(Color.Transparent);

                Color color = Color.FromArgb(100, 100, 100);
                SolidBrush brush = new SolidBrush(Color.FromArgb(150, color));

                foreach (RectangleF exclusionRect in _exclusionRectangles)
                {
                    Rectangle rect = new Rectangle();
                    rect.X = (int)(exclusionRect.X * _videoSize.Width);
                    rect.Y = (int)(exclusionRect.Y * _videoSize.Height);
                    rect.Width = (int)(exclusionRect.Width * _videoSize.Width);
                    rect.Height = (int)(exclusionRect.Height * _videoSize.Height);
                    rect.Y = _videoSize.Height - rect.Height - rect.Y;

                    gExclusion.FillRectangle(brush, rect);
                }

                if (_selectionRectangle != RectangleF.Empty)
                {

                    Rectangle rect = new Rectangle();
                    rect.X = (int)(_selectionRectangle.X * _videoSize.Width);
                    rect.Y = (int)(_selectionRectangle.Y * _videoSize.Height);
                    rect.Width = (int)(_selectionRectangle.Width * _videoSize.Width);
                    rect.Height = (int)(_selectionRectangle.Height * _videoSize.Height);
                    rect.Y = _videoSize.Height - rect.Height - rect.Y;

                    SolidBrush selectionBrush = new SolidBrush(Color.FromArgb(200, color));
                    gExclusion.FillRectangle(selectionBrush, rect);
                    //gExclusion.DrawRectangle(Pens.Black, rect);
                    selectionBrush.Dispose();
                }

                gExclusion.Dispose();
                brush.Dispose();

                g.DrawImage(exclusionRegions, 0, 0);
                exclusionRegions.Dispose();
            }

            if (this.ShowMotion)
            {


                //MotionFrame frame = GetCurrentFrame(SampleTime);

                //GetFrameFromIndex

                if (!IsCurrentFrame(_currentFrameIndex, SampleTime))
                {
                    _currentFrameIndex++;
                    if (!IsCurrentFrame(_currentFrameIndex, SampleTime))
                    {
                        _currentFrameIndex = GetCurrentFrameIndex(SampleTime);
                    }

                }

                MotionFrame frame = GetFrameFromIndex(_currentFrameIndex);

                if (frame != null)
                {

                    MotionFrame currentFrame = frame;

                    Bitmap motion = new Bitmap(_videoSize.Width, _videoSize.Height);
                    Graphics gMotion = Graphics.FromImage(motion);
                    gMotion.Clear(Color.Transparent);
                    gMotion.SmoothingMode = SmoothingMode.AntiAlias;
                    Pen pen = new Pen(DetectedMotionColor, 1);
                    Pen penHighlight = new Pen(HighlightMotionColor, 1);
                    Pen penSelected = new Pen(SelectedVectorMotionColor, 1);


                    foreach (MotionVector vector in frame.Vectors)
                    {
                        bool isValid = true;

                        if (isValid)
                        {
                            Pen linePen = pen;


                            if (vector.Highlight)
                            {
                                linePen = penHighlight;
                            }
                            if (vector.Equals(_selectedMotion))
                            {
                                linePen = penSelected;
                            }
                            gMotion.DrawLine(linePen, vector.Point1, vector.Point2);

                        }
                    }
                    g.DrawImage(motion, 0, 0);
                    motion.Dispose();
                    gMotion.Dispose();

                }

            }

            g.Dispose();


            return 0;
        }


        public int SampleCB(double SampleTime, IMediaSample pSample)
        {
            return 0;
        }







    }
}
