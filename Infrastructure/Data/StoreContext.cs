using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastruce.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        
    }
}