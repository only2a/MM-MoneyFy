using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM_MoneyFy.Model
{
    public class Icon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
        public List<Account> Account { get; set; }
        public List<Category> Category { get; set; }
        public string Type { get; set; }
    }
}
