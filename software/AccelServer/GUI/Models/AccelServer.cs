using ImuServer;
using System;
using System.Diagnostics;
using System.Threading;


namespace GUI
{
    public class AccelServer
	{
		private MainControlModel model;

		private IpBroadcaster ipBroadcaster;
		private DeviceSynchronizer devSync;

		private Thread chkConn;
		private Thread ipBroadcasterThread;
		private Thread synchronizer;

		private readonly TimeSpan INTERVAL = new TimeSpan(0, 0, 0, 0, 100);
		private System.Windows.Threading.DispatcherTimer _timer;

		public AccelServer(int broadcasterPort)
		{			
			ipBroadcaster = new IpBroadcaster(broadcasterPort);
			devSync = new DeviceSynchronizer();
		}

		public void Setup(AppType appType, MainControlModel _model) 
		{
			model = _model;
			devSync.SetAppType(appType, model);
		}

		public void RunServer()
		{
			chkConn = new Thread(new ThreadStart(CheckConnection));
			chkConn.Start();
		}

		public void StartReceiving()
		{
			DBManager.Instance.GetUtils().SetSessionId();
			ChartDataSingleton.Instance.Clear();
			_timer = new System.Windows.Threading.DispatcherTimer();
			_timer.Tick += OnTimerTick;
			_timer.Interval = INTERVAL;
			_timer.Start();
			model.IsRunning = true;
		}

		public void StopReceiving()
		{
			model.IsRunning = false;
		}

		public void StopServer()
		{
			Console.WriteLine("finishing...");
			_timer.Stop();
			StopThread(chkConn);
			StopThread(ipBroadcasterThread);
			if (devSync != null)
				devSync.FinishReceiving();
			StopThread(synchronizer);

			Console.WriteLine("finised");
		}

		//public void SetPropetyRaise()
		//{
		//	devSync.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
		//}

		private void CheckConnection()
		{
			model.NoConnection = true;
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
			model.NoConnection = false;
			StartThreads();
		}

		
		private void StartThreads()
		{
			ipBroadcasterThread = new Thread(new ThreadStart(ipBroadcaster.IpBroadcast));
			ipBroadcasterThread.Start();
			
			synchronizer = new Thread(new ThreadStart(devSync.StartListening));
			synchronizer.Start();
		}

		private void OnTimerTick(object sender, EventArgs e)
		{
			var t1 = DateTime.Now;
			model.DataProcessing = ChartDataSingleton.Instance.ProcessData();
			updatePackageCount();
			Console.WriteLine("Process data: {0}ms", (DateTime.Now-t1).TotalMilliseconds);

			if (!model.IsRunning && !model.DataProcessing)
			{
				_timer.Stop();
			}	
		}
	
		private void StopThread(Thread thread)
		{
			if (thread != null && thread.IsAlive)
			{
				thread.Abort();
				thread.Join();
			}
		}

		private void updatePackageCount()
        {
			int id = ChartDataSingleton.Instance.lastProcessedId;
			if (id == 1)
				model.Device1.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
			if (id == 2)
				model.Device2.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
			if (id == 3)
				model.Device3.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
			if (id == 4)
				model.Device4.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
			if (id == 5)
				model.Device5.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
			if (id == 6)
				model.Device6.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
		}

	
	}
}

