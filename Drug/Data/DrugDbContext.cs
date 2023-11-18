using Drug.Models;
using Microsoft.EntityFrameworkCore;

namespace Drug.Data
{
    public class DrugDbContext : DbContext
    {
        public DrugDbContext(DbContextOptions<DrugDbContext> options) : base(options)
        {


        }


        public DbSet<DrugInfo> Drugs { get; set; }

        public DbSet<CartInfo> Carts { get; set; }

        public DbSet<BillingTable> BillingTables { get; set; }
    }
}
