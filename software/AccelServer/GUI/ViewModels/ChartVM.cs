using LiveCharts;
using LiveCharts.Wpf;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace GUI
{
    class ChartVM: BindableBase
    {
        private AppType appType;

        private ImuServer.DataGetter chartData;

        private ChartLineVM seriaX = new ChartLineVM("Ось X");
        private ChartLineVM seriaY = new ChartLineVM("Ось Y");
        private ChartLineVM seriaZ = new ChartLineVM("Ось Z");
        private LabelVM labels = new LabelVM();

        private SeriesCollection _collection { get; set; }
        public LineSeries SeriaX => seriaX.GetSeria();
        public LineSeries SeriaY => seriaY.GetSeria();
        public LineSeries SeriaZ => seriaZ.GetSeria();
        public IList<string> Labels => labels.GetLabels();


        public ChartVM(AppType appType, ImuServer.DataGetter chartData)
        {
            this.appType = appType;
            this.chartData = chartData;
        }

        public SeriesCollection GetSeriesCollection(int from, int to, int resample, int maxIdx)
        {     
            if (_collection == null)
            {
                _collection = new SeriesCollection() {
                    SeriaX, SeriaY, SeriaZ
                };
            }
            UpdateRanges(from, to, resample, maxIdx);
            return _collection;
        }

        private void UpdateRanges(int from, int to, int resample, int maxIdx)
        {
            switch (appType)
            {
                case AppType.AccelerationMeasurement:
                    UpdateAccelerationRanges(from, to, resample, maxIdx);
                    break;
                case AppType.AnglesMeasurement:
                    UpdateAngleRanges(from, to, resample, maxIdx);
                    break;
                default:
                    Console.WriteLine("Wrong Application type {0}", appType.ToString());
                    break;
            }

        }

        private void UpdateAccelerationRanges(int from, int to, int resample, int maxIdx)
        {
            seriaX.Update(chartData.AccX, from, to, resample, maxIdx);
            seriaY.Update(chartData.AccY, from, to, resample, maxIdx);
            seriaZ.Update(chartData.AccZ, from, to, resample, maxIdx);
            labels.Update(chartData.Labels, from, to, resample, maxIdx);

        }

        private void UpdateAngleRanges(int from, int to, int resample, int maxIdx)
        {
            seriaX.Update(chartData.GyX, from, to, resample, maxIdx);
            seriaY.Update(chartData.GyY, from, to, resample, maxIdx);
            seriaZ.Update(chartData.GyZ, from, to, resample, maxIdx);
            labels.Update(chartData.Labels, from, to, resample, maxIdx);

        }
    }

   
}
