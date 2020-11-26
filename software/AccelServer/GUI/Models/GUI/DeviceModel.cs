using ImuServer;
using Prism.Mvvm;
using System.Windows.Media;

namespace GUI
{
    public enum States
    {
        NotReady, Synchronized, DataReceiving
    }
    public class DeviceModel : BindableBase
    {
        private int id;
        private States status;

        private string title;
        private string deviceIp;
        private string packageCount;

        private MainControlModel parent;
        private Brush[] colors = { Brushes.Red, Brushes.Yellow, Brushes.Green };
        private string[] statusStringArr = { "не подключено", "подключено", "прием" };

        public DeviceModel(int id, MainControlModel model)
        {
            parent = model;
            this.id = id;
            Title = string.Format("Устройство №{0}", id);
            DeviceIp = "---.---.---.---";
            Status = States.NotReady;
            PackageCount = "0";
        }
        
        public string Title { get { return title; } set { title = value; RaisePropertyChanged("Title"); } }
        public States Status
        {
            get => status; set
            {             
                status = value;
                RaisePropertyChanged("StatusColor");
                RaisePropertyChanged("StatusString");
       
            }
        }
        public Brush StatusColor => colors[(int)Status];
        public string StatusString => statusStringArr[(int)Status];
        public string DeviceIp { get => deviceIp; set { deviceIp = value; RaisePropertyChanged("DeviceIp"); } }
        public string PackageCount { get => packageCount; set { packageCount = value; RaisePropertyChanged("PackageCount"); } }

        public void StartReceiving()
        {
            if (Status == States.Synchronized)
                Status = States.DataReceiving;
        }

        public void StopReceiving()
        {
            if (Status == States.DataReceiving)
                Status = States.Synchronized;
        }

        public bool NeedToReceive => parent.IsRunning;

    }
}
