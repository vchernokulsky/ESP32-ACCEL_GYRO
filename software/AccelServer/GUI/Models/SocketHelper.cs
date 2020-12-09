using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUI
{
    class SocketHelper
    {
 
        private int port;

        private Socket localSocket;
        protected Socket socketHandler;

        private bool needReconnect;

        public ManualResetEvent allDone = new ManualResetEvent(false);


        public SocketHelper(int port)
        {
            this.port = port;
            
            needReconnect = true;
        }

        public void Connect()
        {
            if (!needReconnect) return;
            try
            {
                localSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipPoint = NetHelper.GetEndPointIPv4(port);
                localSocket.Bind(ipPoint);
                localSocket.Listen(10);
                Console.WriteLine("SocketHelper | Wait for connection...");

                allDone.Reset();

                localSocket.BeginAccept(new AsyncCallback(AcceptCallback), localSocket);
                // Wait until a connection is made and processed before continuing.
                allDone.WaitOne();

                Console.WriteLine("SocketHelper | Connected");
                needReconnect = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            // Get the socket that handles the client request.
            try
            {
                allDone.Set();
                Socket listener = (Socket)ar.AsyncState;
                socketHandler = listener.EndAccept(ar);
                socketHandler.ReceiveTimeout = 3000;
            }
            catch (Exception ex)
            {
                allDone.Set();
                Console.WriteLine(ex.Message);
            }
           
        }

        public void Abort()
        {
            if (socketHandler != null && socketHandler.Connected)
            {
                socketHandler.Shutdown(SocketShutdown.Both);
                socketHandler.Close();
            }
            else
            {
                localSocket?.Close();
            }
            needReconnect = true;
        }

        public void Close(bool forceClose=false)
        {
            if ((forceClose || !needReconnect) && socketHandler != null)
            {
                try
                {
                    if(socketHandler.Connected)
                        socketHandler.Shutdown(SocketShutdown.Both);
                    socketHandler.Close();
                    localSocket.Close();
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally {
                    needReconnect = true;
                }
               
            }
        }

        public int Receive(byte[] bytes)
        {
            return socketHandler.Receive(bytes);
        }

        public void Flush()
        {
            try
            {
                if (socketHandler == null || !socketHandler.Connected) return;
                var count = socketHandler.Available;
                var totalReceived = 0;
                var data = new byte[count];
                if (count <= 0) return;
                while (totalReceived < count)
                {
                    totalReceived += socketHandler.Receive(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
