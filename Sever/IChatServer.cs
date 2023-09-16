﻿using System;
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

        [OperationContract]
        List<ChatRoom> GenerateDefaultChatRooms(string username);

        //Chat Room Management
        [OperationContract]
        bool CreateChatroom(string chatRoomName, List<User> guestList, bool isPublic);
        [OperationContract]
        void JoinChatRoom(string username, string chatRoomName);
        [OperationContract]

        [OperationContract]
        void LeaveChatRoom(User user, string chatRoomName);

        //Message Distributed
        [OperationContract]
        void SendMessage(Message message);

        //Get updates
        [OperationContract]
        IEnumerable<Message> GetMessageUpdates(string chatroomName, Message lastMessage);
        [OperationContract]
        IEnumerable<ChatRoom> GetChatRoomUpdates(User user);
        [OperationContract]
        IEnumerable<User> GetChatRoomUsers(string chatRoomName);
    }
}