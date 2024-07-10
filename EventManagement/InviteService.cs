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
    public class InviteService : DataService
    {

        DbContext dbContext = new DbContext();

        public void Add(RequestData request)
        {
            string query = "insert into invite(eventId, ownerId, userId, createAt, state) " +
                "value(@eventId, @ownerId, @userId, @createAt, @state)";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@eventId", request.eventId);
                cmd.Parameters.AddWithValue("@ownerId", request.ownerId);
                cmd.Parameters.AddWithValue("@userId", request.userId);
                cmd.Parameters.AddWithValue("@createAt", FormatService.DateTimeToString(request.createAt));
                cmd.Parameters.AddWithValue("@state", 0);

                try
                {
                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: Insert invite");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateInvite(RequestData request)
        {
            string query = "update invite set state = @state where eventId = @eventId and " +
                "ownerId = @ownerId and userId = @userId";

            int state = (request.state == ActionState.Sent ? 0 : request.state == ActionState.Accept ? 1 : 2);
            Console.WriteLine($"State: {state}");
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
                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: Update invite table");
                }
                finally
                {
                    connection.Close();
                }

                if(request.state == ActionState.Accept && result > 0)
                {
                    Console.WriteLine("Adding member");
                    request.memberId = request.userId;
                    (new MemberService()).Add(request);
                } 
            }
        }

        public List<InviteDetail> GetInviteDetailsMeListByUserId(int userId) {
            List<InviteDetail> result = new List<InviteDetail>();

            dbContext.loadInvites();

            foreach (InviteDTO invite in dbContext.invites) {
                //Console.WriteLine($"ownerId {invite.ownerId}\tuserId: {invite.userId}\t eventId: {invite.eventId}\n");
                if (invite.userId == userId && invite.state == ActionState.Sent) {
                    AccountDTO account = (new AccountService()).GetById(invite.ownerId);
                    EventDTO e = (new EventService()).GetById(invite.eventId);
                    result.Add(new InviteDetail(account.id, account.name,
                        e.id, e.name, e.time, e.address));
                }
            }

            Console.WriteLine($"\nTest Get By UserId:\nuserId{userId}\ninviteDetail Size: {result.Count}\n========\n");

            return result;
        }
    }
}
