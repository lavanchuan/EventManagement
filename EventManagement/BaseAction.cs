using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    public class BaseAction : BaseEntity
    {
        public DateTime createAt {get; set;}
        public ActionState state { get; set;}
        public int eventId { get; set; }

        public BaseAction(int id = 0, DateTime createAt = new DateTime(),
            ActionState state = ActionState.Sent,
            int eventId = 0) : base(id) { 
            this.createAt = createAt;
            this.state = state;
            this.eventId = eventId;
        }
    }
}
