using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM_MoneyFy.Model
{
    public class Operation
    {
        public int Id { get; set;  }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public  Category Category { get; set; }
        public int AccountId { get; set; }
        public  Account Account { get; set; }
        public decimal Sum { get; set; }
        public DateTime date { get; set; }

        [NotMapped]
        public Category OperationCategory
        {
            get
            {
                return DataWorker.GetCategoryById(CategoryId);
            }
        }
        [NotMapped]
        public Account OperationAccount
        {
            get
            {
                return DataWorker.GetAccountById(AccountId);
            }
        }
    }
}
