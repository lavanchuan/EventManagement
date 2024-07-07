using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;


namespace EventManagement
{
    public class SocketService
    {
        public static string PATTERN = "\t";
        public static string PATTERN_ITEM = "---";
        public static string REGISTER = "REGISTER";
        public static string LOGIN = "LOGIN";
        public static string CREATE_EVENT = "CREATE_EVENT";
        public static string INVITE = "INVITE";
        public static string REQUEST = "REQUEST";

        public static string REGISTER_RESPONSE = "REGISTER_RESPONSE";
        public static string LOGIN_RESPONSE = "LOGIN_RESPONSE";
        public static string REQUEST_RESPONSE = "REQUEST_RESPONSE";
        public static string INVITE_RESPONSE = "INVITE_RESPONSE";
        public static string CREATE_EVENT_RESPONSE = "CREATE_EVENT_RESPONSE";

        public Socket socket { get; set; }

        public int Send(string msg, EndPoint to) {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                int result = socket.SendTo(buffer, 0, buffer.Length, 0, to);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Send message.");
            }
            return 0;
        }

        public int SendRegister(AccountDTO request, EndPoint to) {

            string msg = REGISTER + PATTERN;
            msg += request.name + PATTERN_ITEM;
            msg += request.username + PATTERN_ITEM;
            msg += request.password;

            return Send(msg, to);
        }

        public int SendLogin(AccountDTO request, EndPoint to) {
            string msg = LOGIN + PATTERN;
            msg += request.username + PATTERN_ITEM;
            msg += request.password;

            return Send(msg, to);
        }

        public int SendRequestEvent(RequestData request, EndPoint to) {
            string msg = REQUEST + PATTERN;
            msg += request.eventId + PATTERN_ITEM;
            msg += request.userId + PATTERN_ITEM;
            msg += FormatService.DateTimeToString(request.createAt);

            return Send(msg, to);
        }

        public int SendCreateNewEvent(RequestData request, EndPoint to)
        {
            string msg = CREATE_EVENT + PATTERN;
            msg += request.name + PATTERN_ITEM;
            msg += FormatService.DateTimeToString(request.time) + PATTERN_ITEM;
            msg += request.address + PATTERN_ITEM;
            msg += request.description + PATTERN_ITEM;
            msg += request.ownerId;

            return Send(msg, to);
        }
    }
}
