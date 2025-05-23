﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace SportsStore.Models
{
  public static class SeedData
  {
    public static void EnsurePopulated(IApplicationBuilder app)
    {
      StoreDbContext dbContext = app
        .ApplicationServices
        .CreateScope()
        .ServiceProvider
        .GetRequiredService<StoreDbContext>();

      if (dbContext.Database.GetPendingMigrations().Any())
      {
        dbContext.Database.Migrate();
      }
      if (!dbContext.Products.Any())
      {
        dbContext.Products.AddRange(
        new Product
        {
          Name = "Kayak",
          Discription = "A boat for one person",
          Category = "Watersports",
          Price = 275
        },
        new Product
        {
          Name = "Lifejacket",
          Discription = "Protective and fashionable",
          Category = "Watersports",
          Price = 48.95m
        },
        new Product
        {
          Name = "Soccer Ball",
          Discription = "FIFA-approved size and weight",
          Category = "Soccer",
          Price = 19.50m
        },
        new Product
        {
          Name = "Corner Flags",
          Discription = "Give your playing field a professional touch",
          Category = "Soccer",
          Price = 34.95m
        },
        new Product
        {
          Name = "Stadium",
          Discription = "Flat-packed 35,000-seat stadium",
          Category = "Soccer",
          Price = 79500
        },
        new Product
        {
          Name = "Thinking Cap",
          Discription = "Improve brain efficiency by 75%",
          Category = "Chess",
          Price = 16
        },
        new Product
        {
          Name = "Unsteady Chair",
          Discription = "Secretly give your opponent a disadvantage",
          Category = "Chess",
          Price = 29.95m
        },
        new Product
        {
          Name = "Human Chess Board",
          Discription = "A fun game for the family",
          Category = "Chess",
          Price = 75
        },
        new Product
        {
          Name = "Bling-Bling King",
          Discription = "Gold-plated, diamond-studded King",
          Category = "Chess",
          Price = 1200
        }
        );
        dbContext.SaveChanges();
      }
    }
  }
}