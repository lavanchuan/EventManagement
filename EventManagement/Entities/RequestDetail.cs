using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Entities
{
    public class RequestDetail
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public int eventId { get; set; }
        public int eventName { get; set; }
        public DateTime eventTime { get; set; }
        public string eventAddress { get; set; }

        public RequestDetail(int userId = 0, string userName = "",
            int eventId = 0, string eventName = "", DateTime eventTime = new DateTime(), string eventAddress = ""){ 
            this.userId = userId;
            this.userName = userName;
            this.eventId = eventId;
            this.eventTime = eventTime;
            this.eventAddress = eventAddress;
        }

        public override string ToString()
        {
            return $"{userId}{SocketService.PATTERN_ITEM}" +
                $"{userName}{SocketService.PATTERN_ITEM}" +
                $"{eventId}{SocketService.PATTERN_ITEM}" +
                $"{eventName}{SocketService.PATTERN_ITEM}" +
                $"{FormatService.DateTimeToString(eventTime)}{SocketService.PATTERN_ITEM}" +
                $"{eventAddress}";
        }

        public static List<RequestDetail> ExtractRequestList(string msg)
        {

            List<RequestDetail> result = new List<RequestDetail>();
            if (msg.Equals("")) return result;

            string[] dataLineStrs = msg.Trim().Split(SocketService.PATTERN_END_LINE);
            string[] items;
            foreach (string dataLine in dataLineStrs)
            {
                if (dataLine.Trim().Equals("")) continue;
                items = dataLine.Trim().Split(SocketService.PATTERN_ITEM);
                result.Add(new RequestDetail(Int32.Parse(items[0]),
                    items[1],
                    Int32.Parse(items[2]),
                    items[3],
                    FormatService.StringToDataTime(items[4]),
                    items[5]));
            }

            return result;
        }

        public static string ToStringLine(List<RequestDetail> requestDetails)
        {
            string result = "";

            for (int i = 0; i < requestDetails.Count; i++)
            {
                result += requestDetails[i].ToString();
                if (i < requestDetails.Count - 1) result += SocketService.PATTERN_END_LINE;
            }

            return result;
        }
    }
}
