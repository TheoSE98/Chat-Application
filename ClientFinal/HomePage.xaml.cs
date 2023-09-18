﻿using DataModels;
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
        private User user { get; set; }

        //Working here 
        private ObservableCollection<Message> CurrentMessages { get; set; }

        private ChatRoom CurrentChatRoom { get; set; }

        public HomePage(string username, IChatServer chatServer)
        {
            InitializeComponent();

            this.username = username;
            usernameTextBox.Text = username;

            user = new User(usernameTextBox.Text);

            ChatRooms = new ObservableCollection<ChatRoom>(chatServer.GetChatRooms());

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
                //ChatRoom selectedChatRoom = (ChatRoom)chatRoomListView.SelectedItem;
                CurrentChatRoom = (ChatRoom)chatRoomListView.SelectedItem;

                Console.WriteLine(CurrentChatRoom.GetHashCode());

                _chatServer.JoinChatRoom(user, CurrentChatRoom.GetName());

                MessageBox.Show($"Joined chat room: {CurrentChatRoom.GetName()}");
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
            if (chatRoomListView.SelectedItem != null)
            {
                // Get the selected chat room
                //ChatRoom selectedChatRoom = (ChatRoom)chatRoomListView.SelectedItem;

                // Check if the user has selected a chat room
                if (CurrentChatRoom != null)
                {
                    string messageContent = messageTextBox.Text;

                    if (!string.IsNullOrEmpty(messageContent))
                    {
                        // Create a message object
                        Message message = new Message
                        {
                            Sender = user,
                            Content = messageContent,
                            Timestamp = DateTime.Now,
                            ChatRoomName = CurrentChatRoom.Name
                        };

                        //selectedChatRoom.GetMessages().Add(message);
                        // Send the message to the server
                        

                        // Clear the message text box
                        messageTextBox.Clear();

                        _chatServer.SendMessage(message);
                    }
                    else
                    {
                        MessageBox.Show("Please enter a message.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a chat room to send a message.");
                }
            }
            else
            {
                MessageBox.Show("Please select a chat room to send a message.");
            }
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

        private void Receive_Click(object sender, RoutedEventArgs e)
        {
            // is the chatroom needing to be updated itself? or something
            CurrentMessages = new ObservableCollection<Message>(_chatServer.GetMessageUpdates(CurrentChatRoom.Name));
            Console.WriteLine("WE have received " + CurrentMessages.Count + " new messages from the server");

            messageListView.ItemsSource = CurrentMessages;

            messageListView.Items.Refresh();

        }

    }
}