using EventManagement;
using System.Collections.Specialized;

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerEvent
{
    public partial class formServer : Form
    {

        public bool running = false;

        Socket server;
        int port = 6666;

        Thread runThread;

        // Injection
        AuthenticationService authenticationService = new AuthenticationService();
        RequestService requestService = new RequestService();
        EventService eventService = new EventService();

        public formServer()
        {
            InitializeComponent();
            
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            running = true;
            Console.WriteLine("Server running...");

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ipAddress, port);
            server = new Socket(endPoint.Address.AddressFamily,
                SocketType.Dgram,
                ProtocolType.Udp);

            server.Bind(endPoint);


            runThread = new Thread(Run);
            runThread.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            running = false;
            Console.WriteLine("Server stopping...");

            if (server != null) { 
                server.Close();
                Console.WriteLine("Server stopped");
            }
        }

        private void Run()
        {

            byte[] bufferSend;
            byte[] bufferRecv = new byte[1024];
            int result;
            string message;

            IPEndPoint client = new IPEndPoint(IPAddress.Any, 0);
            EndPoint clientRemote = (EndPoint)client;

            while (running) {
                result = server.ReceiveFrom(bufferRecv, 1024, 0, ref clientRemote);
                if (result > 0) {
                    message = Encoding.UTF8.GetString(bufferRecv, 0, result);

                    string type = message.Split("\t")[0];
                    string response;
                    int accountId = 0;
                    string name = "name";
                    RequestData request = new RequestData();
                    switch (type) {
                        case "LOGIN":

                            RequestData loginRequestData = new RequestData();
                            loginRequestData.username = message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[0];
                            loginRequestData.password = message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[1];

                            response = authenticationService.Authentication(loginRequestData) ? "TRUE" : "FALSE";
                            
                            if (response.Equals("TRUE"))
                            {
                                accountId = authenticationService.GetIdByUsername(loginRequestData.username);
                                name = authenticationService.GetNameByUsername(loginRequestData.username);
                            }
                            message = SocketService.LOGIN_RESPONSE +
                                SocketService.PATTERN + response +
                                SocketService.PATTERN + accountId +
                                SocketService.PATTERN + name;
                            bufferSend = Encoding.UTF8.GetBytes(message);
                            result = server.SendTo(bufferSend, message.Length, 0, clientRemote);
                            if (result > 0) { 
                                Console.WriteLine("Successfully send login response for client");
                            } else
                            {
                                Console.WriteLine("Failed send login response for client");
                            }

                            break;
                         case "REGISTER":

                            RequestData registerRequestData = new RequestData();
                            registerRequestData.name = message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[0];
                            registerRequestData.username = message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[1];
                            registerRequestData.password = message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[2];

                            response = authenticationService.Register(registerRequestData) ? "TRUE" : "FALSE";
                            if (response.Equals("TRUE")) accountId = authenticationService.GetIdByUsername(registerRequestData.username);
                            message = SocketService.REGISTER_RESPONSE +
                                SocketService.PATTERN + response +
                                SocketService.PATTERN + accountId;
                            bufferSend = Encoding.UTF8.GetBytes(message);
                            result = server.SendTo(bufferSend, message.Length, 0, clientRemote);
                            if (result > 0)
                            {
                                Console.WriteLine("Successfully send register response for client");
                            }
                            else
                            {
                                Console.WriteLine("Failed send register response for client");
                            }

                            break;

                        case "CREATE_EVENT":

                            Console.WriteLine(message);

                            request = new RequestData();
                            request.name = message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[0];
                            request.time = FormatService
                                .StringToDataTime(message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[1]);
                            request.address = message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[2];
                            request.description = message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[3];
                            request.ownerId = Int32.Parse(message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[4]);

                            eventService.AddEvent(request);

                            response =  "TRUE";
                            message = SocketService.CREATE_EVENT_RESPONSE +
                                SocketService.PATTERN + response;
                            bufferSend = Encoding.UTF8.GetBytes(message);
                            result = server.SendTo(bufferSend, message.Length, 0, clientRemote);
                            if (result > 0)
                            {
                                Console.WriteLine("Successfully send create event response for client");
                            }
                            else
                            {
                                Console.WriteLine("Failed send create event response for client");
                            }

                            break;

                        case "REQUEST":
                            request = new RequestData();
                            request.eventId = Int32.
                                Parse(message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[0]);
                            request.eventId = Int32.
                                Parse(message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[1]);
                            request.createAt = FormatService
                                .StringToDataTime(message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[2]);
                            request.ownerId = eventService.GetOwnerIdByEventId(request.eventId);

                            requestService.AddRequest(request);

                            response = "TRUE";
                            message = SocketService.REQUEST_RESPONSE +
                                SocketService.PATTERN + response;
                            bufferSend = Encoding.UTF8.GetBytes(message);
                            result = server.SendTo(bufferSend, message.Length, 0, clientRemote);
                            if (result > 0)
                            {
                                Console.WriteLine("Successfully send request event response for client");
                            }
                            else
                            {
                                Console.WriteLine("Failed send create request response for client");
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
