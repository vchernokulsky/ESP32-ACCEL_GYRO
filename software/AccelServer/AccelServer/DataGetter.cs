using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccelServer
{
	public class DataGetter
    {
        private int id;

        public DataGetter(int id)
        {
            this.id = id;
        }

		public IList<string> Labels
		{
			get
			{
				if (!ChartDataSingleton.Instance._dataLists.ContainsKey(id))
					return new List<string>();
				return ChartDataSingleton.Instance._dataLists[id].TimeStamps;
			}
		}

		public IList<float> AccX
		{
			get
			{
				if (!ChartDataSingleton.Instance._dataLists.ContainsKey(id))
					return new List<float>();
				return ChartDataSingleton.Instance._dataLists[id].AxisXAccelerations;
			}
		}

		public IList<float> AccY
		{
			get
			{
				if (!ChartDataSingleton.Instance._dataLists.ContainsKey(id))
					return new List<float>();
				return ChartDataSingleton.Instance._dataLists[id].AxisYAccelerations;
			}
		}

		public IList<float> AccZ
		{
			get
			{
				if (!ChartDataSingleton.Instance._dataLists.ContainsKey(id))
					return new List<float>();
				return ChartDataSingleton.Instance._dataLists[id].AxisZAccelerations;
			}
		}
	}
}
