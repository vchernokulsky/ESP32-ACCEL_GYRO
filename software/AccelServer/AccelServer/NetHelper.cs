using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ImuServer
{
	internal static class NetHelper
    {
		
        public static IPEndPoint GetEndPointIPv4(int port, string ipAddr="192.168.55.116")
		{
			IPEndPoint localEndPoint = null;
			IPAddress address = null;

			if (ipAddr != null)
			{
				try
				{
					address = IPAddress.Parse(ipAddr);
				}
				catch (Exception ex)
				{
					address = null;
					Console.WriteLine(ex.Message);
				}
			}
			else
			{

				foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
				{
					if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
					{
						Console.WriteLine(ni.Name);
						foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
						{
							if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
							{
								address = ip.Address;
								Console.WriteLine(ip.Address.ToString());
							}
						}
					}
				}
			}

			if (address == null) {
				Console.WriteLine ("WIFI connetion not found");
			} else {
				Console.WriteLine("IP-address: {0}", address.ToString());
				localEndPoint = new IPEndPoint(address, port);
			}
			return localEndPoint;
		}


}
}