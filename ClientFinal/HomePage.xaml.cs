using DataModels;
using Microsoft.Win32;
using MyChatServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
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

namespace ClientFinal
{
    public partial class HomePage : Page
    {
        private string username;
        private IChatServer _chatServer;
        private User user { get; set; }
        /*private ObservableCollection<ChatRoom> ChatRooms { get; set; }*/
        /*private ObservableCollection<Message> CurrentMessages { get; set; }*/
        private ChatRoom CurrentChatRoom { get; set; }
        private MainWindow _mainWindow { get; set; }
        private Boolean continueThreads { get; set; }

        public HomePage(string username, IChatServer chatServer, MainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;

            this.username = username;
            usernameTextBox.Text = username;

            user = new User(usernameTextBox.Text);

            ObservableCollection<ChatRoom> ChatRooms = new ObservableCollection<ChatRoom>(chatServer.GetChatRoomUpdates(user));

            _chatServer = chatServer;

            //Bind the chat rooms to the ListView -> this is how it updates THEO 
            chatRoomListView.ItemsSource = ChatRooms;

            continueThreads = true;

            Thread loadingMessages = new Thread(RefreshMessagesPerSecond);
            loadingMessages.Start();

            Thread loadingChatrooms = new Thread(RefreshChatRoomsPerSecond);
            loadingChatrooms.Start();

            Thread loadingChatRoomParticipants = new Thread(RefreshChatRoomMembersPerSecond);
            loadingChatRoomParticipants.Start();
        }

        private void JoinChatRoom_Click(object sender, RoutedEventArgs e)
        {
            //Check if an item is selected in the ListView
            if (chatRoomListView.SelectedItem != null)
            {
                //Get the selected chat room
                //ChatRoom selectedChatRoom = (ChatRoom)chatRoomListView.SelectedItem;
                CurrentChatRoom = (ChatRoom)chatRoomListView.SelectedItem;

                _chatServer.JoinChatRoom(user, CurrentChatRoom.GetName());

                RefreshMessages();

                currentRoomNameTextBox.Text = CurrentChatRoom.GetName();
            }
            else
            {
                MessageBox.Show("Please select a chat room to join.");
            }
        }

        private void LeaveChatRoom_Click(object sender, RoutedEventArgs e)
        {
            if (!Object.ReferenceEquals(CurrentChatRoom, null))
            {
                String leavingRoomName = CurrentChatRoom.GetName();

                Console.WriteLine("1");
                //this isnt calling
                bool test = _chatServer.LeaveChatRoom(user, CurrentChatRoom.GetName());

                Console.WriteLine("******************: " + test);

                Console.WriteLine("2");
                currentRoomNameTextBox.Text = "";
                CurrentChatRoom = null;

                RefreshMessages();
                RefreshChatRooms();
                RefreshChatRoomsMembers();

                MessageBox.Show($"Left chat room: {leavingRoomName}");

                //set current room to null
                //refresh messages
                //refresh chat room members
                //set current chat room text to null done
                //remove user? done
            }
        }

        //logging user off
        private async void LogOff_Click(object sender, RoutedEventArgs e)
        {
            bool deactivateUser = await _chatServer.Logout(user);

            if(deactivateUser)
            {
                if (_mainWindow._mainFrame.NavigationService.CanGoBack)
                {
                    //remove user from server
                    continueThreads = false;
                    /*loadingMessages.Abort();*/
                    /*loadingChatRooms.Abort();*/
                    _mainWindow._mainFrame.NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Cant go back");
                }
            }
            else
            {
                Console.WriteLine("Cant deactivate user");
            }
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
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
                            Type = "msg",
                            Timestamp = DateTime.Now,
                            ChatRoomName = CurrentChatRoom.Name
                        };                       

                        _chatServer.SendMessage(message);

                        messageTextBox.Clear();

                        RefreshMessages();
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

        private void SendFile_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            if (chatRoomListView.SelectedItem != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                try
                {
                    openFileDialog.Filter = "txt files|*.txt|JPeg Image|*.jpg";
                    openFileDialog.Title = "Choose text or image file to save";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == true)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog.FileName;
                        MessageBox.Show(filePath);

                        Message message = new Message
                        {
                            Sender = user,
                            Content = filePath,
                            Type = System.IO.Path.GetExtension(filePath).ToString(),
                            Timestamp = DateTime.Now,
                            ChatRoomName = CurrentChatRoom.Name
                        };

                        _chatServer.SendMessage(message);

                        RefreshMessages();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error: " + exception.Message);
                }

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

                    RefreshChatRooms();
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
            if(CurrentChatRoom != null)
            {
                RefreshMessages();
            }
            else 
            {
                MessageBox.Show("Please select a chat room to receive messages");
            }
        }

        private async void RefreshMessagesPerSecond()
        {
            while(continueThreads)
            {
                try
                {
                    if (!Object.ReferenceEquals(CurrentChatRoom, null))
                    {
                        Task<ObservableCollection<Message>> getChatRoomMessages = new Task<ObservableCollection<Message>>(getMessages);
                        getChatRoomMessages.Start();

                        ObservableCollection<Message> currChatRoomMessages = await getChatRoomMessages;


                        this.Dispatcher.Invoke((Action)(() =>
                        {
                            messageListView.ItemsSource = currChatRoomMessages;
                            messageListView.Items.Refresh();
                        }));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error caught when updating GUI in RefreshMessagesPerSecond(). Exception - " + e.Message);
                }
                await Task.Delay(1000);
            }
        }

        private ObservableCollection<Message> getMessages()
        {
            return new ObservableCollection<Message>(_chatServer.GetMessageUpdates(CurrentChatRoom.Name));
        }

        private void RefreshMessages()
        {
            // is the chatroom needing to be updated itself? or something
            ObservableCollection<Message> CurrentMessages;
            if (Object.ReferenceEquals(CurrentChatRoom, null))
            {
                CurrentMessages = new ObservableCollection<Message>();
            }
            else
            {
                CurrentMessages = new ObservableCollection<Message>(_chatServer.GetMessageUpdates(CurrentChatRoom.Name));
            }
            Console.WriteLine("WE have received " + CurrentMessages.Count + " new messages from the server");

            //does this write all the messages

            messageListView.ItemsSource = CurrentMessages;
            messageListView.Items.Refresh();
        }

        private async void RefreshChatRoomsPerSecond()
        {
            int numRooms = 0;
            while (continueThreads)
            {
                try
                {
                    Task<ObservableCollection<ChatRoom>> getChatRoom = new Task<ObservableCollection<ChatRoom>>(getChatRooms);
                    getChatRoom.Start();

                    ObservableCollection<ChatRoom> currChatRooms = await getChatRoom;

                    if (numRooms < currChatRooms.Count)
                    {
                        numRooms = currChatRooms.Count;

                        this.Dispatcher.Invoke((Action)(() =>
                        {
                            chatRoomListView.ItemsSource = currChatRooms;
                            chatRoomListView.Items.Refresh();
                        }));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error caught when updating GUI in RefreshChatRoomsPerSecond(). Exception - " + e.Message);
                }
                await Task.Delay(1000);
            }
        }

        private ObservableCollection<ChatRoom> getChatRooms()
        {
/*            try
            {*/
                return new ObservableCollection<ChatRoom>(_chatServer.GetChatRoomUpdates(user));
/*            }
            catch (CommunicationObjectFaultedException e)
            {
                Console.WriteLine("Caught Error");
                return null;
            }*/
        }

        private void RefreshChatRooms()
        {
            ObservableCollection<ChatRoom> currChatRooms = new ObservableCollection<ChatRoom>(_chatServer.GetChatRoomUpdates(user));

            chatRoomListView.ItemsSource = currChatRooms;
            chatRoomListView.Items.Refresh();
        }

        private async void RefreshChatRoomMembersPerSecond()
        {
            int numMembers = 0;
            while (continueThreads)
            {
                try
                {
                    if (!Object.ReferenceEquals(CurrentChatRoom, null))
                    {
                        Task<ObservableCollection<User>> taskGetMembers = new Task<ObservableCollection<User>>(getChatRoomMembers);
                        taskGetMembers.Start();
                        Console.WriteLine("Started task to get members");
                        ObservableCollection<User> currChatRoomMembers = await taskGetMembers;

                        Console.WriteLine(user.GetUsername() + ": Check client count");

                        if (numMembers != currChatRoomMembers.Count)
                        {
                            Console.WriteLine(user.GetUsername() + ": clients need to be added");
                            numMembers = currChatRoomMembers.Count;
                            Console.WriteLine(user.GetUsername() + ": count updated to - " + numMembers);

                            this.Dispatcher.Invoke((Action)(() =>
                            {
                                Console.WriteLine(user.GetUsername() + ": refresh clients");
                                clientListView.ItemsSource = currChatRoomMembers;
                                clientListView.Items.Refresh();
                            }));
                        }
                    }
                    else
                    {
                        numMembers = 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error caught when updating GUI in RefreshChatRoomMembersPerSecond(). Exception - " + e.Message);
                }

                await Task.Delay(1000);
            }
        }
        
        private ObservableCollection<User> getChatRoomMembers()
        {
            return new ObservableCollection<User>(_chatServer.GetChatRoomUsers(CurrentChatRoom.Name));
        }

        private void RefreshChatRoomsMembers()
        {
/*            Task<ObservableCollection<ChatRoom>> getChatRoom = new Task<ObservableCollection<ChatRoom>>(getChatRooms);
            getChatRoom.Start();

            ObservableCollection<ChatRoom> currChatRooms = await getChatRoom;

            this.Dispatcher.Invoke((Action)(() =>
            {
                chatRoomListView.ItemsSource = currChatRooms;
                chatRoomListView.Items.Refresh();
            }));*/

            ObservableCollection<User> getChatRoomMembers;

            if (Object.ReferenceEquals(CurrentChatRoom, null))
            {
                getChatRoomMembers = new ObservableCollection<User>();
            }
            else
            {
                getChatRoomMembers = new ObservableCollection<User>(_chatServer.GetChatRoomUsers(CurrentChatRoom.Name));
            }

            clientListView.ItemsSource = getChatRoomMembers;
            clientListView.Items.Refresh();
        }


        private void CreatePrivateChatRoom(object sender, MouseButtonEventArgs e)
        {
            //TODO: make it so you can't start a private chat with yourself
            // TODO send a string user through to the server
            string participant = (sender as TextBlock).Text;
            if (participant.Equals(user.GetUsername()))
            {
                MessageBox.Show("You can't make a private chatroom with yourself.");
            }
            else
            {
                _chatServer.UserCreatedChatroom("Private Chat with " + participant + " and " +  user.GetUsername(), new List<string>() { participant, user.GetUsername() }, false);

                Console.WriteLine("we are trying to create a private chatroom with " + participant + " and " + user.GetUsername());
                MessageBox.Show("Created Private Chat Room with " + participant + " and " + user.GetUsername() + ".");
            }

            RefreshChatRooms();
        }

        private void OpenContentLink(object sender, RoutedEventArgs e)
        {
            String messageContent = (sender as TextBlock).Text;
            MessageBox.Show(messageContent);

            List<Message> ChatRoomMessages = new List<Message>(_chatServer.GetMessageUpdates(CurrentChatRoom.Name));

            foreach (Message message in ChatRoomMessages)
            {
                if (!message.Type.Equals("msg"))
                {
                    if (message.Content.Equals(messageContent))
                    {
                        Process fileopener = new Process();

                        fileopener.StartInfo.FileName = "explorer";
                        fileopener.StartInfo.Arguments = "\"" + message.getContent() + "\"";
                        fileopener.Start();

                        break;
                    }
                }
            }
        }
    }
}