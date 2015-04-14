using MotionDetector.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MotionDetector
{
    public partial class SettingsForm : Form
    {
        private SettingsViewModel _vm = new SettingsViewModel();


        public SettingsForm(VideoProcessor _videoProcessor, BatchViewModel bvm, VideoPlayer vpl)
        {
            InitializeComponent();
            tbLogsPath.DataBindings.Add(new Binding("Text", _vm, "LogsPath"));
            tbLogsPath.DataBindings.Add(new Binding("Enabled", _vm, "LogsPathEnabled"));
            btnBrowseLogPath.DataBindings.Add(new Binding("Enabled", _vm, "LogsPathEnabled"));
            tbScreenshootsPath.DataBindings.Add(new Binding("Text", _vm, "ScreenshootsPath"));
            _vm.Vp = _videoProcessor;
            _vm.Bvm = bvm;
            _vm.Vpl = vpl;
            _vm.OnOpen();
            
          }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _vm.OnExit();
            Close();
        }

        private void btnBrowseLogPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
                _vm.OnSelectLogsPath(fbd);           
        }

        private void btnBrowseScreenshotsPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
                _vm.OnSelectScreenshootsPath(fbd);     
        }
    }
}
