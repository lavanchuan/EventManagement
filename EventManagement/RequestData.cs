using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    public class RequestData
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }

        public DateTime time { get; set; }
        public string address { get; set; }
        public string description { get; set; }

        public int eventId { get; set; }
        public int ownerId { get; set; }
        public int userId { get; set; }
        public DateTime createAt { get; set; }

        public ActionState state { get; set; }
        public int requestId { get; set; }
        public int inviteId { get; set; }
    }
}
