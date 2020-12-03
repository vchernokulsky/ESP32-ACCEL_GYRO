using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class CommandSocket
    {
        private const string runMsg = "socket_start";
        private const string stopMsg = "socket_stopp";
        private const string askMsg = "socket_is_ok";

        private int port;

        private Socket commandSocket;
        private Socket commandHandler;

        private bool deviceRunning;
        private bool needReconnect;

        public bool DeviceRunning { get => deviceRunning; set => deviceRunning = value; }

        public CommandSocket(int port)
        {
            this.port = port;
            commandSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            deviceRunning = false;
            needReconnect = true;
        }

        public void Connect()
        {
            if (needReconnect)
            {
                IPEndPoint ipPoint = NetHelper.GetEndPointIPv4(port);
                commandSocket.Bind(ipPoint);
                commandSocket.Listen(10);
                commandHandler = commandSocket.Accept();
                needReconnect = false;
            }
        }

        public void Close()
        {
            if(commandHandler != null)
            {
                commandHandler.Shutdown(SocketShutdown.Both);
                commandHandler.Close();
                needReconnect = true;
            }
        }

        public SocketError SendStart()
        {
            return SendMsg(runMsg, true);
        }

        public SocketError SendStop()
        {
            return SendMsg(stopMsg, false);
        }

        private SocketError SendMsg(string sendMsg, bool runningValueOnSuccess)
        {
            string recvdStr = SendReseive(sendMsg);
            if (recvdStr == null)
            {
                Close();
                return SocketError.SocketError;
            }
            if (recvdStr.StartsWith(sendMsg))
            {
                deviceRunning = runningValueOnSuccess;
                return SocketError.Success;
            }
            else
            {
                return SocketError.TimedOut;
            }
        }

        private string SendReseive(string msg)
        {
            string recvdMsg = null;
            try
            {
                commandHandler.Send(Encoding.UTF8.GetBytes(msg));
                byte[] bytes = new byte[32];
                int bytesRec = commandHandler.Receive(bytes);
                recvdMsg = Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return recvdMsg;
        }
    }
}
