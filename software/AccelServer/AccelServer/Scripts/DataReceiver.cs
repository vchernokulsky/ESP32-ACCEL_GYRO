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
			

		public void TryListen()
		{
			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
			IPAddress ipAddress = ipHostInfo.AddressList[0];  
			// получаем адреса для запуска сокета
			IPEndPoint ipPoint = new IPEndPoint(ipAddress, port);

			// создаем сокет
			Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				// связываем сокет с локальной точкой, по которой будем принимать данные
				listenSocket.Bind(ipPoint);

				// начинаем прослушивание
				listenSocket.Listen(10);

				Console.WriteLine("Сервер запущен. Ожидание подключений...");

				while (true)
				{
					Socket handler = listenSocket.Accept();
					// получаем сообщение
				
					int bytesRec = 0; // количество полученных байтов
					byte[] bytes = new byte[5400]; // буфер для получаемых данных
					totalRecv = 0;
					while(running)
					{
						bytesRec = handler.Receive(bytes);
						totalRecv += bytesRec;
						byteList.Add(bytes);
						Console.WriteLine( "Received {0} bytes", bytesRec);
						Console.WriteLine( "TOTAL RECEIVED {0} bytes", totalRecv);

					}


					// закрываем сокет
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

		/*public void StartListening() {  

			try {  
				listener.Listen(1);    
				Console.WriteLine("Waiting for a connection...");  
				handler = listener.Accept();

				totalRecv = 0;
				while(running)
				{
					byte[] bytes = new Byte[5400];
					int bytesRec = handler.Receive(bytes); 
					totalRecv += bytesRec;

					Console.WriteLine( "Received {0} bytes", bytesRec);
					Console.WriteLine( "TOTAL RECEIVED {0} bytes", totalRecv);
					byteList.Add(bytes);
				}
				handler.Shutdown(SocketShutdown.Both);

				handler.Disconnect(true);
				handler.Close();
				if (handler.Connected)
					Console.WriteLine("We're still connnected");
				else
					Console.WriteLine("We're disconnected");					
			} catch (Exception e) {  
				Console.WriteLine(e.ToString());  
			}

			Console.WriteLine("FINISHED");
				 
	}
	*/
}
}
		