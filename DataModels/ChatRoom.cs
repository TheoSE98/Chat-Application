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