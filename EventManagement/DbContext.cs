using EventApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Data;

namespace EventManagement
{
    internal class DbContext
    {
        private DataService dataService;

        public List<AccountDTO> accounts { get; set; }
        public List<EventDTO> events { get; set; }
        public List<InviteDTO> invites { get; set; }
        public List<RequestDTO> requests { get; set; }
        public List<AccountEventDTO> accountEvents { get; set; }

        public DbContext()
        {
            loadData();
        }

        // 
        public void loadData() {
            dataService = new DataService();

            loadAccounts();
            loadEvents();
            loadInvites();
            loadRequests();
            loadAccountEvents();
        }

        private void loadAccounts() {
            accounts = new List<AccountDTO>();
            
            string query = "select id, name, username, password from account";
            string data = dataService.GetDataLine(dataService.GetDataTable(query));
            string[] accountLine = data.Split("\n");
            string[] items;
          
            foreach (string line in accountLine) { 
                string dataItem  = line.Trim();
                if (dataItem.Equals("")) continue;
                items = dataItem.Split("\t");
                accounts.Add(new AccountDTO(Int32.Parse(items[0]),
                    items[1], items[2], items[3]));
            }
            
        }
        private void loadEvents() {
            events = new List<EventDTO>();

            string query = "select id, name, time, address, description from event";
            string data = dataService.GetDataLine(dataService.GetDataTable(query));
            string[] eventLine = data.Split("\n");
            string[] items;

            foreach (string line in eventLine)
            {
                string dataItem = line.Trim();
                if (dataItem.Equals("")) continue;
                items = dataItem.Split("\t");
                events.Add(new EventDTO(Int32.Parse(items[0]),
                    items[1],
                    FormatService.StringToDataTime(items[2], true),
                    items[3],
                    items[4]
                    ));
            }

        }
        private void loadInvites() {
            invites = new List<InviteDTO>();

            string query = "select id, createAt, state, eventId, ownerId, userId from invite";
            string data = dataService.GetDataLine(dataService.GetDataTable(query));
            string[] lines = data.Split("\n");
            string[] items;
            foreach (string line in lines)
            {
                string dataItem = line.Trim();
                if (dataItem.Equals("")) continue;
                items = dataItem.Split("\t");
                ActionState state = Int32.Parse(items[2]) == 0 ? ActionState.Sent :
                     Int32.Parse(items[2]) == 1 ? ActionState.Accept : ActionState.Reject;
                invites.Add(new InviteDTO(Int32.Parse(items[0]),
                    FormatService.StringToDataTime(items[1], true),
                    state,
                    Int32.Parse(items[3]),
                    Int32.Parse(items[4]),
                    Int32.Parse(items[5])
                    ));
            }

        }
        private void loadRequests() {
            requests = new List<RequestDTO>();

            string query = "select id, createAt, state, eventId, ownerId, userId from request";
            string data = dataService.GetDataLine(dataService.GetDataTable(query));
            string[] lines = data.Split("\n");
            string[] items;
            foreach (string line in lines)
            {
                string dataItem = line.Trim();
                if (dataItem.Equals("")) continue;
                items = dataItem.Split("\t");
                ActionState state = Int32.Parse(items[2]) == 0 ? ActionState.Sent :
                     Int32.Parse(items[2]) == 1 ? ActionState.Accept : ActionState.Reject;
                requests.Add(new RequestDTO(Int32.Parse(items[0]),
                    FormatService.StringToDataTime(items[1], true),
                    state,
                    Int32.Parse(items[3]),
                    Int32.Parse(items[4]),
                    Int32.Parse(items[5])
                    ));
            }
        }

        private void loadAccountEvents() {
            accountEvents = new List<AccountEventDTO>();

            string query = "select id, ownerId, eventId from account_event";
            string data = dataService.GetDataLine(dataService.GetDataTable(query));
            string[] lines = data.Split("\n");
            string[] items;
            foreach (string line in lines)
            {
                string dataItem = line.Trim();
                if (dataItem.Equals("")) continue;
                items = dataItem.Split("\t");

                accountEvents.Add(new AccountEventDTO(Int32.Parse(items[0]),
                    Int32.Parse(items[1]), Int32.Parse(items[2])));
            }
        }

        // 
        public bool AddAccount(AccountDTO account) {
            int count = accounts.Count;
            string query = "insert into account(name, username, password) value" +
                " ('" + account.name + "', '" + account.username + "', '" + account.password + "')";

            Console.WriteLine("["+query+"]");

            dataService.GetDataAdapter(query);
            loadAccounts();
            if (accounts.Count > count) return true;
            return false;
        }

    }
}
