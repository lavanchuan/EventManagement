using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    public class AuthenticationService
    {

        DbContext dbContext = new DbContext();
        AccountService accountService = new AccountService();

        public bool Register(RequestData request) {
            if (ExistsUsername(request.username)) return false;
            accountService.AddAccount(request);
            return true;
        }

        public bool Authentication(RequestData request) {
            foreach (AccountDTO account in dbContext.accounts) {
                if (account.username.Equals(request.username) &&
                    account.password.Equals(request.password)) return true;
            }

            return false;
        }

        private bool ExistsUsername(string username) {
            foreach (AccountDTO account in dbContext.accounts) {
                if (account.username.Equals(username)) return true;
            }
            return false;
        }

        public int GetIdByUsername(string username) {
            foreach (AccountDTO account in dbContext.accounts) {
                if (account.username.Equals(username)) return account.id;
            }

            return -1;
        }

        public string GetNameByUsername(string username) {
            foreach (AccountDTO account in dbContext.accounts)
            {
                if (account.username.Equals(username)) return account.name;
            }

            return "";
        }
    }
}
