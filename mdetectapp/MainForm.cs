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
using System.Drawing.Drawing2D;
using System.Diagnostics;


namespace MotionDetector
{
    public partial class MainForm : Form
    {

        private VideoProcessor _videoProcessor;
        private VideoPlayer _videoPlayer;
        private string _videoFilename;
        private MotionLog _mlog = new MotionLog();

        private string FormTitle = "Motion Detector";
        private BatchProcessForm _batchProcessForm;

        private double _tbmult = 100.0;
        private byte _brightness = 128;
        private byte _contrast = 128;
        private byte _threshold = 15;
        private int _criteria = 0;

        private bool _mouseDown = false;
        private Rectangle _selectionRect = Rectangle.Empty;
        private bool _selectionEnabled = false;
        private bool _showVideoRect = false;
        private Point _ptorg = new Point();
        private Rectangle _rectold = new Rectangle();
        private List<Rectangle> _excRctList = new List<Rectangle>();
       
        public MainForm()
        {
            InitializeComponent();
            FormTitle = Text;
            _videoProcessor = new VideoProcessor();   
            _videoPlayer = new VideoPlayer(panelPreview);
            _batchProcessForm = new BatchProcessForm();
        }
        
       
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
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
                _videoPlayer.OpenFile(_videoFilename, panelPreview.Handle, cmbMode.SelectedIndex) ;
                  
                btnStop.Enabled = false;
                btnProcess.Enabled = true;

                trackBarProgress.Maximum = (int)(_videoPlayer.Duration / VideoPlayer.Units * _tbmult );
                progressBarProcess.Value = 0;
                EnableControls(true);
                btnRemoveExcZone.Enabled = false;
                cmbMode.Enabled = true;
                lblMode.Enabled = true;

                ClearExcZones();
                Stop();
                Text = FormTitle + " - " + Path.GetFileName(_videoFilename);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        void UpdateLogFileControls()
        {
            lblLogFile.Visible = true;
            lblLogFilename.Visible = true;
            lblLogFilename.Text = _videoProcessor.LogFile;
            _videoPlayer.LogFile = _videoProcessor.LogFile;
            cmbMode.SelectedIndex = 1;
            lbViewMotion.Items.Clear();
        }
        
        DateTime _startTime;
        private void buttonProcess_Click(object sender, EventArgs e)
        {
            _videoProcessor.OpenFile(_videoFilename);

            _videoProcessor.ClearExcRects();
            foreach (LbItem i in lbExcZones.Items)
                _videoProcessor.AddExcRect(i.RectF);

            _videoProcessor.Threshold = _threshold;
            _videoProcessor.Contrast = _contrast;
            _videoProcessor.Brightness = _brightness;
            _videoProcessor.Criteria = _criteria;

            UpdateLogFileControls();

            _startTime = DateTime.Now;

            btnStop.Enabled = true;
            btnProcess.Enabled = false;
            timerProcess.Start();
            _videoProcessor.Start();
  
            toolStripStatusLabel2.Text = " ";

            lblThreshold.Enabled = false;
            lblContrast.Enabled = false;
            lblBrightness.Enabled = false;
            numericUpDownSkipFrames.Enabled = false;
            numericUpDownThreshold.Enabled = false;
            trackBarThreshold.Enabled = false;
            numericUpDownContrast.Enabled = false;
            trackBarContrast.Enabled = false;
            numericUpDownBrightness.Enabled = false;
            trackBarBrightness.Enabled = false;
            cmbMode.Enabled = false;
            lblMode.Enabled = false;
            btnDefault.Enabled = false;
            gbExcZones.Enabled = false;
            toolBarControls.Enabled = false;
            gbTimeFilter.Enabled = false;
            fileToolStripMenuItem.Enabled = false;
            lblCriteria.Enabled = false;
            cmbCriteria.Enabled = false;
        }
        
        private void buttonStopProcess_Click(object sender, EventArgs e)
        {
            StopProcess();
        }
                
        private void StopProcess()
        {
            _videoProcessor.Stop();
            timerProcess.Stop();
            btnStop.Enabled = false;
            btnProcess.Enabled = true;

            progressBarProcess.Value = 0;
            Cursor.Current = Cursors.WaitCursor;
            _videoPlayer.LogFile = _videoProcessor.LogFile;
            lblLogFilename.Text = _videoProcessor.LogFile;
            LoadMotion();
            UpdateLogLabels();

            lblThreshold.Enabled = true;
            lblContrast.Enabled = true;
            lblBrightness.Enabled = true;
            numericUpDownSkipFrames.Enabled = true;
            numericUpDownThreshold.Enabled = true;
            trackBarThreshold.Enabled = true;
            numericUpDownContrast.Enabled = true;
            trackBarContrast.Enabled = true;
            numericUpDownBrightness.Enabled = true;
            trackBarBrightness.Enabled = true;
            cmbMode.Enabled = true;
            lblMode.Enabled = true;
            btnDefault.Enabled = true;
            gbExcZones.Enabled = true;
            toolBarControls.Enabled = true;
            gbTimeFilter.Enabled = true;
            fileToolStripMenuItem.Enabled = true;
            lblCriteria.Enabled = true;
            cmbCriteria.Enabled = true;

            Cursor.Current = Cursors.Default;
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
            Int64 position = _videoProcessor.Position;
            Int64 duration = _videoProcessor.Duration;
                     

            int progress = (int)(position * _tbmult / duration);
            if (progress < progressBarProcess.Minimum)  progress = progressBarProcess.Minimum;
            if (progress > progressBarProcess.Maximum) progress = progressBarProcess.Maximum;
            
            progressBarProcess.Value = progress;

            TimeSpan elapsedTime = DateTime.Now - _startTime;
            double elapsedSeconds = elapsedTime.TotalSeconds;
            double totalSeconds = progress >0 ?(_tbmult * elapsedSeconds) / progress : 0;
            toolStripStatusLabel2.Text = "Elapsed: " + Utils.FormatTime(elapsedSeconds, false, true) + "  /  Remaining: " + Utils.FormatTime(totalSeconds - elapsedSeconds, false, true);

            if (position >= duration-10000)
            {
                progressBarProcess.Value = progressBarProcess.Maximum;
                StopProcess();
            }
        }

         private void InitControlValues()
        {
            trackBarContrast.Value = _contrast;
            numericUpDownContrast.Value = _contrast;
            trackBarBrightness.Value = _brightness;
            numericUpDownBrightness.Value = _brightness;
            numericUpDownThreshold.Value = _threshold;
            trackBarThreshold.Value = _threshold;
            cmbMode.SelectedIndex = 0;
            cmbCriteria.SelectedIndex = 0;
        }

        #region Contrast
        private void trackBarContrast_Scroll(object sender, EventArgs e)
        {
            _contrast = (byte)trackBarContrast.Value;
            numericUpDownContrast.Value = _contrast;
            UpdateContrast();
        }


        private void numericUpDownContrast_ValueChanged(object sender, EventArgs e)
        {
            _contrast = (byte)numericUpDownContrast.Value;
            trackBarContrast.Value = _contrast;
            UpdateContrast();
        }

        public void UpdateContrast()
        {
            _videoProcessor.Contrast = _contrast;
            _videoPlayer.Contrast = _contrast;
        }
        #endregion

        #region Brightness
        private void trackBarBrightness_Scroll(object sender, EventArgs e)
        {
            _brightness = (byte)trackBarBrightness.Value;
            numericUpDownBrightness.Value = _brightness;
            UpdateBrightness();
        }

        
        private void numericUpDownBrightness_ValueChanged(object sender, EventArgs e)
        {
            _brightness = (byte)numericUpDownBrightness.Value;
            trackBarBrightness.Value = _brightness;
            UpdateBrightness();
        }


        public void UpdateBrightness()
        {
            _videoProcessor.Brightness = _brightness;
            _videoPlayer.Brightness = _brightness;
        }
        #endregion

        #region Threshold
        private void numericUpDownTheshold_ValueChanged(object sender, EventArgs e)
        {
            _threshold = (byte)numericUpDownThreshold.Value;
            trackBarThreshold.Value = _threshold;
            UpdateThreshold();
        }

        private void trackBarThreshold_Scroll(object sender, EventArgs e)
        {
            _threshold = (byte)trackBarThreshold.Value;
            numericUpDownThreshold.Value = _threshold;
            UpdateThreshold();
        }

        private void UpdateThreshold()
        {
            _videoPlayer.Threshold = _threshold;
            _videoProcessor.Threshold = _threshold;
        }
        #endregion

        #region
        private void UpdateCriteria()
        {
            _videoPlayer.Criteria = _criteria;
            _videoProcessor.Criteria = _criteria;
        }
        #endregion

        private void toolBarControls_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == toolBarButtonPlay)
            {
                Play();
            }
            else if (e.Button == toolBarButtonPause)
            {
                Pause();
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
            _videoPlayer.ClearExcRects();
            foreach (LbItem i in lbExcZones.Items)
                _videoPlayer.AddExcRect(i.RectF);

            _videoPlayer.Play();
            ResetButtons();
            toolBarButtonPlay.Pushed = true;
            timerPlay.Start();
            cmbMode.Enabled = false;
            trackBarProgress.Enabled = true;
            lbViewMotion.Enabled = true;
            gbTimeFilter.Enabled = false;
            _showVideoRect = false;
            btnProcess.Enabled = false;
            gbExcZones.Enabled = false;
        }

        private void Pause()
        {
            _videoPlayer.Pause();
            ResetButtons();
            toolBarButtonPause.Pushed = true;
            cmbMode.Enabled = false;
            trackBarProgress.Enabled = true;
            lbViewMotion.Enabled = true;
            gbTimeFilter.Enabled = false;
            btnProcess.Enabled = false;
            gbExcZones.Enabled = false;
        }

        private void Stop()
        {
            _videoPlayer.Stop();
            ResetButtons();
            toolBarButtonStop.Pushed = true;
            timerPlay.Stop();
            SetVideoTime(0, true);
            cmbMode.Enabled = true;
            trackBarProgress.Enabled = false;
            lbViewMotion.Enabled = false;
            gbTimeFilter.Enabled = (cmbMode.SelectedIndex == 1) && (lbViewMotion.Items.Count > 1);
            btnAddExcZone.Enabled = true;
            btnProcess.Enabled = true;
            _showVideoRect = true;
            gbExcZones.Enabled = true;
            panelPreview.Invalidate();
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
            toolBarButtonPlay.Enabled = enable;
            toolBarButtonPause.Enabled = enable;
            toolBarButtonStop.Enabled = enable;
            toolBarButtonDec10Sec.Enabled = enable;
            toolBarButtonInc10Sec.Enabled = enable;
            toolBarButtonStepForward.Enabled = enable;
            toolBarButtonStepBackward.Enabled = enable;

            lbExcZones.Enabled = enable;
            btnAddExcZone.Enabled = enable;
            btnRemoveExcZone.Enabled = enable;
            btnClearExcZones.Enabled = enable;

            saveFileToolStripMenuItem.Enabled = enable;
            exportMotionDataToolStripMenuItem.Enabled = enable;
            
            toolBarControls.Enabled = enable;
            numericUpDownSkipFrames.Enabled = enable;

            numericUpDownSkipFrames.Enabled = enable;
            EnablePreviewControls(cmbMode.SelectedIndex == 0 ? true : false);
            
            EnablePreviewControls(enable);
            btnProcess.Enabled = enable;
            progressBarProcess.Enabled = enable;
            gbExcZones.Enabled = enable;
            lblCriteria.Enabled = enable;
            cmbCriteria.Enabled = enable;
        }

        private void UpdateVideoPosition(double pos)
        {
            _videoPlayer.Position = (Int64)((pos / _tbmult) * VideoPlayer.Units);
        }

        private void trackBarProgress_Scroll(object sender, EventArgs e)
        {
           UpdateVideoPosition(trackBarProgress.Value);
        }

        private void UpdateTrackbarPosition(int pos)
        {
            double w = (double)trackBarProgress.Width - 30;
            double p = (double)pos - 15;

            double d;
            d = (p / w) * (trackBarProgress.Maximum - trackBarProgress.Minimum);
            
            if (d < trackBarProgress.Minimum) d = trackBarProgress.Minimum;
            if (d > trackBarProgress.Maximum) d = trackBarProgress.Maximum;
            
            trackBarProgress.Value = Convert.ToInt32(d);
            UpdateVideoPosition(d);
        }

        private void trackBarProgress_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateTrackbarPosition(e.Location.X);
        }

        private void trackBarProgress_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                UpdateTrackbarPosition(e.Location.X);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitControlValues();
        }

        private void timerPlay_Tick(object sender, EventArgs e)
        {
            Int64 position = _videoPlayer.Position;
            Int64 duration = _videoPlayer.Duration;

            if (position >= duration)
            {
                Stop();
                return;
            }

            SetVideoTime(position, false);
        }

        private void SetVideoTime(double time, bool setTime)
        {
            if (setTime)
                UpdateVideoPosition(time);

            int val = (int)((double)_videoPlayer.Position / VideoPlayer.Units * _tbmult);
            if (val > trackBarProgress.Maximum) val = trackBarProgress.Maximum;
            trackBarProgress.Value = val;
            
            ShowFormatedTime(time / VideoPlayer.Units );
        }
        
        public void ShowFormatedTime(double currentTime)
        {
            double duration = _videoPlayer.Duration / VideoPlayer.Units;
            string strDuration = Utils.FormatTime(duration, false, true);
            string strTime = Utils.FormatTime(currentTime, false, true);
            labelTime.Text = strTime + " / " + strDuration;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _videoPlayer.Reset();
            _videoProcessor.Reset();
            _batchProcessForm.StopOnClose();
        }

        private void EnablePreviewControls(bool enable)
        {
            lblThreshold.Enabled = enable;
            trackBarThreshold.Enabled = enable;
            numericUpDownThreshold.Enabled = enable;

            lblContrast.Enabled = enable;
            trackBarContrast.Enabled = enable;
            numericUpDownContrast.Enabled = enable;

            lblBrightness.Enabled = enable;
            trackBarBrightness.Enabled = enable;
            numericUpDownBrightness.Enabled = enable;
            btnDefault.Enabled = enable;

        }

        private void panelPreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (_selectionEnabled && e.Button == MouseButtons.Left)
            {
                if (_videoPlayer.VideoRect.Contains(e.X, e.Y))
                {
                    _ptorg.X = e.X;
                    _ptorg.Y = e.Y;
                    _mouseDown = true;
                    _selectionRect = new Rectangle(e.X, e.Y, 0, 0);
                }
            }
        }

        private void panelPreview_MouseMove(object sender, MouseEventArgs e)
        {
            if (_selectionEnabled && _mouseDown)
            {
                //   if (_videoPlayer.VideoRect.Contains(e.X, e.Y))
                //   {
                Rectangle vrect = _videoPlayer.VideoRect;
                if (e.X >= vrect.Left && e.Y >= vrect.Top && e.X <= vrect.Right && e.Y <= vrect.Bottom)
                {
                    _selectionRect.Width = e.X - _selectionRect.X;
                    _selectionRect.Height = e.Y - _selectionRect.Y;

                    RectangleF selectionRectNorm = GetPercentRect();

                    int x1 = _ptorg.X > e.X ? e.X : _ptorg.X;
                    int y1 = _ptorg.Y > e.Y ? e.Y : _ptorg.Y;

                    int x2 = _ptorg.X > e.X ? _ptorg.X : e.X;
                    int y2 = _ptorg.Y > e.Y ? _ptorg.Y : e.Y;

                    _rectold = new Rectangle(x1, y1, x2 - x1, y2 - y1);
                    panelPreview.Invalidate();
                }
            }
        }

        private void panelPreview_SizeChanged(object sender, EventArgs e)
        {
            _rectold.Width = 0;
            _rectold.Height = 0;
            _videoPlayer.UpdateVideoSize();
            panelPreview.Invalidate();
        }

        private void panelPreview_Paint(object sender, PaintEventArgs e)
        {
            Pen wpen = new Pen(Color.White, 1);
            
            e.Graphics.DrawRectangle(wpen, _rectold);

            foreach (LbItem lbi in lbExcZones.Items)
                e.Graphics.DrawRectangle(wpen, GetAbsRect(lbi.RectF));

            if (_showVideoRect)
            {
                float[] dashValues = { 20, 5, 20, 5 };
                Pen gpen = new Pen(Color.Gray, 1);
                gpen.DashPattern = dashValues;
                e.Graphics.DrawRectangle(gpen, _videoPlayer.VideoRect);
                e.Graphics.DrawString("video rectangle", new Font("Calibri", 10), new SolidBrush(Color.Gray), new PointF(_videoPlayer.VideoRect.X, _videoPlayer.VideoRect.Y));
            }
        }

           private void panelPreview_MouseUp(object sender, MouseEventArgs e)
        {
            if (_selectionEnabled && _mouseDown)
            {
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
                    _selectionEnabled = false;
                    btnAddExcZone.Enabled = true;

                    LbItem lbi = new LbItem();
                    lbi.RectF = selectionRectNorm;

                    int n = lbExcZones.Items.Add(lbi);
                    lbExcZones.SelectedIndex = lbExcZones.Items.Count - 1;

                    panelPreview.Cursor = Cursors.Default;
                }
            }
        }

        private RectangleF GetPercentRect()
        {
            float xNorm = (float)(_selectionRect.X - _videoPlayer.VideoRect.X) / (float)(_videoPlayer.VideoRect.Width);
            float yNorm = (float)(_selectionRect.Y - _videoPlayer.VideoRect.Y) / (float)(_videoPlayer.VideoRect.Height);
            float widthNorm = (float)_selectionRect.Width / (float)_videoPlayer.VideoRect.Width;
            float heightNorm = (float)_selectionRect.Height / (float)_videoPlayer.VideoRect.Height;

            return new RectangleF(xNorm, yNorm, widthNorm, heightNorm);
        }

        private Rectangle GetAbsRect(RectangleF rectf)
        {
            int x = (int)Math.Round(_videoPlayer.VideoRect.Width * rectf.X + _videoPlayer.VideoRect.X);
            int y = (int)Math.Round(_videoPlayer.VideoRect.Height * rectf.Y + _videoPlayer.VideoRect.Y);
            int width = (int)Math.Round(_videoPlayer.VideoRect.Width * rectf.Width);
            int height = (int)Math.Round(_videoPlayer.VideoRect.Height * rectf.Height); 

            return new Rectangle(x, y, width, height);
        }


        private void buttonAddExclusionZones_Click(object sender, EventArgs e)
        {
            _selectionEnabled = true;
            _showVideoRect = true;
            btnAddExcZone.Enabled = false;
            panelPreview.Cursor = Cursors.Cross;
            panelPreview.Invalidate();
        }

        private void buttonResetExclusionZones_Click(object sender, EventArgs e) 
        {
            _rectold.Width = 0;
            _rectold.Height = 0;

            lbExcZones.Items.RemoveAt(lbExcZones.SelectedIndex);
            panelPreview.Invalidate();
            btnRemoveExcZone.Enabled = false;
        }

        private void ClearExcZones()
        {
            _rectold.Width = 0;
            _rectold.Height = 0;
            lbExcZones.Items.Clear();
            _videoPlayer.ClearExcRects();
        }

        private void btnClearExcZones_Click(object sender, EventArgs e)
        {
            ClearExcZones();
            panelPreview.Invalidate();
            btnRemoveExcZone.Enabled = false;
        }

        private void lbExcZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveExcZone.Enabled = true;
        }

        private void batchProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
           _batchProcessForm.ShowDialog(this);
        }

          
        private void trackBarProgress_KeyDown(object sender, KeyEventArgs e)
        {
            int nrFrames = (int)numericUpDownSkipFrames.Value;
            if (e.KeyCode == Keys.Right)
                _videoPlayer.StepForward(nrFrames);
            if (e.KeyCode == Keys.Left)
                _videoPlayer.StepBackward(nrFrames);
        }

        private void UpdateLogLabels()
        {
            lblLogFile.Visible = (cmbMode.SelectedIndex != 0 && lblLogFilename.Text != "");
            lblLogFilename.Visible = (cmbMode.SelectedIndex != 0 && lblLogFilename.Text != "");
            _videoPlayer.Mode = (VideoPlayerModes)cmbMode.SelectedIndex;
        }

        private void ReloadMotionListView()
        {
            lbViewMotion.Items.Clear();
            for (int i = 0; i < _mlog.Lst.Count; i++)
            {
                ListViewItem item = new ListViewItem(_mlog.Lst[i].Number.ToString());
                item.Tag = _mlog.Lst[i].Time;
                item.SubItems.Add(_mlog.FormatTime(i));
                lbViewMotion.Items.Add(item);
            }
        }

        private void LoadMotion()
        {
            _mlog.Load(_videoPlayer.MotionIndexList);
            ReloadMotionListView();
        }

        private void openLogStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogLog.ShowDialog();
            if (openFileDialogLog.FileName != "")
            {
                Cursor.Current = Cursors.WaitCursor;
                cmbMode.SelectedIndex = 1; // Switch to log displaying mode
                _videoPlayer.LogFile = openFileDialogLog.FileName;
                lblLogFilename.Text = _videoPlayer.LogFile;// openFileDialogLog.FileName;
                LoadMotion();
                UpdateLogLabels();
                gbTimeFilter.Enabled = true;
                saveLogStripMenuItem.Enabled = true;
                Cursor.Current = Cursors.Default;
            }                
        }

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLogLabels();      
        }

        private void lbViewMotion_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lbViewMotion.SelectedItems.Count != 0)
            { 
                ListViewItem lvi = lbViewMotion.SelectedItems[0];
                _videoPlayer.Position = Int64.Parse(lvi.Tag.ToString()) - 100000;
            }
        }

   
        private void btnDoTimeFilter_Click(object sender, EventArgs e)
        {
            _mlog.Filter(udTimeFilter.Value);
            ReloadMotionListView();
        }

        private void btnResetTimeFilter_Click(object sender, EventArgs e)
        {
            LoadMotion();
        }

        private void saveLogStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialogLog.ShowDialog() == DialogResult.OK)
            {
                _mlog.Save(saveFileDialogLog.FileName, _videoPlayer.LogFile);
                MessageBox.Show("Log data is saved");
            }            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsForm frm = new SettingsForm(_videoProcessor, _batchProcessForm.ViewModel, _videoPlayer);
            frm.ShowDialog();
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Q | Keys.Control:
                    //Process action here.
                    _videoPlayer.TakeSShot();
                    return false;
            }

            return false;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            _threshold = 15;
            _brightness = 128;
            _contrast = 128;
            _criteria = 0;

            trackBarThreshold.Value = _threshold;
            numericUpDownThreshold.Value = _threshold;

            trackBarBrightness.Value = _brightness;
            numericUpDownBrightness.Value = _brightness;

            trackBarContrast.Value = _contrast;
            numericUpDownContrast.Value = _contrast;

            cmbCriteria.SelectedIndex = _criteria;
            
            UpdateBrightness();
            UpdateContrast();
            UpdateThreshold();
            UpdateCriteria();
        }

        private void cmbCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            _criteria = cmbCriteria.SelectedIndex;
            UpdateCriteria();
        }
    }
}