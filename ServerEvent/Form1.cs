using EventManagement;
using EventManagement.Entities;
using MySqlX.XDevAPI.Common;
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

        Thread runThread;

        IPEndPoint client;
        EndPoint clientRemote;

        // Injection
        AuthenticationService authenticationService = new AuthenticationService();
        RequestService requestService = new RequestService();
        EventService eventService = new EventService();
        InviteService inviteService = new InviteService();
        public formServer()
        {
            Console.WriteLine("TestCase: FORM 1");
            InitializeComponent();

            Console.WriteLine("TestCase: FORM 2");
            client = new IPEndPoint(IPAddress.Any, 0);
            clientRemote = (EndPoint)client;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            running = true;
            Console.WriteLine("Server running...");

            IPAddress ipAddress = IPAddress.Parse(AppData.SERVER_HOST);
            IPEndPoint endPoint = new IPEndPoint(ipAddress, AppData.PORT);
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

            if (server != null)
            {
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
            int userId;

            string type;
            string response;
            int accountId = 0;
            int ownerId = -1;
            int eventId;

            string name = "name";



            while (running)
            {
                result = server.ReceiveFrom(bufferRecv, 1024, 0, ref clientRemote);
                if (result > 0)
                {
                    message = Encoding.UTF8.GetString(bufferRecv, 0, result);

                    Console.WriteLine();
                    Console.WriteLine("=========================================");
                    Console.WriteLine("MESSAGE: " + message);

                    type = message.Split("\t")[0];

                    RequestData request = new RequestData();
                    switch (type)
                    {
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
                            if (result > 0)
                            {
                                Console.WriteLine("Successfully send login response for client");
                            }
                            else
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

                            response = "TRUE";
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
                            request.userId = Int32.
                                Parse(message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[1]);
                            request.createAt = FormatService
                                .StringToDataTime(message.Split("\t")[1].Split(SocketService.PATTERN_ITEM)[2]);
                            request.ownerId = eventService.GetOwnerIdByEventId(request.eventId);

                            // Check exists eventId
                            if (!eventService.ExistsById(request.eventId))
                            {
                                response = "FALSE";
                                response += SocketService.PATTERN + "NOT_FOUND";
                            }
                            else
                            {
                                requestService.AddRequest(request);
                                response = "TRUE";
                            }


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
                        case "GET_MY_EVENT_LIST":
                            ownerId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim());
                            Thread getEventByOwnerIdThread = new Thread(() => ThreadGetEventByOwnerId(ownerId));
                            getEventByOwnerIdThread.Start();
                            break;

                        case "INVITE_ALL_NEW_PEOPLE":
                            eventId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[0].Trim());
                            ownerId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[1].Trim());

                            Thread inviteAllNewPeople = new Thread(() => ThreadInviteAllNewPeople(eventId, ownerId));
                            inviteAllNewPeople.Start();

                            break;

                        case "GET_INVITE_ME_LIST":
                            userId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim());

                            Thread getInviteMeList = new Thread(() => ThreadGetInviteMeList(userId));
                            getInviteMeList.Start();

                            break;

                        case "GET_REQUEST_ME_LIST":
                            userId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim());

                            Thread getRequestMeList = new Thread(() => ThreadGetRequestMeList(userId));
                            getRequestMeList.Start();

                            break;

                        case "ACCEPT_REQUEST":
                            eventId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[0].Trim());
                            ownerId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[1].Trim());
                            userId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[2].Trim());

                            Thread acceptRequestThread = new Thread(() => ThreadAcceptRequest(eventId, ownerId, userId));
                            acceptRequestThread.Start();

                            break;

                        case "REJECT_REQUEST":
                            eventId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[0].Trim());
                            ownerId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[1].Trim());
                            userId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[2].Trim());

                            Thread rejectRequestThread = new Thread(() => ThreadRejectRequest(eventId, ownerId, userId));
                            rejectRequestThread.Start();

                            break;

                        case "ACCEPT_INVITE":
                            eventId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[0].Trim());
                            ownerId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[1].Trim());
                            userId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[2].Trim());

                            Thread acceptInviteThread = new Thread(() => ThreadAcceptInvite(eventId, ownerId, userId));
                            acceptInviteThread.Start();

                            break;

                        case "REJECT_INVITE":
                            eventId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[0].Trim());
                            ownerId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[1].Trim());
                            userId = Int32.Parse(message.Split(SocketService.PATTERN)[1].Trim()
                                .Split(SocketService.PATTERN_ITEM)[2].Trim());

                            Thread rejectInviteThread = new Thread(() => ThreadRejectInvite(eventId, ownerId, userId));
                            rejectInviteThread.Start();

                            break;

                        default:
                            break;
                    }
                }
            }
        }



        private void ThreadRejectRequest(int eventId, int ownerId, int userId)
        {
            string response = SocketService.REJECT_REQUEST_RESPONSE + SocketService.PATTERN;

            //Console.WriteLine($"EventId: {eventId}");
            //Console.WriteLine($"OwnerId: {ownerId}");
            //Console.WriteLine($"UserId: {userId}");

            if (!authenticationService.ExistsById(userId) || !authenticationService.ExistsById(ownerId) ||
                !eventService.ExistsById(eventId))
            {
                //Console.WriteLine("Checked");

                response += SocketService.FALSE + SocketService.PATTERN;
                response += SocketService.NOT_FOUND;
            }
            else
            {
                RequestData request = new RequestData();
                request.eventId = eventId;
                request.ownerId = ownerId;
                request.userId = userId;
                request.state = ActionState.Reject;

                requestService.UpdateRequest(request);

                response += SocketService.TRUE;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(response);
            int result = server.SendTo(buffer, response.Length, 0, clientRemote);
            if (result > 0)
            {
                Console.WriteLine("SERVER: Successfully send reject request for client.");
            }
            else
            {
                Console.WriteLine("SERVER: Failed send reject request for client!!!");
            }
        }


        private void ThreadAcceptRequest(int eventId, int ownerId, int userId)
        {
            string response = SocketService.ACCEPT_REQUEST_RESPONSE + SocketService.PATTERN;

            //Console.WriteLine($"EventId: {eventId}");
            // Console.WriteLine($"OwnerId: {ownerId}");
            //Console.WriteLine($"UserId: {userId}");

            if (!authenticationService.ExistsById(userId) || !authenticationService.ExistsById(ownerId) ||
                !eventService.ExistsById(eventId))
            {
                //Console.WriteLine("Checked");

                response += SocketService.FALSE + SocketService.PATTERN;
                response += SocketService.NOT_FOUND;
            }
            else
            {
                RequestData request = new RequestData();
                request.eventId = eventId;
                request.ownerId = ownerId;
                request.userId = userId;
                request.state = ActionState.Accept;

                requestService.UpdateRequest(request);

                response += SocketService.TRUE;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(response);
            int result = server.SendTo(buffer, response.Length, 0, clientRemote);
            if (result > 0)
            {
                Console.WriteLine("SERVER: Successfully send accept request for client.");
            }
            else
            {
                Console.WriteLine("SERVER: Failed send accept request for client!!!");
            }
        }

        private void ThreadAcceptInvite(int eventId, int ownerId, int userId)
        {
            string response = SocketService.ACCEPT_INVITE_RESPONSE + SocketService.PATTERN;

            if (!authenticationService.ExistsById(userId) || !authenticationService.ExistsById(ownerId) ||
                !eventService.ExistsById(eventId))
            {
                response += SocketService.FALSE + SocketService.PATTERN;
                response += SocketService.NOT_FOUND;
            }
            else
            {
                RequestData request = new RequestData();
                request.eventId = eventId;
                request.ownerId = ownerId;
                request.userId = userId;
                request.state = ActionState.Accept;

                inviteService.UpdateInvite(request);

                response += SocketService.TRUE;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(response);
            int result = server.SendTo(buffer, response.Length, 0, clientRemote);
            if (result > 0)
            {
                Console.WriteLine("SERVER: Successfully send accept invite for client.");
            }
            else
            {
                Console.WriteLine("SERVER: Failed send accept invite for client!!!");
            }
        }
        
        private void ThreadRejectInvite(int eventId, int ownerId, int userId)
        {
            string response = SocketService.REJECT_INVITE_RESPONSE + SocketService.PATTERN;

            if (!authenticationService.ExistsById(userId) || !authenticationService.ExistsById(ownerId) ||
                !eventService.ExistsById(eventId))
            {
                response += SocketService.FALSE + SocketService.PATTERN;
                response += SocketService.NOT_FOUND;
            }
            else
            {
                RequestData request = new RequestData();
                request.eventId = eventId;
                request.ownerId = ownerId;
                request.userId = userId;
                request.state = ActionState.Reject;

                inviteService.UpdateInvite(request);

                response += SocketService.TRUE;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(response);
            int result = server.SendTo(buffer, response.Length, 0, clientRemote);
            if (result > 0)
            {
                Console.WriteLine("SERVER: Successfully send reject invite for client.");
            }
            else
            {
                Console.WriteLine("SERVER: Failed send reject invite for client!!!");
            }
        }

        private void ThreadGetRequestMeList(int userId)
        {
            string response = SocketService.GET_REQUEST_ME_LIST_RESPONSE + SocketService.PATTERN;

            if (!authenticationService.ExistsById(userId))
            {
                response += SocketService.FALSE + SocketService.PATTERN;
                response += SocketService.NOT_FOUND;
            }
            else
            {
                response += SocketService.TRUE + SocketService.PATTERN;
                List<RequestDetail> requestDetails = requestService.GetRequestDetailsMeListByUserId(userId);
                response += RequestDetail.ToStringLine(requestDetails);
            }

            byte[] buffer = Encoding.UTF8.GetBytes(response);
            int result = server.SendTo(buffer, response.Length, 0, clientRemote);
            if (result > 0)
            {
                Console.WriteLine("SERVER: Successfully send request me list for client.");
            }
            else
            {
                Console.WriteLine("SERVER: Failed send request me list for client!!!");
            }
        }

        private void ThreadGetInviteMeList(int userId)
        {
            string response = SocketService.GET_INVITE_ME_LIST_RESPONSE + SocketService.PATTERN;

            if (!authenticationService.ExistsById(userId))
            {
                response += SocketService.FALSE + SocketService.PATTERN;
                response += SocketService.NOT_FOUND;
            }
            else
            {
                response += SocketService.TRUE + SocketService.PATTERN;
                List<InviteDetail> inviteDetails = inviteService.GetInviteDetailsMeListByUserId(userId);
                response += InviteDetail.ToStringLine(inviteDetails);
            }

            byte[] buffer = Encoding.UTF8.GetBytes(response);
            int result = server.SendTo(buffer, response.Length, 0, clientRemote);
            if (result > 0)
            {
                Console.WriteLine("SERVER: Successfully send invite list for client.");
            }
            else
            {
                Console.WriteLine("SERVER: Failed send invite list for client!!!");
            }
        }

        private void ThreadGetEventByOwnerId(int ownerId)
        {
            string response = SocketService.GET_MY_EVENT_LIST_RESPONSE + SocketService.PATTERN;
            if (!authenticationService.ExistsById(ownerId))
            {
                response += SocketService.FALSE + SocketService.PATTERN;
                response += SocketService.NOT_FOUND;
            }
            else
            {
                response += SocketService.TRUE + SocketService.PATTERN;
                List<EventDTO> events = eventService.GetByOwnerId(ownerId);
                response += EventDTO.ToStringLine(events);
            }

            //Console.WriteLine($"[DEBUG]:[Form1.ThreadGetEventByOwnerId]: [response]: {response}");

            byte[] buffer = Encoding.UTF8.GetBytes(response);
            int result = server.SendTo(buffer, response.Length, 0, clientRemote);

            if (result > 0)
            {
                Console.WriteLine("SERVER: Successfully send event list for client.");
            }
            else
            {
                Console.WriteLine("SERVER: Failed send event list for client!!!");
            }
        }

        private void ThreadInviteAllNewPeople(int eventId, int ownerId)
        {
            string response = SocketService.INVITE_ALL_NEW_PEOPLE_RESPONSE + SocketService.PATTERN;
            if (authenticationService.ExistsById(ownerId) &&
                eventService.ExistsById(eventId))
            {

                int quantity = eventService.InviteAllNewPeople(eventId, ownerId);

                response += SocketService.TRUE + SocketService.PATTERN;
                response += quantity;

            }
            else
            {
                response += SocketService.FALSE + SocketService.PATTERN;
                response += SocketService.NOT_FOUND;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(response);
            int result = server.SendTo(buffer, response.Length, 0, clientRemote);
            if (result > 0)
            {
                Console.WriteLine("SERVER: Successfully send event list for client.");
            }
            else
            {
                Console.WriteLine("SERVER: Failed send event list for client!!!");
            }
        }
    }
}
