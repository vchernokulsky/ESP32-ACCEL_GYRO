using ImuServer;
using LiveCharts;
using Prism.Mvvm;
using System.Collections.Generic;

namespace GUI
{
    public class ChartControlVM: BindableBase
    {
        private int deviceId;
        private DataGetter chartData;
        private IList<string> properties = new List<string>();

        private ChartVM chartVM;
        private SpinnerVM resamplingSpinner;
        private SlideButtonsVM slideButtons;

        private string selfName;

       

        public ChartControlVM(int deviceId, AppType appType) 
        {
            this.deviceId = deviceId;
            chartData = new DataGetter(deviceId);
        
            selfName = "Chart" + deviceId.ToString();
            chartVM = new ChartVM(appType, chartData);

            properties.Add("SeriesCollection");
            properties.Add("Labels");
            resamplingSpinner = new SpinnerVM(1, 5, properties);
            resamplingSpinner.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };

            slideButtons = new SlideButtonsVM(0, 300, properties);
            slideButtons.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };

        }

        public string DeviceName => "Устройство №" + deviceId.ToString();
        public IList<string> Labels { get { return chartVM.Labels; } }
        public SeriesCollection SeriesCollection
        {
            get
            {              
                var resample = ResamplingSpinner.Count;
                var maxIdx = SlideButtons.From + SlideButtons.Length * resample;
                SlideButtons.IsNextEnabled = DataReceiver.running | (chartData.Labels.Count - maxIdx - SlideButtons.Step > 0);
                SlideButtons.IsPrevEnabled = SlideButtons.From - SlideButtons.Step > 0;
                return chartVM.GetSeriesCollection(SlideButtons.From, SlideButtons.To, resample, maxIdx);
            }
            private set { RaisePropertyChanged("SeriesCollection"); RaisePropertyChanged(selfName); }
        }

        public SpinnerVM ResamplingSpinner { get => resamplingSpinner; set { resamplingSpinner = value; RaisePropertyChanged("ResamplingSpinner"); RaisePropertyChanged("SeriesCollection"); RaisePropertyChanged("Labels"); RaisePropertyChanged(selfName); } }
        public SlideButtonsVM SlideButtons { get => slideButtons; set { slideButtons = value; RaisePropertyChanged("SlideButtons"); RaisePropertyChanged("SeriesCollection"); RaisePropertyChanged("Labels"); RaisePropertyChanged(selfName); } }


    }
}
