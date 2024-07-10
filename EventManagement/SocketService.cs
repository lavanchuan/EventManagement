using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Org.BouncyCastle.Asn1.Ocsp;


namespace EventManagement
{
    public class SocketService
    {
        public static string PATTERN = "\t";
        public static string PATTERN_ITEM = "---";
        public static string PATTERN_END_LINE = "\n";

        public static string REGISTER = "REGISTER";
        public static string LOGIN = "LOGIN";
        public static string CREATE_EVENT = "CREATE_EVENT";
        public static string INVITE = "INVITE";
        public static string REQUEST = "REQUEST";
        public static string GET_MY_EVENT_LIST = "GET_MY_EVENT_LIST";
        public static string INVITE_ALL_NEW_PEOPLE = "INVITE_ALL_NEW_PEOPLE";
        public static string GET_INVITE_ME_LIST = "GET_INVITE_ME_LIST";
        public static string GET_REQUEST_ME_LIST = "GET_REQUEST_ME_LIST";
        public static string ACCEPT_REQUEST= "ACCEPT_REQUEST";
        public static string REJECT_REQUEST = "REJECT_REQUEST";
        public static string ACCEPT_INVITE = "ACCEPT_INVITE";
        public static string REJECT_INVITE = "REJECT_INVITE";

        public static string REGISTER_RESPONSE = "REGISTER_RESPONSE";
        public static string LOGIN_RESPONSE = "LOGIN_RESPONSE";
        public static string REQUEST_RESPONSE = "REQUEST_RESPONSE";
        public static string INVITE_RESPONSE = "INVITE_RESPONSE";
        public static string CREATE_EVENT_RESPONSE = "CREATE_EVENT_RESPONSE";
        public static string GET_MY_EVENT_LIST_RESPONSE = "GET_MY_EVENT_LIST_RESPONSE";
        public static string INVITE_ALL_NEW_PEOPLE_RESPONSE = "INVITE_ALL_NEW_PEOPLE_RESPONSE";
        public static string GET_INVITE_ME_LIST_RESPONSE = "GET_INVITE_ME_LIST_RESPONSE";
        public static string GET_REQUEST_ME_LIST_RESPONSE = "GET_REQUEST_ME_LIST_RESPONSE";
        public static string ACCEPT_REQUEST_RESPONSE = "ACCEPT_REQUEST_RESPONSE";
        public static string REJECT_REQUEST_RESPONSE = "REJECT_REQUEST_RESPONSE";
        public static string ACCEPT_INVITE_RESPONSE = "ACCEPT_INVITE_RESPONSE";
        public static string REJECT_INVITE_RESPONSE = "REJECT_INVITE_RESPONSE";

        public static string TRUE = "TRUE";
        public static string FALSE = "FALSE";

        public static string NOT_FOUND = "NOT_FOUND";

        public static int MESSAGE_LENGTH_MAX = 100;

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

        public int SendGetMyEventList(int id, EndPoint to)
        {
            string msg = GET_MY_EVENT_LIST + PATTERN;
            msg += id;

            return Send(msg, to);
        }

        public int SendInviteAllNewPeople(RequestData request, EndPoint to) {
            string msg = INVITE_ALL_NEW_PEOPLE + PATTERN;
            msg += request.eventId + PATTERN_ITEM;
            msg += request.ownerId;

            return Send(msg, to);
        }
        
        public int SendGetInviteMeList(RequestData request, EndPoint to) {
            string msg = GET_INVITE_ME_LIST + PATTERN;
            msg += request.userId;

            return Send(msg, to);
        }
        
        public int SendGetRequestMeList(RequestData request, EndPoint to) {
            string msg = GET_REQUEST_ME_LIST + PATTERN;
            msg += request.userId;

            return Send(msg, to);
        }
        
        public int SendAcceptRequest(RequestData request, EndPoint to) {
            string msg = ACCEPT_REQUEST + PATTERN;
            msg += request.eventId + PATTERN_ITEM;
            msg += request.ownerId + PATTERN_ITEM;
            msg += request.userId;

            return Send(msg, to);
        }
        
        public int SendRejectRequest(RequestData request, EndPoint to) {
            string msg = REJECT_REQUEST + PATTERN;
            msg += request.eventId + PATTERN_ITEM;
            msg += request.ownerId + PATTERN_ITEM;
            msg += request.userId;

            return Send(msg, to);
        }
        
        public int SendAcceptInvite(RequestData request, EndPoint to) {
            string msg = ACCEPT_INVITE + PATTERN;
            msg += request.eventId + PATTERN_ITEM;
            msg += request.ownerId + PATTERN_ITEM;
            msg += request.userId;

            return Send(msg, to);
        }
        
        public int SendRejectInvite(RequestData request, EndPoint to) {
            string msg = REJECT_INVITE + PATTERN;
            msg += request.eventId + PATTERN_ITEM;
            msg += request.ownerId + PATTERN_ITEM;
            msg += request.userId;

            return Send(msg, to);
        }

        // send message data (multiple packet)
        public bool SendMessageData(string type, EndPoint to, string message) {

            int byteSend;
            int startIdx = 0;
            int msgLength;
            int packetNumber = message.Length / MESSAGE_LENGTH_MAX + 
                (message.Length % MESSAGE_LENGTH_MAX != 0 ? 1 : 0);
            string msg;
            do
            {
                Console.WriteLine("\nSending packet number: " + packetNumber);

                msgLength = int.Min(MESSAGE_LENGTH_MAX, message.Length - startIdx);
                msg = message.Substring(startIdx, msgLength);
                startIdx += msgLength;
                Console.WriteLine($"\n[DEBUG]:[SendMessageData]: [msg]: {msg}\n");
                msg = type + PATTERN + TRUE + PATTERN + msg + PATTERN + (packetNumber--);

                byte[] buffer = Encoding.UTF8.GetBytes(msg);

                byteSend = socket.SendTo(buffer, msg.Length, 0, to);
                if (byteSend == 0) return false;
            } while (packetNumber > 0);

            return true;
        }

        // recv
        public int RecvFromServer(Socket socket, ref byte[] buffer, int size, SocketFlags flag, ref EndPoint from)
        {
            return socket.ReceiveFrom(buffer, size, flag, ref from);
        }

    }
}
