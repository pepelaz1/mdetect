using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using MotionDetector.Properties;


// Modes

// 0 - Detection preview
// 1 - Displaying log
// 2 - Simple playback
// 3 - Processing

namespace MotionDetector
{
    public enum VideoPlayerModes : int { DetectionPreview = 0, DisplayLog, SimplePlayback, Process};

    public class VideoPlayer 
    {
        [DllImport("streams.dll")]
        static extern void _OpenP(String file, IntPtr owner, int mode);
        [DllImport("streams.dll")] 
        static extern void _ResetP();
        [DllImport("streams.dll")]
        static extern void _RunP();
        [DllImport("streams.dll")]
        static extern void _PauseP();
        [DllImport("streams.dll")]
        static extern void _StopP();
        [DllImport("streams.dll")]
        static extern void _UpdateVideoSizeP();
        [DllImport("streams.dll")]
        static extern Int64 _GetDurationP();
        [DllImport("streams.dll")]
        static extern Int64 _GetPositionP();
        [DllImport("streams.dll")]
        static extern void _SetPositionP(Int64 position);
        [DllImport("streams.dll")]
        static extern void _StepForwardP(int nframes);
        [DllImport("streams.dll")]
        static extern void _StepBackwardP(int nframes);
        [DllImport("streams.dll")]
        static extern byte _GetThresholdP();
        [DllImport("streams.dll")]
        static extern void _SetThresholdP(byte threshold);
        [DllImport("streams.dll")]
        static extern byte _GetBrightnessP();
        [DllImport("streams.dll")]
        static extern void _SetBrightnessP(byte brightness);
        [DllImport("streams.dll")]
        static extern byte _GetContrastP();
        [DllImport("streams.dll")]
        static extern void _SetContrastP(byte contrast);
        [DllImport("streams.dll")]
        static extern IntPtr _GetLogFileNameP();
        [DllImport("streams.dll")]
        static extern void _SetLogFileNameP(String logfile);
        [DllImport("streams.dll")]
        static extern int _GetModeP();
        [DllImport("streams.dll")]
        static extern void _SetModeP(int mode);
        [DllImport("streams.dll")]
        static extern int _GetFramesCountP();
        [DllImport("streams.dll")]
        static extern IntPtr _GetMotionIndexDataP();
        [DllImport("streams.dll")]
        static extern Rectangle _GetVideoRectP();
        [DllImport("streams.dll")]
        static extern void _AddExcRectP(float left, float top, float right, float bottom);
        [DllImport("streams.dll")]
        static extern void _ClearExcRectsP();
        [DllImport("streams.dll")]
        static extern void _SetScreenshootsPathP(String path);
        [DllImport("streams.dll")]
        static extern int _TakeSShotP();
        [DllImport("streams.dll")]
        static extern void _SetCriteriaP(int criteria);
        [DllImport("streams.dll")]
        static extern int _GetCriteriaP();


         public static Int64 Units = 10000000;

        private List<RectangleF> _exclusionRectangles = new List<RectangleF>();
        private RectangleF _selectionRectangle = RectangleF.Empty;
        private String _logfile;
        private List<MotionIndex> _midxlst = new List<MotionIndex>();
        private VideoPlayerModes _mode;

        public Color DetectedMotionColor;
        public Color HighlightMotionColor;
        public Color SelectedVectorMotionColor;


        public byte Threshold
        {
            set { _SetThresholdP(value); }
            get { return _GetThresholdP(); }
        }

        public byte Brightness
        {
            set { _SetBrightnessP(value); }
            get { return _GetBrightnessP(); }
        }

        public byte Contrast
        {
            set { _SetContrastP(value); }
            get { return _GetContrastP(); }
        }

        public int Criteria
        {
            set { _SetCriteriaP(value); }
            get { return _GetCriteriaP(); }
        }

        public String ScreenshootsPath
        {
            get { return Settings.Default.ScreenshootsPath; }
        }

        public String LogFile
        {
            set {
                _logfile = value;
                _SetLogFileNameP(value);
                PopulateIndexList();
            }
            get {
                _logfile = Marshal.PtrToStringAnsi(_GetLogFileNameP());
                return _logfile;
            }         
        }

        public Rectangle VideoRect
        {
            get { 
                return _GetVideoRectP(); 
            }
        }

        public VideoPlayerModes Mode
        {
            set {
                _mode = value;
                _SetModeP((int)_mode); 
            }
            get { return (VideoPlayerModes)_GetModeP(); }
        }

        public VideoPlayer(Panel panelPreview)
        {
            _mode = 0;
        }

        public void OpenFile(String filename, IntPtr owner, int mode)
        {
            _OpenP(filename, owner, mode);
        }

        public void Play()
        {
            _SetLogFileNameP(_logfile);
            UpdateScreenshootsPath();
            _RunP();
        }

        public void Pause()
        {
            _PauseP();
        }

        public void Stop()
        {
            _StopP();
        }

        public void StepForward(int nrFrames)
        {
            _StepForwardP(nrFrames);
        }

        public void StepBackward(int nrFrames)
        {
            _StepBackwardP(nrFrames);
        }

        public void UpdateVideoSize()
        {
            _UpdateVideoSizeP();
        }

        public Int64 Position
        {
            get
            {
                Int64 position = 0;
                try
                {
                    position = _GetPositionP();
                }
                catch { }
                return position;
            }

            set
            {
                try
                {
                    _SetPositionP(value);
                }
                catch { }
            }
        }

        public Int64 Duration
        {
            get
            {
                Int64 duration = 0;
                try
                {
                    duration = _GetDurationP();
                }
                catch { }
                return duration;
            }
        }

        public int FramesCount
        {
            get
            {
                int cnt = 0;
                try
                {
                    cnt = _GetFramesCountP();
                }
                catch { }
                return cnt;
            }
        }

        public List<MotionIndex> MotionIndexList
        {
            get
            {
                return _midxlst;
            }
        }       
                
        public List<RectangleF> GetExclusionRectangles()
        {
            return _exclusionRectangles;
        }
              
        public void SetSelectionRectangle(RectangleF rectangle)
        {
            _selectionRectangle = rectangle;
        }

        public void Reset()
        {
            _ResetP();
        }

        private void PopulateIndexList()
        {
            if (_mode == VideoPlayerModes.DisplayLog)
            {
                _midxlst.Clear();

                IntPtr src = _GetMotionIndexDataP();
                if (src.ToInt32() != 0)
                {
                    byte[] dst = new byte[FramesCount * 16];
                    Marshal.Copy(src, dst, 0, FramesCount * 16);

                    int i = 0;
                    int cnt = FramesCount;
                    for (; ; )
                    {
                        MotionIndex mi = new MotionIndex();
                        mi.Time = BitConverter.ToInt64(dst, i);
                        i += 8;
                        mi.Number = BitConverter.ToInt32(dst, i);
                        i += 4;
                        mi.Position = BitConverter.ToInt32(dst, i);
                        i += 4;
                        if (mi.Position >= 0)
                            _midxlst.Add(mi);
                        if (i >= cnt * 16)
                            break;
                    }
                }

            }
        }

        public void AddExcRect(RectangleF r)
        {
            _AddExcRectP(r.Left, r.Top, r.Right, r.Bottom);
        }

        public void ClearExcRects()
        {
            _ClearExcRectsP();
        }

        public void TakeSShot()
        {
            _TakeSShotP();
        }

        public void UpdateScreenshootsPath()
        {
            _SetScreenshootsPathP(ScreenshootsPath);
        }
     }
}
