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

        public void AddParticipants(List<User> users)
        {
            participants.AddRange(users);
            //DEBUGGING 
            Console.WriteLine($"Participants added to chat room '{name}': {string.Join(", ", users.Select(user => user.GetUsername()))}");
        }

    }
}