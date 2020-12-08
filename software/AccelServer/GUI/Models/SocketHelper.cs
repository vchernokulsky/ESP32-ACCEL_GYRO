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
            
            needReconnect = true;
        }

        public void Connect()
        {
            if (needReconnect)
            {
                localSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipPoint = NetHelper.GetEndPointIPv4(port);
                localSocket.Bind(ipPoint);
                localSocket.Listen(10);
                Console.WriteLine("SocketHelper | Wait for connection...");
                socketHandler = localSocket.Accept();
                Console.WriteLine("SocketHelper | Connected");
                needReconnect = false;
            }
        }

        public void Close(bool forceClose=false)
        {
            if ((forceClose || !needReconnect) && socketHandler != null)
            {
                socketHandler.Shutdown(SocketShutdown.Both);
                socketHandler.Close();
                localSocket.Close();
                needReconnect = true;
            }
        }

        public int Receive(byte[] bytes)
        {
            return socketHandler.Receive(bytes);
        }
    }
}
