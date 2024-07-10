using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    public class MemberDTO : BaseEntity
    {
        public int eventId { get; set; }
        public int memberId { get; set; }

        public MemberDTO(int id = 0, int eventId = 0, int memberId = 0) : base(id)
        {
            this.eventId = eventId;
            this.memberId = memberId;
        }
    }
}
