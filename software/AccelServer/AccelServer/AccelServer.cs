using GUI;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Threading;

namespace ImuServer
{
	public class AccelServer : BindableBase
	{
		public static string UserName = "";

		private IpBroadcaster controller;
		private DeviceSynchronizer devSync;

		private Thread chkConn;
		private Thread ipBroadcaster;
		private Thread synchronizer;

		public bool NoConnection = true;
		
		public AccelServer(int broadcasterPort)
		{
			controller = new IpBroadcaster(broadcasterPort);
			devSync = new DeviceSynchronizer();
		}

		public void SetAppType(AppType appType) 
		{
			DBManager.Instance.SetAppType(appType);
			devSync.SetAppType(appType);
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
				try
				{
					Thread.Sleep(3000);
				}
				catch(Exception ex)
				{
					Debug.WriteLine(ex.Message);
					break;
				}
			}
			NoConnection = false;
			RaisePropertyChanged("NoConnection");
			RaisePropertyChanged("StartEnabled");
			RaisePropertyChanged("StopEnabled");
			StartThreads();
		}

		public void RunServer()
		{
			chkConn = new Thread(new ThreadStart(CheckConnection));
			chkConn.Start();
		}


		public void StartThreads()
		{
			ipBroadcaster = new Thread(new ThreadStart(controller.IpBroadcast));
			ipBroadcaster.Start();
			
			synchronizer = new Thread(new ThreadStart(devSync.StartListening));
			synchronizer.Start();
		}

		private readonly TimeSpan INTERVAL = new TimeSpan(0, 0, 0, 0, 100);
		private System.Windows.Threading.DispatcherTimer _timer;
		private void OnTimerTick(object sender, EventArgs e)
		{
			var t1 = DateTime.Now;
			var isProcessed = ChartDataSingleton.Instance.ProcessData();
			RaisePropertyChanged("Device1");
			RaisePropertyChanged("Device2");
			RaisePropertyChanged("Device3");
			RaisePropertyChanged("Device4");
			RaisePropertyChanged("Device5");
			RaisePropertyChanged("Device6");
			Console.WriteLine("Process data: {0}ms", (DateTime.Now-t1).TotalMilliseconds);

			if (!DataReceiver.running && !isProcessed)
			{
				_timer.Stop();
				RaisePropertyChanged("StartEnabled");
			}

			
		}
		public void StartReceiving()
		{
			DBManager.Instance.GetUtils().SetSessionId();
			ChartDataSingleton.Instance.Clear();
		
			_timer = new System.Windows.Threading.DispatcherTimer();
			_timer.Tick += OnTimerTick;
			_timer.Interval = INTERVAL;
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

		public DeviceInfo GetDeviceInfo(int id)
        {
			DeviceInfo ret = null; 
			if(devSync!= null && devSync.deviceList!= null && devSync.deviceList.ContainsKey(id))
				ret = devSync.deviceList[id];
			return ret;
        }
	}
}

