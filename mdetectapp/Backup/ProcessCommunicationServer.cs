using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Lifetime;
using System.Drawing;
using System.Runtime.Serialization;

namespace MotionDetector
{
    public class ProcessCommunicationServer : System.MarshalByRefObject 
    {
        public const int ServerPort = 36917;
        public const string ServerName = "MotionDetector";

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public static BatchProcessForm BatchForm = null;
        //public static ProcessSettings Settings = new ProcessSettings();

        public ProcessSettings GetSettings()
        {
            return BatchForm.GetSettings();
        }

        public void SetProgress(int processId, double progress, double elapsedSeconds)
        {
            BatchForm.SetProgress(processId, progress, elapsedSeconds);
        }


        public void ProcessCompleted(int processId)
        {
            BatchForm.ProcessCompleted(processId);
        }

        public void ProcessError(int processId, string message)
        {
            BatchForm.ProcessError(processId, message);
        }


    }


    [Serializable]
    public class ProcessSettings
    {
        public List<RectangleF> ExclusionRectangles;
        public double FrameReductionFactor;
        public int FrameSkip;
        public bool FilterNoise;
        public double Sensitivity;
        public double SensitivityHigh;
        public float Contrast;
        public float Brightness;



    }

}
