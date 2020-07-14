using System;
using System.Collections.Generic;

namespace AccelServer
{
	public class AccGyroList
	{

		private float acc2si;
		private float gyro2si;

		private DateTime SyncTime;
		private int SyncTicks;

		public List<Accelerometer> agList;

		public AccGyroList(DateTime SyncTime, int SyncTicks, int GMAX=2, int DEGMAX = 250)
		{
			this.SyncTime = SyncTime;
			this.SyncTicks = SyncTicks;
			acc2si = (2.0f * (float)GMAX * 9.8f) / 65536.0f;
			gyro2si = (2.0f * (float)DEGMAX) / 65536.0f;
			agList = new List<Accelerometer> ();
		}

		public void put(byte[] bytes)
		{
			Accelerometer acc = new Accelerometer (SyncTime, SyncTicks, bytes, acc2si, gyro2si);
			acc.WriteToConsole ();
			agList.Add (acc);
		}

	}
	
	public class Accelerometer
	{

		public DateTime Time;
		public float AcX;
		public float AcY;
		public float AcZ;
		public float Tmp;
		public float GyX;
		public float GyY;
		public float GyZ;

		public Accelerometer ()
		{
			
		}
		public Accelerometer (DateTime SyncTime, int SyncTicks, byte[] bytes, float acc2si, float gyro2si)
		{
			SetFromBytes (SyncTime,SyncTicks, bytes, acc2si, gyro2si);
		}

		public int ParseImuBytes(byte low, byte high)
		{
			if ((~low & 0x80) != 0)
				return low << 8 | high;
			return -(((low ^ 255) << 8) | (high ^ 255) + 1);
		}

		public void SetFromBytes(DateTime SyncTime, int SyncTicks, byte[] bytes, float acc2si, float gyro2si)
		{
			if (bytes.Length != 18) {
				Console.WriteLine ("Wrong package size: {0}", bytes.Length);
				return;
			}


			AcX = acc2si * (float)ParseImuBytes(bytes[4], bytes[5]);
			AcY = acc2si * (float)ParseImuBytes(bytes[6], bytes[7]);
			AcZ = acc2si * (float)ParseImuBytes(bytes[8], bytes[9]);
			Tmp = (float)ParseImuBytes(bytes[10], bytes[11])/340.0f + 36.53f;
			GyX = gyro2si * (float)ParseImuBytes(bytes[12], bytes[13]);
			GyY = gyro2si * (float)ParseImuBytes(bytes[14], bytes[15]);
			GyZ = gyro2si * (float)ParseImuBytes(bytes[16], bytes[17]);

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

