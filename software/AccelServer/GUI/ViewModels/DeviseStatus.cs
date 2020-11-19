using ImuServer;
using Prism.Mvvm;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace GUI
{
    public enum States
    {
        NotReady, Synchronized, DataReceiving
    }
    public class DeviseStatus : BindableBase
    {
        private int id;
        private AccelServer accelServer;
        private string title;
        private Brush statusColor;
        private string statusString;
        private string deviceIp;
        private string packageCount;


        private Brush[] colors = { Brushes.Red, Brushes.Yellow, Brushes.Green };
        private string[] status = { "не подключено", "подключено", "прием" };

        public DeviseStatus(int id, ImuServer.AccelServer accelServer)
        {
            this.id = id;
            this.accelServer = accelServer;
        }
       
        private Brush GetColor()
        {
            if (accelServer.isReceiving(id))
            {
                return colors[(int)States.DataReceiving];
            }
            if (accelServer.isSynchronized(id))
            {
                return colors[(int)States.Synchronized];
            }
            return colors[(int)States.NotReady];
           
        }

        private string GetStatus()
        {
            if (accelServer.isReceiving(id))
            {
                return status[(int)States.DataReceiving];
            }
            if (accelServer.isSynchronized(id))
            {
                return status[(int)States.Synchronized];
            }
            return status[(int)States.NotReady];

        }

        private string GetIp()
        {
            DeviceInfo devInf = accelServer.GetDeviceInfo(id);
            string ret = "---.---.---.---";
            if (devInf != null && devInf.Ip.Length > 0)
                ret = devInf.Ip;
            return ret;

        }


        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string GetPackageCount()
        {
            return ChartDataSingleton.Instance.ReceivedMeasurementCount(id).ToString();
        }

        public void update()
        {
            StatusColor = GetColor();
            StatusString = GetStatus();
            DeviceIp = GetIp();
            PackageCount = GetPackageCount();
        }

        public Brush StatusColor { get => statusColor; set { statusColor = value; RaisePropertyChanged("StatusColor"); } }
        public string StatusString { get => statusString; set { statusString = value; RaisePropertyChanged("StatusString"); } }
        public string DeviceIp { get => deviceIp; set { deviceIp = value; RaisePropertyChanged("DeviceIp"); } }
        public string PackageCount { get => packageCount; set { packageCount = value; RaisePropertyChanged("PackageCount"); } }
    }
}
