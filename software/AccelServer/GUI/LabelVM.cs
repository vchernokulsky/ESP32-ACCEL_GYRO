using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace GUI
{
    class LabelVM: BindableBase
    {
        private IList<string> labels { get; set; }
     
        public IList<string> GetLabels()
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

        public void Update(IList<string> fromList, int from, int to, int resample, int maxIdx)
        {
            if (maxIdx <= fromList.Count)
            {
                int i = 0;
                labels = fromList.Skip(from).Take((to - from) * resample).Where(x => i++ % resample == 0).Select(x => x).ToList();

            }
        
        }

    }
}
