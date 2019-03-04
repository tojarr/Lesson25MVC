using Lesson25.Models;
using Lesson25.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson25.Repositories
{
    public class AccountRepository
    {
        private List<Account> accountList = new List<Account>
        {
            new Account
            {
                UserName = "Mr Garry Potter",
                Login = "garry",
                Password = "potter"
            },
            new Account
            {
                UserName = "Lady Gaga",
                Login = "lady",
                Password = "fox"
            }
        };

        public Account GetAccount(LoginVm loginVm)
        {
            return accountList
                 .SingleOrDefault(a => a.Login == loginVm.Login &&
                     a.Password == loginVm.Password);
        }

        public bool IsAccountExists(string login)
        {
            return accountList.Any(a => a.Login == login);
        }
    }
}