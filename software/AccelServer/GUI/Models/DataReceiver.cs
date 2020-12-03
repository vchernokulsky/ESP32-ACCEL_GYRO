using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ImuServer;

namespace GUI
{
	public class DataReceiver
	{
		private int id;
		private int port;
		private int port2;
		private DeviceModel device;



		public DataReceiver (DeviceModel _device, int id, int port, int port2)
		{
			device = _device;
			this.id = id;
			this.port = port;
			this.port2 = port2;
		}
			
		public void StartListening()
		{
			IPEndPoint ipPoint = NetHelper.GetEndPointIPv4(port);
			Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			CommandSocket commandSocket = new CommandSocket(port2);
			try
			{			
				listenSocket.Bind(ipPoint);
				listenSocket.Listen(10);
				Console.WriteLine("DataReceiver: Waiting for connection...");
				Socket handler = listenSocket.Accept();
				
				while (true)
				{

					commandSocket.Connect();
					int bytesRec = 0; 

                    while (device.NeedToReceive && !commandSocket.DeviceRunning)
                    {
						SocketError err = commandSocket.SendStart();
						if(err == SocketError.TimedOut)
                        {
							Thread.Sleep(100);
                        } 
						else
                        {
							if (err == SocketError.SocketError)
							{
								commandSocket.Close();
							}
							break;
						}					
					}

					if (!commandSocket.DeviceRunning)
                    {
						continue;
                    }

					while (device.NeedToReceive)
					{
						device.StartReceiving();
						byte[] bytes = new byte[5400];
						bytesRec = handler.Receive(bytes);
						ChartDataSingleton.Instance.PutData(new ReceivedObject(id, bytesRec, bytes));							
					}

					while (commandSocket.DeviceRunning)
					{
						SocketError err = commandSocket.SendStop();
						if (err == SocketError.TimedOut)
						{
							Thread.Sleep(100);
						}
						else
						{
							if (err == SocketError.SocketError)
							{
								commandSocket.Close();
							}
							break;
						}
					}

					Thread.Sleep(100);

				}
				handler.Shutdown(SocketShutdown.Both);
				handler.Close();

				commandSocket.Close();
				device.StopReceiving();
			}
			catch(Exception ex)
			{
				device.StopReceiving();
				Console.WriteLine(ex.Message);
			}
		}

	

	}
}
		