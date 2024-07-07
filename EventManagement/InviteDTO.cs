using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    internal class InviteDTO : BaseAction
    {
        public int ownerId { get; set; }
        public int userId { get; set; }

        public InviteDTO(int id = 0, 
            DateTime time = new DateTime(),
            ActionState state = ActionState.Sent,
            int eventId = 0,
            int ownerId = 0,
            int userId = 0) { }
    }
}
