using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    public class RequestDTO : BaseAction
    {
        public int ownerId { get; set; }
        public int userId { get; set; }

        public RequestDTO(int id = 0,
            DateTime createAt = new DateTime(),
            ActionState state = ActionState.Sent,
            int eventId = 0,
            int ownerId = 0,
            int userId = 0) : base (id, createAt, state, eventId)
        { 
            this.ownerId = ownerId;
            this.userId = userId;
        }
    }
}
