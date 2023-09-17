﻿using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyChatServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class ChatServer : IChatServer
    {
        private ChatService _chatService;

        public ChatServer() 
        {
            _chatService = new ChatService();
        }

        public bool CreateChatroom(string chatRoomName, List<User> guestList, bool isPublic)
        {
             return _chatService.CreateChatroom(chatRoomName, guestList, isPublic);
        }

        public List<ChatRoom> GenerateDefaultChatRooms(string username)
        {
            // Call the corresponding method in the ChatServer
            return _chatService.GenerateDefaultChatRooms(username); //From here we call the ChatService 
        }

        public IEnumerable<ChatRoom> GetChatRoomUpdates(User user)
        {
            return _chatService.GetChatRoomUpdates(user);
        }

        public IEnumerable<User> GetChatRoomUsers(string chatRoomName)
        {
            return _chatService.GetChatRoomUsers(chatRoomName);
        }

        public List<Message> GetMessageUpdates(string chatRoomName)
        {
            return _chatService.GetMessageUpdates(chatRoomName);
        }

        public void JoinChatRoom(string username, string chatRoomName)
        {
            throw new NotImplementedException();
        }

        public void LeaveChatRoom(User user, string chatRoomName)
        {
            _chatService.LeaveChatRoom(user, chatRoomName);
        }

        public async Task<bool> Login(string username)
        {
            return await _chatService.Login(username);
        }

        public void RemoveChatRoom(string chatRoomName)
        {
            //_chatService.RemoveChatRoom(chatRoomName);
            throw new NotImplementedException();
        }

        public void SendMessage(Message message)
        {
            _chatService.SendMessage(message);
        }

    }
}