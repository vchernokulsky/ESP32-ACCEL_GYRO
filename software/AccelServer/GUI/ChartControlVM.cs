﻿using AccelServer;
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
        private int deviceId;
        private DataGetter chartData;
        private IList<string> properties = new List<string>();

        private ChartVM chartVM;
        private SpinnerVM resamplingSpinner;
        private SlideButtonsVM slideButtons;

        private string selfName;

       

        public ChartControlVM(int deviceId) 
        {
            this.deviceId = deviceId;
            chartData = new DataGetter(deviceId);
            //accelServer.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            // accelServer.SetPropetyRaise();
            selfName = "Chart" + deviceId.ToString();
            chartVM = new ChartVM(chartData);

            properties.Add("SeriesCollection");
            properties.Add("Labels");
            resamplingSpinner = new SpinnerVM(1, 5, properties);
            resamplingSpinner.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };

            slideButtons = new SlideButtonsVM(0, 300, properties);
            slideButtons.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };

        }

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
