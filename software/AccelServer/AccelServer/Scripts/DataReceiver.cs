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

		public DataReceiver (int p)
		{
			byteList = new List<byte[]> ();
			port = p;
		}

		public void StartListening() {  
			// Data buffer for incoming data.  

		

			// Establish the local endpoint for the socket.  
			// Dns.GetHostName returns the name of the
			// host running the application.  
			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
			IPAddress ipAddress = ipHostInfo.AddressList[0];  
			IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);  

			// Create a TCP/IP socket.  
			Socket listener = new Socket(ipAddress.AddressFamily,  
				SocketType.Stream, ProtocolType.Tcp );  

			// Bind the socket to the local endpoint and
			// listen for incoming connections.  
			try {  
				listener.Bind(localEndPoint);  
				listener.Listen(1);  

				// Start listening for connections.  
				  
				Console.WriteLine("Waiting for a connection...");  
				// Program is suspended while waiting for an incoming connection.  
				Socket handler = listener.Accept();  
				totalRecv = 0;
				while(running)
				{
					byte[] bytes = new Byte[5400];
					int bytesRec = handler.Receive(bytes); 
					totalRecv += bytesRec;

					//String data = Encoding.UTF8.GetString(bytes,0,bytesRec);  
					// Show the data on the console.  
					Console.WriteLine( "Received {0} bytes", bytesRec);
					Console.WriteLine( "TOTAL RECEIVED {0} bytes", totalRecv);
					byteList.Add(bytes);
				}

				handler.Shutdown(SocketShutdown.Both);  
				handler.Close();  
				  

			} catch (Exception e) {  
				Console.WriteLine(e.ToString());  
			}  
				 
	}
}
}
		