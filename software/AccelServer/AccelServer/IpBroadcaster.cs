using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;

namespace AccelServer
{
	public class IpBroadcaster
	{
		private int port;
		public IpBroadcaster (int port)
		{
			this.port = port;
		}

		public void IpBroadcast()
		{
			UdpClient udpClient = new UdpClient();
			//var endPoint = new IPEndPoint(IPAddress.Parse("192.168.55.116"), port);
			var endPoint = NetHelper.GetEndPointIPv4(15000, "192.168.55.116");
			udpClient.Client.Bind(endPoint);

			var data = Encoding.UTF8.GetBytes("192.168.55.116");
			while(true)
			{
				udpClient.Send(data, data.Length, "255.255.255.255", port);
				Thread.Sleep(3000);
			}
		}
	}
}

