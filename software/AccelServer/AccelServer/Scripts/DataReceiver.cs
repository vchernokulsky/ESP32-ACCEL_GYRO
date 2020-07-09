using System;
using System.Net;  
using System.Net.Sockets;  
using System.Text; 
using System.Collections.Generic;

namespace AccelServer
{
	public class DataReceiver
	{
		public static bool running;
		private List<byte[]> byteList;
		private int port;
		private int totalRecv;

		public DataReceiver (int port)
		{
			byteList = new List<byte[]> ();
			this.port = port;
		}
			

		public void StartListening()
		{
			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
			IPAddress ipAddress = ipHostInfo.AddressList[0];  
			IPEndPoint ipPoint = new IPEndPoint(ipAddress, port);
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
					byte[] bytes = new byte[5400]; 
					totalRecv = 0;
					while(running)
					{
						bytesRec = handler.Receive(bytes);
						totalRecv += bytesRec;
						byteList.Add(bytes);
						Console.WriteLine( "Received {0} bytes", bytesRec);
						Console.WriteLine( "TOTAL RECEIVED {0} bytes", totalRecv);

					}
						
					handler.Shutdown(SocketShutdown.Both);
					handler.Close();

					if(byteList.Count > 0)
					{
						Console.WriteLine( "byteList len {0} ", byteList.Count);
						byteList = new List<byte[]>();
					} else {
						Console.WriteLine( "=== STOPPED ===");
					}
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

	}
}
		