using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Collections.Generic;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;
using Prism;
using Prism.Events;

namespace AccelServer
{

	
	public class StateObject {
		// Client  socket.
		public Socket workSocket = null;
		// Size of receive buffer.
		public const int BufferSize = 5400;
		// Receive buffer.
		public byte[] buffer = new byte[BufferSize];
		// Received data string.
		public StringBuilder sb = new StringBuilder();  
	}

	public class DeviceInfo
	{
		public int Id { get; set; }
		public int Type { get; set; }
		public string Ip { get; set; }
		public int SyncTicks { get; set; }

		public DateTime SyncTime { get; set; }
		public int Port { get; set; }

		public DataReceiver dt_recv { get; set; }
		public Thread data_receiver { get; set; }
	}

	public class PortInfo
	{
		public int Port { get; set; }

		public PortInfo(int n)
		{
			Port = n;
		}
	}

	public class DeviceSynchronizer: BindableBase
	{
		// Thread signal.
		private static Mutex mut = new Mutex();
		private List<Thread> dataRcvList;
		public ManualResetEvent allDone = new ManualResetEvent(false);
		public Dictionary<int, DeviceInfo> deviceList = new Dictionary<int, DeviceInfo>();
		public int cur_device_port = 10000;

		public IList<string> Labels => GetLabels();
		public IList<float> AccX => GetXAxisAccelerations();
		public IList<float> AccY => GetYAxisAccelerations();
		public IList<float> AccZ => GetZAxisAccelerations();

		public DeviceSynchronizer() {
			dataRcvList = new List<Thread> ();
			
		}

		public IList<string> GetLabels()
		{
			if (deviceList != null && deviceList.ContainsKey(1))
			{
				if (deviceList[1].dt_recv != null && deviceList[1].dt_recv.agList != null)
				{
					return deviceList[1].dt_recv.agList.TimeStamps;
				}
			}
			return new List<string>();
		}

		public IList<float> GetXAxisAccelerations()
		{
			if (deviceList != null && deviceList.ContainsKey(1))
			{
				if (deviceList[1].dt_recv != null && deviceList[1].dt_recv.agList != null)
				{
					return deviceList[1].dt_recv.agList.AxisXAccelerations;
				}
			}
			return new List<float>();
		}
		public IList<float> GetYAxisAccelerations()
		{
			if (deviceList != null && deviceList.ContainsKey(1))
			{
				if (deviceList[1].dt_recv != null && deviceList[1].dt_recv.agList != null)
				{
					return deviceList[1].dt_recv.agList.AxisYAccelerations;
				}
			}
			return new List<float>();
		}

		public IList<float> GetZAxisAccelerations()
		{
			if (deviceList != null && deviceList.ContainsKey(1))
			{
				if (deviceList[1].dt_recv != null && deviceList[1].dt_recv.agList != null)
				{
					return deviceList[1].dt_recv.agList.AxisZAccelerations;
				}
			}
			return new List<float>();
		}

		public void FinishReceiving()
		{
			foreach (Thread t in dataRcvList) {
				t.Abort ();
				t.Join ();
			}
		}

		public void StartListening() {
			var localEndPoint = NetHelper.GetEndPointIPv4(9875);
			// Create a TCP/IP socket.
			Socket listener = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp );

			// Bind the socket to the local endpoint and listen for ed43247f-6f4e-41fe-a559-b393adb6458cincoming connections.
			try {
				listener.Bind(localEndPoint);
				listener.Listen(100);

				while (true) {
					// Set the event to nonsignaled state.
					allDone.Reset();

					// Start an asynchronous socket to listen for connections.
					Console.WriteLine("Waiting for a connection...");
					listener.BeginAccept(new AsyncCallback(AcceptCallback), listener );

					// Wait until a connection is made before continuing.
					allDone.WaitOne();
				}

			} catch (Exception e) {
				Console.WriteLine(e.ToString());
			}

			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();

		}

		public void AcceptCallback(IAsyncResult ar) {
			// Signal the main thread to continue.
			allDone.Set();

			// Get the socket that handles the client request.
			Socket listener = (Socket) ar.AsyncState;
			Socket handler = listener.EndAccept(ar);

			// Create the state object.
			StateObject state = new StateObject();
			state.workSocket = handler;
			handler.BeginReceive( state.buffer, 0, StateObject.BufferSize, 0,
				new AsyncCallback(ReadCallback), state);
		}

		public void ReadCallback(IAsyncResult ar) {
			String content = String.Empty;

			// Retrieve the state object and the handler socket
			// from the asynchronous state object.
			StateObject state = (StateObject) ar.AsyncState;
			Socket handler = state.workSocket;

			// Read data from the client socket. 
			int bytesRead = handler.EndReceive(ar);
			DateTime cur_time = DateTime.Now;

			if (bytesRead > 0) {
				// There  might be more data, so store the data received so far.
				state.sb.Append(Encoding.ASCII.GetString(
					state.buffer,0,bytesRead));

				// Check for end-of-file tag. If it is not there, read 
				// more data.
				content = state.sb.ToString();

				// All the data has been read from the 
				// client. Display it on the console.
				Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
					content.Length, content );


				try
				{
					DeviceInfo info = JsonConvert.DeserializeObject<DeviceInfo>(content);

					mut.WaitOne();
					if(deviceList.ContainsKey(info.Id))
					{
						Console.WriteLine("device with id={0} has synchronized", info.Id);
						PortInfo portInfo = new PortInfo(deviceList[info.Id].Port);
						String output = JsonConvert.SerializeObject(portInfo);
						Send(handler, output);
					}
					else
					{
						info.Port = cur_device_port++;
						info.SyncTime = cur_time;
						info.dt_recv = new DataReceiver (info.Id, info.Type, info.Port, info.SyncTime, info.SyncTicks);

						ChartDataSingleton.Instance.SetSyncTime(info.Id, info.SyncTime, info.SyncTicks);

						info.dt_recv.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
						Thread data_receiver = new Thread(new ThreadStart(info.dt_recv.StartListening));
						dataRcvList.Add(data_receiver);

						info.data_receiver = data_receiver;
						deviceList.Add(info.Id, info);
						deviceList[info.Id].data_receiver.Start();

						RaisePropertyChanged(String.Concat("DeviceColor",info.Id.ToString()));

						PortInfo portInfo = new PortInfo(info.Port);
						String output = JsonConvert.SerializeObject(portInfo);
						Send(handler, output);
					}
					mut.ReleaseMutex();
				}
				catch (Exception e)
				{
					Console.WriteLine("{0} Exception caught.", e);
				}

			}
		}

		private void Send(Socket handler, String data) {
			// Convert the string data to byte data using ASCII encoding.

			Console.WriteLine (data);
			byte[] byteData = Encoding.ASCII.GetBytes(data);

			// Begin sending the data to the remote device.
			handler.BeginSend(byteData, 0, byteData.Length, 0,
				new AsyncCallback(SendCallback), handler);
		}

		private void SendCallback(IAsyncResult ar) {
			try {
				// Retrieve the socket from the state object.
				Socket handler = (Socket) ar.AsyncState;

				// Complete sending the data to the remote device.
				int bytesSent = handler.EndSend(ar);
				Console.WriteLine("Sent {0} bytes to client.", bytesSent);

				handler.Shutdown(SocketShutdown.Both);
				handler.Close();

			} catch (Exception e) {
				Console.WriteLine(e.ToString());
			}
		}   
	}
}