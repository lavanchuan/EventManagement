using System.ComponentModel;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using EventManagement;

namespace EventApp
{
    public class DataService
    {
        protected string server { get; set; }
        protected string database { get; set; }
        protected string username { get; set; }
        protected string password { get; set; }

        public MySqlConnection connection { get; set; }

        public DataService(string server = "LOCALHOST",
            string database = "db_event",
            string username = "root",
            string password = "")
        {
            this.server = server;
            this.database = database;
            this.username = username;
            this.password = password;

            connection = new MySqlConnection(connectString());
        }

        private string connectString()
        {
            return "SERVER=" + server + " ;" +
                "DATABASE=" + database + " ;" +
                "UID=" + username + " ;" +
                "PASSWORD=" + password + " ;";
        }

        public MySqlDataAdapter GetDataAdapter(string query)
        {
            return new MySqlDataAdapter(query, connection);
        }

        public DataTable GetDataTable(string query)
        {
            try
            {
                MySqlDataAdapter adapter = GetDataAdapter(query);

                DataTable dataTable = new DataTable();

                connection.Open();

                adapter.Fill(dataTable);

                connection.Close();

                return dataTable;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR");
                
                throw new Exception();
            }
        }

     
        public string GetDataLine(DataTable dataTable) {
            string datas = "";
            try
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        datas += item + "\t";
                    }
                    datas += "\n";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR");
            }
            return datas;
        }

        
    }
}
