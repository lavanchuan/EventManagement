using EventApp;
using EventManagement.Entities;
using EventManagement.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    public class RequestService : DataService
    {

        DbContext dbContext = new DbContext();

        public void AddRequest(RequestData request)
        {
            Console.WriteLine("DEBUG: eventId" + request.eventId);
            Console.WriteLine("DEBUG: ownerId" + request.ownerId);
            Console.WriteLine("DEBUG: userIdId" + request.userId);
            Console.WriteLine("DEBUG: createAt" + FormatService.DateTimeToString(request.createAt));


            string query = "insert into request(eventId, ownerId, userId, state, createAt) " +
                "value(@eventId, @ownerId, @userId, @state, @createAt)";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@eventId", request.eventId);
                cmd.Parameters.AddWithValue("@ownerId", request.ownerId);
                cmd.Parameters.AddWithValue("@userId", request.userId);
                cmd.Parameters.AddWithValue("@state", 0);
                cmd.Parameters.AddWithValue("@createAt", FormatService.DateTimeToString(request.createAt));

                try
                {
                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: Insert request table");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateRequest(RequestData request)
        {
            string query = "update request set state = @state where eventId = @eventId and " +
                "ownerId = @ownerId and userId = @userId";

            int state = (request.state == ActionState.Sent ? 0 : request.state == ActionState.Accept ? 1 : 2);

            int result = 0;

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@eventId", request.eventId);
                cmd.Parameters.AddWithValue("@ownerId", request.ownerId);
                cmd.Parameters.AddWithValue("@userId", request.userId);
                cmd.Parameters.AddWithValue("@state", state);

                try
                {
                    connection.Open();

                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: Update request table");
                }
                finally
                {
                    connection.Close();
                }
            }

            //Console.WriteLine("Result: " + result);

            // update member
            if (request.state == ActionState.Accept && result > 0) {
                request.memberId = request.userId;

                (new MemberService()).Add(request);
            }
        }

        public List<RequestDetail> GetRequestDetailsMeListByUserId(int userId)
        {
            List<RequestDetail> result = new List<RequestDetail>();

            dbContext.loadRequests();

            foreach (RequestDTO request in dbContext.requests)
            {
                if (request.ownerId == userId && request.state == ActionState.Sent)
                {
                    AccountDTO account = (new AccountService()).GetById(request.userId);
                    EventDTO e = (new EventService()).GetById(request.eventId);
                    result.Add(new RequestDetail(account.id, account.name,
                        e.id, e.name, e.time, e.address));
                }
            }

            //Console.WriteLine($"\nTest Get By UserId:\nuserId{userId}\ninviteDetail Size: {result.Count}\n========\n");

            return result;
        }
    }
}
