using ImuServer;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Media;

namespace GUI
{
    public class MainControlVM: BindableBase
    {
        private AccelServer accelServer = new AccelServer(15000);

        private DeviseStatus Device1;
        private DeviseStatus Device2;
        private DeviseStatus Device3;
        private DeviseStatus Device4;
        private DeviseStatus Device5;
        private DeviseStatus Device6;

        private ChartControlVM chart1;
        private ChartControlVM chart2;
        private ChartControlVM chart3;
        private ChartControlVM chart4;
        private ChartControlVM chart5;
        private ChartControlVM chart6;


        public MainControlVM(AppType appType)
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

            chart1 = new ChartControlVM(1, appType);
            chart2 = new ChartControlVM(2, appType);
            chart3 = new ChartControlVM(3, appType);
            chart4 = new ChartControlVM(4, appType);
            chart5 = new ChartControlVM(5, appType);
            chart6 = new ChartControlVM(6, appType);

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

        public ChartControlVM Chart1 { get => chart1; set { chart1 = value; RaisePropertyChanged("Chart1"); } }
        public ChartControlVM Chart2 { get => chart2; set { chart1 = value; RaisePropertyChanged("Chart2"); } }
        public ChartControlVM Chart3 { get => chart3; set { chart1 = value; RaisePropertyChanged("Chart3"); } }
        public ChartControlVM Chart4 { get => chart4; set { chart1 = value; RaisePropertyChanged("Chart4"); } }
        public ChartControlVM Chart5 { get => chart5; set { chart1 = value; RaisePropertyChanged("Chart5"); } }
        public ChartControlVM Chart6 { get => chart6; set { chart1 = value; RaisePropertyChanged("Chart6"); } }
    }
}
