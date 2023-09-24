using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;


namespace DataModels
{
    [DataContract]
    public class ChatRoom
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        private List<User> participants;
        [DataMember]
        private List<Message> messages;
        [DataMember]
        public bool IsPublic { get; set; }
        [DataMember]
        private List<User> guestList;

        public ChatRoom()
        {
            participants = new List<User>();
            messages = new List<Message>();
            guestList = new List<User>();
        }

        public string GetName()
        {
            return Name;
        }

        public List<User> GetParticipants()
        {
            return participants;
        }

        public void addMessage(Message message)
        {
            // TODO: add some error handling ???
            this.messages.Add(message);
        }

        public List<Message> getMessageUpdates()
        {
            return messages;
        }

        public List<Message> GetMessages()
        {
            return messages;
        }

        public bool GetIsPublic()
        {
            return IsPublic;
        }

        public List<User> GetGuestList()
        {
            return guestList;
        }

        public bool AmIAllowedIn(User user)
        {
            if (IsPublic)
            {
                return true;
            }
            else
            {
                foreach (User guest in guestList)
                {
                    Console.WriteLine("guests: " + guest.GetUsername());
                }
                Console.WriteLine("user: " + user.GetUsername());
                // THIS LINE IS PROBLEMATIC
                if (guestList.Any(guest => guest.GetUsername().Equals(user.GetUsername())))
                {
                    Console.WriteLine("You're allowed in!");
                    return true;
                }
                Console.WriteLine("You're not allowed in");
                return false;
            }
        }
        
        public void addUser(User user)
        {
            if (user != null)
            {
                if (IsPublic)
                {
                    // public chatroom
                    if (!participants.Contains(user))
                    {
                        participants.Add(user);
                        Console.WriteLine("Added user " + user.GetUsername());
                    }
                    else
                    {
                        Console.WriteLine("User is already in the chatroom");
                    }
                }
                else
                {
                    // private chatroom
                    if (guestList.Contains(user))
                    {
                        participants.Add(user);
                    }
                    else
                    {
                        Console.WriteLine("User not allowed in chatroom");
                    }
                }
            }
            else
            {
                Console.WriteLine("User is null");
            }
        }

        public void removeUser(User user)
        {
            if (user != null)
            {
                var currUser = participants.Find(tempUser => tempUser.GetUsername().Equals(user.GetUsername()));

                if (!Object.ReferenceEquals(currUser, null))
                {
                    participants.Remove(currUser);
                    Console.WriteLine("Removed user successfully");
                }
                else
                {
                    Console.WriteLine("User not in chatroom");
                }
            }
            else
            {
                Console.WriteLine("User is null");
            }
        }

        public void AddParticipants(List<User> users)
        {
            participants.AddRange(users);
            Console.WriteLine($"Participants added to chat room '{Name}': {string.Join(", ", users.Select(user => user.GetUsername()))}");
        }
        public void AddGuestList(List<User> users)
        {
            guestList.AddRange(users);
            Console.WriteLine($"Guests added to chat room guest list'{Name}': {string.Join(", ", users.Select(user => user.GetUsername()))}");
        }
        public void RemoveAllParticipants()
        {
            participants.Clear();
            Console.WriteLine($"All participants removed from chat room '{Name}'.");
        }
    }
}