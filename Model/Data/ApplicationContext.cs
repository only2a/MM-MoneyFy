using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MM_MoneyFy.Model.Data
{
    class ApplicationContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Currency> Currencies { get; set; }


        public ApplicationContext() : base()
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DMITRY;Database=MoneyFy;User=User;Trusted_Connection=True;");
        }
    }
}
