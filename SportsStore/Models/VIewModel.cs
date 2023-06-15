using System.Linq;

namespace SportsStore.Models
{
  public class VIewModel
  {
    public IQueryable<Product> Products { get; set; }
    public Pages Pages { get; set; }
    public Order Order { get; set; }
    public string CurrentCatrgory { get; set; }
  }
}
