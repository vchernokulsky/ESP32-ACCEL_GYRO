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
        private ImuServer.AccelServer accelServer;
        private string title;
        private Brush statusColor;
        private string statusString;


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


        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged("Title");
            }
        }

        public void update()
        {
            StatusColor = GetColor();
            StatusString = GetStatus();
        }

        public Brush StatusColor { get => statusColor; set { statusColor = value; RaisePropertyChanged("StatusColor"); } }
        public string StatusString { get => statusString; set { statusString = value; RaisePropertyChanged("StatusString"); } }
    }
}
