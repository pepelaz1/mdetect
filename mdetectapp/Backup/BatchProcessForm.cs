using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;



namespace MotionDetector
{
    public partial class BatchProcessForm : Form
    {
        private const int NrProcessors = 10;

        private FileProcessItem[] _processorItems;
        private System.Threading.Thread[] _processorThreads;
        private Process[] _processes;

        private int _runningProcesses;

        public BatchProcessForm()
        {
            InitializeComponent();

            _processorItems = new FileProcessItem[NrProcessors];

            _processorThreads = new System.Threading.Thread[NrProcessors];
            _processes = new Process[NrProcessors];

            for (int i = 0; i < NrProcessors; i++)
            {
                _processorItems[i] = null;
            }
            numericUpDownParalel.Maximum = NrProcessors;
            _runningProcesses = 0;
        }



        public float Brightness = 0;
        public float Contrast = 100;
        public int FrameSkip = 1;
        public List<RectangleF> ExclusionRectangles = new List<RectangleF>();
        public bool FilterNoise = false;
        public double FrameReductionFactor = 1;
        public double Sensitivity = 10;
        public double SensitivityHigh = 1000;

        public ProcessSettings GetSettings()
        {
            ProcessSettings settings = new ProcessSettings();
            settings.Brightness = this.Brightness;
            settings.Contrast = this.Contrast;
            settings.ExclusionRectangles = this.ExclusionRectangles;
            settings.FilterNoise = this.FilterNoise;
            settings.FrameReductionFactor = this.FrameReductionFactor;
            settings.FrameSkip = this.FrameSkip;
            settings.Sensitivity = this.Sensitivity;
            settings.SensitivityHigh = this.SensitivityHigh;

            return settings;
        }





        


        // todo: stop item on deleting
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ListViewItem> selectedItems = new List<ListViewItem>();
            foreach (ListViewItem item in listViewFiles.SelectedItems)
            {
                selectedItems.Add(item);
            }

            foreach (ListViewItem item in selectedItems)
            {
                listViewFiles.Items.Remove(item);

                for (int i = 0; i < NrProcessors; i++)
                {
                    FileProcessItem processItem = _processorItems[i];
                    if (processItem == item)
                    {
                        try
                        {
                            _processorItems[i] = null;
                            try
                            {
                                _processes[i].Kill();
                            }
                            catch { }
                            _processes[i] = null;
                            
                        }
                        catch { }
                    }
                }
            }

        }



        private void contextMenuStripFileItem_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = listViewFiles.SelectedItems.Count == 0;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialogMov.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in openFileDialogMov.FileNames)
                {
                    FileProcessItem fileItem = new FileProcessItem(filename);
                    listViewFiles.Items.Add(fileItem);
                }
            }
        }

        private void buttonClearList_Click(object sender, EventArgs e)
        {
            Stop();

            listViewFiles.Items.Clear();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            numericUpDownParalel.Enabled = false;
            _runningProcesses = 0;
            timerUpdateProcess.Start();

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Stop();
        }


        public void Stop()
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            numericUpDownParalel.Enabled = true;
            timerUpdateProcess.Stop();

            for (int i = 0; i < NrProcessors; i++)
            {
                _processorItems[i] = null;
                try
                {
                    _processes[i].Kill();
                }
                catch { }
                _processes[i] = null;

            }

            foreach (ListViewItem item in listViewFiles.Items)
            {
                FileProcessItem fileItem = item as FileProcessItem;
                if (fileItem != null && fileItem.State == FileProcessItem.FileProcessState.Processing)
                {
                    fileItem.SetState(FileProcessItem.FileProcessState.Stopped, 0);
                }

            }
        }



        private void timerUpdateProcess_Tick(object sender, EventArgs e)
        {
            int paralelProcesses = (int)numericUpDownParalel.Value;
            if (_runningProcesses < paralelProcesses)
            {

                foreach (ListViewItem item in listViewFiles.Items)
                {
                    FileProcessItem fileItem = item as FileProcessItem;

                    if (fileItem != null && fileItem.State == FileProcessItem.FileProcessState.Stopped)
                    {

                        for (int i = 0; i < paralelProcesses; i++)
                        {
                            if (_processorItems[i] == null)
                            {
                                try
                                {
                                    _processorItems[i] = fileItem;
                                    //fileItem.State == FileProcessItem.FileProcessState.Processing;
                                    fileItem.SetState(FileProcessItem.FileProcessState.Processing, "0 %");


                                    System.Threading.ParameterizedThreadStart ts = new System.Threading.ParameterizedThreadStart(ThreadHandler);
                                    System.Threading.Thread thread = new System.Threading.Thread(ts);
                                    _processorThreads[i] = thread;
                                    thread.Start(i);
                                    _runningProcesses++;
                                }
                                catch
                                {
                                    _processorItems[i] = null;
                                    //fileItem.State == FileProcessItem.FileProcessState.Stopped;
                                    fileItem.SetState(FileProcessItem.FileProcessState.Stopped, 0);
                                }
                                break;
                            }


                        }

                        break;
                    }

                }

            }


        }


        private void ThreadHandler(object arg)
        {
            int index = (int)arg;
            try
            {
                
                _processorItems[index].SetState(FileProcessItem.FileProcessState.Processing, "0 %");

                Process process = new Process();
                process.StartInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.Arguments = _processorItems[index].Filename;
                _processes[index] = process;
                _processes[index].Start();


                while (!_processes[index].HasExited)
                {
                    System.Threading.Thread.Sleep(50);
                    Application.DoEvents();
                }

                //Utils.WriteLog("HasExited: " + process.Id + "  Time: " + DateTime.Now.ToLongTimeString());


                if (_processes[index].ExitCode == -100)
                {
                    throw new Exception();
                }
                else if (_processes[index].ExitCode < 0)
                {

                }
                else
                {
                    _processes[index] = null;
                    _processorItems[index].SetState(FileProcessItem.FileProcessState.Completed, 100);
                    _processorItems[index] = null;
                }



            }
            catch (Exception ex)
            {
                if(_processorItems[index] != null) _processorItems[index].SetState(FileProcessItem.FileProcessState.Error, 0);
                //Utils.WriteLog("Error start: " + ex.ToString());
                _processes[index] = null;
                //_processorItems[timerIndex].SetState(FileProcessItem.FileProcessState.Error, 0);
                _processorItems[index] = null;

            }
            _runningProcesses--;
        }






        private int GetIndexByProcessId(int processId)
        {
            int processIndex = -1;
            for (int i = 0; i < NrProcessors; i++)
            {
                if (_processes[i] != null && _processes[i].Id == processId)
                {
                    processIndex = i;

                    break;
                }
            }
            return processIndex;
        }


        public void SetProgress(int processId, double progress, double elapsedSeconds)
        {
            int processIndex = GetIndexByProcessId(processId);

            if (processIndex >= 0)
            {
                double totalSeconds = (100.0 * elapsedSeconds) / progress;
                string timeStr = "Elapsed: " + Utils.FormatTime(elapsedSeconds, false, true) + "  /  Remaining: " + Utils.FormatTime(totalSeconds - elapsedSeconds, false, true);

                string progressStr = "" + (int)progress + "% - " + timeStr;
                try
                {
                    _processorItems[processIndex].SetState(FileProcessItem.FileProcessState.Processing, progressStr);
                }
                catch { }

            }
        }


        public void ProcessCompleted(int processId)
        {
            int processIndex = GetIndexByProcessId(processId);

            if (processIndex >= 0)
            {
                try
                {
                    _processorItems[processIndex].SetState(FileProcessItem.FileProcessState.Completed, 100);
                    _processorItems[processIndex] = null;
                    try
                    {
                        //_processes[processIndex].Kill();
                    }
                    catch { }
                    _processes[processIndex] = null;

                }
                catch { }

            }
        }

        public void ProcessError(int processId, string message)
        {
            int processIndex = GetIndexByProcessId(processId);

            if (processIndex >= 0)
            {
                try
                {

                    _processorItems[processIndex].SetState(FileProcessItem.FileProcessState.Error, 0);
                    try
                    {
                        _processorItems[processIndex] = null;
                        try
                        {
                            //_processes[processIndex].Kill();
                        }
                        catch { }
                        _processes[processIndex] = null;

                    }
                    catch { }

                }
                catch { }

            }
        }





        public void Start()
        {
            buttonStart_Click(null, null);

        }

        private void BatchProcessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }



    }






    public class FileProcessItem : ListViewItem
    {

        public string Filename = "";
        private ListViewSubItem _statusSubItem = null;
        public enum FileProcessState { Stopped, Processing, Completed, Error };
        public FileProcessState State = FileProcessState.Stopped;

        public FileProcessItem(string filename)
        {
            this.Filename = filename;

            this.Text = filename;

            
            _statusSubItem = new ListViewSubItem(this, "");
            this.SubItems.Add(_statusSubItem);
            SetState(FileProcessState.Stopped, 0);

        }


        public void SetState(FileProcessState state, object arg)
        {
            this.State = state;

            if (state == FileProcessState.Stopped)
            {
                _statusSubItem.Text = "Stopped";
            }
            else if (state == FileProcessState.Completed)
            {
                _statusSubItem.Text = "Completed";
            }
            else if (state == FileProcessState.Processing)
            {
                _statusSubItem.Text = "Processing: " + arg;
            }
            else if (state == FileProcessState.Error)
            {
                _statusSubItem.Text = "Error";
            }

            if (this.ListView != null)
            {
                //this.ListView.Refresh();
            }

            
        }





    }






}