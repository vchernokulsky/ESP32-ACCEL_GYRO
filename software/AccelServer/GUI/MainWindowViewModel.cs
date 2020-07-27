using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using AccelServer;
using LiveCharts;
using LiveCharts.Wpf;
using Prism.Commands;
using Prism.Mvvm;

namespace GUI
{
    class MainWindowViewModel : BindableBase
    {
        private AccelServer.AccelServer accelServer = new AccelServer.AccelServer(15000);
        
        private DeviseStatus Device1;
        private DeviseStatus Device2;
        private DeviseStatus Device3;
        private DeviseStatus Device4;
        private DeviseStatus Device5;
        private DeviseStatus Device6;

        private ChartControlVM chart1;
        private SpinnerVM spinner;

        public MainWindowViewModel()
        {
            accelServer.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            accelServer.SetPropetyRaise();
            

            OnContentRendered = new DelegateCommand(() => accelServer.RunServer());
            OnStartClicked = new DelegateCommand(() => accelServer.StartReceiving());
            OnStopClicked = new DelegateCommand(() => accelServer.StopReceiving());
            OnClosing = new DelegateCommand<object>(obj => accelServer.StopServer());

           
            Device1 = new DeviseStatus(1, accelServer) { Title = "Устройство №1" };
            Device2 = new DeviseStatus(2, accelServer) { Title = "Устройство №2" };
            Device3 = new DeviseStatus(3, accelServer) { Title = "Устройство №3" };
            Device4 = new DeviseStatus(4, accelServer) { Title = "Устройство №4" };
            Device5 = new DeviseStatus(5, accelServer) { Title = "Устройство №5" };
            Device6 = new DeviseStatus(6, accelServer) { Title = "Устройство №6" };

            spinner = new SpinnerVM(1, 5);

            chart1 = new ChartControlVM(accelServer);

        }

        public DelegateCommand OnContentRendered { get; }
        public DelegateCommand OnStartClicked { get; }
        public DelegateCommand OnStopClicked { get; }
        public DelegateCommand<object> OnClosing { get; }

        public bool NoConnection => accelServer.NoConnection;
        public bool StopEnabled => DataReceiver.running;
        public bool StartEnabled => (!(NoConnection || DataReceiver.running));

      
        public Brush DeviceColor1 => Device1.StatusColor;
        public Brush DeviceColor2 => Device2.StatusColor;
        public Brush DeviceColor3 => Device3.StatusColor;
        public Brush DeviceColor4 => Device4.StatusColor;
        public Brush DeviceColor5 => Device5.StatusColor;
        public Brush DeviceColor6 => Device6.StatusColor;

        public SpinnerVM Spinner { get => spinner; set { spinner = value; RaisePropertyChanged("Spinner"); } }

        public ChartControlVM Chart1 { get => chart1; set { chart1 = value; RaisePropertyChanged("Chart1"); } }
    }
}
