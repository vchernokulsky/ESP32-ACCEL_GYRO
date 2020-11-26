﻿using ImuServer;
using Prism.Commands;
using Prism.Mvvm;

namespace GUI
{
    public class MainControlVM : BindableBase
    {
        private AccelServer accelServer = new AccelServer(15000);

        private DeviceModel device1;
        private DeviceModel device2;
        private DeviceModel device3;
        private DeviceModel device4;
        private DeviceModel device5;
        private DeviceModel device6;

        private string userName;

        private bool extraDeviceVis;
        private string lblGroup;



        public MainControlVM(AppType appType)
        {
            accelServer.SetAppType(appType);
            accelServer.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            accelServer.SetPropetyRaise();


            OnContentRendered = new DelegateCommand(() => accelServer.RunServer());
            OnStartClicked = new DelegateCommand(() => accelServer.StartReceiving());
            OnStopClicked = new DelegateCommand(() => accelServer.StopReceiving());
            OnClosing = new DelegateCommand<object>(obj => accelServer.StopServer());


            Device1 = new DeviceModel(1, accelServer) { Title = "Устройство №1" };
            Device2 = new DeviceModel(2, accelServer) { Title = "Устройство №2" };
            Device3 = new DeviceModel(3, accelServer) { Title = "Устройство №3" };
            Device4 = new DeviceModel(4, accelServer) { Title = "Устройство №4" };
            Device5 = new DeviceModel(5, accelServer) { Title = "Устройство №5" };
            Device6 = new DeviceModel(6, accelServer) { Title = "Устройство №6" };

            Device1.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            Device2.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            Device3.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            Device4.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            Device5.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            Device6.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };

            ExtraDeviceVis = (appType == AppType.AccelerationMeasurement) ? true : false;
            LblGroup = (appType == AppType.AccelerationMeasurement) ? "Груз" : "Бревна";

        }

        public DelegateCommand OnContentRendered { get; }
        public DelegateCommand OnStartClicked { get; }
        public DelegateCommand OnStopClicked { get; }
        public DelegateCommand<object> OnClosing { get; }

        public bool NoConnection => accelServer.NoConnection;
        public bool StopEnabled { get { 
                RaisePropertyChanged("Device1");
                RaisePropertyChanged("Device2");
                RaisePropertyChanged("Device3");
                RaisePropertyChanged("Device4");
                RaisePropertyChanged("Device5");
                RaisePropertyChanged("Device6");
                return DataReceiver.running; } } 
        public bool StartEnabled => (!(NoConnection || DataReceiver.running));




        public string UserName { get => userName; set { userName = value; AccelServer.UserName = value; } }

        public bool ExtraDeviceVis { get => extraDeviceVis; set { extraDeviceVis = value; RaisePropertyChanged("ExtraDeviceVis"); } }

        public string LblGroup { get => lblGroup; set { lblGroup = value; RaisePropertyChanged("LblGroup"); } }

        public DeviceModel Device1 { get { device1.update(); return device1; } set { device1 = value; RaisePropertyChanged("Device1"); } }
        public DeviceModel Device2 { get { device2.update(); return device2; } set { device2 = value; RaisePropertyChanged("Device2"); } }
        public DeviceModel Device3 { get { device3.update(); return device3; } set { device3 = value; RaisePropertyChanged("Device3"); } }
        public DeviceModel Device4 { get { device4.update(); return device4; } set { device4 = value; RaisePropertyChanged("Device4"); } }
        public DeviceModel Device5 { get { device5.update(); return device5; } set { device5 = value; RaisePropertyChanged("Device5"); } }
        public DeviceModel Device6 { get { device6.update(); return device6; } set { device6 = value; RaisePropertyChanged("Device6"); } }

    }
}
