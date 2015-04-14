using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MotionDetector
{
    public enum BatchState { Created, Processing, Completed, Incomplete, Error };

    public class BatchModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }   

        private VideoProcessor _vp;

        private String _file;
        public String File
        {
            get { return _file;  }
            set { _file = value; }
        }
        
        private byte _threshold;
        public byte Threshold
        {
            get { return _threshold; }
            set { _threshold = value; }
        }

        private byte _brightness;
        public byte Brightness
        {
            get { return _brightness; }
            set { _brightness = value; }
        }

        private byte _contrast;
        public byte Contrast
        {
            get { return _contrast; }
            set { _contrast = value; }
        }

        private BatchState _state;
        public BatchState State
        {
            get { return _state; }
            set { 
                _state = value;
                OnPropertyChanged("State");
                OnPropertyChanged("BatchStateStr");
            }
        }

        private DateTime _start_time;

        public String _elapsed_time;
        public String ElapsedTimeStr
        {
            get { return _elapsed_time; }
            set
            {
                _elapsed_time = value;
                OnPropertyChanged("ElapsedTimeStr");
            }
        }

        public String _remaining_time;
        public String RemainingTimeStr
        {
            get { return _remaining_time; }
            set
            {
                _remaining_time = value;
                OnPropertyChanged("RemainingTimeStr");
            }
        }

        public String BatchStateStr
        {
            get {
                switch (State)
                {
                    case BatchState.Created:
                        return "Created";
                    case BatchState.Processing:
                        return "Processing";
                    case BatchState.Completed:
                        return "Completed";
                    case BatchState.Incomplete:
                        return "Incomplete";
                    case BatchState.Error:
                        return "Error";
                }
                return "Undefined";
            }
        }

        private String _percent_complete_str;
        public String PercentCompleteStr
        {
            get { return _percent_complete_str; }
            set {
                if (_percent_complete_str != value)
                {
                    _percent_complete_str = value;
                    OnPropertyChanged("PercentCompleteStr");
                }
            }
        }

        public BatchModel()
        {
            _file = "";
            _threshold = 15;
            _contrast = 128;
            _brightness = 128;
            Reset();
        }

        public BatchModel(String file, byte threshold, byte brightness, byte contrast)
        {
            _file = file;
            _threshold = threshold;
            _brightness = brightness;
            _contrast = contrast;
            Reset();
         }

        public BatchModel(String file)
        {
            _file = file;
            _threshold = 15;
            _contrast = 128;
            _brightness = 128;
            Reset();
        }

        public void Reset()
        {
            State = BatchState.Created;
            _percent_complete_str = "0,00%";
            _start_time = DateTime.Now;
            _elapsed_time = "00:00:00";
            _remaining_time = "00:00:00";
        }

        public void Start()
        {
            _vp = new VideoProcessor();
            _vp.OpenFile(File);
            _vp.Threshold = Threshold;
            _vp.Brightness = Brightness;
            _vp.Contrast = Contrast;
            _vp.Start();
            State = BatchState.Processing;
            _start_time = DateTime.Now;
        }

        public void Stop()
        {
            if (_vp != null)
                _vp.Stop();

            if (State == BatchState.Processing)
            {
                Int64 position = _vp.Position;
                Int64 duration = _vp.Duration;
                if (position >= duration - 10000)
                {
                    State = BatchState.Completed;
                }
                else
                {
                    State = BatchState.Incomplete;
                }
            }
        }

        public void Update()
        {
            Int64 position = _vp.Position;
            Int64 duration = _vp.Duration;

            PercentCompleteStr = (((double)position / (double)duration) * 100).ToString("N2") + "%";
   
            TimeSpan elapsedTime = DateTime.Now - _start_time;
            TimeSpan remainingTime = elapsedTime;

            double posf = (double)position / 10000000;
            double durf = (double)duration / 10000000;
            if (posf > 0)
            {
                double remsecs = (durf * elapsedTime.TotalSeconds) / posf - elapsedTime.TotalSeconds;
                remainingTime = TimeSpan.FromSeconds(remsecs);
            }
      
            ElapsedTimeStr = Utils.FormatTime(elapsedTime.TotalSeconds, false, true);
            RemainingTimeStr = Utils.FormatTime(remainingTime.TotalSeconds, false, true);
            
            if (position >= duration - 10000)
            {
                Stop();
            }
        }
    }
}
