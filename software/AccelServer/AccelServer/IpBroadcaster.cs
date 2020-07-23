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
			var endPoint = NetHelper.GetEndPointIPv4(15000);
			udpClient.Client.Bind(endPoint);

			var data = Encoding.UTF8.GetBytes(endPoint.Address.ToString());
			try
			{
				while (true)
				{
					udpClient.Send(data, data.Length, "255.255.255.255", port);
					Thread.Sleep(3000);
				}
			} 
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}

