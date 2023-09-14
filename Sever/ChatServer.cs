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
            throw new NotImplementedException();
        }

        public IEnumerable<ChatRoom> GetChatRoomUpdates(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetChatRoomUsers(string chatRoomName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetMessageUpdates(string username, int lastMessageId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

    }
}