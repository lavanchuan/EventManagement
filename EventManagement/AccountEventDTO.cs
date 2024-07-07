using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    internal class AccountEventDTO : BaseEntity
    {
        public int ownerId { get; set; }
        public int eventId { get; set; }

        public AccountEventDTO(int id = 0, int ownerId = 0, int eventId = 0) : base(id){
            this.ownerId = ownerId;
            this.eventId = eventId;
        }
    }
}
