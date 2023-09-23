using System.Windows;
using System.Windows.Controls;
using ServerInterface;

namespace ClientFinal
{
    public partial class LoginPage : Page
    {
        private IChatServer _chatServer;
        private MainWindow _mainWindow;

        public LoginPage(IChatServer chatServer, MainWindow mainWindow)
        {
            InitializeComponent();
            _chatServer = chatServer;
            _mainWindow = mainWindow;    
        }

        private async void Button_LogIn(object sender, RoutedEventArgs e)
        {
            string username = usernameEntryBox.Text;

            if (!string.IsNullOrEmpty(username))
            {

                loginProgressBar.Visibility = Visibility.Visible;

                bool isUnique = await _chatServer.Login(username);

                loginProgressBar.Visibility = Visibility.Collapsed;

                if (isUnique)
                {
                    _mainWindow._mainFrame.NavigationService.Navigate(new HomePage(username, _chatServer, _mainWindow));
                }
                else
                {
                    MessageBox.Show("Username is not unique. Please choose a different username.");
                }
            }
            else
            {
                MessageBox.Show("Username cannot be empty. Please enter a valid username.");
            }
        }
    }
}