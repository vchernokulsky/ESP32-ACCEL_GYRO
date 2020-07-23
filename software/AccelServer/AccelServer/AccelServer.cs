using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace AccelServer
{
	public class AccelServer : BindableBase
	{
		private int broadcasterPort;
		private IpBroadcaster controller;
		private DeviceSynchronizer devSync;

		private bool isFirstPackage = true;

		private Thread chkConn;
		private Thread ipBroadcaster;
		private Thread synchronizer;

		public bool NoConnection = true;
		public IList<string> Labels
		{
			get
			{
				if (!ChartDataSingleton.Instance._dataLists.ContainsKey(1))
					return new List<string>();
				return ChartDataSingleton.Instance._dataLists[1].TimeStamps;
			}
		}

		public IList<float> AccX
		{
			get
			{
				if (!ChartDataSingleton.Instance._dataLists.ContainsKey(1))
					return new List<float>();
				return ChartDataSingleton.Instance._dataLists[1].AxisXAccelerations;
			}
		}

		public IList<float> AccY
		{
			get
			{
				if (!ChartDataSingleton.Instance._dataLists.ContainsKey(1))
					return new List<float>();
				return ChartDataSingleton.Instance._dataLists[1].AxisYAccelerations;
			}
		}

		public IList<float> AccZ
		{
			get
			{
				if (!ChartDataSingleton.Instance._dataLists.ContainsKey(1))
					return new List<float>();
				return ChartDataSingleton.Instance._dataLists[1].AxisZAccelerations;
			}
		}

		public AccelServer(int broadcasterPort)
		{
			this.broadcasterPort = broadcasterPort;
			controller = new IpBroadcaster(broadcasterPort);
			devSync = new DeviceSynchronizer();
		}

		public void SetPropetyRaise()
		{
			devSync.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
		}

		public bool isSynchronized(int key)
		{
			if(devSync != null && devSync.deviceList != null && devSync.deviceList.ContainsKey(key))
			{
				return true;
			}
			return false;
		}

		public bool isReceiving(int key)
		{
			if (devSync != null && devSync.deviceList != null && devSync.deviceList.ContainsKey(key) && devSync.deviceList[key].dt_recv.gettingData)
			{
				return true;
			}
			return false;
		}

		private void CheckConnection()
		{
			RaisePropertyChanged("StartEnabled");
			RaisePropertyChanged("StopEnabled");
			while (NetHelper.GetEndPointIPv4(10000) == null)
			{
				Thread.Sleep(3000);
			}
			NoConnection = false;
			RaisePropertyChanged("NoConnection");
			RaisePropertyChanged("StartEnabled");
			RaisePropertyChanged("StopEnabled");
			StartThreads();
		}

		public void RunServer()
		{
			chkConn = ipBroadcaster = new Thread(new ThreadStart(CheckConnection));
			chkConn.Start();
		}


		public void StartThreads()
		{
			ipBroadcaster = new Thread(new ThreadStart(controller.IpBroadcast));
			ipBroadcaster.Start();
			
			synchronizer = new Thread(new ThreadStart(devSync.StartListening));
			synchronizer.Start();
		}

		private System.Windows.Threading.DispatcherTimer _timer;
		private void OnTimerTick(object sender, EventArgs e)
		{
			var t1 = DateTime.Now;
			var isProcessed = ChartDataSingleton.Instance.ProcessData();
			Console.WriteLine("Process data: {0}ms", (DateTime.Now-t1).TotalMilliseconds);

			if (!DataReceiver.running && !isProcessed)
			{
				_timer.Stop();
				RaisePropertyChanged("StartEnabled");
			}

			if (isFirstPackage)
			{
				RaisePropertyChanged("SeriesCollection");
				RaisePropertyChanged("Labels");
				isFirstPackage = false;
			}
			
		}
		public void StartReceiving()
		{
			ChartDataSingleton.Instance.Clear();

			_timer = new System.Windows.Threading.DispatcherTimer();
			_timer.Tick += OnTimerTick;
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
			_timer.Start();

			DataReceiver.running = true;
			RaisePropertyChanged("StartEnabled");
			RaisePropertyChanged("StopEnabled");
		}

		public void StopReceiving()
		{
			DataReceiver.running = false;
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

		public void StopServer()
		{
			Console.WriteLine ("finishing...");

			StopThread(chkConn);
			StopThread(ipBroadcaster);
			if(devSync != null)
				devSync.FinishReceiving ();
			StopThread(synchronizer);
	
			Console.WriteLine ("finised");
		}
	}
}

