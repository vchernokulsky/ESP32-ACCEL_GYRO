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

		private Thread chkConn;
		private Thread ipBroadcaster;
		private Thread synchronizer;

		public bool NoConnection = true;
		public string[] Labels
		{
			get
			{
				return devSync.Labels;
			}
		}

		public List<float> AccX
		{
			get
			{
				return devSync.AccX;
			}
		}

		public List<float> AccY
		{
			get
			{
				return devSync.AccY;
			}
		}

		public List<float> AccZ
		{
			get
			{
				return devSync.AccZ;
			}
		}

		public ObservableCollection<KeyValuePair<string, float>> data
        {
            get
            {
                return devSync.data;
            }
        }

		public ObservableCollection<KeyValuePair<string, float>> data2
		{
			get
			{
				return devSync.data2;
			}
		}

		public ObservableCollection<KeyValuePair<string, float>> data3
		{
			get
			{
				return devSync.data3;
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

		private void _CheckConnection()
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
			chkConn = ipBroadcaster = new Thread(new ThreadStart(_CheckConnection));
			chkConn.Start();
		}


		public void StartThreads()
		{
			
			
			ipBroadcaster = new Thread(new ThreadStart(controller.IpBroadcast));
			ipBroadcaster.Start();

			
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

