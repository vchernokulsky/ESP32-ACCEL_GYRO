using ImuServer;
using Prism.Commands;
using Prism.Mvvm;

namespace GUI
{
    public class MainControlVM : BindableBase
    {
       
        private MainControlModel model = new MainControlModel();
        private AccelServer accelServer = new AccelServer(15000);

        public MainControlVM(AppType appType)
        {
            model.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            model.IsAccelMeasurementApp = (appType == AppType.AccelerationMeasurement);

            accelServer.Setup(appType, model);
            //accelServer.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            //accelServer.SetPropetyRaise();

            OnContentRendered = new DelegateCommand(() => accelServer.RunServer());
            OnStartClicked = new DelegateCommand(() => accelServer.StartReceiving());
            OnStopClicked = new DelegateCommand(() => accelServer.StopReceiving());
            OnClosing = new DelegateCommand<object>(obj => accelServer.StopServer());

        }

        public DelegateCommand OnContentRendered { get; }
        public DelegateCommand OnStartClicked { get; }
        public DelegateCommand OnStopClicked { get; }
        public DelegateCommand<object> OnClosing { get; }

        public bool NoConnection => model.NoConnection;
        public bool StopEnabled => model.StopEnabled;
        public bool StartEnabled => model.StartEnabled;  
        public bool ExtraDeviceVis => model.ExtraDeviceVis;
        public string LblGroup => model.LblGroup;
        public DeviceModel Device1 => model.Device1;
        public DeviceModel Device2 => model.Device2;
        public DeviceModel Device3 => model.Device3;
        public DeviceModel Device4 => model.Device4;
        public DeviceModel Device5 => model.Device5;
        public DeviceModel Device6 => model.Device6;

        public string UserName { get => model.UserName; set { model.UserName = value; ChartDataSingleton.Instance.userName = value; } }

    }
}
