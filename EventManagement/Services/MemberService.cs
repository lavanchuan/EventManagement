using EventApp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Services
{
    public class MemberService : DataService
    {

        public void Add(RequestData request) {
            string query = "insert into member(eventId, memberId) " +
                "value(@eventId, @memberId)";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@eventId", request.eventId);
                cmd.Parameters.AddWithValue("@memberId", request.memberId);

                try
                {
                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: Insert member");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
