using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Windows.Forms;



namespace MotionDetector
{





    public class MotionVectorItem : ListViewItem
    {

        public MotionVector Vector = null;



        public MotionVectorItem(MotionVector vector)
        //    : base()
        {
            this.Vector = vector;

            this.Text = Utils.FormatTime(vector.StartTime, true, false);
            //this.SubItems.Clear();
            /*
            int count = 0;
            foreach (MotionVector vector in this.Frame.Vectors)
            {
                if (vector.Highlight) count++;
            }*/
            //this.SubItems.Add(new ListViewSubItem(this, string.Format("({0:0},{1:0})->({2:0},{3:0})", this.Vector.Point1.X, this.Vector.Point1.Y, this.Vector.Point2.X, this.Vector.Point2.Y)));

            this.SubItems.Add(new ListViewSubItem(this, string.Format("{0:0}", this.Vector.Norm)));

            //this.SubItems.Add(new ListViewSubItem(this, string.Format("({0:0},{1:0})", this.Vector.Point1.X, this.Vector.Point1.Y)));
            //this.SubItems.Add(new ListViewSubItem(this, string.Format("({0:0},{1:0})", this.Vector.Point2.X, this.Vector.Point2.Y)));
        }





    }



    public class MotionFrameItem : ListViewItem
    {
        public MotionFrame Frame;


        public MotionVector Vector = null;



        public MotionFrameItem(MotionFrame frame, int nrSelected)
        //    : base()
        {
            this.Frame = frame;

            this.Text = Utils.FormatTime(this.Frame.Time1, true, false);
            //this.SubItems.Clear();
            /*
            int count = 0;
            foreach (MotionVector vector in this.Frame.Vectors)
            {
                if (vector.Highlight) count++;
            }*/
            this.SubItems.Add(new ListViewSubItem(this, string.Format("({0:0}/{1:0})", nrSelected, this.Frame.Vectors.Count)));

            this.SubItems.Add(new ListViewSubItem(this, string.Format("({0:0})", this.Frame.BiggestVector)));

            //this.SubItems.Add(new ListViewSubItem(this, string.Format("({0:0},{1:0})", this.Vector.Point1.X, this.Vector.Point1.Y)));
            //this.SubItems.Add(new ListViewSubItem(this, string.Format("({0:0},{1:0})", this.Vector.Point2.X, this.Vector.Point2.Y)));
        }





    }






    [Serializable]
    public class MotionData : SerializableXml<MotionData>
    {
        public string VideoFilename;
        public List<MotionFrame> MotionFrames;

        public MotionData()
        {
            this.VideoFilename = "";
            this.MotionFrames = new List<MotionFrame>();
        }



    }





    [Serializable]
    public class MotionFrame // : SerializableXml<MotionFrame>
    {

        public List<MotionVector> Vectors;
        public double Time1;
        public double Time2;


        public MotionFrame()
        {
            this.Time1 = 0;
            this.Time2 = 0;
            this.Vectors = new List<MotionVector>();
        }

        public MotionFrame(double time1, double time2, List<MotionVector> vectors)
        {
            this.Time1 = time1;
            this.Time2 = time2;
            this.Vectors = vectors;

        }


        private double _biggestVector = -1;
        [XmlIgnore]
        public double BiggestVector
        {
            get
            {
                if (_biggestVector < 0)
                {
                    double maxNorm = 0;
                    foreach (MotionVector vector in this.Vectors)
                    {
                        if (vector.Norm > maxNorm)
                        {
                            maxNorm = vector.Norm;
                        }
                    }
                    _biggestVector = maxNorm;
                }
                return _biggestVector;
            }

        }


    }


    [Serializable]
    public class MotionVector
    {
        public PointF Point1;
        public PointF Point2;

        public bool Highlight;

        public bool Deleted;
        public double StartTime = 0;


        public MotionVector()
        {
            this.Point1 = PointF.Empty;
            this.Point2 = PointF.Empty;
            Highlight = false;
            Deleted = false;
        }

        public MotionVector(PointF point1, PointF point2)
        {
            this.Point1 = point1;
            this.Point2 = point2;
            Highlight = false;
        }


        private double _norm = -1;
        public double Norm
        {
            get
            {
                if (_norm >= 0) return _norm;

                double diffX = this.Point2.X - this.Point1.X;
                double diffY = this.Point2.Y - this.Point1.Y;

                double vectorNorm = Math.Sqrt(diffX * diffX + diffY * diffY);
                _norm = vectorNorm;
                return vectorNorm;
            }
        }

        public double Direction
        {
            get
            {
                double diffX = this.Point2.X - this.Point1.X;
                double diffY = this.Point2.Y - this.Point1.Y;

                double angle = Math.Atan(diffY / diffX);
                return angle * 180.0 / Math.PI;
            }

        }


        public override bool Equals(object obj)
        {
            MotionVector other = obj as MotionVector;
            if (other != null)
            {
                return other.Point1.Equals(this.Point1) && other.Point2.Equals(this.Point2);
            }
            return false;
        }

    }



}
