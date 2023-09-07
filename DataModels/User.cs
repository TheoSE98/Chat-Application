using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class User
    {
        private string username;
        private List<ChatRoom> chatRooms = new List<ChatRoom>();

        public User(string username)
        {
            this.username = username;
        }

        public string GetUsername()
        {
            return username;
        }

        public List<ChatRoom> GetChatRooms()
        {
            return chatRooms;
        }
    }
}