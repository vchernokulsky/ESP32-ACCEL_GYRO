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
		ERROR_STATE
	}

	public class DataReceiver
	{
		private int id;

		private DeviceModel device;
		private SocketHelper receiverSocket;
		private CommandSocket commandSocket;
		private ChargeGetter _chargeGetter;

		private bool needLoop;
		private bool finished;
        private bool threadFinished;

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
			_chargeGetter = new ChargeGetter(commandSocket);
		}

		public void Abort()
        {

            receiverSocket.Close(true);
            commandSocket.Close(true);
			State = ReceiverState.FINISHING;
			//receiverSocket.Close(true);
			//commandSocket.Close(true);
			//needLoop = false;
        }

        public void Finish()
        {
            receiverSocket.Abort();
            commandSocket.Abort();
			needLoop = false;
        }

		public void WaitFinishing()
        {
            while (!Finished)
            {
				Thread.Sleep(10);
            }
        }

        public void WaitThreadFinishing()
        {
            while (!threadFinished)
            {
                Thread.Sleep(10);
            }
        }

		public void Loop()
        {
            needLoop = true;
            threadFinished = false;

			while (needLoop)
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
                        else 
							AskCommand();
						break;
					case ReceiverState.START_COMMAND:
						//State may be changed to:
						//START_COMMAND || RECEIVING
						State = StartCommand();
						break;
					case ReceiverState.RECEIVING:
						if (device.NeedToReceive)
						{
							if (Receive().Equals(SocketError.SocketError))
								State = ReceiverState.ERROR_STATE;
						}else
                        {
							State = ReceiverState.STOP_COMMAND;
                        }

						break;
					case ReceiverState.STOP_COMMAND:
						//State may be changed to:
						//STOP_COMMAND || WAIT_FOR_RECEIVE
						State = StopCommand();
						break;
					case ReceiverState.FINISHING:

						State = ReceiverState.IDLE;
						Finished = true;
						break;

					case ReceiverState.ERROR_STATE:
						device.SetNotReady();
						Console.WriteLine(@"Wait reconnection");
						Thread.Sleep(10);
						break;


                }
			}
            threadFinished = true;
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

		private void AskCommand()
		{
			commandSocket.Connect();
			SocketError err = _chargeGetter.SyncCharge();
			switch (err)
			{
				case SocketError.Success:
					device.BatteryCharge = _chargeGetter.Charge;
					device.SetSynchronized();
					break;
				case SocketError.SocketError:
					commandSocket.Close();
					device.SetNotReady();
					break;
				case SocketError.TimedOut:
					Thread.Sleep(10);
					break;
			}
		}

		private SocketError Receive()
        {
			SocketError result = SocketError.SocketError;
			//Console.WriteLine("receiving data");
			device.StartReceiving();
			try
			{
				byte[] bytes = new byte[5400];
				int bytesRec = receiverSocket.Receive(bytes);
				ChartDataSingleton.Instance.PutData(new ReceivedObject(id, bytesRec, bytes));
				result = SocketError.Success;
			} catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
            }
			return result;
		}

		

	}
}
		