using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class ChargeInfo
    {
        public int BatteryCharge { get; set; }
    }
    class ChargeGetter
    {
        private const int TIMEOUT_COUNT = 3;
        private int _timeout;
        private DateTime _lastSync;
        private CommandSocket _commandSocket;
        private SocketError _curState;
        private int _errorCount;
        private int _charge;

        public int Charge { get => _charge; set => _charge = value; }

        public ChargeGetter(CommandSocket commandSocket, int timout=5000)
        {
            _commandSocket = commandSocket;
            _timeout = timout;
            _curState = SocketError.TimedOut;
            _errorCount = 0;
            _lastSync = DateTime.Now;
        }

        private int GetCharge()
        {
            int result = -1;

            string content = _commandSocket.SendAskMsg();
            try
            {
                ChargeInfo info = JsonConvert.DeserializeObject<ChargeInfo>(content);
                if(info != null)
                {
                    result = info.BatteryCharge;
                    _lastSync = DateTime.Now;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return result;
            
        }

        public SocketError SyncCharge()
        {
            if (DateTime.Now.Subtract(_lastSync).TotalMilliseconds > _timeout)
            {
                int charge = GetCharge();
                if (charge >= 0)
                {
                    Charge = charge;
                    _curState = SocketError.Success;
                }
                else
                {
                    _errorCount += 1;
                    if (_errorCount >= TIMEOUT_COUNT)
                    {
                        _curState = SocketError.SocketError;
                    } else
                    {
                        _curState = SocketError.TimedOut;
                    }
                }
            }
            return _curState;
        }



    }
}
