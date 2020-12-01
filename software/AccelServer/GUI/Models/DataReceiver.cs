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

		private string runMsg = "socket_start";
		private string stopMsg = "socket_stopp";

		private bool device_running = false;


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

			IPEndPoint ipPoint2 = NetHelper.GetEndPointIPv4(port2);
			Socket commandSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{			
				listenSocket.Bind(ipPoint);
				listenSocket.Listen(10);
				Console.WriteLine("DataReceiver: Waiting for connection...");
				commandSocket.Bind(ipPoint2);
				commandSocket.Listen(10);

				Socket handler = listenSocket.Accept();
				Socket comandHandler = commandSocket.Accept();

				while (true)
				{
					
					int bytesRec = 0; 

                    while (device.NeedToReceive && !device_running)
                    {
						comandHandler.Send(Encoding.UTF8.GetBytes(runMsg));
						byte[] bytes = new byte[32];
						bytesRec = comandHandler.Receive(bytes);
						string recvdMsg = Encoding.UTF8.GetString(bytes);
                        if (recvdMsg.StartsWith(runMsg))
                        {
							device_running = true;
                        } else
                        {
							Thread.Sleep(100);
                        }
					}

					while (device.NeedToReceive)
					{
						device.StartReceiving();
						byte[] bytes = new byte[5400];
						bytesRec = handler.Receive(bytes);
						ChartDataSingleton.Instance.PutData(new ReceivedObject(id, bytesRec, bytes));							
					}
					
                    while (device_running)
                    {
                        comandHandler.Send(Encoding.UTF8.GetBytes(stopMsg));
                        byte[] bytes = new byte[32];
                        bytesRec = comandHandler.Receive(bytes);
                        string recvdMsg = Encoding.UTF8.GetString(bytes);
                        if (recvdMsg.StartsWith(stopMsg))
                        {
                            device_running = false;
                        }
                        else
                        {
                            Thread.Sleep(100);
                        }
                    }

					Thread.Sleep(100);

				}
				handler.Shutdown(SocketShutdown.Both);
				handler.Close();
				comandHandler.Shutdown(SocketShutdown.Both);
				comandHandler.Close();
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
		