using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    internal class RequestDTO : BaseAction
    {
        private int ownerId { get; set; }
        private int userId { get; set; }

        public RequestDTO(int id = 0,
            DateTime time = new DateTime(),
            ActionState state = ActionState.Sent,
            int eventId = 0,
            int ownerId = 0,
            int userId = 0)
        { }
    }
}
