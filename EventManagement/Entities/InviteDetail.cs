using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Entities
{
    public class InviteDetail
    {
        public int ownerId { get; set; }
        public string ownerName { get; set; }

        public int eventId { get; set; }
        public string eventName { get; set; }
        public DateTime eventTime { get; set; }
        public string eventAddress { get; set; }
        
        public InviteDetail(int ownerId = 0, string ownerName = "",
            int eventId = 0, string eventName = "", DateTime eventTime = new DateTime(),
            string eventAddress = "") { 
            this.ownerId = ownerId;
            this.ownerName = ownerName;

            this.eventId = eventId;
            this.eventName = eventName;
            this.eventTime = eventTime;
            this.eventAddress = eventAddress;
        }

        public override string ToString() {
            return $"{ownerId}{SocketService.PATTERN_ITEM}" +
                $"{ownerName}{SocketService.PATTERN_ITEM}" +
                $"{eventId}{SocketService.PATTERN_ITEM}" +
                $"{eventName}{SocketService.PATTERN_ITEM}" +
                $"{FormatService.DateTimeToString(eventTime)}{SocketService.PATTERN_ITEM}" +
                $"{eventAddress}";
        }

        public static List<InviteDetail> ExtractInviteList(string msg) {

            List<InviteDetail> result = new List<InviteDetail> ();
            if (msg.Equals("")) return result;

            string[] dataLineStrs = msg.Trim().Split(SocketService.PATTERN_END_LINE);
            string[] items;
            foreach (string dataLine in dataLineStrs) {
                if (dataLine.Trim().Equals("")) continue;
                items = dataLine.Trim().Split(SocketService.PATTERN_ITEM);
                result.Add(new InviteDetail(Int32.Parse(items[0]),
                    items[1],
                    Int32.Parse(items[2]),
                    items[3],
                    FormatService.StringToDataTime(items[4]),
                    items[5]));
            }

            return result;
        }

        public static string ToStringLine(List<InviteDetail> inviteDetails) {
            string result = "";

            for (int i = 0; i < inviteDetails.Count; i++) { 
                result += inviteDetails[i].ToString();
                if (i < inviteDetails.Count - 1) result += SocketService.PATTERN_END_LINE;
            }

            return result;
        }
    }
}
