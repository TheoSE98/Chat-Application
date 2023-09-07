﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Sever
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("******************************************");
            Console.WriteLine("* Welcome to the Chat Server            *");
            Console.WriteLine("******************************************");
            Console.ResetColor();

            ServiceHost host = null;

            try
            {
                //TcpBinding for communication  
                NetTcpBinding tcp = new NetTcpBinding();

                //Instance of DataServer that implements 
                IChatSever service = new ChatServer();

                host = new ServiceHost(typeof(ChatServer));


                host.AddServiceEndpoint(typeof(IChatSever), tcp, "net.tcp://0.0.0.0:8100/ChatService");

                host.Open();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n******************************************");
                Console.WriteLine("* Chat Server is online.                 *");
                Console.WriteLine("* Press Enter to exit.                   *");
                Console.WriteLine("******************************************");
                Console.ResetColor();

                Console.ReadLine();

            }
            catch (Exception e)
            {
                //Handle any errors when attempting to start the server 
                Console.WriteLine("An error has occured when attempting to start server: " + e.Message);
            }
            finally
            {
                //Check host is open before closing 
                if (host != null && host.State == CommunicationState.Opened)
                {
                    try
                    {
                        host.Close();
                        Console.WriteLine("Chat Server is shutting down.");
                    }
                    //Handles Errors when server is closed
                    catch (Exception ex) { Console.WriteLine("An error occured when attempting to close server " + ex.Message); }
                }
            }
        }
    }
}
