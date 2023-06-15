namespace SportsStore.Models
{
  public class CartViewModel
  {
    public Cart Cart { get; set; }
    public string ReturnUrl { get; set; }
    public Order  Order { get; set; }
  }
}
