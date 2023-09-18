using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyChatServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class ChatServer : IChatServer
    {
        public int randomInt { get; set; }
        private ChatService _chatService;

        public ChatServer() 
        {
            _chatService = new ChatService();
            Random random = new Random();
            randomInt = random.Next(10000);
        }

        public bool CreateChatroom(string chatRoomName, List<User> guestList, bool isPublic)
        {
             return _chatService.CreateChatroom(chatRoomName, guestList, isPublic);
        }

        public List<ChatRoom> GenerateDefaultChatRooms(string username)
        {
            // Call the corresponding method in the ChatServer
            //return _chatService.GenerateDefaultChatRooms(username); //From here we call the ChatService 
            return _chatService.GenerateDefaultChatRooms(); //From here we call the ChatService
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

        public void JoinChatRoom(User user, string chatRoomName)
        {
            _chatService.JoinChatRoom(user, chatRoomName);
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

        public List<ChatRoom> GetChatRooms()
        {
            return _chatService.GetChatRooms();
        }

        public int GetRandomInt()
        {
            return randomInt;
        }

    }
}