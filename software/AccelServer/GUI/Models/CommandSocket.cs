using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class CommandSocket : SocketHelper
    {
        private const string runMsg = "socket_start";
        private const string stopMsg = "socket_stopp";
        private const string askMsg = "socket_is_ok";

        private bool deviceRunning;
 

        public bool DeviceRunning { get => deviceRunning; set => deviceRunning = value; }

        public CommandSocket(int port): base(port)
        {

            this.deviceRunning = false;
        }

      
        public SocketError SendStart()
        {
            return SendMsg(runMsg, true);
        }

        public SocketError SendStop()
        {
            return SendMsg(stopMsg, false);
        }

        public string SendAskMsg()
        {
            return SendReceive(askMsg);
        }

        private SocketError SendMsg(string sendMsg, bool runningValueOnSuccess)
        {
            string recvdStr = SendReceive(sendMsg);
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

        private string SendReceive(string msg)
        {
            string recvdMsg = null;
            try
            {
                
                socketHandler.Send(Encoding.UTF8.GetBytes(msg));
                byte[] bytes = new byte[32];
                int bytesRec = socketHandler.Receive(bytes);
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
