using System;
using System.Net;
using System.Net.Sockets;
using ImuServer;

namespace GUI
{
	public class DataReceiver
	{
		private int id;
		private int port;
		private DeviceModel device;

		public DataReceiver (DeviceModel _device, int id, int port)
		{
			device = _device;
			this.id = id;
			this.port = port;
		}
			
		public void StartListening()
		{
			IPEndPoint ipPoint = NetHelper.GetEndPointIPv4(port);
			Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{			
				listenSocket.Bind(ipPoint);
				listenSocket.Listen(10);
				Console.WriteLine("DataReceiver: Waiting for connection...");

				while (true)
				{
					Socket handler = listenSocket.Accept();
					int bytesRec = 0; 
					while(device.NeedToReceive)
					{
						device.StartReceiving();
						byte[] bytes = new byte[5400];
						bytesRec = handler.Receive(bytes);
						ChartDataSingleton.Instance.PutData(new ReceivedObject(id, bytesRec, bytes));              
					}						
					handler.Shutdown(SocketShutdown.Both);
					handler.Close();
					device.StopReceiving();
				}
			}
			catch(Exception ex)
			{
				device.StopReceiving();
				Console.WriteLine(ex.Message);
			}
		}
	
	}
}
		