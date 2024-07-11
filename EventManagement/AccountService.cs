using EventApp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    public class AccountService : DataService
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

            Console.WriteLine($"[GetById] accountId({accountId})");

            dbContext.loadAccounts();

            foreach (AccountDTO acc in dbContext.accounts) {
                if (acc.id == accountId) return acc;
            }

            return null;
        }

        public int GetMaxAccountId() {
            dbContext.loadAccounts();
            int result = -1;
            foreach (AccountDTO acc in dbContext.accounts) {
                result = int.Max(result, acc.id);
            }
            return result;
        }

    }
}
