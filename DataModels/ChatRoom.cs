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
        private string name;
        private List<User> participants;
        private List<Message> messages;
        [DataMember]
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

        public void AddParticipants(List<User> users)
        {
            participants.AddRange(users);
            //DEBUGGING 
            Console.WriteLine($"Participants added to chat room '{name}': {string.Join(", ", users.Select(user => user.GetUsername()))}");
        }



    }
}