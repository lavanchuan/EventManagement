using EventApp;
using EventManagement.Services;
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

        public void AddEvent(RequestData request)
        {
            string query = "insert into event(name, time, address, description) " +
                "value(@name, @time, @address, @description)";

            int result = 0;

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", request.name);
                cmd.Parameters.AddWithValue("@time", FormatService.DateTimeToString(request.time));
                cmd.Parameters.AddWithValue("@address", request.address);
                cmd.Parameters.AddWithValue("@description", request.description);

                try
                {
                    connection.Open();

                    result = cmd.ExecuteNonQuery();
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

            Console.WriteLine("Max EventId: " + eventId);
            Console.WriteLine("OwnerId: " + request.ownerId);

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
            dbContext.loadEvents();

            foreach (EventDTO e in dbContext.events) {
                if (e.id == id) return true;
            }
            return false;
        }

        public int GetOwnerIdByEventId(int eventId)
        {
            foreach (AccountEventDTO accountEvent in (new DbContext()).accountEvents) {
                if (accountEvent.eventId == eventId) return accountEvent.ownerId;
            }

            return -1;
        }

        public List<EventDTO> GetByOwnerId(int ownerId) {
            List<EventDTO> result = new List<EventDTO>();

            dbContext.loadData();
            foreach (AccountEventDTO accountEvent in dbContext.accountEvents) {
                if (accountEvent.ownerId == ownerId) {
                    foreach (EventDTO item in dbContext.events) {
                        if (accountEvent.eventId == item.id) {
                            result.Add(item);
                        }
                    }
                }
            }

            return result;
        }

        public int InviteAllNewPeople(int eventId, int ownerId)
        {
            int counter = 0;
            List<int> memberIds = new List<int>();// account id da la thanh vien
            memberIds.Add(ownerId);

            dbContext.loadMembers();
            dbContext.loadAccounts();
            dbContext.loadInvites();

            // add memberId into memberIds
            foreach (MemberDTO m in dbContext.members) {
                if (m.eventId == eventId && !memberIds.Contains(m.memberId)) {
                    memberIds.Add(m.memberId);
                }
            }

            foreach (InviteDTO i in dbContext.invites) {
                if (i.eventId == eventId && !memberIds.Contains(i.userId))
                {
                    memberIds.Add(i.userId);
                }
            }

            foreach (AccountDTO acc in dbContext.accounts) {
                if (!memberIds.Contains(acc.id))
                {
                    memberIds.Add(acc.id);

                    if (InviteOneNewPeople(eventId, acc.id)) counter++;
                }
            }

            return counter;
        }

        public bool InviteOneNewPeople(int eventId, int userId) {
            dbContext.loadInvites();

            foreach (InviteDTO i in dbContext.invites) { 
                if(i.eventId == eventId && i.userId == userId) return false;
            }

            RequestData request = new RequestData();
            request.eventId = eventId;
            request.userId = userId;
            request.ownerId = GetOwnerIdByEventId(eventId);
            request.createAt = DateTime.Now;

            (new InviteService()).Add(request);

            return true;
        }

        public EventDTO GetById(int eventId) {
            dbContext.loadEvents();

            foreach (EventDTO e in dbContext.events) {
                if (e.id == eventId) return e;
            }

            return null;
        }
    }
}
