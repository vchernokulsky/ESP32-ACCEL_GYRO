using Prism.Mvvm;
using System;
using System.Threading;

namespace AccelServer
{
	public class AccelServer: BindableBase
	{
		private int broadcasterPort;
		private IpBroadcaster controller;
		private DeviceSynchronizer devSync;

		private Thread main;
		private Thread ipBroadcaster;
		private Thread synchronizer;

		public bool NoConnection = true;
		
		public AccelServer (int broadcasterPort)
		{
			this.broadcasterPort = broadcasterPort;
		}

		public void CheckConnection()
		{
			while (NetHelper.GetEndPointIPv4(10000, "192.168.55.116") == null)
			{
				Thread.Sleep(3000);
			}
			NoConnection = false;
			RaisePropertyChanged("NoConnection");
		}

		public void StartThreads()
		{
			
			controller = new IpBroadcaster(broadcasterPort);
			ipBroadcaster = new Thread(new ThreadStart(controller.IpBroadcast));
			ipBroadcaster.Start();

			devSync = new DeviceSynchronizer();
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

			if (main.IsAlive)
			{
				main.Abort();
				main.Join();
			}

			Console.WriteLine ("finised");
		}
	}
}

