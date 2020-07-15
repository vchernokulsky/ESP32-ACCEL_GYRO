using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace AccelServer
{
	internal static class NetHelper
    {
		
        public static IPEndPoint GetEndPointIPv4(int port)
		{
			IPEndPoint localEndPoint = null;
			IPAddress address = null;
			foreach(NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
			{
				if(ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
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