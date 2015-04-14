using MotionDetector.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace MotionDetector
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private Dictionary<Keys, String> _keynames = new Dictionary<Keys, String>();

        public String LogsPath
        {
            get { return Settings.Default.LogsPath; }
            set { 
                Settings.Default.LogsPath = value;
                OnPropertyChanged("LogsPath");
            }
        }

        public String ScreenshootsPath
        {
            get { return Settings.Default.ScreenshootsPath; }
            set
            {
                Settings.Default.ScreenshootsPath = value;
                OnPropertyChanged("ScreenshootsPath");
            }
        }

        public Boolean LogsPathEnabled
        {
            get {
                if (Vp == null)
                    return true;

                return (Vp.State <= 0) && (Bvm.State <=0) ;
            }
        }


        private VideoProcessor _vp;
        public VideoProcessor Vp
        {
            set { _vp = value; }
            get { return _vp; }
        }

        private BatchViewModel _bvm;
        public BatchViewModel Bvm
        {
            set { _bvm = value; }
            get { return _bvm; }
        }


        private VideoPlayer _vpl;
        public VideoPlayer Vpl
        {
            set { _vpl = value; }
            get { return _vpl; }

        }

        public SettingsViewModel()
        {
            PopulateKeyNames();
        }
        
        private void PopulateKeyNames()
        {
           
        }

        public void OnSelectLogsPath(FolderBrowserDialog fbd)
        {
            LogsPath = fbd.SelectedPath;
        }

        public void OnSelectScreenshootsPath(FolderBrowserDialog fbd)
        {
            ScreenshootsPath = fbd.SelectedPath;
        }

     
        public void OnOpen()
        {
            Settings.Default.Reload();
        }

        public void OnExit()
        {
            Settings.Default.Save();
            if (Vpl != null)
                Vpl.UpdateScreenshootsPath();
        }
    }
}
