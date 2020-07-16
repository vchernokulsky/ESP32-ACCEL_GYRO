using AccelServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private AccelServer.AccelServer Accel;

        public MainWindow()
        {
            InitializeComponent(); 
        }

        private void OnBtnStart_Click(object sender, RoutedEventArgs e)
        {
            Accel.StartReceiving();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            Accel.StopReceiving();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Loaded");
           
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Console.WriteLine("ContentRendered");
            Accel = new AccelServer.AccelServer(15000);
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            Accel.FinishThreads();
            Console.WriteLine("!!!EXIT!!!");
        }
    }
}
