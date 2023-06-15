using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;

namespace SportsStore
{
  public class Startup
  {
    private IConfiguration config;

    public Startup(IConfiguration config)
    {
      this.config = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews();
      services.AddDbContext<StoreDbContext>(opts =>
        opts.UseSqlServer(config.GetConnectionString("SportStoreConnection"))
      );
      services.AddScoped<IStoreRepository, EFStoreRepository>();
      services.AddScoped<IOrderRepository, EFOrderRepository>();
      services.AddDistributedMemoryCache();
      services.AddSession();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseStatusCodePages();////////new for the test xunit
      app.UseStaticFiles();
      app.UseSession();
      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "catpage",
          pattern: "{category}/Page{productPage:int}",
          defaults: new { controller = "Home", action = "Index" });
        endpoints.MapControllerRoute(
          name: "page",
          pattern: "Page{productPage:int}",
          defaults: new { controller = "Home", action = "Index" });
        endpoints.MapControllerRoute(
          name: "category",
          pattern: "{category}",
          defaults: new { controller = "Home", action = "Index", productPage = 1 });
      });
      //app.UseEndpoints(endpoints =>
      //{
      //  endpoints.MapControllerRoute(
      //      name: "default",
      //      pattern: "{controller=Home}/{action=Index}/{id?}");
      //});
      //app.UseEndpoints(endpoints =>
      //{
      //  endpoints.MapControllerRoute(
      //      name: "cart",
      //      pattern: "{controller=Cart}/{action=Index}/{id?}");
      //});
      //app.UseEndpoints(endpoints =>
      //{
      //  endpoints.MapControllerRoute(
      //      name: "product",
      //      pattern: "{controller=Product}/{action=List}/{category?}/{page?}");
      //});
      //app.UseEndpoints(endpoints =>
      //{
      //  endpoints.MapControllerRoute(
      //      name: "nav",
      //      pattern: "",
      //    defaults: new { controller = "Home", action = "Index", productPage = 1 });
      //});



      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
      });

      SeedData.EnsurePopulated(app);
    }
  }
}
