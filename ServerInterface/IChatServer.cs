﻿using DataModels;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ServerInterface
{
    [ServiceContract]
    public interface IChatServer
    {
        //User Management 
        [OperationContract]
        Task<bool> Login(string username);
        [OperationContract]
        Task<bool> Logout(User pUser);

        [OperationContract]
        List<ChatRoom> GenerateDefaultChatRooms(string username);

        //Chat Room Management
        [OperationContract]
        bool CreateChatroom(string chatRoomName, List<User> guestList, bool isPublic);
        [OperationContract]
        void UserCreatedChatroom(string chatRoomName, List<String> guestList, bool isPublic);
        [OperationContract]
        void JoinChatRoom(User user, string chatRoomName);
        [OperationContract]
        bool LeaveChatRoom(User user, string chatRoomName);

        //Message Distributed
        [OperationContract]
        void SendMessage(Message message);

        //Get updates
        [OperationContract]
        List<Message> GetMessageUpdates(string chatroomName);
        [OperationContract]
        List<ChatRoom> GetChatRoomUpdates(User user);
        [OperationContract]
        IEnumerable<User> GetChatRoomUsers(string chatRoomName);
        [OperationContract]
        List<ChatRoom> GetChatRooms(); // TODO: should we pass in the username to verify identity?
    }
}