using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using AccelServer;
using Prism.Commands;
using Prism.Mvvm;

namespace GUI
{
   
    class MainWindowViewModel: BindableBase
    {
        private AccelServer.AccelServer accelServer = new AccelServer.AccelServer(15000);

        public MainWindowViewModel()
        {
            accelServer.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };

            OnContentRendered = new DelegateCommand(() => accelServer.RunServer());

            OnStartClicked = new DelegateCommand(() => accelServer.StartReceiving());
            OnStopClicked = new DelegateCommand(() => accelServer.StopReceiving());
            OnClosing = new DelegateCommand<object>(obj => accelServer.StopServer());
        }

      

        public DelegateCommand OnContentRendered { get; }
        public DelegateCommand OnStartClicked { get; }
        public DelegateCommand OnStopClicked { get; }
        public DelegateCommand<object> OnClosing { get; }

        public bool NoConnection => accelServer.NoConnection;
        public bool StopEnabled => DataReceiver.running;
        public bool StartEnabled => (!(NoConnection || DataReceiver.running));

    }
}
