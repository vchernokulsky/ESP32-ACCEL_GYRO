using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using AccelServer;
using LiveCharts;
using LiveCharts.Wpf;
using Prism.Commands;
using Prism.Mvvm;

namespace GUI
{
    enum Resampling
    {
        X1=1,
        X2=2,
        X3=3,
        X4=4,
        X5=5
    }


    class MainWindowViewModel : BindableBase
    {
        private AccelServer.AccelServer accelServer = new AccelServer.AccelServer(15000);

        private DeviseStatus Device1;
        private DeviseStatus Device2;
        private DeviseStatus Device3;
        private DeviseStatus Device4;
        private DeviseStatus Device5;
        private DeviseStatus Device6;

        private int _from = 25;
        private int _to = 225;
        private int _step = 150;



        public MainWindowViewModel()
        {
            accelServer.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            accelServer.SetPropetyRaise();


            OnContentRendered = new DelegateCommand(() => accelServer.RunServer());

            OnStartClicked = new DelegateCommand(() => accelServer.StartReceiving());
            OnStopClicked = new DelegateCommand(() => accelServer.StopReceiving());
            OnClosing = new DelegateCommand<object>(obj => accelServer.StopServer());

            OnPrevClick = new DelegateCommand(() => { this.From -= _step; this.To -= _step; RaisePropertyChanged("SeriesCollection"); RaisePropertyChanged("Labels"); });
            OnNextClick = new DelegateCommand(() => { this.From += _step; this.To += _step; RaisePropertyChanged("SeriesCollection"); RaisePropertyChanged("Labels"); });
            

            Device1 = new DeviseStatus(1, accelServer) { Title = "Устройство №1" };
            Device2 = new DeviseStatus(2, accelServer) { Title = "Устройство №2" };
            Device3 = new DeviseStatus(3, accelServer) { Title = "Устройство №3" };
            Device4 = new DeviseStatus(4, accelServer) { Title = "Устройство №4" };
            Device5 = new DeviseStatus(5, accelServer) { Title = "Устройство №5" };
            Device6 = new DeviseStatus(6, accelServer) { Title = "Устройство №6" };

            YFormatter = value => value.ToString("0.00");



        }

        private ChartValues<float> accXCV { get; set; }
        private ChartValues<float> accYCV { get; set; }
        private ChartValues<float> accZCV { get; set; }

        private IList<string> labels { get; set; }
        private LineSeries accXSeria { get; set; }
        private LineSeries accYSeria { get; set; }
        private LineSeries accZSeria { get; set; }

        private SeriesCollection _collection { get; set; }

        private IList<string> GetLabels()
        {
            if (labels == null)
            {
                if (labels == null)
                {
                    labels = new List<string>();
                }
               
            }
            return labels;
        }

        private LineSeries GetAccX()
        {
            if (accXSeria == null) { 
                if(accXCV == null)
                {
                    accXCV = new ChartValues<float>();
                }
                accXSeria = new LineSeries
                {
                    Title = "AccX",
                    Values = accXCV
                };
            }

            return accXSeria;
        }

        private LineSeries GetAccY()
        {
            if (accYSeria == null)
            {
                if (accYCV == null)
                {
                    accYCV = new ChartValues<float>();
                }
                accYSeria = new LineSeries
                {
                    Title = "AccY",
                    Values = accYCV
                };
            }

            return accYSeria;
        }

        private LineSeries GetAccZ()
        {
            if (accZSeria == null)
            {
                if (accZCV == null)
                {
                    accZCV = new ChartValues<float>();
                }
                accZSeria = new LineSeries
                {
                    Title = "AccZ",
                    Values = accZCV
                };
            }

            return accZSeria;
        }

        private SeriesCollection GetSeriesCollection()
        {
            if(_collection == null)
            {
                _collection = new SeriesCollection() { 
                    AccXSeria, AccYSeria, AccZSeria
                };
            }
            UpdateRanges();
            return _collection;
        }

        private void UpdateRanges()
        {
            var length = _to - _from;
            var resample = (int)this.Resampling;
            var maxIdx = _from + length * resample;


            if (maxIdx <= accelServer.Labels.Count)
            {
                int i = 0;
                labels = accelServer.Labels.Skip(_from).Take(length * resample).Where(x => i++ % resample == 0).Select(x => x).ToList();
            }

            if (maxIdx <= accelServer.AccX.Count)
            {
                int i = 0;
                var e = accelServer.AccX.Skip(_from).Take(length*resample).Where(x => i++ % resample == 0).Select(x => x);
                accXCV.Clear();
                accXCV.AddRange(e);
            }

            if (maxIdx <= accelServer.AccY.Count)
            {
                int i = 0;
                var e = accelServer.AccY.Skip(_from).Take(length * resample).Where(x => i++ % resample == 0).Select(x => x);
                accYCV.Clear();
                accYCV.AddRange(e);
            }

            if (maxIdx <= accelServer.AccZ.Count)
            {
                int i = 0;
                var e = accelServer.AccZ.Skip(_from).Take(length * resample).Where(x => i++ % resample == 0).Select(x => x);
                accZCV.Clear();
                accZCV.AddRange(e);
            
            }

            IsNextEnabled = DataReceiver.running | (accelServer.Labels.Count - _from - _step * resample - length * resample > 0);
            IsPrevEnabled = _from - _step * resample > 0;
        }

        public Func<float, string> YFormatter { get; set; }


        public IList<string> Labels => GetLabels();
        

        public DelegateCommand OnPrevClick { get; }
        public DelegateCommand OnNextClick { get; }

        public LineSeries AccXSeria => GetAccX();
        public LineSeries AccYSeria => GetAccY();
        public LineSeries AccZSeria => GetAccZ();
        public SeriesCollection SeriesCollection => GetSeriesCollection();
  
        public DelegateCommand OnContentRendered { get; }
        public DelegateCommand OnStartClicked { get; }
        public DelegateCommand OnStopClicked { get; }
        public DelegateCommand<object> OnClosing { get; }

        public bool NoConnection => accelServer.NoConnection;
        public bool StopEnabled => DataReceiver.running;
        public bool StartEnabled => (!(NoConnection || DataReceiver.running));

        private bool _nextEnabled = false;
        public bool IsNextEnabled
        {
            get { return _nextEnabled; }
            set 
            { 
                _nextEnabled = value;
                RaisePropertyChanged("IsNextEnabled");
            }
        }

        private bool _prevEnabled = false;
        public bool IsPrevEnabled
        {
            get { return _prevEnabled; }
            set 
            { 
                _prevEnabled = value;
                RaisePropertyChanged("IsPrevEnabled");
            }
        }

        public Brush DeviceColor1 => Device1.StatusColor;
        public Brush DeviceColor2 => Device2.StatusColor;
        public Brush DeviceColor3 => Device3.StatusColor;
        public Brush DeviceColor4 => Device4.StatusColor;
        public Brush DeviceColor5 => Device5.StatusColor;
        public Brush DeviceColor6 => Device6.StatusColor;

        public int From { get { return _from; } private set { _from = value; RaisePropertyChanged("From"); } }
        public int To { get { return _to; } private set { _to = value; RaisePropertyChanged("To"); RaisePropertyChanged("SeriesCollection"); RaisePropertyChanged("Labels"); } }

        private Resampling resampling = Resampling.X1;
        public Resampling Resampling
        {
            get { return resampling; }
            set
            {
                if (resampling == value)
                    return;

                resampling = value;
                RaisePropertyChanged("Resampling");
                RaisePropertyChanged("IsResamplingX1");
                RaisePropertyChanged("IsResamplingX2");
                RaisePropertyChanged("IsResamplingX3");
                RaisePropertyChanged("SeriesCollection");
                RaisePropertyChanged("Labels");

            }
        }
        public bool IsResamplingX1
        {
            get { return Resampling == Resampling.X1; }
            set { Resampling = value ? Resampling.X1 : Resampling; }
        }

        public bool IsResamplingX2
        {
            get { return Resampling == Resampling.X2; }
            set { Resampling = value ? Resampling.X2 : Resampling; }
        }

        public bool IsResamplingX3
        {
            get { return Resampling == Resampling.X3; }
            set { Resampling = value ? Resampling.X3 : Resampling; }
        }

        public bool IsResamplingX4
        {
            get { return Resampling == Resampling.X4; }
            set { Resampling = value ? Resampling.X4 : Resampling; }
        }

        public bool IsResamplingX5
        {
            get { return Resampling == Resampling.X5; }
            set { Resampling = value ? Resampling.X5 : Resampling; }
        }
    }
}
