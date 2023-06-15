using System.Linq;

namespace SportsStore.Models
{
  public class EFStoreRepository : IStoreRepository
  {
    private StoreDbContext dbContext;
    public EFStoreRepository(StoreDbContext dbContext)
    {
      this.dbContext = dbContext;
    }
    public IQueryable<Product> Products => dbContext.Products;
  }
}
