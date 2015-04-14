using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MotionDetector
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //if (args.Length == 1)
            //{
              //  BatchInstanceForm batchForm = new BatchInstanceForm();
              //  batchForm.Filename = args[0];
              //  Application.Run(batchForm);
           // }
           // else
           // {
                Application.Run(new MainForm());
           // }
        }
    }
}