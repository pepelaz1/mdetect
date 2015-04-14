using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace MotionDetector
{
    public class BatchViewModel : INotifyPropertyChanged
    {
        private Timer _timer = new Timer();

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }      

        private BindingList<BatchModel> _batch_items = new BindingList<BatchModel>();
        public BindingList<BatchModel> BatchItems
        {
            get { return _batch_items; }
        }

        private Boolean _grid_enabled;
        public Boolean GridEnabled
        {
            get { return _grid_enabled;  }
            set { 
                _grid_enabled = value;
                OnPropertyChanged("GridEnabled");
            }
        }

        private Boolean _openbtn_enabled;
        public Boolean OpenBtnEnabled
        {
            get { return _openbtn_enabled; }
            set
            {
                _openbtn_enabled = value;
                OnPropertyChanged("OpenBtnEnabled");
            }
        }

        private Boolean _clearlstbtn_enabled;
        public Boolean ClearLstBtnEnabled
        {
            get { return _clearlstbtn_enabled; }
            set
            {
                _clearlstbtn_enabled = value;
                OnPropertyChanged("ClearLstBtnEnabled");
            }
        }

        private Boolean _startbtn_enabled;
        public Boolean StartBtnEnabled
        {
            get { return _startbtn_enabled; }
            set
            {
                _startbtn_enabled = value;
                OnPropertyChanged("StartBtnEnabled");
            }

        }

        private Boolean _stopbtn_enabled;
        public Boolean StopBtnEnabled
        {
            get { return _stopbtn_enabled; }
            set
            {
                _stopbtn_enabled = value;
                OnPropertyChanged("StopBtnEnabled");
            }
        }

        private Boolean _udparallel_enabled;
        public Boolean UdParallelEnabled
        {
            get { return _udparallel_enabled; }
            set
            {
                _udparallel_enabled = value;
                OnPropertyChanged("UdParallelEnabled");
            }
        }

        private int _parallel_count;
        public int ParallelCount
        {
            get { return _parallel_count; }
            set
            {
                _parallel_count = value;
                OnPropertyChanged("ParallelCount");
            }
        }

        public int ProcessingCount
        {
            get { 
                int ret = 0;
                foreach (BatchModel bm in BatchItems)
                    ret += (bm.State == BatchState.Processing) ? 1 : 0;
                return ret;
            }
        }

        public int State
        {
            get  {
                int cnt = 0;
                foreach (BatchModel bm in BatchItems)
                    cnt += (bm.State == BatchState.Processing) ? 1 : 0;
                return cnt == 0 ? 0 : 1;
            }
        }

        public BatchViewModel()
        {
            ParallelCount = 1;
            _timer.Interval = 100;
            _timer.Tick += new EventHandler(Tick);
            EnableProcessingProps(true);
        }

        public void Clear()
        {
            _batch_items.Clear();
        }

        public void AddItem(String filename)
        {
            _batch_items.Add(new BatchModel(filename));
        }

        public void Start()
        {
            foreach (BatchModel bm in BatchItems)
                bm.Reset();            
                            
            _timer.Start();
            EnableProcessingProps(false);
        }

        public void Complete()
        {
            _timer.Stop();
            EnableProcessingProps(true);            
        }

        public void StopOnClose()
        {
            foreach (BatchModel bm in BatchItems)
                bm.Stop();
        }

        public void Stop()
        {
            foreach (BatchModel bm in BatchItems)
                bm.Stop();
            _timer.Stop();
            EnableProcessingProps(true);     
        }

        private void EnableProcessingProps(bool enable)
        {
            GridEnabled = enable;
            OpenBtnEnabled = enable;
            ClearLstBtnEnabled = enable;
            StartBtnEnabled = enable;
            StopBtnEnabled = !enable;
            UdParallelEnabled = enable;
        }

        private void Tick(object sender, EventArgs e)
        {
            if (ProcessingCount < ParallelCount)
            {
                foreach (BatchModel bm in BatchItems)
                {
                    if (bm.State == BatchState.Created)
                    {
                        bm.Start();
                        break;
                    }
                }
            }
            int cnt = 0;
            foreach ( BatchModel bm in BatchItems)
            {
                if (bm.State == BatchState.Processing)
                    bm.Update();
                cnt += bm.State == BatchState.Completed ? 1 : 0;
            }
     
            if (cnt == BatchItems.Count)
                Complete();
        }
    }
}
