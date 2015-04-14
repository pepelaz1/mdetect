using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MotionDetector
{
    public class MotionLog
    {
        private List<MotionIndex> _lst;
        public List<MotionIndex> Lst
        {
            get { return _lst; }
        }

        public void Load(List<MotionIndex> list)
        {
            _lst = new List<MotionIndex>(list);
        }

        public String FormatTime(int i)
        {
            Int64 time = Int64.Parse(Lst[i].Time.ToString());

            int msecs = (int)(time / 10000);
            int hours = msecs / 3600000;
            msecs -= hours * 3600000;
            int minutes = msecs / 60000;
            msecs -= minutes * 60000;
            int seconds = msecs / 1000;
            msecs -= seconds * 1000;

            return String.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, msecs / 10);
        }

        public void Filter(decimal distance)
        {
            List<MotionIndex> newlist = new List<MotionIndex>();
            int units = (int)(distance * 100) * 10000;
            newlist.Add(_lst[0]);
            for (int i = 1; i < _lst.Count - 1; i++)
            {
                if (_lst[i].Time - _lst[i - 1].Time <= units && _lst[i + 1].Time - _lst[i].Time <= units)
                    newlist.Add(_lst[i]);
            }
            newlist.Add(_lst[_lst.Count - 1]);
            _lst = newlist;
        }

        public void Save(String filename, String oldlog)
        {
            // Examine old log file to find out index file
            String oldidx = oldlog.Replace(Path.GetExtension(oldlog), ".midx");

            // Make new index filename
            String newidx = filename.Replace(Path.GetExtension(filename), ".midx");

            // Copy index file
            File.Copy(oldidx, newidx,true);

            int n = 0;
            using (StreamReader src = new StreamReader(oldlog))
            {
                using (StreamWriter dst = new StreamWriter(filename))
                {
                     String line;
                    while ((line = src.ReadLine()) != null)
                    {
                        if (line.Contains("Frame:"))
                        {
                            String []parts = line.Split(" ,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            int frm = Int32.Parse(parts[1]);
                            if (frm == _lst[n].Number)
                            {
                                dst.WriteLine(line);
                                while ((line = src.ReadLine()) != "")
                                    dst.WriteLine(line);
                                dst.Write(Environment.NewLine);
                                n++;
                            }

                        }
                    }
                    dst.Close();
                }
                src.Close();
            }   
        }
    }
}
