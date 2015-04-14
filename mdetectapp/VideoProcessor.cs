using System;
using System.Collections.Generic;
using System.Text;
//using DirectShowLib;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
//using OpenCvSharp;
using System.IO;
using MotionDetector.Properties;





namespace MotionDetector
{
    public class VideoProcessor //: ISampleGrabberCB
    {
        [DllImport("streams.dll")]
        static extern int _CreateD();
        [DllImport("streams.dll")]
        static extern void _ClearAllD();
        [DllImport("streams.dll")]
        static extern void _OpenD(int id, String file, String logpath);
        [DllImport("streams.dll")]
        static extern void _ResetD(int id);
        [DllImport("streams.dll")]
        static extern void _RunD(int id);
        [DllImport("streams.dll")]
        static extern void _PauseD();
        [DllImport("streams.dll")]
        static extern void _StopD(int id);
        [DllImport("streams.dll")]
        static extern int _GetStateD(int id);
        [DllImport("streams.dll")]
        static extern byte _GetThresholdD(int id);
        [DllImport("streams.dll")]
        static extern void _SetThresholdD(int id, byte threshold);
        [DllImport("streams.dll")]
        static extern Int64 _GetPositionD(int id);
        [DllImport("streams.dll")]
        static extern Int64 _GetDurationD(int id);
        [DllImport("streams.dll")]
        static extern byte _GetBrightnessD(int id);
        [DllImport("streams.dll")]
        static extern void _SetBrightnessD(int id, byte brightness);
        [DllImport("streams.dll")]
        static extern byte _GetContrastD(int id);
        [DllImport("streams.dll")]
        static extern void _SetContrastD(int id, byte contrast);
        [DllImport("streams.dll")]
        static extern IntPtr _GetLogFileNameD(int id);
        [DllImport("streams.dll")]
        static extern void _AddExcRectD(int id, float left, float top, float right, float bottom);
        [DllImport("streams.dll")]
        static extern void _ClearExcRectsD(int id);
        [DllImport("streams.dll")]
        static extern void _SetCriteriaD(int id, int criteria);
        [DllImport("streams.dll")]
        static extern int _GetCriteriaD(int id);
       

        private List<RectangleF> _exclusionRectangles = new List<RectangleF>();
        private int _id;

        public byte Threshold
        {
            set { _SetThresholdD(_id, value); }
            get { return _GetThresholdD(_id); }
        }

        public byte Brightness
        {
            set { _SetBrightnessD(_id, value); }
            get { return _GetBrightnessD(_id); }
        }

        public byte Contrast
        {
            set { _SetContrastD(_id, value); }
            get { return _GetContrastD(_id); }
        }

        public int Criteria
        {
            set { _SetCriteriaD(_id, value); }
            get { return _GetCriteriaD(_id); }
        }

        public String LogPath
        {
            get {
                return Settings.Default.LogsPath;
            }
        }

        public String LogFile
        {
            get {
                return Marshal.PtrToStringAnsi(_GetLogFileNameD(_id));
            }
        }

        public VideoProcessor()
        {
            _id = _CreateD(); 
        }
        
        public void OpenFile(String filename)
        {
            _OpenD(_id, filename, LogPath);
        }

        public void Start()
        {
            _RunD(_id);
        }
 

        public void Stop()
        {
            _StopD(_id);        
        }
        
        public Int64 Position
        {
            get
            {
                Int64 position = 0;
                try
                {
                    position = _GetPositionD(_id);
                }
                catch { }
                return position;
            }
        }

        public Int64 Duration
        {
            get
            {
                Int64 duration = 0;
                try
                {
                    duration = _GetDurationD(_id);
                }
                catch { }
                return duration;
            }
        }

        public int State
        {
            get
            {               
               return _GetStateD(_id);
            }
        }

        public void ReleaseInterfaces()
        {
            Stop();
        }
  
        public void Reset()
        {
            _ResetD(_id);
        }

        public void AddExcRect(RectangleF r)
        {
            _AddExcRectD(_id, r.Left, r.Top, r.Right, r.Bottom);
        }
        
        public void ClearExcRects()
        {
            _ClearExcRectsD(_id);
        }
    }
}
