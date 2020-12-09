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

		private readonly TimeSpan _interval = new TimeSpan(0, 0, 0, 0, 100);
		private System.Windows.Threading.DispatcherTimer _timer;
        private Thread checkWifiConnection;

        public AccelServer(int broadcasterPort)
		{			
			ipBroadcaster = new IpBroadcaster(broadcasterPort);
			devSync = new DeviceSynchronizer();
		}

		public void Setup(AppType appType, MainControlModel model) 
		{
			this.model = model;
			devSync.SetAppType(appType, this.model);
		}

		public void RunServer()
		{
            chkConn = new Thread(new ThreadStart(CheckConnection)) {Name = "CheckConnectionStartThreads"};
            chkConn.Start();
		}

		public void StartReceiving()
		{
			model.SessionId = ChartDataSingleton.Instance.SetSessionId();
			ChartDataSingleton.Instance.Clear();
			_timer = new System.Windows.Threading.DispatcherTimer();
			_timer.Tick += OnTimerTick;
			_timer.Interval = _interval;
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
			if(_timer !=null && _timer.IsEnabled)
				_timer.Stop();
			StopThread(chkConn);
			StopThread(ipBroadcasterThread);
            devSync?.FinishReceiving();
            StopThread(synchronizer);

			Console.WriteLine("finised");
		}



        private bool IsConnected()
        {
            bool result = false;
            try
            {
                var endpoint = NetHelper.GetEndPointIPv4(10000);
                result = endpoint != null;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

		private void CheckConnection()
		{
			model.NoConnection = true;
			while (!IsConnected())
            {
                Thread.Sleep(3000);
            }
			model.NoConnection = false;
			StartThreads();
		}

		
		private void StartThreads()
		{
			ipBroadcasterThread = new Thread(new ThreadStart(ipBroadcaster.IpBroadcast)){Name = "IpBroadcaster"};
			ipBroadcasterThread.Start();
			
			synchronizer = new Thread(new ThreadStart(devSync.StartListening)){Name = "DeviceSynchromizer"};
			synchronizer.Start();

			checkWifiConnection = new Thread(new ThreadStart(WifiChecker)){Name = "WifiChecker"};
			checkWifiConnection.Start();
		}

        private void WifiChecker()
        {
            while (true)
            {
                model.NoConnection = !IsConnected();
				Thread.Sleep(10000);
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
		{
            model.DataProcessing = ChartDataSingleton.Instance.ProcessData();
			UpdatePackageCount();

            if (!model.IsRunning && !model.DataProcessing)
			{
				_timer.Stop();
			}	
		}
	
		private void StopThread(Thread thread)
        {
            if (thread == null || !thread.IsAlive) return;
            thread.Abort();
            thread.Join();
        }

		private void UpdatePackageCount()
        {
			int id = ChartDataSingleton.Instance.lastProcessedId;
			switch (id)
            {
                case 1:
                    model.Device1.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
                    break;
                case 2:
                    model.Device2.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
                    break;
                case 3:
                    model.Device3.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
                    break;
                case 4:
                    model.Device4.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
                    break;
                case 5:
                    model.Device5.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
                    break;
                case 6:
                    model.Device6.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
                    break;
            }
        }

	
	}
}

