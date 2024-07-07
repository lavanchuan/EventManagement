using EventApp;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    public class EventService : DataService
    {

        DbContext dbContext = new DbContext();

        public EventService() : base() { }

        public void AddEvent(RequestData request)
        {
            string query = "insert into event(name, time, address, description) " +
                "value(@name, @time, @address, @description)";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", request.name);
                cmd.Parameters.AddWithValue("@time", FormatService.DateTimeToString(request.time));
                cmd.Parameters.AddWithValue("@address", request.address);
                cmd.Parameters.AddWithValue("@description", request.description);

                try
                {
                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: Insert event");
                }
                finally
                {
                    connection.Close();
                }
            }

            // 
            query = "select max(id) from event";
            string data = GetDataLine(GetDataTable(query));
            int eventId = Int32.Parse(data.Trim());

            // 
            query = "insert into account_event(eventId, ownerId) value (@eventId, @ownerId)";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@eventId", eventId);
                cmd.Parameters.AddWithValue("@ownerId", request.ownerId);

                try
                {
                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: Insert account_event");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool ExistsById(int id) {
            foreach (EventDTO e in dbContext.events) { 
                if(e.id == id) return true;
            }
            return false;
        }

        public int GetOwnerIdByEventId(int eventId)
        {
            foreach(AccountEventDTO accountEvent in (new DbContext()).accountEvents) {
                if (accountEvent.eventId == eventId) return accountEvent.ownerId;
            }

            return -1;
        }
    }
}
