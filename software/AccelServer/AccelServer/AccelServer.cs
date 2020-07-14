using System;
using System.Threading;

namespace AccelServer
{
	public class AccelServer
	{
		
		public AccelServer (int broadcasterPort)
		{
			IpBroadcaster controller = new IpBroadcaster(broadcasterPort);
			Thread ipBroadcaster = new Thread(new ThreadStart(controller.IpBroadcast));
			ipBroadcaster.Start();

			Thread synchronizer = new Thread(new ThreadStart(DeviceSynchronizer.StartListening));
			synchronizer.Start();
		}

		public void StartReceiving()
		{
			DataReceiver.running = true;
		}

		public void StopReceiving()
		{
			DataReceiver.running = false;
		}
	}
}

