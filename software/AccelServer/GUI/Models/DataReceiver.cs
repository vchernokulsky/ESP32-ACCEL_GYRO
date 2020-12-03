using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ImuServer;

namespace GUI
{
	public class DataReceiver
	{
		private int id;

		private DeviceModel device;
		private SocketHelper receiverSocket;
		private CommandSocket commandSocket;

		private bool needLoop;
		private bool finished;



		public DataReceiver (DeviceModel _device, int id, int port, int port2)
		{
			device = _device;
			this.id = id;

			needLoop = false;
			finished = true;


			receiverSocket = new SocketHelper(port);
			commandSocket = new CommandSocket(port2);
		}

		public void Abort()
        {
			receiverSocket.Close();
			commandSocket.Close();
			needLoop = false;
        }

		public void WaitFinishing()
        {
            while (!finished)
            {
				Thread.Sleep(10);
            }
        }
			
		public void StartListening()
		{

			
			try
			{
				receiverSocket.Connect();
				Console.WriteLine("DataReceiver: Waiting for connection...");

				needLoop = true;
				finished = false;
				while (needLoop)
				{

					commandSocket.Connect();
					int bytesRec = 0;
					//Console.WriteLine(String.Format("needLoop={0} && device.NeedToReceive = {1} && commandSocket.DeviceRunning = {2}", needLoop, device.NeedToReceive, commandSocket.DeviceRunning));
					while (needLoop && device.NeedToReceive && !commandSocket.DeviceRunning)
                    {
						Console.WriteLine("Sending start Command");
						SocketError err = commandSocket.SendStart();
						if(err == SocketError.TimedOut)
                        {
							Thread.Sleep(100);
                        } 
						else
                        {
							if (err == SocketError.SocketError)
							{
								commandSocket.Close();
							}
							break;
						}					
					}

					if (needLoop && !commandSocket.DeviceRunning)
                    {
						continue;
                    }

					while (needLoop && device.NeedToReceive)
					{
						Console.WriteLine("receiving data");
						device.StartReceiving();
						byte[] bytes = new byte[5400];
						bytesRec = receiverSocket.Receive(bytes);
						ChartDataSingleton.Instance.PutData(new ReceivedObject(id, bytesRec, bytes));							
					}

					while (needLoop && commandSocket.DeviceRunning)
					{
						Console.WriteLine("Sending stop Command");
						SocketError err = commandSocket.SendStop();
						if (err == SocketError.TimedOut)
						{
							Thread.Sleep(100);
						}
						else
						{
							if (err == SocketError.SocketError)
							{
								commandSocket.Close();
							}
							break;
						}
					}

					Thread.Sleep(100);

				}
				finished = true;

				
			}
			catch (ThreadAbortException ex)
			{
				receiverSocket.Close();
				commandSocket.Close();
				device.StopReceiving();
				Console.WriteLine("Thread is aborted and the code is " + ex.ExceptionState);
			}
			catch (Exception ex)
			{
				receiverSocket.Close();
				commandSocket.Close();
				device.StopReceiving();
				Console.WriteLine(ex.Message);
			}
		}

	}
}
		