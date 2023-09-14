using DataModels;
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

namespace ClientFinal
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private string username;
        private List<ChatRoom> availableChatRooms;

        public HomePage(string username, List<ChatRoom> chatRooms)
        {
            InitializeComponent();

            this.username = username;
            usernameTextBlock.Text = username;

            availableChatRooms = chatRooms;

            //Bind the chat rooms to the ListView
            chatRoomListView.ItemsSource = availableChatRooms;
        }

        private void JoinChatRoom_Click(object sender, RoutedEventArgs e)
        {
            //Check if an item is selected in the ListView
            if (chatRoomListView.SelectedItem != null)
            {
                //Get the selected chat room
                ChatRoom selectedChatRoom = (ChatRoom)chatRoomListView.SelectedItem;

                MessageBox.Show($"Joined chat room: {selectedChatRoom.GetName()}");
            }
            else
            {
                MessageBox.Show("Please select a chat room to join.");
            }
        }
    }
}