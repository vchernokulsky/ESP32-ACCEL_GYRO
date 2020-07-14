using AccelServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            int port = 15000;
            IpBroadcaster controller = new IpBroadcaster(port);
            Thread ipBroadcaster = new Thread(new ThreadStart(controller.IpBroadcast));
            ipBroadcaster.Start();

            Thread synchronizer = new Thread(new ThreadStart(DeviceSynchronizer.StartListening));
            synchronizer.Start();
        }

        private void OnBtnStart_Click(object sender, RoutedEventArgs e)
        {
            DataReceiver.running = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            DataReceiver.running = false;
        }

        private void OnWiFiRadio_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void OnEthRadio_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnIfaceSelect_Click(object sender, RoutedEventArgs e)
        {
            if(radioEth.IsChecked.Value)
            {

            }
        }
    }
}
