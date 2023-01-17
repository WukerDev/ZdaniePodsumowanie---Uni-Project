using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdaniePodsumowanie
{
    internal class obsluga : DbContext
    {
        public DbSet<pomiar> pomiar { get; set; }
        public DbSet<sensor> sensor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = pomiar.db");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
