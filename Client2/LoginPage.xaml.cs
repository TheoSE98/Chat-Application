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

namespace Client2
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public void Button_LogIn(object sender, EventArgs e)
        {
            String username;
            username = usernameEntryBox.Text;

            /*            NavigationContext navigationContext;
                        string id = navigationContext.Parameters["ID"];
            */

            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }
    }

}