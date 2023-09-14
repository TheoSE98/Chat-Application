using System;
using System.Collections.Generic;
using System.Linq;
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
using DataModels;
using MyChatServer;

namespace ClientFinal
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private IChatServer _chatServer;
        private MainWindow _mainWindow; 


        public LoginPage(IChatServer chatServer, MainWindow mainWindow)
        {
            InitializeComponent();
            //ChatService = new ChatService();
            _chatServer = chatServer;
            _mainWindow = mainWindow;
        }

        private void Button_LogIn(object sender, RoutedEventArgs e)
        {
            string username = usernameEntryBox.Text;

            if (!string.IsNullOrEmpty(username))
            {
                bool isUnique = _chatServer.Login(username);

                if (isUnique)
                {
                    //Navigate to the home page and pass the username
                    _mainWindow._mainFrame.NavigationService.Navigate(new HomePage(username));
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