using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ImuServer;

namespace GUI
{
    enum ReceiverState
	{
		IDLE,
		WAIT_FOR_RECEIVE,
		START_COMMAND,
		STOP_COMMAND,
		RECEIVING,
		FINISHING,
		ABORT
	}

	public class DataReceiver
	{
		private int id;

		private DeviceModel device;
		private SocketHelper receiverSocket;
		private CommandSocket commandSocket;

		private bool needLoop;
		private bool finished;

		private Object _locker = new Object();
		private ReceiverState _state;

        public bool Finished { get => finished; set { finished = value; Console.WriteLine("finsihed = " + finished); } }

        internal ReceiverState State { get => _state; set { lock (_locker) { _state = value; } } }

        public DataReceiver (DeviceModel _device, int id, int port, int port2)
		{
			device = _device;
			this.id = id;

			needLoop = false;
			Finished = true;


			receiverSocket = new SocketHelper(port);
			commandSocket = new CommandSocket(port2);
		}

		public void Abort()
        {
			State = ReceiverState.FINISHING;
			//receiverSocket.Close(true);
			//commandSocket.Close(true);
			//needLoop = false;
        }

		public void WaitFinishing()
        {
            while (!Finished)
            {
				Thread.Sleep(10);
            }
        }
		
		public void Loop()
        {
			while(true)
            {
				switch(State)
                {
					case ReceiverState.IDLE:
						receiverSocket.Connect();
						commandSocket.Connect();
						State = ReceiverState.WAIT_FOR_RECEIVE;
						Finished = false;
						break;

					case ReceiverState.WAIT_FOR_RECEIVE:
                        if (device.NeedToReceive)
							State = ReceiverState.START_COMMAND;
						break;
					case ReceiverState.START_COMMAND:
						//State may be changed to:
						//START_COMMAND || RECEIVING
						State = StartCommand();
						break;
					case ReceiverState.RECEIVING:
						if (device.NeedToReceive)
							Receive();
						else
							State = ReceiverState.STOP_COMMAND;
						break;
					case ReceiverState.STOP_COMMAND:
						//State may be changed to:
						//STOP_COMMAND || WAIT_FOR_RECEIVE
						State = StopCommand();
						break;
					case ReceiverState.FINISHING:
                        receiverSocket.Close(true);
                        commandSocket.Close(true);
						State = ReceiverState.IDLE;
						Finished = true;
						break;


                }
			}
        }

		private ReceiverState StartCommand()
        {
			ReceiverState result = ReceiverState.START_COMMAND;

			commandSocket.Connect();
			SocketError err = commandSocket.SendStart();
			switch (err)
            {
				case SocketError.Success:
					result = ReceiverState.RECEIVING;
					break;
				case SocketError.SocketError:
					commandSocket.Close();
					break;
				case SocketError.TimedOut:
					Thread.Sleep(10);
					break;
            }
			return result;
		}

		private ReceiverState StopCommand()
		{
			ReceiverState result = ReceiverState.STOP_COMMAND;

			commandSocket.Connect();
			SocketError err = commandSocket.SendStop();
			switch (err)
			{
				case SocketError.Success:
					result = ReceiverState.WAIT_FOR_RECEIVE;
					break;
				case SocketError.SocketError:
					commandSocket.Close();
					break;
				case SocketError.TimedOut:
					Thread.Sleep(10);
					break;
			}
			return result;
		}

		private void Receive()
        {
			//Console.WriteLine("receiving data");
			device.StartReceiving();
			byte[] bytes = new byte[5400];
			int bytesRec = receiverSocket.Receive(bytes);
			ChartDataSingleton.Instance.PutData(new ReceivedObject(id, bytesRec, bytes));
		}

		public void StartListening()
		{
			try
			{
				receiverSocket.Connect();
				Console.WriteLine("DataReceiver: Waiting for connection...");

				needLoop = true;
				Finished = false;
				while (needLoop)
				{
					commandSocket.Connect();
					int bytesRec = 0;
					//Console.WriteLine(String.Format("needLoop={0} && device.NeedToReceive = {1} && commandSocket.DeviceRunning = {2}", needLoop, device.NeedToReceive, commandSocket.DeviceRunning));
					while (needLoop && device.NeedToReceive && !commandSocket.DeviceRunning)
					{
						Console.WriteLine("Sending start Command");
						SocketError err = commandSocket.SendStart();
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
			finally 
			{
				Finished = true;
			}
		}

	}
}
		