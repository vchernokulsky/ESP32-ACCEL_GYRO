using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class SocketHelper
    {
 
        private int port;

        private Socket localSocket;
        protected Socket socketHandler;

        private bool needReconnect;

       
        public SocketHelper(int port)
        {
            this.port = port;
            localSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            needReconnect = true;
        }

        public void Connect()
        {
            if (needReconnect)
            {
                IPEndPoint ipPoint = NetHelper.GetEndPointIPv4(port);
                localSocket.Bind(ipPoint);
                localSocket.Listen(10);
                socketHandler = localSocket.Accept();
                needReconnect = false;
            }
        }

        public void Close()
        {
            if (!needReconnect && socketHandler != null)
            {
                socketHandler.Shutdown(SocketShutdown.Both);
                socketHandler.Close();
                needReconnect = true;
            }
        }

        public int Receive(byte[] bytes)
        {
            return socketHandler.Receive(bytes);
        }
    }
}
