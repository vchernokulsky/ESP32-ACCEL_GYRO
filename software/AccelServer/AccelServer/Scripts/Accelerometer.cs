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

		public void SetFromBytes(DateTime SyncTime, int SyncTicks, byte[] bytes)
		{
			if (bytes.Length != 19) {
				Console.WriteLine ("Wrong package size: {0}", bytes.Length);
				return;
			}
			AcX = BytesToInt (bytes [4], bytes [5]);
			AcY = BytesToInt (bytes [6], bytes [6]);
			AcZ = BytesToInt (bytes [8], bytes [9]);
			Tmp = BytesToInt (bytes [10], bytes [11]);
			GyX = BytesToInt (bytes [12], bytes [13]);
			GyY = BytesToInt (bytes [14], bytes [15]);
			GyZ = BytesToInt (bytes [16], bytes [17]);

			if (BitConverter.IsLittleEndian) {
				uint ticks = BitConverter.ToUInt32 (bytes, 0);
				Time = SyncTime.AddMilliseconds (ticks - SyncTicks);
			} else {
				Array.Reverse (bytes);
				uint ticks = BitConverter.ToUInt32 (bytes, 14);
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
			Console.WriteLine ("---- {0} ----", Time.ToString());
			Console.WriteLine ("AccX={0}  AccY={1}  AccZ={2}", AcX, AcY, AcZ);
			Console.WriteLine ("Tmp={0}", Tmp);
			Console.WriteLine ("GyX={0}  GyY={1}  GyZ={2}", GyX, GyY, GyZ);
			Console.WriteLine (" ");
		}
	}
}

