using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace AccelServer
{
	public class ConnectionController
	{
		private int port;
		public ConnectionController (int port)
		{
			this.port = port;
		}

		public void IpBroadcast()
		{
			UdpClient udpClient = new UdpClient();
			udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));
			var host = Dns.GetHostEntry(Dns.GetHostName());
			String addres = (host.AddressList [0]).ToString();
			var data = Encoding.UTF8.GetBytes(addres);
			while(true)
			{
				udpClient.Send(data, data.Length, "255.255.255.255", port);
				Thread.Sleep(3000);
			}

		}
	}
}

