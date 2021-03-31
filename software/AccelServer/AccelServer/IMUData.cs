using System;
using System.Collections.Generic;

namespace ImuServer
{
	public class IMUDataListDecorator
	{

		

	}

	public class IMUDataList
	{
		private int id;

		private float acc2si;
		private float gyro2si;

		private DateTime SyncTime;
		private int SyncTicks;

		public List<IMUData> agList;



		public IMUDataList(int id, DateTime SyncTime, int SyncTicks, int GMAX=2, int DEGMAX = 250)
		{
			this.id = id;
			this.SyncTime = SyncTime;
			this.SyncTicks = SyncTicks;
			acc2si = (2.0f * (float)GMAX * 9.8f) / 65536.0f;
			gyro2si = (2.0f * (float)DEGMAX) / 65536.0f;
		}

		public IMUData GetImuData(byte[] bytes)
		{
			IMUData data = new IMUData (id, SyncTime, SyncTicks, bytes, acc2si, gyro2si);
			//data.WriteToConsole ();
			return data;
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


			//TODO: change values for real devices
			float correctionAxisZ = id <= 4 ? 9.8f : 0.0f;

			AcX = (float)Math.Round(acc2si * (ParseImuBytes(bytes[4], bytes[5]) /*- MpuCalibraion.Instance.offset[id].AccX */), 4);
			AcY = (float)Math.Round(acc2si * (ParseImuBytes(bytes[6], bytes[7]) /*- MpuCalibraion.Instance.offset[id].AccY*/), 4);
			AcZ = (float)Math.Round(acc2si * (ParseImuBytes(bytes[8], bytes[9]) /*- MpuCalibraion.Instance.offset[id].AccZ*/), 4) - correctionAxisZ;
			Tmp = ParseImuBytes(bytes[10], bytes[11])/340.0f + 36.53f;
			GyX = (float)Math.Round(gyro2si * (ParseImuBytes(bytes[12], bytes[13]) /*- MpuCalibraion.Instance.offset[id].GyX*/), 4);
			GyY = (float)Math.Round(gyro2si * (ParseImuBytes(bytes[14], bytes[15]) /*- MpuCalibraion.Instance.offset[id].GyY*/), 4);
			GyZ = (float)Math.Round(gyro2si * (ParseImuBytes(bytes[16], bytes[17]) /*- MpuCalibraion.Instance.offset[id].GyZ*/), 4);

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

