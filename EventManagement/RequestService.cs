using EventApp;
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
        public void AddRequest(RequestData request)
        {
            string query = "insert into request(eventId, ownerId, userId, createAt) " +
                "value(@eventId, @ownerId, @userId, @createAt)";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@eventId", request.eventId);
                cmd.Parameters.AddWithValue("@ownerId", request.ownerId);
                cmd.Parameters.AddWithValue("@userId", request.userId);
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
            string query = "update request set state = @state where id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@state", request.state);
                cmd.Parameters.AddWithValue("@id", request.requestId);

                try
                {
                    connection.Open();

                    cmd.ExecuteNonQuery();
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
        }
    }
}
