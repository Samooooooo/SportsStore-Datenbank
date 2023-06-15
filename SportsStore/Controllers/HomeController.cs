using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using System;
using System.Linq;

namespace SportsStore.Controllers
{
  public class HomeController : Controller
  {
    private IStoreRepository repository;
    public HomeController(IStoreRepository repository)
    {
      this.repository = repository;
    }

    public IActionResult Index(string category, int productPage = 1)
    {
      VIewModel model = new VIewModel()
      {
        Products = repository.Products
          .Where(p => category == null || p.Category == category)
          .OrderBy(p => p.ProductID)
          .Skip((productPage - 1) * Pages.PageSize)
          .Take(Pages.PageSize),
        Pages = new Pages()
        {
          CurrentPage = productPage,
          TotalPages = (int)Math.Ceiling((decimal)repository.Products
            .Where(p => category == null || p.Category == category)
            .Count() / Pages.PageSize)
        }
      };
      return View("Index", model);
    }
    [HttpGet]
    public ActionResult Cart(string returnUrl)
    {
      CartViewModel cartModel = new CartViewModel()
      {
        Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart(),
        ReturnUrl = returnUrl ?? "/"
      };
      return View(cartModel);
    }

    [HttpPost]
    public IActionResult Cart(long productId, string returnUrl)
    {
      Cart cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
      Product products = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
      cart.AddItem(products, 1);
      HttpContext.Session.SetJson("cart", cart);
      return RedirectToAction(nameof(Cart), new { returnUrl });
    }
    public IActionResult RemoveLine(long productId, string returnUrl)
    {
      Cart cart = HttpContext.Session.GetJson<Cart>("cart");
      Product product = cart.cartLines.First(p => p.Product.ProductID == productId).Product;
      cart.RemoveLine(product);
      HttpContext.Session.SetJson("cart", cart);
      return RedirectToAction(nameof(Cart), new { returnUrl });
    }
    public IActionResult IncreasLine(long productId, string returnUrl)
    {
      Cart cart = HttpContext.Session.GetJson<Cart>("cart");
      CartLine line = cart.cartLines
              .First(l => l.Product.ProductID == productId);
      line.Quantity++;
      HttpContext.Session.SetJson("cart", cart);
      return RedirectToAction(nameof(Cart), new { returnUrl });
    }
    public IActionResult DecreasLine(long productId, string returnUrl)
    {
      Cart cart = HttpContext.Session.GetJson<Cart>("cart");
      CartLine line = cart.cartLines
              .First(l => l.Product.ProductID == productId);
      line.Quantity -= (line.Quantity > 1 ? 1 : 0);
      HttpContext.Session.SetJson("cart", cart);
      return RedirectToAction(nameof(Cart), new { returnUrl });
    }
  }
}