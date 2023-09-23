using ChatServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
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

namespace ClientFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IChatServer chatServer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IChatServer> channelFactory = new ChannelFactory<IChatServer>(new NetTcpBinding(), "net.tcp://localhost:8100/ChatService");

            // Create a channel to communicate with the service.
            chatServer = channelFactory.CreateChannel();

            // Navigate to the login page.
            _mainFrame.Navigate(new LoginPage(chatServer, this));

        }
    }
}