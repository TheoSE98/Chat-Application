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
        private User sender;
        [DataMember]
        private object content;
        [DataMember]
        private DateTime timestamp;
        public Message(User sender, object content, DateTime time)
        {
            this.sender = sender;
            this.content = content;
            this.timestamp = time;
        }

        public User getSender()
        {
            return sender;
        }

        public object getContent()
        {
            return content;
        }

        public DateTime getTimestamp()
        {
            return timestamp;
        }
    }
}
