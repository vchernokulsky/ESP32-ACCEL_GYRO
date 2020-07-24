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
    class ChartLineVM: BindableBase
    {
        private string title;
        private ChartValues<float> chartValues { get; set; }
        private LineSeries seria { get; set; }
        public LineSeries LineSeria => GetSeria();

        public ChartLineVM(string title)
        {
            this.title = title;
        }
        public LineSeries GetSeria()
        {
            if (seria == null)
            {
                if (chartValues == null)
                {
                    chartValues = new ChartValues<float>();
                }
                seria = new LineSeries
                {
                    Title = title,
                    Values = chartValues
                };
            }

            return seria;
        }

        public void Update(IList<float> fromList, int from, int to, int resample, int maxIdx)
        {
            if (maxIdx <= fromList.Count)
            {
                int i = 0;
                var e = fromList.Skip(from).Take((to - from) * resample).Where(x => i++ % resample == 0).Select(x => x);
                chartValues.Clear();
                chartValues.AddRange(e);
            }
        }
    }
}
