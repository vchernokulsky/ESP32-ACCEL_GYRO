using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace AccelServer
{
	internal static class NetHelper
    {
		private static string interfaceType = "WIFI";

        public static IPEndPoint GetEndPointIPv4(int port, string ipAddr = "")
		{
			IPEndPoint localEndPoint = null;

			NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

			var hostName = Dns.GetHostName();
			var hostEntry = Dns.GetHostEntry(hostName);

			foreach (var address in hostEntry.AddressList)
			{
				if (address.AddressFamily == AddressFamily.InterNetwork)
				{
					localEndPoint = new IPEndPoint(address, port);
					break;
				}
			}
			return localEndPoint;
		}
	}
}
