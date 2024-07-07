using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Utilities;

namespace EventManagement
{
    internal class Server
    {
        public Server() { 
            string hostname = "127.0.0.1";
            IPAddress ipAddress = IPAddress.Parse(hostname);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket server = new Socket(ipEndPoint.Address.AddressFamily,
                SocketType.Dgram,
                ProtocolType.Udp);

            IPEndPoint client = new IPEndPoint(IPAddress.Any, 0);
            EndPoint clientEP = (EndPoint)client;

            server.Bind(ipEndPoint);

            byte[] buffer = new byte[1024];
            int result;
            string msg;
            do
            {
                Console.WriteLine("Waiting...");
                result = server.ReceiveFrom(buffer, 1024, 0, ref clientEP);
                msg = Encoding.UTF8.GetString(buffer, 0, result);
                Console.WriteLine(msg);
                if (msg.Equals("Close")) break;
            } while (true);

            server.Close();
        }
    }
}
