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
            List<string> defaultRoomNames = new List<string>
            {
                "Default Room 1",
                "Default Room 2",
                "Default Room 3",
                "Default Room 4",
                "Default Room 5"
            };

            bool roomExists = chatRooms.Any(room => room.GetName() == roomName || defaultRoomNames.Contains(roomName));

            if (roomExists)
            {
                Console.WriteLine($"Chat room creation failed: Room '{roomName}' already exists.");
            }
            else
            {
                var newChatRoom = new ChatRoom
                {
                    Name = roomName,
                    IsPublic = isPublic
                };
                newChatRoom.AddParticipants(participants);
                chatRooms.Add(newChatRoom);

                Console.WriteLine($"Chat room '{roomName}' created successfully.");
            }

            return !roomExists;
        }

        public List<ChatRoom> GenerateDefaultChatRooms(string username)
        {

            List<ChatRoom> defaultChatRooms = new List<ChatRoom>();

            for (int i = 1; i <= 5; i++)
            {
                ChatRoom defaultRoom = new ChatRoom
                {
                    Name = $"Default Room {i}",
                    IsPublic = true
                };

                User user = new User(username);

                // Add the user to the default chat room
                defaultRoom.AddParticipants(new List<User> { user });

                // Add the default chat room to the main list of chat rooms
                chatRooms.Add(defaultRoom);

                // Add the default chat room to the list of default chat rooms
                defaultChatRooms.Add(defaultRoom);
            }

            return defaultChatRooms;
        }

        // It doesnt say we need it but kinda makes sense to have it (Drops all users aswell from the room)
        // I might be missing some logic so feel free to refactor it....
        public bool RemoveChatRoom(string roomName)
        {
            var chatRoomToRemove = chatRooms.FirstOrDefault(room => room.GetName() == roomName);

            if (chatRoomToRemove != null)
            {
            
                chatRooms.Remove(chatRoomToRemove);

                // Remove all participants from the chat room -> Hopefully my thinking is ok here 
                chatRoomToRemove.RemoveAllParticipants();

                Console.WriteLine($"Chat room '{roomName}' removed successfully.");
            }
            else
            {
                Console.WriteLine($"Chat room '{roomName}' not found. Removal failed.");
            }

            return chatRoomToRemove != null;
        }

        //Like might need this not sure ?? 
        public List<ChatRoom> GetChatRooms()
        {
            return chatRooms;
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