﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    [DataContract]
    public class ChatRoom
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        private List<User> participants;
        [DataMember]
        private List<Message> messages;
        [DataMember]
        public bool IsPublic { get; set; }
        [DataMember]
        private List<User> guestList;

        public ChatRoom()
        {
            //this.Name = name;
            //this.IsPublic = isPublic;
            participants = new List<User>();
            messages = new List<Message>();
            guestList = new List<User>();
        }

        public string GetName()
        {
            return Name;
        }

        public List<User> GetParticipants()
        {
            return participants;
        }

        public void addMessage(Message message)
        {
            // TODO: add some error handling ???
            this.messages.Add(message);
        }

        // TODO update the interface or figure out a better method
        public List<Message> getMessageUpdates()
        {

            Console.WriteLine("Users in the chatroom are: ");
            foreach (User user in participants)
            {
                Console.WriteLine(user);
            }

            Console.WriteLine("Messages in the chatroom are:");
            foreach (Message message in messages)
            {
                Console.WriteLine(message.Content.ToString());
            }
            return messages;
        }

        public List<Message> GetMessages()
        {
            return messages;
        }

        public bool GetIsPublic()
        {
            return IsPublic;
        }

        public List<User> GetGuestList()
        {
            return guestList;
        }

        public bool AmIAllowedIn(User user)
        {
            if (IsPublic)
            {
                return true;
            }
            else
            {
                if (guestList.Contains(user))
                {
                    return true;
                }
                return false;
            }
        }
        
        // TODO: should these functions move to ChatService?
        // TODO: should this be writing to console? maybe in chat service probably maybe throws fault exception
        // we might need a logger class
        public void addUser(User user)
        {
            if (user != null)
            {
                if (IsPublic)
                {
                    // public chatroom
                    if (!participants.Contains(user))
                    {
                        participants.Add(user);
                        Console.WriteLine("Added user sucessfully");
                    }
                    else
                    {
                        Console.WriteLine("User is already in the chatroom");
                    }
                }
                else
                {
                    // private chatroom
                    if (guestList.Contains(user))
                    {
                        participants.Add(user);
                    }
                    else
                    {
                        Console.WriteLine("User not allowed in chatroom");
                    }
                }
            }
            else
            {
                Console.WriteLine("User is null");
            }
        }

        public void removeUser(User user)
        {
            if (user != null)
            {
                if (participants.Contains(user))
                {
                    participants.Remove(user);
                    Console.WriteLine("Removed user successfully");
                }
                else
                {
                    Console.WriteLine("User not in chatroom");
                }
            }
            else
            {
                Console.WriteLine("User is null");
            }
        }

        public void AddParticipants(List<User> users)
        {
            participants.AddRange(users);
            //DEBUGGING
            Console.WriteLine($"Participants added to chat room '{Name}': {string.Join(", ", users.Select(user => user.GetUsername()))}");
        }
        public void RemoveAllParticipants()
        {
            participants.Clear();
            //DEBUGGING 
            Console.WriteLine($"All participants removed from chat room '{Name}'.");
        }
    }
}