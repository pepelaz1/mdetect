using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;


namespace MotionDetector
{
    public partial class MainForm : Form
    {

        private VideoProcessor _videoProcessor;
        private VideoPlayer _videoPlayer;
        private string _videoFilename;

        private string FormTitle = "Motion Detector";

        private BatchProcessForm _batchProcessForm;

        private SettingsForm _settingsForm;


        public MainForm()
        {
            InitializeComponent();
            this.FormTitle = this.Text;

            _videoProcessor = new VideoProcessor();
            _videoProcessor.ProcessCompleted += new EventHandler(_videoProcessor_ProcessCompleted);
            _videoPlayer = new VideoPlayer(panelPreview);

            _batchProcessForm = new BatchProcessForm();

            ProcessCommunicationServer.BatchForm = _batchProcessForm;

            _settingsForm = new SettingsForm();

        }


        private void RegisterServer()
        {
            BinaryServerFormatterSinkProvider srvFormatter = new BinaryServerFormatterSinkProvider();
            TcpServerChannel channel = new TcpServerChannel(ProcessCommunicationServer.ServerPort);

            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(ProcessCommunicationServer), ProcessCommunicationServer.ServerName, WellKnownObjectMode.Singleton);
            //ChannelServices.UnregisterChannel(channel);
        }






        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogMov.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFileDialogMov.FileName);
            }
        }




        private bool OpenFile(string filename)
        {

            try
            {
                _videoFilename = filename;
                _videoPlayer.OpenFile(_videoFilename);
                StopProcess();
                trackBarProgress.Maximum = (int)(_videoPlayer.Duration * 100);
                EnableControls(true);
                Stop();
                this.Text = FormTitle + " - " + Path.GetFileName(_videoFilename);
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }



        /*
        Timer _timerProcess = null;
        void _timerProcess_Tick(object sender, EventArgs e)
        {
            _timerProcess.Enabled = false;


            try
            {
                int frameSkip = (int)numericUpDownFrames.Value;

                _videoProcessor.FrameReductionFactor = (double)numericUpDownFrameDivider.Value;
                _videoProcessor.FrameSkip = frameSkip;
                _videoProcessor.FilterNoise = checkBoxFilterNoise.Checked;
                _videoProcessor.OpenFile(_videoFilename);

                List<RectangleF> exclusionRectangles = _videoPlayer.GetExclusionRectangles();
                List<RectangleF> rectListProcessor = _videoProcessor.GetExclusionRectangles();
                rectListProcessor.Clear();
                foreach (RectangleF rect in exclusionRectangles)
                {
                    rectListProcessor.Add(rect);
                }

                _startTime = DateTime.Now;

                buttonStopProcess.Enabled = true;
                buttonProcess.Enabled = false;
                timerProcess.Start();
                _videoProcessor.Start();
                timerProcess.Stop();



                UpdateMotionTimes();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing file: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            toolStripStatusLabel2.Text = " ";

        }
        */

        DateTime _startTime;
        private void buttonProcess_Click(object sender, EventArgs e)
        {
            /*
            if (_timerProcess == null)
            {
                _timerProcess = new Timer();
                _timerProcess.Interval = 100;
                _timerProcess.Tick += new EventHandler(_timerProcess_Tick);
            }
            _timerProcess.Start();
            return;
            */
            try
            {
                int frameSkip = (int)numericUpDownFrames.Value;

                _videoProcessor.FrameReductionFactor = (double)numericUpDownFrameDivider.Value;
                _videoProcessor.FrameSkip = frameSkip;
                _videoProcessor.FilterNoise = checkBoxFilterNoise.Checked;
                _videoProcessor.OpenFile(_videoFilename);

                List<RectangleF> exclusionRectangles = _videoPlayer.GetExclusionRectangles();
                List<RectangleF> rectListProcessor = _videoProcessor.GetExclusionRectangles();
                rectListProcessor.Clear();
                foreach (RectangleF rect in exclusionRectangles)
                {
                    rectListProcessor.Add(rect);
                }

                _startTime = DateTime.Now;

                buttonStopProcess.Enabled = true;
                buttonProcess.Enabled = false;
                timerProcess.Start();
                _videoProcessor.Start();
                timerProcess.Stop();

                

                UpdateMotionTimes();

                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing file: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            toolStripStatusLabel2.Text = " ";
        }





        private int NrItemsPerPage = 100;
        private int _currentPage = 1;
        private int _totalPages = 0;
        private Queue<MotionVector> _deletedVectors = new Queue<MotionVector>();
        private List<MotionVector> _selectedVectors = new List<MotionVector>();

        private void UpdateMotionTimes()
        {

            List<MotionFrame> motionFrames = _videoProcessor.GetMotion();
            /*
            for (int i = 0; i < 5; i++)
            {
                motionFrames.AddRange(_videoProcessor.GetMotion());
            }
            */

            double thresholdMotion = (double)numericUpDownThreshold.Value;
            double thresholdMotionHigh = (double)numericUpDownThresholdHigh.Value;

            _selectedVectors = new List<MotionVector>();

            int nrSelectedVectors = 0;
            foreach (MotionFrame motionFrame in motionFrames)
            {
                
                foreach (MotionVector vector in motionFrame.Vectors)
                {
                    vector.Highlight = vector.Norm >= thresholdMotion && vector.Norm <= thresholdMotionHigh;
                    if (vector.Highlight)
                    {
                        nrSelectedVectors++;
                    }
                    vector.Deleted = false;
                    vector.StartTime = motionFrame.Time1;

                    _selectedVectors.Add(vector);
                }

            }

            _currentPage = 1;
            _totalPages = (nrSelectedVectors / NrItemsPerPage);
            if (nrSelectedVectors % NrItemsPerPage > 0) _totalPages++;

            _deletedVectors.Clear();

            buttonPrevPage.Enabled = true;
            buttonNextPage.Enabled = true;
            buttonPrevPage10.Enabled = true;
            buttonNextPage10.Enabled = true;

            _videoPlayer.SetMotion(motionFrames);

            UpdateMotionPage();

        }


        private void UpdateMotionPage()
        {
            List<MotionFrame> motionFrames = _videoProcessor.GetMotion();

            int startIndex = (_currentPage - 1) * NrItemsPerPage;

            listViewMotion.Items.Clear();

            labelCurrentPage.Text = _currentPage.ToString();
            labelTotalPages.Text = _totalPages.ToString();

            int nrSelectedVectors = 0;
            //foreach (MotionFrame motionFrame in motionFrames)
            //{
                
                //foreach (MotionVector vector in motionFrame.Vectors)
            foreach (MotionVector vector in _selectedVectors)
            {
                if (vector.Highlight && !vector.Deleted)
                {
                    

                    if (nrSelectedVectors < startIndex + NrItemsPerPage)
                    {
                        if (nrSelectedVectors >= startIndex)
                        {
                            MotionVectorItem motionItem = new MotionVectorItem(vector);

                            listViewMotion.Items.Add(motionItem);
                        }
                    }
                    else
                    {
                        return;
                    }
                    nrSelectedVectors++;
                }
            }



            //}




            
            /*
            for (int i = startIndex; i < startIndex + NrItemsPerPage && i < motionFrames.Count; i++)
            {

                MotionFrame motionFrame = motionFrames[i];

                MotionFrameItem motionFrameItem = new MotionFrameItem(motionFrame);

                listViewMotion.Items.Add(motionFrameItem);

            }
            */



        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewMotion.SelectedItems)
            {
                try
                {
                    MotionVectorItem selectedVector = item as MotionVectorItem;
                    selectedVector.Vector.Deleted = true;
                    _deletedVectors.Enqueue(selectedVector.Vector);
                    undoDeleteToolStripMenuItem.Enabled = true;

                    UpdateMotionPage();

                }
                catch { }
            }
        }


        private void contextMenuStripVector_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = listViewMotion.SelectedItems.Count == 0;
        }


        private void undoDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MotionVector vector = _deletedVectors.Dequeue();
                vector.Deleted = false;
                undoDeleteToolStripMenuItem.Enabled = _deletedVectors.Count > 0;

                UpdateMotionPage();
            }
            catch { }
        }



        private void listViewMotion_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                /*
                if (e.Column == 1)
                {
                    return;
                }
                */

                _currentPage = 1;

                List<MotionFrame> motionFrames = _videoProcessor.GetMotion();

                Comparison<MotionVector> comparison = null;
                if (e.Column == 0)
                {
                    comparison = new Comparison<MotionVector>(compareMotionByTime);
                }
                else if (e.Column == 1)
                {
                    comparison = new Comparison<MotionVector>(compareMotionByVectorSize);
                }


                _selectedVectors.Sort(comparison);


                UpdateMotionPage();
            }
            catch { }
        }


        int compareMotionByTime(MotionVector vector1, MotionVector vector2)
        {
            return (int)(1000.0 * (vector1.StartTime - vector2.StartTime));
        }

        int compareMotionByVectorSize(MotionVector vector1, MotionVector vector2)
        {
            int biggestVector1 = (int)(1000.0 * vector1.Norm);
            int biggestVector2 = (int)(1000.0 * vector2.Norm);
            return biggestVector2 - biggestVector1;
        }




        private void listViewMotion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MotionVectorItem item = listViewMotion.SelectedItems[0] as MotionVectorItem;

                _videoPlayer.SetSelectedMotion(item.Vector);
                if (item != null)
                {
                    SetVideoTime(item.Vector.StartTime, true);
                }
            }
            catch { }
        }





        private void buttonPrevPage_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdateMotionPage();
            }
        }

        private void buttonNextPage_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                UpdateMotionPage();
            }
        }

        private void buttonPrevPage10_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage -= 10;
                if (_currentPage < 1) _currentPage = 1;
                UpdateMotionPage();
            }
        }

        private void buttonNexPage10_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage += 10;
                if (_currentPage > _totalPages) _currentPage = _totalPages;
                UpdateMotionPage();
            }
        }






        private void buttonStopProcess_Click(object sender, EventArgs e)
        {
            StopProcess();
        }



        private void StopProcess()
        {
            try
            {
                _videoProcessor.Stop();
                timerProcess.Stop();
                buttonStopProcess.Enabled = false;
                buttonProcess.Enabled = true;
                progressBarProcess.Value = 0;
            }
            catch { }
        }


        void _videoProcessor_ProcessCompleted(object sender, EventArgs e)
        {
            timerProcess.Stop();
            TimeSpan elapsedTime = DateTime.Now - _startTime;
            MessageBox.Show("Processing completed in " + Utils.FormatTime(elapsedTime.TotalSeconds, false, true), "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

            StopProcess();
        }


      
        private void timerProcess_Tick(object sender, EventArgs e)
        {
            try
            {
                double progress = _videoProcessor.GetCurrentTime() * 100.0 / _videoProcessor.GetDuration();
                progressBarProcess.Value = (int)progress;

                TimeSpan elapsedTime = DateTime.Now - _startTime;
                double elapsedSeconds = elapsedTime.TotalSeconds;
                double totalSeconds = (100.0 * elapsedSeconds) / progress;
                toolStripStatusLabel2.Text = "Elapsed: " + Utils.FormatTime(elapsedSeconds, false, true) + "  /  Remaining: " + Utils.FormatTime(totalSeconds - elapsedSeconds, false, true);
            }
            catch { }
        }

        private void buttonUpdateThreshold_Click(object sender, EventArgs e)
        {
            buttonUpdateThreshold.Enabled = false;
            try
            {
                UpdateMotionTimes();
            }
            catch { }
        }







        private void numericUpDownThreshold_ValueChanged(object sender, EventArgs e)
        {
            float threshold = (float)numericUpDownThreshold.Value;
            SetThreshold(threshold);
            buttonUpdateThreshold.Enabled = true;
        }

        public void SetThreshold(float threshold)
        {
            numericUpDownThreshold.Value = (decimal)threshold;
            //trackBarThreshold.Value = (int)threshold;
            //_videoProcessor.Threshold = threshold;
        }



        private void trackBarContrast_Scroll(object sender, EventArgs e)
        {
            float contrast = (float)trackBarContrast.Value;
            SetContrast(contrast);
        }


        private void numericUpDownContrast_ValueChanged(object sender, EventArgs e)
        {
            float contrast = (float)numericUpDownContrast.Value;
            SetContrast(contrast);
        }

        public void SetContrast(float contrast)
        {
            numericUpDownContrast.Value = (decimal)contrast;
            trackBarContrast.Value = (int)contrast;
            contrast = (contrast * 128) / 100;
            _videoProcessor.Contrast = contrast;
            _videoPlayer.Contrast = contrast;
        }

        private void trackBarBrightness_Scroll(object sender, EventArgs e)
        {
            float brightness = (float)trackBarBrightness.Value;
            SetBrightness(brightness);
        }

        private void numericUpDownBrightness_ValueChanged(object sender, EventArgs e)
        {
            float brightness = (float)numericUpDownBrightness.Value;
            SetBrightness(brightness);
        }


        public void SetBrightness(float brightness)
        {
            numericUpDownBrightness.Value = (decimal)brightness;
            trackBarBrightness.Value = (int)brightness;
            brightness = (brightness * 256) / 100; //ffdshow scale
            _videoProcessor.Brightness = brightness;
            _videoPlayer.Brightness = brightness;
        }

        private void toolBarControls_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {

            if (e.Button == toolBarButtonPlay)
            {
                Play();
            }
            else if (e.Button == toolBarButtonPause)
            {
                _videoPlayer.Pause();
                ResetButtons();
                toolBarButtonPause.Pushed = true;
            }
            else if (e.Button == toolBarButtonStop)
            {
                Stop();
            }
            else if (e.Button == toolBarButtonStepBackward)
            {
                int nrFrames = (int)numericUpDownSkipFrames.Value;
                _videoPlayer.StepBackward(nrFrames);
                ResetButtons();
                toolBarButtonPause.Pushed = true;
                timerPlay_Tick(null, null);
            }
            else if (e.Button == toolBarButtonStepForward)
            {
                int nrFrames = (int)numericUpDownSkipFrames.Value;
                _videoPlayer.StepForward(nrFrames);
                ResetButtons();
                toolBarButtonPause.Pushed = true;
                timerPlay_Tick(null, null);
            }
        }


        private void Play()
        {
            _videoPlayer.Play();
            ResetButtons();
            toolBarButtonPlay.Pushed = true;
            timerPlay.Start();
        }

        private void Stop()
        {
            _videoPlayer.Stop();
            ResetButtons();
            toolBarButtonStop.Pushed = true;
            timerPlay.Stop();
            SetVideoTime(0, true);
        }

        private void ResetButtons()
        {
            foreach (ToolBarButton button in toolBarControls.Buttons)
            {
                button.Enabled = true;
                button.Pushed = false;
            }
        }


        private void EnableControls(bool enable)
        {
            //toolBarControls.Enabled = enable;
            
            toolBarButtonPlay.Enabled = enable;
            toolBarButtonPause.Enabled = enable;
            toolBarButtonStop.Enabled = enable;
            toolBarButtonDec10Sec.Enabled = enable;
            toolBarButtonInc10Sec.Enabled = enable;
            toolBarButtonStepForward.Enabled = enable;
            toolBarButtonStepBackward.Enabled = enable;
            
            trackBarProgress.Enabled = enable;

            buttonProcess.Enabled = enable;

            buttonAddExclusionZones.Enabled = enable;
            buttonResetExclusionZones.Enabled = enable;

            saveFileToolStripMenuItem.Enabled = enable;
            exportMotionDataToolStripMenuItem.Enabled = enable;
        }


        private void trackBarProgress_Scroll(object sender, EventArgs e)
        {
            try
            {
                _videoPlayer.Position = trackBarProgress.Value / 100.0;
            }
            catch { }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            EnableControls(false);

            try
            {
                RegisterServer();
            }
            catch { }

            UpdateMotionColors();
        }

        private void timerPlay_Tick(object sender, EventArgs e)
        {
            try
            {
                double position = _videoPlayer.Position;
                double duration = _videoPlayer.Duration;

                if (position >= duration)
                {
                    Stop();
                    return;
                }

                SetVideoTime(position, false);


            }
            catch
            { }
        }


        private void SetVideoTime(double time, bool setTime)
        {
            trackBarProgress.Value = (int)(time * 100.0);
            if(setTime) _videoPlayer.Position = time;
            ShowFormatedTime(time);
        }





        public void ShowFormatedTime(double currentTime)
        {
            double duration = _videoPlayer.Duration;

            string strDuration = Utils.FormatTime(duration, false, true);
            string strTime = Utils.FormatTime(currentTime, false, true);

            labelTime.Text = strTime + " / " + strDuration;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _videoPlayer.ReleaseInterfaces();
                _videoProcessor.ReleaseInterfaces();
                _batchProcessForm.Stop();
            }
            catch { }
        }









        private void checkBoxShowMotion_CheckedChanged(object sender, EventArgs e)
        {
            _videoPlayer.ShowMotion = checkBoxShowMotion.Checked;
        }










        private bool _mouseDown = true;
        private Rectangle _selectionRect = Rectangle.Empty;
        private bool _selectionEnabled = false;
        private Rectangle _videoRectangle = Rectangle.Empty;



        private void panelPreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (_selectionEnabled && e.Button == MouseButtons.Left)
            {

                _videoRectangle = _videoPlayer.GetVideoRectangle();


                Point point = Cursor.Position;
                Rectangle rectPanel = panelPreview.RectangleToScreen(_videoRectangle);

                if (rectPanel.Contains(point))
                {
                    _mouseDown = true;
                    _selectionRect = new Rectangle(e.X + _videoRectangle.X, e.Y + _videoRectangle.Y, 0, 0);
             
                }

            }
        }



        private void panelPreview_MouseMove(object sender, MouseEventArgs e)
        {
            if (_selectionEnabled && _mouseDown)
            {

                _selectionRect.Width = e.X - _selectionRect.X;
                _selectionRect.Height = e.Y - _selectionRect.Y;


                if (_selectionRect.X + _selectionRect.Width > _videoRectangle.Right)
                {
                    _selectionRect.Width = _videoRectangle.Right - _selectionRect.X;
                }
                if (_selectionRect.X + _selectionRect.Width < _videoRectangle.Left)
                {
                    _selectionRect.Width = _videoRectangle.Left - _selectionRect.X;
                }
                if (_selectionRect.Y + _selectionRect.Height > _videoRectangle.Bottom)
                {
                    _selectionRect.Height = _videoRectangle.Bottom - _selectionRect.Y;
                }
                if (_selectionRect.Y + _selectionRect.Height < _videoRectangle.Top)
                {
                    _selectionRect.Height = _videoRectangle.Top - _selectionRect.Y;
                }

                RectangleF selectionRectNorm = GetPercentRect();
                _videoPlayer.SetSelectionRectangle(selectionRectNorm);


            }
        }



        private void panelPreview_MouseUp(object sender, MouseEventArgs e)
        {
            if (_selectionEnabled && _mouseDown)
            {
                panelPreview.Refresh();
                _mouseDown = false;
                if (_selectionRect != Rectangle.Empty && _selectionRect.Width != 0 && _selectionRect.Height != 0)
                {
                    if (_selectionRect.Width < 0)
                    {
                        _selectionRect.Width = -_selectionRect.Width;
                        _selectionRect.X -= _selectionRect.Width;
                    }
                    if (_selectionRect.Height < 0)
                    {
                        _selectionRect.Height = -_selectionRect.Height;
                        _selectionRect.Y -= _selectionRect.Height;
                    }



                    RectangleF selectionRectNorm = GetPercentRect();
                    _videoPlayer.GetExclusionRectangles().Add(selectionRectNorm);
                    _videoPlayer.SetSelectionRectangle(RectangleF.Empty);
                    
                    _selectionEnabled = false;
                    buttonAddExclusionZones.Enabled = true;

                }
            }


        }


        private RectangleF GetPercentRect()
        {
            float xNorm = (float)(_selectionRect.X - _videoRectangle.X) / (float)_videoRectangle.Width;
            float yNorm = (float)(_selectionRect.Y - _videoRectangle.Y) / (float)_videoRectangle.Height;
            float widthNorm = (float)_selectionRect.Width / (float)_videoRectangle.Width;
            float heightNorm = (float)_selectionRect.Height / (float)_videoRectangle.Height;


            if (widthNorm < 0)
            {
                widthNorm = Math.Abs(widthNorm);
                xNorm -= widthNorm;
            }

            if (heightNorm < 0)
            {
                heightNorm = Math.Abs(heightNorm);
                yNorm -= heightNorm;
            }

            RectangleF selectionRectNorm = new RectangleF(xNorm, yNorm, widthNorm, heightNorm);
            return selectionRectNorm;
        }


        private void buttonAddExclusionZones_Click(object sender, EventArgs e)
        {
            _selectionEnabled = true;
            buttonAddExclusionZones.Enabled = false;
            //this.Cursor = Cursors.Cross;
            //panelPreview.Cursor = Cursors.Cross;
        }

        private void buttonResetExclusionZones_Click(object sender, EventArgs e)
        {
            try
            {
                _videoPlayer.GetExclusionRectangles().Clear();
            }
            catch { }
        }

        private void numericUpDownFrames_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxFilterNoise_CheckedChanged(object sender, EventArgs e)
        {
            buttonUpdateThreshold.Enabled = true;
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.Now;
                //string dateString = string.Format("{0:00}-{1:00}-{2:0000}_{3:00}-{4:00}-{5:00}", date.Month, date.Day, date.Year, date.hou
                string dateString = string.Format("_{0:MM-dd-yyyy_hh-mm-ss}", date);
                
                saveFileDialogLog.FileName = Path.GetDirectoryName(_videoFilename) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(_videoFilename) + dateString + ".txt";

                if (saveFileDialogLog.ShowDialog() == DialogResult.OK)
                {
                    List<string> lines = new List<string>();
                    string separator = "+------------+--------------+--------------+----------+";
                    lines.Add(separator);
                    lines.Add(string.Format("|    Time    | Point1 (X,Y) | Point2 (X,Y) |   Size   |"));
                    lines.Add(separator);


                    List<MotionFrame> motionFrames = _videoProcessor.GetMotion();

                    //foreach (MotionFrame motionFrame in motionFrames)
                    //{
                        //foreach (MotionVector vector in motionFrame.Vectors)
                        foreach (MotionVector vector in _selectedVectors)
                        {
                            if (vector.Highlight && !vector.Deleted)
                            {
                                string point1 = string.Format("({0:0},{1:0})", vector.Point1.X, vector.Point1.Y);
                                string point2 = string.Format("({0:0},{1:0})", vector.Point2.X, vector.Point2.Y);

                                string time = Utils.FormatTime(vector.StartTime, true, true);

                                //00.00.00.0
                                //string line = string.Format("Time:{0:0}\t\t\tStart Point:{1}\t\t\tEnd Point:{2}", time.StartTime, time.Vector.Point1, time.Vector.Point2);
                                string line = string.Format("| {0,10} | {1,12} | {2,12} | {3,8:0} |", time, point1, point2,vector.Norm);
                                lines.Add(line);
                            }
                        }
                    //}



                    lines.Add(separator);

                    File.WriteAllLines(saveFileDialogLog.FileName, lines.ToArray());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportMotionDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialogMotion.ShowDialog() == DialogResult.OK)
                {
                    MotionData motionData = new MotionData();
                    motionData.VideoFilename = _videoFilename;

                    /*
                    List<MotionFrame> motionFrames = new List<MotionFrame>();
                    foreach (MotionFrame frame in _videoProcessor.GetMotion())
                    {
                        MotionFrame newFrame = new MotionFrame();
                        foreach (MotionVector vector in frame.Vectors)
                        {
                            //if (vector.IsValid)
                            {
                                newFrame.Vectors.Add(vector);
                                newFrame.Time1 = frame.Time1;
                                newFrame.Time2 = frame.Time2;
                            }
                        }
                        motionFrames.Add(newFrame);
                    }
                    */

                    motionData.MotionFrames = _videoProcessor.GetMotion(); // motionFrames;
                    motionData.Save(saveFileDialogMotion.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importMotionDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogMotion.ShowDialog() == DialogResult.OK)
                {
                    MotionData motionData = MotionData.Load(openFileDialogMotion.FileName);
                    
                   
                    OpenFile(motionData.VideoFilename);

                    _videoProcessor.SetMotion(motionData.MotionFrames);

                    UpdateMotionTimes();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing data: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void batchProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {


            List<RectangleF> exclusionRectangles = _videoPlayer.GetExclusionRectangles();
            List<RectangleF> rectListProcessor = new List<RectangleF>();
            rectListProcessor.Clear();
            foreach (RectangleF rect in exclusionRectangles)
            {
                rectListProcessor.Add(rect);
            }

            _batchProcessForm.Brightness = _videoProcessor.Brightness;
            _batchProcessForm.Contrast = _videoProcessor.Contrast;
            _batchProcessForm.FrameSkip = (int)numericUpDownFrames.Value;
            _batchProcessForm.FrameReductionFactor = (double)numericUpDownFrameDivider.Value;
            _batchProcessForm.FilterNoise = checkBoxFilterNoise.Checked;
            _batchProcessForm.ExclusionRectangles = rectListProcessor;
            _batchProcessForm.Sensitivity = (double)numericUpDownThreshold.Value;
            _batchProcessForm.SensitivityHigh = (double)numericUpDownThresholdHigh.Value;

            _batchProcessForm.Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsForm.ShowDialog();

            UpdateMotionColors();
        }


        private void UpdateMotionColors()
        {
            _videoPlayer.DetectedMotionColor = _settingsForm.DetectedMotionColor;
            _videoPlayer.HighlightMotionColor = _settingsForm.HighlightMotionColor;
            _videoPlayer.SelectedVectorMotionColor = _settingsForm.SelectedVectorColor;
        }

        private void numericUpDownThresholdHigh_ValueChanged(object sender, EventArgs e)
        {
            //float threshold = (float)numericUpDownThreshold.Value;
            //SetThreshold(threshold);
            buttonUpdateThreshold.Enabled = true;
        }





















    }
}