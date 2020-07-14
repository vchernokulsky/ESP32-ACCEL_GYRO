using System;
using System.Collections.Generic;

namespace AccelServer
{
	public class AccGyroList
	{
		private DateTime SyncTime;
		private int SyncTicks;

		public List<Accelerometer> agList;

		public AccGyroList(DateTime SyncTime, int SyncTicks)
		{
			this.SyncTime = SyncTime;
			this.SyncTicks = SyncTicks;
			agList = new List<Accelerometer> ();
		}

		public void put(byte[] bytes)
		{
			Accelerometer acc = new Accelerometer (SyncTime, SyncTicks, bytes);
			acc.WriteToConsole ();
			agList.Add (acc);
		}

	}
	
	public class Accelerometer
	{
		public DateTime Time;
		public int AcX;
		public int AcY;
		public int AcZ;
		public float Tmp;
		public int GyX;
		public int GyY;
		public int GyZ;

		public Accelerometer ()
		{
			
		}
		public Accelerometer (DateTime SyncTime, int SyncTicks, byte[] bytes)
		{
			SetFromBytes (SyncTime,SyncTicks, bytes);
		}

		public int ParseImuBytes(byte low, byte high)
		{
			if ((~low & 0x80) != 0)
				return low << 8 | high;
			return -(((low ^ 255) << 8) | (high ^ 255) + 1);
		}

		public void SetFromBytes(DateTime SyncTime, int SyncTicks, byte[] bytes)
		{
			if (bytes.Length != 19) {
				Console.WriteLine ("Wrong package size: {0}", bytes.Length);
				return;
			}
<<<<<<< HEAD:software/AccelServer/AccelServer/Scripts/Accelerometer.cs
			AcX = BytesToInt (bytes [4], bytes [5]);
			AcY = BytesToInt (bytes [6], bytes [6]);
			AcZ = BytesToInt (bytes [8], bytes [9]);
			Tmp = BytesToInt (bytes [10], bytes [11]);
			GyX = BytesToInt (bytes [12], bytes [13]);
			GyY = BytesToInt (bytes [14], bytes [15]);
			GyZ = BytesToInt (bytes [16], bytes [17]);

=======

			AcX = ParseImuBytes(bytes[4], bytes[5]);
			AcY = ParseImuBytes(bytes[6], bytes[7]);
			AcZ = ParseImuBytes(bytes[8], bytes[9]);
			Tmp = ParseImuBytes(bytes[10], bytes[11]);
			GyX = ParseImuBytes(bytes[12], bytes[13]);
			GyY = ParseImuBytes(bytes[14], bytes[15]);
			GyZ = ParseImuBytes(bytes[16], bytes[17]);
>>>>>>> bef30f4b6a536ca53e8bd2347bd713681489d63b:software/AccelServer/AccelServer/Accelerometer.cs
			if (BitConverter.IsLittleEndian) {
				uint ticks = BitConverter.ToUInt32 (bytes, 0);
				Time = SyncTime.AddMilliseconds (ticks - SyncTicks);
			} else {
				Array.Reverse (bytes);
<<<<<<< HEAD:software/AccelServer/AccelServer/Scripts/Accelerometer.cs
				uint ticks = BitConverter.ToUInt32 (bytes, 14);
=======
				int ticks = BitConverter.ToInt32 (bytes, 14);
>>>>>>> bef30f4b6a536ca53e8bd2347bd713681489d63b:software/AccelServer/AccelServer/Accelerometer.cs
				Time = SyncTime.AddMilliseconds (ticks - SyncTicks);
			}
		}
		private int BytesToInt(byte firstbyte, byte secondbyte)
		{
			int ret;
			if ((~firstbyte & 0x80) != 0) {
				ret = firstbyte << 8 | secondbyte;
			} else {
				ret = -(((firstbyte ^ 255) << 8) | (secondbyte ^ 255) + 1);
			}
			return ret;
					
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

