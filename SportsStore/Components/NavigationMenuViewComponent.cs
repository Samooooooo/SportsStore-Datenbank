using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Components
{
  public class NavigationMenuViewComponent : ViewComponent
  {
    public NavigationMenuViewComponent(IStoreRepository repository)
    {
      this.repository = repository;
    }
    private IStoreRepository repository;
    public IViewComponentResult Invoke()
    {
      ViewBag.SelectedCategory = RouteData?.Values["category"];
      return View(repository.Products
        .Select(p => p.Category)
        .Distinct()
        .OrderBy(p => p)
        );
    }
  }
}
