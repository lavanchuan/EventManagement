using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Responses
{
    public class ServerResponse
    {
        public string type { get; set; }
        public string result { get; set; }
        public string msg { get; set; }
        public string other { get; set; }

        public ServerResponse(string message) {

            type = "";
            result = "";
            msg = "";
            other = "";

            string[] items = message.Split(SocketService.PATTERN);
            for(int i = 0; i < items.Length; i++)
            {
                switch (i) {
                    case 0: type = items[i]; break;
                    case 1: result = items[i]; break;
                    case 2: msg = items[i]; break;
                    default: other = items[i]; break;
                }
            }
        }
    }
}
