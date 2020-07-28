using LiveCharts;
using LiveCharts.Wpf;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GUI
{
    class ChartVM: BindableBase
    {
        private AccelServer.DataGetter chartData;

        private ChartLineVM accXSeria = new ChartLineVM("Ось X");
        private ChartLineVM accYSeria = new ChartLineVM("Ось Y");
        private ChartLineVM accZSeria = new ChartLineVM("Ось Z");
        private LabelVM labels = new LabelVM();

        private SeriesCollection _collection { get; set; }

        public LineSeries AccXSeria => accXSeria.GetSeria();
        public LineSeries AccYSeria => accYSeria.GetSeria();
        public LineSeries AccZSeria => accZSeria.GetSeria();
        public IList<string> Labels => labels.GetLabels();


        public ChartVM(AccelServer.DataGetter chartData)
        {
            this.chartData = chartData;
        }

        public SeriesCollection GetSeriesCollection(int from, int to, int resample, int maxIdx)
        {     
            if (_collection == null)
            {
                _collection = new SeriesCollection() {
                    AccXSeria, AccYSeria, AccZSeria
                };
            }
            UpdateRanges(from, to, resample, maxIdx);
            return _collection;
        }

        private void UpdateRanges(int from, int to, int resample, int maxIdx)
        {
            accXSeria.Update(chartData.AccX, from, to, resample, maxIdx);
            accYSeria.Update(chartData.AccY, from, to, resample, maxIdx);
            accZSeria.Update(chartData.AccZ, from, to, resample, maxIdx);
            labels.Update(chartData.Labels, from, to, resample, maxIdx);

        }
    }

   
}
