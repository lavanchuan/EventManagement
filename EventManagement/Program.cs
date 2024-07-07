using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace EventManagement
{
    internal static class Program
    {
        private const int serverPort = 6666;
        private const int clientPort = 6666;
        private static UdpClient udpClient = new UdpClient(clientPort);
        private static IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), serverPort);

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]


        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new LoginForm());

            //Server server = new Server();

            Client client = new Client();

        }

        private static void SendMessage(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            udpClient.Send(data, data.Length, serverEP);
        }

        private static void ReceiveMessages()
        {
            try
            {
                while (true)
                {
                    byte[] data = udpClient.Receive(ref serverEP);
                    string message = Encoding.ASCII.GetString(data);
                    Console.WriteLine($"Received from server: {message}");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine($"SocketException: {e.Message}");
            }
            finally
            {
                udpClient.Close();
            }
        }
    }
}