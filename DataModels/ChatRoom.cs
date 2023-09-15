using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class ChatRoom
    {
        private string name;
        private List<User> participants;
        private List<Message> messages;
        private bool isPublic;
        private List<User> guestList;

        public ChatRoom(string name, bool isPublic)
        {
            this.name = name;
            this.isPublic = isPublic;
            participants = new List<User>();
            messages = new List<Message>();
            guestList = new List<User>();
        }

        public string GetName()
        {
            return name;
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
            return isPublic;
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
            Console.WriteLine($"Participants added to chat room '{name}': {string.Join(", ", users.Select(user => user.GetUsername()))}");
        }



    }
}