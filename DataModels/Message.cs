using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public User Sender { get; set; }

        [DataMember]
        public object Content { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public string ChatRoomName { get; set; }
        public Message()
        {
            //this.sender = sender;
            //this.content = content;
            //this.timestamp = time;
            //this.chatRoomName = chatRoomName;
        }

        public User getSender()
        {
            return Sender;
        }

        public object getContent()
        {
            return Content;
        }

        public DateTime getTimestamp()
        {
            return Timestamp;
        }

        public string getChatRoomName()
        {
            return ChatRoomName;
        }
    }
}
