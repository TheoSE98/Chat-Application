using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataModels;

namespace MyChatServer
{
    [ServiceContract]
    public interface IChatServer
    {
        //User Management 
        [OperationContract]
        Task<bool> Login(string username);

        //Chat Room Management
        [OperationContract]
        void CreateChatroom(string chatRoomName, List<User> guestList, bool isPublic);
        [OperationContract]
        void JoinChatRoom(string username, string chatRoomName);
        [OperationContract]
        void LeaveChatRoom(string username, string chatRoomName);

        //Message Distributed
        [OperationContract]
        void SendMessage(Message message);

        //Get updates
        [OperationContract]
        IEnumerable<Message> GetMessageUpdates(string username, int lastMessageId);
        [OperationContract]
        IEnumerable<ChatRoom> GetChatRoomUpdates(string username);
        [OperationContract]
        IEnumerable<User> GetChatRoomUsers(string chatRoomName);
    }
}