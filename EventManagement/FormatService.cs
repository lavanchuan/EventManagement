using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    public class FormatService
    {
        public static string DateTimeToString(DateTime time) {
            return time.Year + "-" +
                time.Month + "-" +
                time.Day + " " +
                time.Hour + ":" +
                time.Minute;
        }

        public static DateTime StringToDataTime(string dateTime) {
            string[] dateItems = dateTime.Split(' ')[0].Split('-');
            string[] timeItems = dateTime.Split(' ')[1].Split(':');
            int year = Int32.Parse(dateItems[0]);
            int month = Int32.Parse(dateItems[1]);
            int day = Int32.Parse(dateItems[2]);

            int hour = Int32.Parse(timeItems[0]);
            int minute = Int32.Parse(timeItems[1]);

            return new DateTime(year, month, day, hour, minute, 0);
        }
        
        public static DateTime StringToDataTime(string dateTime, bool fromSql) {
            string[] dateItems = dateTime.Split(' ')[0].Split('/');
            string[] timeItems = dateTime.Split(' ')[1].Split(':');
            int year = Int32.Parse(dateItems[2]);
            int month = Int32.Parse(dateItems[1]);
            int day = Int32.Parse(dateItems[0]);

            int hour = Int32.Parse(timeItems[0]);
            int minute = Int32.Parse(timeItems[1]);

            return new DateTime(year, month, day, hour, minute, 0);
        }
    }
}
