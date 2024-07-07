using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    internal class EventDTO : BaseEntity
    {
        public string name {  get; set; }
        public DateTime time { get; set; }
        public string address { get; set; }
        public string description { get; set; }

        public EventDTO(int id = 0, string name = "", DateTime time = default,
            string address = "",
            string description = "") : base(id) { 
            this.name = name;
            this.time = time;
            this.description = description;
            this.address = address;
        }

        public string ToString()
        {
            return $"({id})\t{name}\t{FormatService.DateTimeToString(time)}\t" +
                $"{address}\t{description}";
        }
    }
}
