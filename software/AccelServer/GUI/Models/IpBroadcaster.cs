using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GUI.Models
{
    public class IpBroadcaster
	{
		private readonly int _port;
        private bool _running;
        private bool _needRun;

        public IpBroadcaster (int port)
		{
			this._port = port;
			_running = false;
            _needRun = true;
        }

        public void Abort()
        {
            _needRun = false;
        }

        public void WaitFinishing()
        {
            while (_running)
            {
                Thread.Sleep(10);
            }
        }
        public void IpBroadcast()
		{
			var udpClient = new UdpClient();
			var endPoint = NetHelper.GetEndPointIPv4(15000);
			udpClient.Client.Bind(endPoint);

			var data = Encoding.UTF8.GetBytes(endPoint.Address.ToString());
            try
            {
                _running = true;
                while (_needRun)
                {
                    udpClient.Send(data, data.Length, "255.255.255.255", _port);
                    Thread.Sleep(3000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _running = false;
            }
		}
	}
}	

