using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MotionDetector
{
    public class LbItem
    {
        public RectangleF RectF;
        public override string ToString()
        {
            return String.Format("{0:0.0000} , {1:0.0000} , {2:0.0000} , {3:0.0000}", RectF.Left, RectF.Top, RectF.Right, RectF.Bottom); ;
        }       
    }
}
