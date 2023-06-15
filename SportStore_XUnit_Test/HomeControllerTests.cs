using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using SportsStore.Controllers;
using SportsStore.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace SportStore_XUnit_Test
{
  public class HomeControllerTests
  {
    [Fact]
    public void CanUseRepository()
    {
      //Arrange
      Product[] products =
      {
        new Product(){ProductID = 1, Name="P1"},
        new Product(){ProductID = 2, Name="P2"},
      };
      Mock<IStoreRepository> mockRepository = new Mock<IStoreRepository>();
      mockRepository.SetupGet(m => m.Products).Returns(products.AsQueryable());

      HomeController homeController = new HomeController(mockRepository.Object);

      //Act
      var indexResult = (homeController.Index(null) as ViewResult)
        .ViewData
        .Model as VIewModel;
      Product[] prodArray = indexResult.Products.ToArray();

      //Assert
      Assert.Equal(2, prodArray.Length);
      Assert.Equal("P1", prodArray[0].Name);
      Assert.Equal("P2", prodArray[1].Name);
    }
  }
}
