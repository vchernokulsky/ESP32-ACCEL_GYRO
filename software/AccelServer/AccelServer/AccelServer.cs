using System;
using System.Threading;

namespace AccelServer
{
	public class AccelServer
	{
		private IpBroadcaster controller;
		private DeviceSynchronizer devSync;

		Thread ipBroadcaster;
		Thread synchronizer;
		
		public AccelServer (int broadcasterPort)
		{
			while (NetHelper.GetEndPointIPv4 (10000) == null) {
				Thread.Sleep (3000);
			}
			controller = new IpBroadcaster(broadcasterPort);
			ipBroadcaster = new Thread(new ThreadStart(controller.IpBroadcast));
			ipBroadcaster.Start();

			devSync = new DeviceSynchronizer ();
			synchronizer = new Thread(new ThreadStart(devSync.StartListening));
			synchronizer.Start();
		}

		public void StartReceiving()
		{
			DataReceiver.running = true;
		}

		public void StopReceiving()
		{
			DataReceiver.running = false;
		}

		public void FinishThreads()
		{
			Console.WriteLine ("finishing...");
			ipBroadcaster.Abort ();
			ipBroadcaster.Join ();

			devSync.FinishReceiving ();
			synchronizer.Abort ();
			synchronizer.Join ();

			Console.WriteLine ("finised");
		}
	}
}

