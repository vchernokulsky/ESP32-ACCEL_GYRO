using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace AccelServer
{
	public class ConnectionController
	{
		public ConnectionController ()
		{
		}

		public void SendIp()
		{

			int PORT = 9876;
			UdpClient udpClient = new UdpClient();
			udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, PORT));
			var data = Encoding.UTF8.GetBytes("ABCD");
			udpClient.Send(data, data.Length, "255.255.255.255", PORT); 
		}
	}
}

