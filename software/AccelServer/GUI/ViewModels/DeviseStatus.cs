using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUI
{
    public enum States
    {
        NotReady, Synchronized, DataReceiving
    }
    class DeviseStatus : BindableBase
    {
        private int id;
        private AccelServer.AccelServer accelServer;
        private string title;


        private Brush[] colors = { Brushes.Red, Brushes.Yellow, Brushes.Green };

        public DeviseStatus(int id, AccelServer.AccelServer accelServer)
        {
            this.id = id;
            this.accelServer = accelServer;
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



        public Brush GetColor()

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


        //public String Title => title;
        public Brush StatusColor => GetColor();

    }
}
