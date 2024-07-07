using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    internal class ValidService
    {
        public bool IsValidRequestLogin(RequestData request) {
            if (request.username == null || request.username.Equals("")) return false;
            if (request.password == null || request.password.Equals("")) return false;
            return true;
        }
        
        public bool IsValidRequestRegister(RequestData request) {
            if (request.username == null || request.username.Equals("")) return false;
            if (request.password == null || request.password.Equals("")) return false;
            if (request.name == null || request.name.Equals("")) return false;
            return true;
        }

        public bool IsValidRequestCreateNewEvent(RequestData request) {
            if (request.name == null || request.name.Equals("")) return false;
            if (request.address == null || request.address.Equals("")) return false;
            return true;
        }
    }
}
