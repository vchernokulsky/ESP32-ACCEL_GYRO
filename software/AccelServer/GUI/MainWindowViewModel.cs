using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class MainWindowViewModel: INotifyPropertyChanged
    {
        private bool startEnabled = false;
        private bool stopEnabled = false;

        public bool StartEnabled
        {
            get
            {
                return startEnabled;
            }

            set
            {
                startEnabled = value;
                NotifyPropertyChanged("StartEnabled");
            }
        }

        public bool StopEnabled
        {
            get
            {
                return stopEnabled;
            }

            set
            {
                stopEnabled = value;
                NotifyPropertyChanged("StopEnabled");
            }
        }



        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
