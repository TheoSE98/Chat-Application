using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataModels
{
    public class ChatService
    {
        private List<User> allUsers;
        private List<User> loggedInUsers;
        private List<ChatRoom> chatRooms;
        private DateTime loginTime;

        public ChatService() 
        {
            allUsers = new List<User>();
            loggedInUsers = new List<User>();
            chatRooms = new List<ChatRoom>();
            GenerateDefaultChatRooms();

        }

        public async Task<bool> Login(string username)
        {
            await Task.Delay(100);

            loginTime = DateTime.Now;

            bool isUnique = false;
            bool currentlyActive = false;

            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Login failed: Username cannot be null or empty.");
            }
            else
            {
                isUnique = !allUsers.Any(user => user.GetUsername() == username);

                if (isUnique)
                {
                    var newUser = new User(username);

                    allUsers.Add(newUser);
                    loggedInUsers.Add(newUser);

                    Console.WriteLine($"User '{username}' has been created and logged in at {loginTime}.");
                }
                else
                {
                        Console.WriteLine("Inside Login count check:");

                        currentlyActive = loggedInUsers.Any(user => user.GetUsername() == username);

                    //error line 54
                    if (!currentlyActive)
                    {
                        //add user to currently active
                        User newUser = allUsers.Find(user => user.GetUsername() == username);

                        loggedInUsers.Add(newUser);

                        Console.WriteLine($"User '{username}' logged into their existing account at {loginTime}.");

                        isUnique = !isUnique;
                    }
                    else
                    {
                        Console.WriteLine($"Login failed: Username '{username}' is not unique. Login attempt at {loginTime}.");
                    }
                }
            }

            return isUnique;
        }

        public async Task<bool> Logout(User pUser)
        {
            await Task.Delay(100);

            bool userExit = false;

            var currUser = allUsers.Find(user => user.GetUsername() == pUser.GetUsername());

            if (currUser != null)
            {
                loggedInUsers.Remove(currUser);

                Console.WriteLine($"User '{pUser.GetUsername()}' has been removed from the active user list.");

                userExit = true;
            }
            else
            {
                Console.WriteLine($"User '{pUser.GetUsername()}' cannot be removed from the active user list.");
            }

            return userExit;
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

        public void UserCreatedChatroom(string roomName, List<string> guestList, bool isPublic)
        {
            Console.WriteLine("Inside user created chatroom method");
            List<User> actualGuests = new List<User>();
            // look up the user objects
            foreach (string userString in guestList)
            {
                Console.WriteLine("userString: " + userString);
                foreach (User user in allUsers)
                {
                    if (user.GetUsername().Equals(userString))
                    {
                        Console.WriteLine("Added a new uuser");
                        actualGuests.Add(user);
                    }
                }
            }
            CreateChatroom(roomName, actualGuests, isPublic);
        }

        public List<ChatRoom> GenerateDefaultChatRooms()
        {

            List<ChatRoom> defaultChatRooms = new List<ChatRoom>();

            for (int i = 1; i <= 5; i++)
            {
                ChatRoom defaultRoom = new ChatRoom
                {
                    Name = $"Default Room {i}",
                    IsPublic = true
                };

                //User user = new User(username);

                // Add the user to the default chat room
                //defaultRoom.AddParticipants(new List<User> { user });

                // Add the default chat room to the main list of chat rooms
                chatRooms.Add(defaultRoom);

                // Add the default chat room to the list of default chat rooms
                defaultChatRooms.Add(defaultRoom);
            }

            return defaultChatRooms;
        }


        //Like might need this not sure ?? 
        public List<ChatRoom> GetChatRooms()
        {
            return chatRooms;
        }

        public void JoinChatRoom(User user, string chatRoomName)
        {
            foreach (ChatRoom room in chatRooms)
            {
                if (room.GetName().Equals(chatRoomName))
                {
                    room.addUser(user);
                    break;
                }
            }
        }

        public void LeaveChatRoom(User user, string chatRoomName)
        {
            foreach (ChatRoom room in chatRooms)
            {
                if (room.GetName().Equals(chatRoomName))
                {
                    room.removeUser(user);
                    break;
                }
            }
        }

        // Message distribution methods
        public void SendMessage(Message message)
        {
            //This works really well but we can you LINQ for faster look up 

            /*string chatRoomName = message.getChatRoomName();
            foreach (ChatRoom room in chatRooms)
            {
                if (room.GetName().Equals(chatRoomName))
                {
                    room.addMessage(message);
                    break;
                }
            }*/
            
            string chatRoomName = message.getChatRoomName();
            Console.WriteLine("we are looking up " + chatRoomName);

            // Use a dictionary for faster lookup if you have many chat rooms
            ChatRoom targetChatRoom = chatRooms.FirstOrDefault(room => room.GetName().Equals(chatRoomName));

            if (targetChatRoom != null)
            {
                targetChatRoom.addMessage(message);
                Console.WriteLine("Added message: " + message.getContent().ToString());
            }
            else
            {
                // Ideally throw some form of FaultException 
                Console.WriteLine($"Chat room '{chatRoomName}' not found. Message not sent.");
            }
        }

        // Get updates methods
        public List<Message> GetMessageUpdates(string chatRoomName)
        {
            foreach (ChatRoom room in chatRooms)
            {
                if (room.GetName().Equals(chatRoomName))
                {
                    Console.WriteLine("Getting updates for charoom " + chatRoomName);
                    return room.getMessageUpdates();
                }
            }
            Console.WriteLine("ERROR: Couldn't find the chatroom");
            // TODO need a fault exeption or something here
            return null;
        }

        public List<ChatRoom> GetChatRoomUpdates(User user)
        {
            List<ChatRoom> allAllowedRooms = new List<ChatRoom>();
            foreach (ChatRoom room in chatRooms)
            {
                if ((room.GetIsPublic()) || room.AmIAllowedIn(user))
                {
                    allAllowedRooms.Add(room);
                    Console.WriteLine("adding room " + room.GetName());
                }
            }
            return allAllowedRooms;
        }

        public IEnumerable<User> GetChatRoomUsers(string roomName)
        {
            foreach (ChatRoom room in chatRooms)
            {
                if (room.GetName().Equals(roomName))
                {
                    return room.GetParticipants();
                }
            }
            // TODO: throw exception
            return null;
        }
    }
}