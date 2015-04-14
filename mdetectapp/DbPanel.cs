using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MotionDetector
{
    public partial class DbPanel : Panel
    {
        public DbPanel()
        {
            // Set the value of the double-buffering style bits to true.
            DoubleBuffered = true;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        } 
    }
}
