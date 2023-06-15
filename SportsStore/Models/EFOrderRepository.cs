using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq;

namespace SportsStore.Models
{
  public class EFOrderRepository : IOrderRepository
  {
    private StoreDbContext dbContext;
    public EFOrderRepository(StoreDbContext dbContext)
    {
      this.dbContext = dbContext;
    }
    public IQueryable<Order> Orders => dbContext.Orders
              .Include(o => o.Lines)
              .ThenInclude(l => l.Product);

    public void SaveOrder(Order order)
    {
      dbContext.AttachRange(order.Lines.Select(l => l.Product));
      if (order.OrderID == 0)
      {
        dbContext.Orders.Add(order);
      }
      dbContext.SaveChanges();
    }
  }
}
