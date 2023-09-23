using System;
using System.Runtime.Serialization;

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
        public string Type { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public string ChatRoomName { get; set; }

        public Message()
        {

        }

        public User getSender()
        {
            return Sender;
        }

        public object getContent()
        {
            return Content;
        }

        public string getType()
        {
            return Type;
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