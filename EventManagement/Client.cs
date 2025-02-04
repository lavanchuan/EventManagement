﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManagement
{
    internal class Client
    {
        public Client() {

            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint endPoint = new IPEndPoint(hostEntry.AddressList[0], 11000);

            Socket s = new Socket(endPoint.Address.AddressFamily,
                SocketType.Dgram,
                ProtocolType.Udp);

            byte[] msg = Encoding.ASCII.GetBytes("This is a test");
            Console.WriteLine("Sending data.");
            // This call blocks.
            s.SendTo(msg, 0, msg.Length, SocketFlags.None, endPoint);
            s.Close();
        }
    }
}
