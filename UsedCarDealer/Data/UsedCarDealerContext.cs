using Microsoft.EntityFrameworkCore;
using UsedCarDealer.Entities;
using UsedCarDealer.Models;

namespace UsedCarDealer.Data
{
    public class UsedCarDealerContext : DbContext
    {
        public UsedCarDealerContext (DbContextOptions<UsedCarDealerContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Car { get; set; }
    }
}
