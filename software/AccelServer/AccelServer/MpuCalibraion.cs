using System;
using System.Collections.Generic;

namespace AccelServer
{
	public class CalibrationCoeffitients
	{
		public float AccX { get; set; }
		public float AccY { get; set; }
		public float AccZ { get; set; }
		public float GyX { get; set; }
		public float GyY { get; set; }
		public float GyZ { get; set; }

	}

	public class MpuCalibraion
	{
		public static Dictionary<int, CalibrationCoeffitients> offset = new Dictionary<int, CalibrationCoeffitients>()
		{
			{ 1, new CalibrationCoeffitients { AccX=762.5751f , AccY=64.98919f, AccZ=-310.5371f, GyX=-112.82f, GyY=-55.02f, GyZ=-44.24f} },
			{ 1, new CalibrationCoeffitients { AccX=762.5751f , AccY=64.98919f, AccZ=-310.5371f, GyX=-112.82f, GyY=-55.02f, GyZ=-44.24f} },
			{ 2, new CalibrationCoeffitients { AccX=0.0f , AccY=0.0f, AccZ=0.0f, GyX=0.0f, GyY=0.0f, GyZ=0.0f} },
			{ 3, new CalibrationCoeffitients { AccX=0.0f , AccY=0.0f, AccZ=0.0f, GyX=0.0f, GyY=0.0f, GyZ=0.0f} },
			{ 4, new CalibrationCoeffitients { AccX=0.0f , AccY=0.0f, AccZ=0.0f, GyX=0.0f, GyY=0.0f, GyZ=0.0f} },
			{ 5, new CalibrationCoeffitients { AccX=0.0f , AccY=0.0f, AccZ=0.0f, GyX=0.0f, GyY=0.0f, GyZ=0.0f} },
			{ 6, new CalibrationCoeffitients { AccX=0.0f , AccY=0.0f, AccZ=0.0f, GyX=0.0f, GyY=0.0f, GyZ=0.0f} },

		};

	}
}

