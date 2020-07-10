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
			if (BitConverter.IsLittleEndian) {
				uint ticks = BitConverter.ToUInt32 (bytes, 0);
				Time = SyncTime.AddMilliseconds (ticks - SyncTicks);
				AcX = BitConverter.ToInt16 (bytes, 4);
				AcY = BitConverter.ToInt16 (bytes, 6);
				AcZ = BitConverter.ToInt16 (bytes, 8);
				Tmp = BitConverter.ToInt16 (bytes, 10);
				GyX = BitConverter.ToInt16 (bytes, 12);
				GyY = BitConverter.ToInt16 (bytes, 14);
				GyZ = BitConverter.ToInt16 (bytes, 16);

			} else {
				Array.Reverse (bytes);
				GyZ = BitConverter.ToInt16 (bytes, 0);
				GyY = BitConverter.ToInt16 (bytes, 2);
				GyX = BitConverter.ToInt16 (bytes, 4);
				Tmp = BitConverter.ToInt16 (bytes, 6);
				AcZ = BitConverter.ToInt16 (bytes, 8);
				AcY = BitConverter.ToInt16 (bytes, 10);
				AcX = BitConverter.ToInt16 (bytes, 12);
				uint ticks = BitConverter.ToUInt32 (bytes, 14);
				Time = SyncTime.AddMilliseconds (ticks - SyncTicks);
			}


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

