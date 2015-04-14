using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace MotionDetector
{
    public class Utils
    {
        public static void WriteLog(string str)
        {
            string LogFilename = "log.txt";
            try
            {
                File.AppendAllText(Application.StartupPath + "\\" + LogFilename, DateTime.Now.ToString() + "  -->  " + str + Environment.NewLine + Environment.NewLine);
            }
            catch { }
        }


        public static string FormatTime(double timeSeconds, bool showDecimal, bool showHours)
        {
            if (timeSeconds < 0) timeSeconds = 0;

            int seconds = (int)(timeSeconds % 60);
            int minutes = (int)(timeSeconds / 60) % 60;
            int hours = (int)(timeSeconds / 60) / 60;

            string strTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            if (hours > 0 || showHours) strTime = string.Format("{0:00}:", hours) + strTime;

            if (showDecimal) strTime = strTime + string.Format(".{0:0}", (timeSeconds - (int)timeSeconds) * 10);


            return strTime;
        }





    }
}
