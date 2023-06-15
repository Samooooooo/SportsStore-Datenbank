using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace SportStore_XUnit_Test
{
  public class CartTests
  {
    [Fact]
    public void CanAddNewLines()
    {
      //Arrange
      Cart cart = new Cart();
      Product p1 = new Product() { ProductID = 1, Name = "P1" };
      Product P2 = new Product() { ProductID = 2, Name = "P2" };

      //Act
      cart.AddItem(p1, 1);
      cart.AddItem(P2, 3);
      List<CartLine> result = cart.cartLines;

      //Assert
      Assert.Equal(2, result.Count);
      Assert.Equal("P1", result[0].Product.Name);
      Assert.Equal(1, result[0].Quantity);
      Assert.Equal("P2", result[1].Product.Name);
      Assert.Equal(3, result[1].Quantity);
    }
    [Fact]
    public void CanAddQuintityForExistingProduct()
    {
      //Arrange
      Cart cart = new Cart();
      Product p1 = new Product() { ProductID = 1, Name = "P1" };

      //Act
      cart.AddItem(p1, 1);
      cart.AddItem(p1, 2);
      List<CartLine> result = cart.cartLines;

      //Assert
      //Assert
      Assert.Single(result);
      Assert.Equal("P1", result[0].Product.Name);
      Assert.Equal(3, result[0].Quantity);
    }
    [Fact]
    public void CanRemoveLine()
    {
      //Arrange
      Cart cart = new Cart();
      Product p1 = new Product() { ProductID = 1, Name = "P1" };
      Product p2 = new Product() { ProductID = 2, Name = "P2" };
      Product p3 = new Product() { ProductID = 3, Name = "P2" };

      //Act
      cart.AddItem(p1, 5);
      cart.AddItem(p2, 5);
      cart.AddItem(p3, 5);
      cart.RemoveLine(p2);

      List<CartLine> result = cart.cartLines;

      //Assert
      Assert.Equal(2, result.Count);
      Assert.Empty(result.Where(p => p.Product == p2));
    }
    [Fact]
    public void CanCalculateTotalValue()
    {
      //Arrange
      Cart cart = new Cart();
      Product p1 = new Product() { ProductID = 1, Price = 100M };
      Product p2 = new Product() { ProductID = 2, Price = 50M };

      //Act
      cart.AddItem(p1, 5);
      cart.AddItem(p2, 5);
      cart.AddItem(p2, 5);
      cart.ComuteTotalValue();

      //Assert
      Assert.Equal(1000M, cart.ComuteTotalValue());
    }
    [Fact]
    public void CanClear()
    {
      //Arrange
      Cart cart = new Cart();
      Product p1 = new Product() { ProductID = 1, Price = 100M };
      Product p2 = new Product() { ProductID = 2, Price = 50M };

      //Act
      cart.AddItem(p1, 5);
      cart.AddItem(p2, 5);
      cart.AddItem(p2, 5);
      cart.Clear();
      List<CartLine> result = cart.cartLines;

      //Assert
      Assert.Empty(result);
    }
  }
}
