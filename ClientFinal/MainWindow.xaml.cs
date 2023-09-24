using ServerInterface;
using System.ServiceModel;
using System.Windows;

namespace ClientFinal
{
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

            chatServer = channelFactory.CreateChannel();

            _mainFrame.Navigate(new LoginPage(chatServer, this));

        }
    }
}