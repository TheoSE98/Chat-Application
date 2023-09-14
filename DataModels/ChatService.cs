using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class ChatService
    {
        private List<User> users; 
        private List<ChatRoom> chatRooms;

        public ChatService() 
        {
            users = new List<User>();
            chatRooms = new List<ChatRoom>();
        }

        public bool Login(string username)
        {
            DateTime loginTime = DateTime.Now;

            bool isUnique = false;

            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Login failed: Username cannot be null or empty.");
            }
            else
            {
                // Check if the username is unique
                isUnique = !users.Any(user => user.GetUsername() == username);

                if (isUnique)
                {
                    // Create a new user with the provided username
                    var newUser = new User(username);

                    // Add the new user to the list of users
                    users.Add(newUser);

                    // Display a success message
                    Console.WriteLine($"User '{username}' logged in at {loginTime}.");
                }
                else
                {
                    // Display an error message for non-unique usernames and the login timestamp
                    Console.WriteLine($"Login failed: Username '{username}' is not unique. Login attempt at {loginTime}.");
                }
            }

            return isUnique;
        }

        // Chat room management methods
        public bool CreateChatroom(string roomName, List<User> participants, bool isPublic)
        {
            // Check if a chat room with the same name already exists
            bool roomExists = chatRooms.Any(room => room.GetName() == roomName);

            if (roomExists)
            {
                // Display an error message for existing chat rooms
                Console.WriteLine($"Chat room creation failed: Room '{roomName}' already exists.");
            }
            else
            {
                // Create a new chat room and add participants
                var newChatRoom = new ChatRoom(roomName, isPublic);
                newChatRoom.AddParticipants(participants);
                chatRooms.Add(newChatRoom);

                // Display a success message for the created chat room
                Console.WriteLine($"Chat room '{roomName}' created successfully.");
            }

            // Return whether the chat room was created successfully
            return !roomExists;
        }

        public bool JoinChatRoom(string username, string roomName)
        {
            throw new NotImplementedException();
        }

        public bool LeaveChatRoom(string username, string roomName)
        {
            throw new NotImplementedException();
        }

        // Message distribution methods
        public void SendMessage(Message message)
        {
            throw new NotImplementedException();
        }

        // Get updates methods
        public IEnumerable<Message> GetMessageUpdates(string roomName, int lastMessageId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChatRoom> GetChatRoomUpdates(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetChatRoomUsers(string roomName)
        {
            throw new NotImplementedException();
        }
    }
}