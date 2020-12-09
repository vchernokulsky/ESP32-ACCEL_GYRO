using System;
using System.Threading;

namespace GUI.Models
{
    public class WifiChecker
    {
        private bool _isRunning;
        private bool _needRun;
        private readonly MainControlModel _model;

        public WifiChecker(MainControlModel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _isRunning = false;
            _needRun = true;
        }

        public void Abort()
        {
            _needRun = false;
        }

        public void WaitFinishing()
        {
            while (_isRunning)
            {
                Thread.Sleep(10);
            }
        }

        private bool IsConnected()
        {
            var result = false;
            try
            {
                var endpoint = NetHelper.GetEndPointIPv4(10000);
                result = endpoint != null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public bool WaitUntilConnected()
        {
            _model.NoConnection = true;
            _isRunning = true;
            while (!IsConnected() && _needRun)
            {
                Thread.Sleep(3000);
            }
            _model.NoConnection = false;
            _isRunning = false;
            return _needRun;
        }


        public void WifiMonitor()
        {
            _isRunning = true;
            while (_needRun)
            {
                _model.NoConnection = !IsConnected();
                Thread.Sleep(10000);
            }

            _isRunning = false;
        }
    }
}