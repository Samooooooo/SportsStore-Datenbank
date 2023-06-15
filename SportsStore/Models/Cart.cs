using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
  public class CartLine
  {
    public int CartLineID { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
  }
  public class Cart
  {
    public List<CartLine> cartLines { get; set; } = new List<CartLine>();
    
    public void AddItem(Product product, int quantity)
    {
      CartLine line = cartLines
            .Where(l => l.Product.ProductID == product.ProductID)
            .FirstOrDefault();
      if (line == null)
      {
        cartLines.Add(new CartLine
        {
          Product = product,
          Quantity = quantity
        });
      }
      else
      {
        line.Quantity += quantity;
      }
    }
    public void RemoveLine(Product product)
    {
      cartLines.RemoveAll(l => l.Product.ProductID == product.ProductID);
    }
    public decimal ComuteTotalValue()
    {
      return cartLines.Sum(l => l.Product.Price * l.Quantity);
    }
    public void Clear()
    {
      cartLines.Clear();
    }
  }
}
