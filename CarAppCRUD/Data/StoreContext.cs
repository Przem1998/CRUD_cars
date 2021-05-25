using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
       // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         //   => optionsBuilder.UseSqlite("Data Source=UseCarDealer.db")
    }
}
