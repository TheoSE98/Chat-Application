using DataModels;
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

        public void CreateChatroom(string chatRoomName, List<User> guestList, bool isPublic)
        {
            _chatService.CreateChatroom(chatRoomName, guestList, isPublic);
        }

        public List<ChatRoom> GenerateDefaultChatRooms(string username)
        {
            // Call the corresponding method in the ChatServer
            return _chatService.GenerateDefaultChatRooms(username);
        }

        public IEnumerable<ChatRoom> GetChatRoomUpdates(User user)
        {
            return _chatService.GetChatRoomUpdates(user);
        }

        public IEnumerable<User> GetChatRoomUsers(string chatRoomName)
        {
            return _chatService.GetChatRoomUsers(chatRoomName);
        }

        public IEnumerable<Message> GetMessageUpdates(string chatRoomName, Message lastMessage)
        {
            return _chatService.GetMessageUpdates(chatRoomName, lastMessage);
        }

        public void JoinChatRoom(string username, string chatRoomName)
        {
            throw new NotImplementedException();
        }

        public void LeaveChatRoom(string username, string chatRoomName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Login(string username)
        {
            return await _chatService.Login(username);
        }

        public void SendMessage(Message message)
        {
            _chatService.SendMessage(message);
        }

    }
}