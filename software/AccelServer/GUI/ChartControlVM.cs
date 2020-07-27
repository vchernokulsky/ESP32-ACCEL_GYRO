using AccelServer;
using LiveCharts;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class ChartControlVM: BindableBase
    {
        private AccelServer.AccelServer accelServer;
        private IList<string> properties = new List<string>();

        private ChartVM chartVM;

        private int _from = 25;
        private int _to = 225;
        private int _step = 150;

        private bool _nextEnabled = false;
        private bool _prevEnabled = false;

        private SpinnerVM resamplingSpinner;
       

        public DelegateCommand OnPrevClick { get; }
        public DelegateCommand OnNextClick { get; }

        public ChartControlVM(AccelServer.AccelServer accelServer) 
        {
            this.accelServer = accelServer;
            accelServer.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            accelServer.SetPropetyRaise();
            chartVM = new ChartVM(accelServer);

            properties.Add("SeriesCollection");
            properties.Add("Labels");
            resamplingSpinner = new SpinnerVM(1, 5, properties);
            resamplingSpinner.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };

            OnPrevClick = new DelegateCommand(() => { this.From -= _step; this.To -= _step; RaisePropertyChanged("SeriesCollection"); RaisePropertyChanged("Labels"); });
            OnNextClick = new DelegateCommand(() => { this.From += _step; this.To += _step; RaisePropertyChanged("SeriesCollection"); RaisePropertyChanged("Labels"); });

        }

        public IList<string> Labels { get { return chartVM.Labels; } }
        public SeriesCollection SeriesCollection
        {
            get
            {
                var length = _to - _from;
                var resample = ResamplingSpinner.Count;
                var maxIdx = _from + length * resample;
                IsNextEnabled = DataReceiver.running | (accelServer.Labels.Count - _from - _step - length * resample > 0);
                IsPrevEnabled = _from - _step > 0;
                return chartVM.GetSeriesCollection(_from, _to, resample, maxIdx);
            }
            private set { RaisePropertyChanged("SeriesCollection"); RaisePropertyChanged("Chart1"); }
        }

        public bool IsNextEnabled
        {
            get { return _nextEnabled; }
            set
            {
                _nextEnabled = value;
                RaisePropertyChanged("IsNextEnabled");
            }
        }
        public bool IsPrevEnabled
        {
            get { return _prevEnabled; }
            set
            {
                _prevEnabled = value;
                RaisePropertyChanged("IsPrevEnabled");
            }
        }

        public int From { get { return _from; } private set { _from = value; RaisePropertyChanged("From"); } }
        public int To { get { return _to; } private set { _to = value; RaisePropertyChanged("To"); RaisePropertyChanged("SeriesCollection"); RaisePropertyChanged("Labels"); } }
        public SpinnerVM ResamplingSpinner { get => resamplingSpinner; set { resamplingSpinner = value; RaisePropertyChanged("ResamplingSpinner"); RaisePropertyChanged("SeriesCollection"); RaisePropertyChanged("Labels"); RaisePropertyChanged("Chart1"); } }


    }
}
