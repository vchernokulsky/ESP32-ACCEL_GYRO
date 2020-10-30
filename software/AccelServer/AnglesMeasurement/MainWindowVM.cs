using GUI;
using ImuServer;
using Prism.Commands;
using Prism.Mvvm;

namespace AnglesMeasurement
{
    class MainWindowVM : BindableBase
    {
        private AppType APP_TYPE = AppType.AnglesMeasurement;
        private MainControlVM mainControlVM;
        public DelegateCommand OnContentRendered { get; }
        public DelegateCommand<object> OnClosing { get; }
        public MainControlVM MainControlVM { get => mainControlVM; set { mainControlVM = value; RaisePropertyChanged("MainControlVM"); } }

        public MainWindowVM()
        {
            DBManager.Instance.Init("AnglesMeasurement.sqlite");

            MainControlVM = new MainControlVM(APP_TYPE);
            MainControlVM.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            OnContentRendered = MainControlVM.OnContentRendered;
            OnClosing = MainControlVM.OnClosing;
            RaisePropertyChanged("MainControlVM");
        }
    }
}
