﻿using GUI;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Threading;

namespace ImuServer
{
	public class AccelServer : BindableBase
	{
		public static string UserName = "";
		public static int SessionId = 0;

		private IpBroadcaster controller;
		private DeviceSynchronizer devSync;

		private bool isFirstPackage = true;

		private Thread chkConn;
		private Thread ipBroadcaster;
		private Thread synchronizer;

		private AppType appType;

		public bool NoConnection = true;
		
		public AccelServer(int broadcasterPort)
		{
			controller = new IpBroadcaster(broadcasterPort);
			devSync = new DeviceSynchronizer();
		}

		public void SetAppType(AppType appType) 
		{
			this.appType = appType;
			DBManager.Instance.SetAppType(appType);
			SessionId = DBManager.Instance.GetUtils().GetSessionId();
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

		private readonly TimeSpan INTERVAL = new TimeSpan(0, 0, 0, 0, 100);
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

			if (isFirstPackage && ChartDataSingleton.Instance.Count(1) > 1000)
			{
				RaisePropertyChanged("SeriesCollection");
				RaisePropertyChanged("Labels");
				RaisePropertyChanged("Chart1");
				isFirstPackage = false;
				Console.WriteLine("FIRST PACKAGE RECIEVED!!!");
			}
			
		}
		public void StartReceiving()
		{
			SessionId++;
			ChartDataSingleton.Instance.Clear();
			isFirstPackage = true;

			_timer = new System.Windows.Threading.DispatcherTimer();
			_timer.Tick += OnTimerTick;
			_timer.Interval = INTERVAL;
			_timer.Start();

			DataReceiver.running = true;
			RaisePropertyChanged("SeriesCollection");
			RaisePropertyChanged("Labels");
			RaisePropertyChanged("Chart1");
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

