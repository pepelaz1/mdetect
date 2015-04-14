using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.IO;


namespace MotionDetector
{
    public partial class BatchInstanceForm : Form
    {
        public BatchInstanceForm()
        {
            InitializeComponent();
        }


        public string Filename = "";

        private ProcessCommunicationServer _server = null;

        private VideoProcessor _videoProcessor;
        private DateTime _startTime;

        private int _processId;

        private TcpClientChannel _tcpChannel;


        private void BatchInstanceForm_Load(object sender, EventArgs e)
        {
            RunProcessor();
        }



        private void RunProcessor()
        {

            try
            {
                _processId = System.Diagnostics.Process.GetCurrentProcess().Id;

                string serverPath = "tcp://localhost";
                _tcpChannel = new TcpClientChannel();
                ChannelServices.RegisterChannel(_tcpChannel, false);
                _server = (ProcessCommunicationServer)Activator.GetObject(typeof(ProcessCommunicationServer), serverPath + ":" + ProcessCommunicationServer.ServerPort + "/" + ProcessCommunicationServer.ServerName);

                ProcessSettings settings = _server.GetSettings();

                _videoProcessor = new VideoProcessor();
                _videoProcessor.Brightness = settings.Brightness;
                _videoProcessor.Contrast = settings.Contrast;
                _videoProcessor.FilterNoise = settings.FilterNoise;
                _videoProcessor.FrameReductionFactor = settings.FrameReductionFactor;
                _videoProcessor.FrameSkip = settings.FrameSkip;
                _videoProcessor.GetExclusionRectangles().AddRange(settings.ExclusionRectangles);

                _videoProcessor.OpenFile(this.Filename);
                _videoProcessor.ProcessCompleted += new EventHandler(_videoProcessor_ProcessCompleted);

                _startTime = DateTime.Now;

                timerProgress.Start();
                _videoProcessor.Start();
                timerProgress.Stop();


                MotionData motionData = new MotionData();
                motionData.VideoFilename = Filename;

                motionData.MotionFrames = _videoProcessor.GetMotion();
                string saveFilename = Path.GetDirectoryName(Filename) + "\\" + Path.GetFileNameWithoutExtension(Filename) + ".motion";
                motionData.Save(saveFilename);

                //Utils.WriteLog("Exit: " + _processId + "  Time: " + DateTime.Now.ToLongTimeString());

                //Environment.Exit(0);

                //_server.ProcessCompleted(_processId);

            }
            catch (Exception ex)
            {
                try
                {
                    ChannelServices.UnregisterChannel(_tcpChannel);
                }
                catch { }

                //Utils.WriteLog("Client: " + ex.ToString());
                Environment.Exit(-100);
                //_server.ProcessError(_processId, ex.Message);
            }

            try
            {
                ChannelServices.UnregisterChannel(_tcpChannel);
            }
            catch { }

            this.Close();
        }

        void _videoProcessor_ProcessCompleted(object sender, EventArgs e)
        {
            _videoProcessor.Stop();
            timerProgress.Stop();
        }

        private void timerProgress_Tick(object sender, EventArgs e)
        {
            try
            {
                double progress = _videoProcessor.GetCurrentTime() * 100.0 / _videoProcessor.GetDuration();

                TimeSpan elapsedTime = DateTime.Now - _startTime;
                double elapsedSeconds = elapsedTime.TotalSeconds;

                _server.SetProgress(_processId, progress, elapsedSeconds);
                
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RunProcessor();
        }


        private bool _activated = false;
        private void BatchInstanceForm_Activated(object sender, EventArgs e)
        {
            if (!_activated)
            {
                _activated = true;
                //RunProcessor();
            }
        }




    }
}