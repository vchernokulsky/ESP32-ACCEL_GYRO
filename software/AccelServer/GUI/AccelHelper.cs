using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GUI
{
    class AccelHelper
    {
        private AccelServer.AccelServer accelServer;

        public AccelHelper()
        {
            accelServer = new AccelServer.AccelServer(1500);
        }

        public void CheckConnection(Label lbl)
        {
            Thread wifi_check = new Thread(new ThreadStart(() => StartCheckConnection(lbl)));
            wifi_check.Start();
        }

        private void StartCheckConnection(Label lbl)
        {
            Dispatcher.CurrentDispatcher.Invoke(new Action (() => { lbl.Visibility = System.Windows.Visibility.Visible; })); 
            accelServer.CheckConnection();
            Dispatcher.CurrentDispatcher.Invoke(new Action(() => { lbl.Visibility = System.Windows.Visibility.Hidden; }));

        }
    }
}
