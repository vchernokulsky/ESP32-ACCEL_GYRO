using Prism.Mvvm;

namespace GUI
{
    public class MainControlModel : BindableBase
    {
        private bool isAccelMeasurementApp;
        private bool noConnection;
        private bool isRunning;
        private bool dataProcessed;
        private int sessionId;
        private string userName;

        private DeviceModel device1;
        private DeviceModel device2;
        private DeviceModel device3;
        private DeviceModel device4;
        private DeviceModel device5;
        private DeviceModel device6;

        public MainControlModel()
        {
            Device1 = new DeviceModel(1, this);
            Device2 = new DeviceModel(2, this);
            Device3 = new DeviceModel(3, this);
            Device4 = new DeviceModel(4, this);
            Device5 = new DeviceModel(5, this);
            Device6 = new DeviceModel(6, this);

            Device1.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            Device2.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            Device3.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            Device4.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            Device5.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            Device6.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
        }
        public bool IsAccelMeasurementApp { get => isAccelMeasurementApp; set { isAccelMeasurementApp = value; RaisePropertyChanged("LblGroup"); RaisePropertyChanged("ExtraDeviceVis"); } }
        public bool NoConnection
        {
            get => noConnection; set
            {
                noConnection = value;
                RaisePropertyChanged("NoConnection");
                RaisePropertyChanged("StartEnabled");
            }
        }
        public bool IsRunning
        {
            get => isRunning; set
            {
                isRunning = value;
                if (value)
                    StartReceiving();
                else
                    StopReceiving();
                RaisePropertyChanged("StartEnabled");
                RaisePropertyChanged("StopEnabled");
            }
        }
        public bool DataProcessing
        {
            get => dataProcessed; set
            {
                if (value != dataProcessed)
                {
                    dataProcessed = value;
                    RaisePropertyChanged("StartEnabled");
                }

            }
        }


        public string LblGroup => (IsAccelMeasurementApp) ? "Груз" : "Бревна";
        public bool ExtraDeviceVis => (IsAccelMeasurementApp) ? true : false;
        public string UserName { get => userName; set => userName = value; }
        public bool StartEnabled => (!(NoConnection || IsRunning || DataProcessing));
        public bool StopEnabled => IsRunning;


        public DeviceModel Device1 { get => device1; set { device1 = value; RaisePropertyChanged("Device1"); } }
        public DeviceModel Device2 { get => device2; set { device2 = value; RaisePropertyChanged("Device2"); } }
        public DeviceModel Device3 { get => device3; set { device3 = value; RaisePropertyChanged("Device3"); } }
        public DeviceModel Device4 { get => device4; set { device4 = value; RaisePropertyChanged("Device4"); } }
        public DeviceModel Device5 { get => device5; set { device5 = value; RaisePropertyChanged("Device5"); } }
        public DeviceModel Device6 { get => device6; set { device6 = value; RaisePropertyChanged("Device6"); } }

        public int SessionId { get => sessionId; set {
                sessionId = value;
                RaisePropertyChanged("SessionIdStr"); } }
        public string SessionIdStr { 
            get {
                if (SessionId == 0)
                    return "-/-";
                else
                    return SessionId.ToString(); 
            } }

        public DeviceModel GetDeviceById(int id)
        {
            if (id == 1)
                return Device1;
            if (id == 2)
                return Device2;
            if (id == 3)
                return Device3;
            if (id == 4)
                return Device4;
            if (id == 5)
                return Device5;
            if (id == 6)
                return Device6;
            return null;
        }

        private void StartReceiving()
        {
            Device1.StartReceiving();
            Device2.StartReceiving();
            Device3.StartReceiving();
            Device4.StartReceiving();
            Device5.StartReceiving();
            Device6.StartReceiving();
        }
        private void StopReceiving()
        {
            Device1.StopReceiving();
            Device2.StopReceiving();
            Device3.StopReceiving();
            Device4.StopReceiving();
            Device5.StopReceiving();
            Device6.StopReceiving();

        }
    }
}
