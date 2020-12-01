using System;
using System.Threading;

namespace GUI
{
    public class DeviceInfo
	{
		public int Id { get; set; }
		public int Type { get; set; }
		public string Ip { get; set; }
		public int SyncTicks { get; set; }
		public int BatteryCharge { get; set; }

		public Coordinates AccelOffset { get; set; }
		public Coordinates GyroOffset { get; set; }

		public DateTime SyncTime { get; set; }
		public int Port { get; set; }
		public int CommandPort { get; set; }

		public DataReceiver dt_recv { get; set; }
		public Thread data_receiver { get; set; }
	}
}
