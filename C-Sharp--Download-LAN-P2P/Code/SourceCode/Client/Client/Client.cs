using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-=====CLIENT=====-");

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string ClientName;
            Console.Write("Login Name: ");
            ClientName = Console.ReadLine();

            Console.WriteLine("Connect......\n");

            client.Connect(ipep);
            new ClientThread(client, ClientName);

        }
    }

    class ClientThread
    {
        private Socket client;
        private string ClientName;
        private Thread threadSend;
        private Thread threadReceive;

        public ClientThread(Socket client, string Name)
        {
            this.client = client;
            ClientName = Name;

            threadSend = new Thread(new ThreadStart(SendMessage));
            threadReceive = new Thread(new ThreadStart(ReceiveMessage));

            threadSend.Start();
            threadReceive.Start();
        }

        void SendMessage()
        {
            NetworkStream ns = new NetworkStream(client);
            StreamWriter sw = new StreamWriter(ns);

            string s = "";

            while (true)
            {
                s = Console.ReadLine();

                if (s.ToLower() == "quit")
                    break;

                sw.WriteLine("  " + this.ClientName + ": " + s);
                sw.Flush();
            }

            client.Close();
        }

        void ReceiveMessage()
        {
            NetworkStream ns = new NetworkStream(client);
            StreamReader sr = new StreamReader(ns);

            string s = "";

            while (true)
            {
                s = sr.ReadLine();
                Console.WriteLine(s);
            }
        }
    }
}
