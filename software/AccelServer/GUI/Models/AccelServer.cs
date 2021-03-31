using ImuServer;
using System;
using System.Threading;

namespace GUI.Models
{
    public class AccelServer
	{
		private MainControlModel _model;

		private readonly IpBroadcaster _ipBroadcaster;
		private readonly DeviceSynchronizer _devSync;
        private WifiChecker _wifiChecker;

		private Thread _chkConn;
		private Thread _ipBroadcasterThread;
		private Thread _synchronizer;

		private readonly TimeSpan _interval = new TimeSpan(0, 0, 0, 0, 100);
		private System.Windows.Threading.DispatcherTimer _timer;
        private Thread _checkWifiConnection;

        public AccelServer(int broadcasterPort)
		{			
			_ipBroadcaster = new IpBroadcaster(broadcasterPort);
			_devSync = new DeviceSynchronizer();
		}

		public void Setup(AppType appType, MainControlModel model) 
		{
			this._model = model;
			_devSync.SetAppType(appType, this._model);
			_wifiChecker = new WifiChecker(_model);
		}

		public void RunServer()
		{
            _chkConn = new Thread(new ThreadStart(CheckConnection)) {Name = "CheckConnectionStartThreads"};
            _chkConn.Start();
		}

		public void StartReceiving()
		{
			_model.SessionId = ChartDataSingleton.Instance.SetSessionId();
			ChartDataSingleton.Instance.Clear();
			_timer = new System.Windows.Threading.DispatcherTimer();
			_timer.Tick += OnTimerTick;
			_timer.Interval = _interval;
			_timer.Start();
			_model.IsRunning = true;
		}

		public void StopReceiving()
		{
			_model.IsRunning = false;
		}

		public void StopServer()
		{
			Console.WriteLine(@"finishing...");
			if(_timer !=null && _timer.IsEnabled)
				_timer.Stop();

           
			_wifiChecker.Abort();
			_wifiChecker.WaitFinishing();
            StopThread(_checkWifiConnection);

            _ipBroadcaster?.Abort();
			_ipBroadcaster?.WaitFinishing();
			StopThread(_ipBroadcasterThread);

            _devSync?.FinishReceiving();
            _devSync?.Abort();
            _devSync?.WaitFinishing();
            StopThread(_synchronizer);

			Console.WriteLine(@"finished");
		}



       

		private void CheckConnection()
		{
			if (_wifiChecker.WaitUntilConnected()) 
                StartThreads();
		}

		
		private void StartThreads()
		{
			_ipBroadcasterThread = new Thread(new ThreadStart(_ipBroadcaster.IpBroadcast)){Name = "IpBroadcaster"};
			_ipBroadcasterThread.Start();
			
			_synchronizer = new Thread(new ThreadStart(_devSync.StartListening)){Name = "DeviceSynchromizer"};
			_synchronizer.Start();

			_checkWifiConnection = new Thread(new ThreadStart(_wifiChecker.WifiMonitor)){Name = "WifiMonitor" };
			_checkWifiConnection.Start();
		}

       

        private void OnTimerTick(object sender, EventArgs e)
		{
            _model.DataProcessing = ChartDataSingleton.Instance.ProcessData();
			UpdatePackageCount();

            if (!_model.IsRunning && !_model.DataProcessing)
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
                    _model.Device1.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
                    break;
                case 2:
                    _model.Device2.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
                    break;
                case 3:
                    _model.Device3.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
                    break;
                case 4:
                    _model.Device4.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
                    break;
                case 5:
                    _model.Device5.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
                    break;
                case 6:
                    _model.Device6.PackageCount = ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
                    break;
            }
        }

	
	}
}

