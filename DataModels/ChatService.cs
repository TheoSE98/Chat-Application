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
        private DateTime loginTime;

        public ChatService() 
        {
            users = new List<User>();
            chatRooms = new List<ChatRoom>();
        }

        public async Task<bool> Login(string username)
        {
            await Task.Delay(100);

            loginTime = DateTime.Now;

            bool isUnique = false;

            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Login failed: Username cannot be null or empty.");
            }
            else
            {
                isUnique = !users.Any(user => user.GetUsername() == username);

                if (isUnique)
                {
                    var newUser = new User(username);

                    users.Add(newUser);

                    Console.WriteLine($"User '{username}' logged in at {loginTime}.");
                }
                else
                {
                    Console.WriteLine($"Login failed: Username '{username}' is not unique. Login attempt at {loginTime}.");
                }
            }

            return isUnique;
        }

        //Chat room management methods
        public bool CreateChatroom(string roomName, List<User> participants, bool isPublic)
        {
            bool roomExists = chatRooms.Any(room => room.GetName() == roomName);

            if (roomExists)
            {
                Console.WriteLine($"Chat room creation failed: Room '{roomName}' already exists.");
            }
            else
            {
                var newChatRoom = new ChatRoom(roomName, isPublic);
                newChatRoom.AddParticipants(participants);
                chatRooms.Add(newChatRoom);

                Console.WriteLine($"Chat room '{roomName}' created successfully.");
            }

            return !roomExists;
        }

        public List<ChatRoom> GenerateDefaultChatRooms(string username)
        {
            List<ChatRoom> defaultChatRooms = new List<ChatRoom>();

            ChatRoom defaultRoom1 = new ChatRoom("Default Room 1", isPublic: true);
            ChatRoom defaultRoom2 = new ChatRoom("Default Room 2", isPublic: true);

            User user = new User(username);

            // Add the user to these default chat rooms
            defaultRoom1.AddParticipants(new List<User> { user });
            defaultRoom2.AddParticipants(new List<User> { user });
            //defaultRoom2.AddParticipants(new User(username));

            defaultChatRooms.Add(defaultRoom1);
            defaultChatRooms.Add(defaultRoom2);

            return defaultChatRooms;
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

        public List<ChatRoom> GetChatRooms()
        {
            return chatRooms;
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