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
		private Thread chkConn;
		private Thread ipBroadcaster;
		private Thread synchronizer;

		public bool NoConnection = true;

		
		public AccelServer (int broadcasterPort)
		{
			this.broadcasterPort = broadcasterPort;
		}

		private void _CheckConnection()
		{
			RaisePropertyChanged("StartEnabled");
			RaisePropertyChanged("StopEnabled");
			while (NetHelper.GetEndPointIPv4(10000, "192.168.55.116") == null)
			{
				Thread.Sleep(3000);
			}
			NoConnection = false;
			RaisePropertyChanged("NoConnection");
			RaisePropertyChanged("StartEnabled");
			RaisePropertyChanged("StopEnabled");
		}

		public void CheckConnection()
		{
			chkConn = ipBroadcaster = new Thread(new ThreadStart(_CheckConnection));
			chkConn.Start();
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
			RaisePropertyChanged("StartEnabled");
			RaisePropertyChanged("StopEnabled");
		}

		public void StopReceiving()
		{
			DataReceiver.running = false;
			RaisePropertyChanged("StartEnabled");
			RaisePropertyChanged("StopEnabled");
		}

		private void StopThread(Thread thread)
		{
			if (thread != null && thread.IsAlive)
			{
				thread.Abort();
				thread.Join();
			}
		}

		public void FinishThreads()
		{
			Console.WriteLine ("finishing...");

			StopThread(main);
			StopThread(ipBroadcaster);
			devSync.FinishReceiving ();
			StopThread(synchronizer);
	
			Console.WriteLine ("finised");
		}
	}
}

