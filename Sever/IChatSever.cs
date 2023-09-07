using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;

namespace Sever
{
    public interface IChatSever
    {
        //User Management 
        void loging(string username);

        //Chat Room Management
        void CreateChatroom(string chatRoomName, List<User> guestList, bool isPublic);
        void JoinChatRoom(string username, string chatRoomName);
        void LeaveChatRoom(string username, string chatRoomName);

        //Message Distributed
        void SendMessage(Message message);

        //Get updates
        IEnumerable<Message> GetMessageUpdates(string username, int lastMessageId);
        IEnumerable<ChatRoom> GetChatRoomUpdates(string username);
        IEnumerable<User> GetChatRoomUsers(string chatRoomName);
    }
}