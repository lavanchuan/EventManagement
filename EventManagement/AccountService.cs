using EventApp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    internal class AccountService : DataService
    {
        DbContext dbContext = new DbContext();

        public void AddAccount(RequestData request) {
            string query = "insert into account(name, username, password) " +
                "value(@name, @username, @password)";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", request.name);
                cmd.Parameters.AddWithValue("@username", request.username);
                cmd.Parameters.AddWithValue("@password", request.password);

                try
                {
                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception e) {
                    Console.WriteLine("ERROR: Insert account");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public AccountDTO GetById(int accountId) {
            dbContext.loadAccounts();

            foreach (AccountDTO acc in dbContext.accounts) {
                if (acc.id == acc.id) return acc;
            }

            return null;
        }

    }
}
