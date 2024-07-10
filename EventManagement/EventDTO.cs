using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    public class EventDTO : BaseEntity
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

        public static List<EventDTO> ExtractEventList(string msg) {
            Console.WriteLine($"[DEBUG]:[EventDTO.ExtractEventList]: [msg]: {msg}");
            List<EventDTO> result = new List<EventDTO>();
            
            string[] eventLineStrs = msg.Split(SocketService.PATTERN_END_LINE);
            string[] items;
            foreach(string eventLineStr in eventLineStrs) {
            Console.WriteLine($"[DEBUG]:[EventDTO.ExtractEventList]: [eventLineStr]:{eventLineStr}");
                if (eventLineStr.Trim().Equals("")) continue;
                items = eventLineStr.Split(SocketService.PATTERN_ITEM);
                result.Add(new EventDTO(Int32.Parse(items[0]),
                    items[1],
                    FormatService.StringToDataTime(items[2]),
                    items[3],
                    items[4]));
            }

            return result;
        }

        /// <summary>
        /// Chuyển list sang dạng chuỗi
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        public static string ToStringLine(List<EventDTO> events) {
            string result = "";

            for (int i = 0; i < events.Count; i++) { 
                result += events[i].id + SocketService.PATTERN_ITEM;
                result += events[i].name + SocketService.PATTERN_ITEM;
                result += FormatService.DateTimeToString(events[i].time) + SocketService.PATTERN_ITEM;
                result += events[i].address + SocketService.PATTERN_ITEM;
                result += events[i].description;
                if(i < events.Count - 1) result += SocketService.PATTERN_END_LINE;
            }

            return result;
        }
    }
}
