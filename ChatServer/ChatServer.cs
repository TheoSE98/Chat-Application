using DataModels;
using ServerInterface;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace MyChatServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
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

        public void UserCreatedChatroom(string chatRoomName, List<string> guestList, bool isPublic)
        {
            _chatService.UserCreatedChatroom(chatRoomName, guestList, isPublic);
        }

        public List<ChatRoom> GenerateDefaultChatRooms(string username)
        {
            return _chatService.GenerateDefaultChatRooms();
        }

        public List<ChatRoom> GetChatRoomUpdates(User user)
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

        public void JoinChatRoom(User user, string chatRoomName)
        {
            _chatService.JoinChatRoom(user, chatRoomName);
        }

        public bool LeaveChatRoom(User user, string chatRoomName)
        {
            return _chatService.LeaveChatRoom(user, chatRoomName);
        }

        public async Task<bool> Login(string username)
        {
            return await _chatService.Login(username);
        }

        public async Task<bool> Logout(User user)
        {
            return await _chatService.Logout(user);
        }

        public void SendMessage(Message message)
        {
            _chatService.SendMessage(message);
        }

        public List<ChatRoom> GetChatRooms()
        {
            return _chatService.GetChatRooms();
        }
    }
}