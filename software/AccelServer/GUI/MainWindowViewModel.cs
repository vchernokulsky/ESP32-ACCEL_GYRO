using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using AccelServer;
using Prism.Commands;
using Prism.Mvvm;

namespace GUI
{
   
    class MainWindowViewModel: BindableBase
    {
        private AccelServer.AccelServer accelServer = new AccelServer.AccelServer(15000);

        public MainWindowViewModel()
        {
            accelServer.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            OnContentRendered = new DelegateCommand(() => accelServer.CheckConnection());
        }

        public DelegateCommand OnContentRendered { get; }

        public bool NoConnection => accelServer.NoConnection;









        private bool startEnabled = true;
        private bool stopEnabled = false;

        private String lbl = "";
        public String Lbl
        {
            get
            {
                return lbl;
            }

            set
            {
                lbl = value;
                RaisePropertyChanged("Lbl");
            }
        }

        //private RelayCommand _onContentRendered;
        //public RelayCommand onContentRendered
        //{
        //    get
        //    {
        //        return _onContentRendered ?? (_onContentRendered = new RelayCommand(obj => test()));
        //    }
        //}

        public void test()
        {
            for (int i = 0; i < 10; i++)
            {
                Lbl = i.ToString();
                Thread.Sleep(500);
            }
        }

        public bool StartEnabled
        {
            get
            {
                return startEnabled;
            }

            set
            {
                startEnabled = value;
                RaisePropertyChanged("StartEnabled");
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
                RaisePropertyChanged("StopEnabled");
            }
        }


    }
}
