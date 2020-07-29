using Prism.Commands;
using Prism.Mvvm;
using GUI;
namespace AccelerationMeasurement
{
    class MainWindowVM : BindableBase
    {
        private AppType APP_TYPE = AppType.AccelerationMeasurement;
        private MainControlVM mainControlVM;
        public DelegateCommand OnContentRendered { get; }
        public DelegateCommand<object> OnClosing { get; }
        public MainControlVM MainControlVM { get => mainControlVM; set { mainControlVM = value; RaisePropertyChanged("MainControlVM"); } }

        public MainWindowVM()
        {            
            MainControlVM = new MainControlVM(APP_TYPE);
            MainControlVM.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            OnContentRendered = MainControlVM.OnContentRendered;
            OnClosing = MainControlVM.OnClosing;
            RaisePropertyChanged("MainControlVM");

        }
    }
}
