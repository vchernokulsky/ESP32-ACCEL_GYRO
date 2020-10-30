using System.Collections.Generic;

namespace ImuServer
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

		public IList<float> GyX
		{
			get
			{
				if (!ChartDataSingleton.Instance._dataLists.ContainsKey(id))
					return new List<float>();
				return ChartDataSingleton.Instance._dataLists[id].AxisXAngles;
			}
		}

		public IList<float> GyY
		{
			get
			{
				if (!ChartDataSingleton.Instance._dataLists.ContainsKey(id))
					return new List<float>();
				return ChartDataSingleton.Instance._dataLists[id].AxisYAngles;
			}
		}

		public IList<float> GyZ
		{
			get
			{
				if (!ChartDataSingleton.Instance._dataLists.ContainsKey(id))
					return new List<float>();
				return ChartDataSingleton.Instance._dataLists[id].AxisZAngles;
			}
		}
	}
}
