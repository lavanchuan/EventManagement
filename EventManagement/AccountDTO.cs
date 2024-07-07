using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    public class AccountDTO : BaseEntity
    {
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public AccountDTO(int id = 0, string name = "", string username = "", string password = "") : base(id) {
            this.name = name;
            this.username = username;
            this.password = password;
        }


    }
}
