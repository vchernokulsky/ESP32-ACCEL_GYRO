using GUI;
using ImuServer;
using Prism.Commands;
using Prism.Mvvm;

namespace AnglesMeasurement
{
    internal class MainWindowVm : BindableBase
    {
        private AppType APP_TYPE = AppType.AnglesMeasurement;
        private MainControlVm _mainControlVm;
        public DelegateCommand OnContentRendered { get; }
        public DelegateCommand<object> OnClosing { get; }
        public MainControlVm MainControlVm { get => _mainControlVm; set { _mainControlVm = value; RaisePropertyChanged("MainControlVm"); } }

        public MainWindowVm()
        {
            DBManager.Instance.Init("AnglesMeasurement.sqlite");

            MainControlVm = new MainControlVm(APP_TYPE);
            MainControlVm.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            OnContentRendered = MainControlVm.OnContentRendered;
            OnClosing = MainControlVm.OnClosing;
            RaisePropertyChanged("MainControlVm");
        }
    }
}
