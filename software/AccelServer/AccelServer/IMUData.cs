using System;
using System.Collections.Generic;

namespace AccelServer
{
	public class IMUDataListDecorator
	{

		

	}

	public class IMUDataList
	{
		private readonly string TIME_FORMAT = "H:mm:ss fff";

		private int id;

		private float acc2si;
		private float gyro2si;

		private DateTime SyncTime;
		private int SyncTicks;

		public List<IMUData> agList;

		private readonly IList<float> _axisXAccelerations = new List<float>();
		private readonly IList<float> _axisYAccelerations = new List<float>();
		private readonly IList<float> _axisZAccelerations = new List<float>();
		private readonly IList<float> _axisXAngles = new List<float>();
		private readonly IList<float> _axisYAngles = new List<float>();
		private readonly IList<float> _axisZAngles = new List<float>();
		private readonly IList<string> _timeStamps = new List<string>();

		public IList<float> AxisXAccelerations { get => _axisXAccelerations; }
		public IList<float> AxisYAccelerations { get => _axisYAccelerations; }
		public IList<float> AxisZAccelerations { get => _axisZAccelerations; }
		public IList<float> AxisXAngles { get => _axisXAngles; }
		public IList<float> AxisYAngles { get => _axisYAngles; }
		public IList<float> AxisZAngles { get => _axisZAngles; }
		public IList<string> TimeStamps { get => _timeStamps; }

		public IMUDataList(int id, DateTime SyncTime, int SyncTicks, int GMAX=2, int DEGMAX = 250)
		{
			this.id = id;
			this.SyncTime = SyncTime;
			this.SyncTicks = SyncTicks;
			acc2si = (2.0f * (float)GMAX * 9.8f) / 65536.0f;
			gyro2si = (2.0f * (float)DEGMAX) / 65536.0f;
			agList = new List<IMUData> ();
		}

		public void PutBytes(byte[] bytes)
		{
			IMUData data = new IMUData (id, SyncTime, SyncTicks, bytes, acc2si, gyro2si);
			//data.WriteToConsole ();
			agList.Add (data);

			_axisXAccelerations.Add(data.AcX);
			_axisYAccelerations.Add(data.AcY);
			_axisZAccelerations.Add(data.AcZ);

			_axisXAngles.Add(data.GyX);
			_axisYAngles.Add(data.GyY);
			_axisZAngles.Add(data.GyZ);

			_timeStamps.Add(data.Time.ToString(TIME_FORMAT));
		}
	}
	
	public class IMUData
	{

		public DateTime Time;
		public float AcX;
		public float AcY;
		public float AcZ;
		public float Tmp;
		public float GyX;
		public float GyY;
		public float GyZ;

		public IMUData ()
		{
			
		}
		public IMUData (int id, DateTime SyncTime, int SyncTicks, byte[] bytes, float acc2si, float gyro2si)
		{
			SetFromBytes (id, SyncTime,SyncTicks, bytes, acc2si, gyro2si);
		}

		public float ParseImuBytes(byte low, byte high)
		{
			if ((~low & 0x80) != 0)
				return low << 8 | high;
			return (float)( -(((low ^ 255) << 8) | (high ^ 255) + 1));
		}

		public void SetFromBytes(int id, DateTime SyncTime, int SyncTicks, byte[] bytes, float acc2si, float gyro2si)
		{
			if (bytes.Length != 18) {
				Console.WriteLine ("Wrong package size: {0}", bytes.Length);
				return;
			}


			AcX = acc2si * (ParseImuBytes(bytes[4], bytes[5]) - MpuCalibraion.offset[id].AccX);
			AcY = acc2si * (ParseImuBytes(bytes[6], bytes[7]) - MpuCalibraion.offset[id].AccY);
			AcZ = acc2si * (ParseImuBytes(bytes[8], bytes[9]) - MpuCalibraion.offset[id].AccZ);
			Tmp = ParseImuBytes(bytes[10], bytes[11])/340.0f + 36.53f;
			GyX = gyro2si * (ParseImuBytes(bytes[12], bytes[13]) - MpuCalibraion.offset[id].GyX);
			GyY = gyro2si * (ParseImuBytes(bytes[14], bytes[15]) - MpuCalibraion.offset[id].GyY);
			GyZ = gyro2si * (ParseImuBytes(bytes[16], bytes[17]) - MpuCalibraion.offset[id].GyZ);

			if (BitConverter.IsLittleEndian) {
				int ticks = BitConverter.ToInt32 (bytes, 0);
				Time = SyncTime.AddMilliseconds (ticks - SyncTicks);
			} else {
				Array.Reverse (bytes);
				int ticks = BitConverter.ToInt32 (bytes, 14);
				Time = SyncTime.AddMilliseconds (ticks - SyncTicks);
			}
		}

		public void WriteToConsole()
		{
			Console.WriteLine ("---- {0:MM/dd/yy H:mm:ss fff} ----", Time);
			Console.WriteLine ("AccX={0}  AccY={1}  AccZ={2}", AcX, AcY, AcZ);
			Console.WriteLine ("Tmp={0}", Tmp);
			Console.WriteLine ("GyX={0}  GyY={1}  GyZ={2}", GyX, GyY, GyZ);
			Console.WriteLine (" ");
		}
	}
}

