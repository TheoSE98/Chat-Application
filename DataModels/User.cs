using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public List<ChatRoom> chatRooms; 

        public User(string username)
        {
            this.username = username;
            chatRooms = new List<ChatRoom>();
        }

        public string GetUsername()
        {
            return username;
        }

        public List<ChatRoom> GetChatRooms()
        {
            return chatRooms;
        }

        public override string ToString()
        {
            return username;
        }

    }
}