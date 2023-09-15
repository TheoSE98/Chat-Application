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
                var newChatRoom = new ChatRoom 
                { 
                    Name = roomName,
                    IsPublic = isPublic 
                };
                    //(roomName, isPublic);
                newChatRoom.AddParticipants(participants);
                chatRooms.Add(newChatRoom);

                Console.WriteLine($"Chat room '{roomName}' created successfully.");
            }

            return !roomExists;
        }

        //LOGIC HERE MUST BE PRETTY COOKED 
        public List<ChatRoom> GenerateDefaultChatRooms(string username)
        {

            ChatRoom defaultRoom1 = new ChatRoom
            {
                Name = "Default Room 1",
                IsPublic = true
            };

            ChatRoom defaultRoom2 = new ChatRoom
            {
                Name = "Default Room 2",
                IsPublic = true
            };

            ChatRoom defaultRoom3 = new ChatRoom
            {
                Name = "Default Room 3",
                IsPublic = true
            };

            ChatRoom defaultRoom4 = new ChatRoom
            {
                Name = "Default Room 4",
                IsPublic = true
            };

            ChatRoom defaultRoom5 = new ChatRoom
            {
                Name = "Default Room 5",
                IsPublic = true
            };

            User user = new User(username);

            // Add the user to these default chat rooms
            defaultRoom1.AddParticipants(new List<User> { user });
            defaultRoom2.AddParticipants(new List<User> { user });
            defaultRoom3.AddParticipants(new List<User> { user });
            defaultRoom4.AddParticipants(new List<User> { user });
            defaultRoom5.AddParticipants(new List<User> { user });
            //defaultRoom2.AddParticipants(new User(username));

            chatRooms.Add(defaultRoom1);
            chatRooms.Add(defaultRoom2);
            chatRooms.Add(defaultRoom3);
            chatRooms.Add(defaultRoom4);
            chatRooms.Add(defaultRoom5);

            return chatRooms;
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