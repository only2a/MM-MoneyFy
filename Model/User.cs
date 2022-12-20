using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM_MoneyFy.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Account> Accounts { get; set; }
        public bool IsAdmin { get; set; }

        [NotMapped]
        public List<Account> AccountsOfCurrentUser
        {
            get
            {
                return DataWorker.GetAllAccountsByUserId(Id);
            }
        }

    }
}
