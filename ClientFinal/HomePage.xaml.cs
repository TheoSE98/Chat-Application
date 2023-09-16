using DataModels;
using MyChatServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<ChatRoom> ChatRooms { get; set; }
        private IChatServer _chatServer;

        public HomePage(string username, List<ChatRoom> chatRooms, IChatServer chatServer)
        {
            InitializeComponent();

            this.username = username;
            usernameTextBox.Text = username;


            ChatRooms = new ObservableCollection<ChatRoom>(chatRooms);
            _chatServer = chatServer;

            //Bind the chat rooms to the ListView
            chatRoomListView.ItemsSource = ChatRooms;
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

        private void LogOff_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateChatRoom_Click(object sender, RoutedEventArgs e)
        {
            string roomName = newChatRoomTextBox.Text;

            // Check if the room name is not empty
            if (!string.IsNullOrEmpty(roomName))
            {
                // Call the server method to create the chat room
                bool roomCreated =  _chatServer.CreateChatroom(roomName, new List<User>(), isPublic: true);

                if (roomCreated)
                {
                    MessageBox.Show($"Chat room '{roomName}' created successfully.");

                    var newChatRoom = new ChatRoom { Name = roomName, IsPublic = true };
                    ChatRooms.Add(newChatRoom);
                }
                else
                {
                    MessageBox.Show($"Chat room '{roomName}' already exists.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a chat room name.");
            }
        }
    }
}