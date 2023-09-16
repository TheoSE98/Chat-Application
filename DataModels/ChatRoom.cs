using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    [DataContract]
    public class ChatRoom
    {
        [DataMember]
        public string Name { get; set; }
        private List<User> participants;
        private List<Message> messages;
        [DataMember]
        public bool IsPublic { get; set; }
        private List<User> guestList;

        public ChatRoom()
        {
            //this.Name = name;
            //this.IsPublic = isPublic;
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

        // TODO this code is a bit weird, may have to rethink our approach, will have to test it out in practice
        public IEnumerable<Message> getMessageUpdates(Message lastReceivedmessage)
        {
            List<Message> messages = new List<Message>();
            bool foundMessage = false;

            foreach (Message message in this.messages)
            { 
                if (lastReceivedmessage == message)
                {
                    foundMessage = true;
                }
                if (foundMessage)
                {
                    messages.Add(message);
                }
            }

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
            if (isPublic)
            {
                return true;
            }
            else
            {
                if (guestList.Contains(user))
                {
                    return true;
                }
                return false;
            }
        }
        
        // TODO: should these functions move to ChatService?
        // TODO: should this be writing to console? maybe in chat service probably maybe throws fault exception
        // we might need a logger class
        public void addUser(User user)
        {
            if (user != null)
            {
                if (isPublic)
                {
                    // public chatroom
                    if (!participants.Contains(user))
                    {
                        participants.Add(user);
                        Console.WriteLine("Added user sucessfully");
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
                if (participants.Contains(user))
                {
                    participants.Remove(user);
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
            //DEBUGGING 
            Console.WriteLine($"Participants added to chat room '{Name}': {string.Join(", ", users.Select(user => user.GetUsername()))}");
        }
        public void RemoveAllParticipants()
        {
            participants.Clear();
            //DEBUGGING 
            Console.WriteLine($"All participants removed from chat room '{Name}'.");
        }
    }
}