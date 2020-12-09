using GUI.Models;
using ImuServer;
using Prism.Commands;
using Prism.Mvvm;

namespace GUI
{
    public class MainControlVm : BindableBase
    {
       
        private readonly MainControlModel _model = new MainControlModel();
        private readonly AccelServer _accelServer = new AccelServer(15000);

        public MainControlVm(AppType appType)
        {
            _model.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            _model.IsAccelMeasurementApp = (appType == AppType.AccelerationMeasurement);

            _accelServer.Setup(appType, _model);
            
            OnContentRendered = new DelegateCommand(() => _accelServer.RunServer());
            OnStartClicked = new DelegateCommand(() => _accelServer.StartReceiving());
            OnStopClicked = new DelegateCommand(() => _accelServer.StopReceiving());
            OnClosing = new DelegateCommand<object>(obj => _accelServer.StopServer());

        }

        public DelegateCommand OnContentRendered { get; }
        public DelegateCommand OnStartClicked { get; }
        public DelegateCommand OnStopClicked { get; }
        public DelegateCommand<object> OnClosing { get; }

        public bool NoConnection => _model.NoConnection;
        public bool StopEnabled => _model.StopEnabled;
        public bool StartEnabled => _model.StartEnabled;  
        public bool ExtraDeviceVis => _model.ExtraDeviceVis;
        public string LblGroup => _model.LblGroup;
        public DeviceModel Device1 => _model.Device1;
        public DeviceModel Device2 => _model.Device2;
        public DeviceModel Device3 => _model.Device3;
        public DeviceModel Device4 => _model.Device4;
        public DeviceModel Device5 => _model.Device5;
        public DeviceModel Device6 => _model.Device6;

        public string SessionIdStr => _model.SessionIdStr;

        public string UserName { get => _model.UserName; set { _model.UserName = value; ChartDataSingleton.Instance.userName = value; } }

    }
}
