using Microsoft.EntityFrameworkCore;
namespace SportsStore.Models
{
  public class StoreDbContext : DbContext
  {
    public StoreDbContext(DbContextOptions<StoreDbContext> opts) : base(opts)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
  }
}
