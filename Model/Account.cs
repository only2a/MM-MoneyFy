using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM_MoneyFy.Model
{
    public class Account
    {
        public int Id { get; set; }
        public int iconId { get; set; }
        public  Icon icon {get;set;}
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public List<Operation> Operations { get; set; }
        public int userID { get; set; }
        public  User user { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        [NotMapped]
        public string IconPath
        {
            get
            {
                return DataWorker.GetIconPathById(iconId);
            }
        }
        [NotMapped]
        public string CurrencyName
        {
            get
            {
                return DataWorker.GetCurrencyNameById(CurrencyId);
            }
        }

        [NotMapped]
        public string UserName
        {
            get
            {
                return DataWorker.GetUserNameById(userID);
            }
        }

        [NotMapped]
        public List<Operation> OperationsOfCurrentAccount
        {
            get
            {
                return DataWorker.GetAllOperationsByAccountId(Id);
            }
        }
    }
}
