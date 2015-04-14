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
        private  BatchViewModel _vm = new BatchViewModel();
        public BatchViewModel ViewModel
        {
            get { return _vm; }
        }

        public BatchProcessForm()
        {
            InitializeComponent();

            gvBatch.AutoGenerateColumns = false;
            gvBatch.DataSource = _vm.BatchItems;
            gvBatch.DataBindings.Add(new Binding("Enabled", _vm, "GridEnabled"));
            btnOpen.DataBindings.Add(new Binding("Enabled", _vm, "OpenBtnEnabled"));
            btnClearList.DataBindings.Add(new Binding("Enabled", _vm, "ClearLstBtnEnabled"));
            btnStart.DataBindings.Add(new Binding("Enabled", _vm, "StartBtnEnabled"));
            btnStop.DataBindings.Add(new Binding("Enabled", _vm, "StopBtnEnabled"));
            udParallel.DataBindings.Add(new Binding("Enabled", _vm, "UdParallelEnabled"));
            udParallel.DataBindings.Add(new Binding("Value", _vm, "ParallelCount"));
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialogMov.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in openFileDialogMov.FileNames)
                    _vm.AddItem(filename);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        public void Start()
        {
             _vm.Start();
        }

        public void Stop()
        {
            _vm.Stop();
        }

        public void StopOnClose()
        {
            _vm.StopOnClose();
        }

        private void BatchProcessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void buttonClearList_Click(object sender, EventArgs e)
        {
            _vm.Clear();
        }
    
    }
}