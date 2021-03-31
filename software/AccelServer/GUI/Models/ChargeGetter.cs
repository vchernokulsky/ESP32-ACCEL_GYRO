using System;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace GUI.Models
{
    class ChargeInfo
    {
        public int BatteryCharge { get; set; }
    }
    class ChargeGetter
    {
        private const int TimeoutCount = 3;
        private readonly int _timeout;
        private DateTime _lastSync;
        private readonly CommandSocket _commandSocket;
        private SocketError _curState;
        private int _errorCount;

        public int Charge { get; set; }

        public ChargeGetter(CommandSocket commandSocket, int timeout=5000)
        {
            _commandSocket = commandSocket;
            _timeout = timeout;
            _curState = SocketError.Success;
            _errorCount = 0;
            _lastSync = DateTime.Now;
        }

        private int GetCharge()
        {
            var content = _commandSocket.SendAskMsg();
            var info = JsonConvert.DeserializeObject<ChargeInfo>(content);
            if(info != null)
            {
                var result = info.BatteryCharge;
                _lastSync = DateTime.Now;
                return result;
            }
            else
            {
                throw new Exception("no Json");
            }
        }

        public void Reset()
        {
            _errorCount = 0;
            _curState = SocketError.TimedOut;
        }

        public SocketError SyncCharge()
        {
            //if (DateTime.Now.Subtract(_lastSync).TotalMilliseconds > _timeout)
            var ms = DateTime.Now.Subtract(_lastSync).TotalMilliseconds;
            if (ms < _timeout) return _curState;
            try
            {
                var charge = GetCharge();
                Charge = charge;
                _curState = SocketError.Success;
                _errorCount = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _errorCount += 1;
                _curState = _errorCount >= TimeoutCount ? SocketError.SocketError : SocketError.TimedOut;
            }
            return _curState;
        }



    }
}
