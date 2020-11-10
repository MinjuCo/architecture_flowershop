using System;
using System.Collections.Generic;
using BasicRestApi.API.Controllers;
using BasicRestApi.API.Models;
using BasicRestApi.API.Models.Domain;
using BasicRestApi.API.Models.Web;
using BasicRestApi.API.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Snapshooter.Xunit;
using Xunit;

namespace BasicRestApi.Tests.Unit
{
    public class ShopControllerTests : IDisposable
    {
        //Mock: to verify that the object under test writes some specific data to the database
        private readonly Mock<ILogger<ShopController>> _loggerMock;
        private readonly Mock<IShopRepository> _shopRepoMock;
        private readonly ShopController _shopController;

        public ShopControllerTests()
        {
            // Ignore whatever logging is being done. We still need to mock it to avoid
            // null reference exceptions; loose mocks just handle whatever you throw at them.
            _loggerMock = new Mock<ILogger<ShopController>>(MockBehavior.Loose);
            _shopRepoMock = new Mock<IShopRepository>(MockBehavior.Strict);
            _shopController = new ShopController(_shopRepoMock.Object, _loggerMock.Object);
        }
        public void Dispose()
        {
            _loggerMock.VerifyAll();
            _shopRepoMock.VerifyAll();

            _loggerMock.Reset();
            _shopRepoMock.Reset();
        }

        [Fact]
        public void TestGetAllShops()
        {
            var returnSet = new[]
            {
                new Shop
                {
                    Bouquets = new List<Bouquet>(200),
                    Id = 1,
                    Name = "Test shop 1"
                },
                new Shop
                {
                    Bouquets = new List<Bouquet>(200),
                    Id = 2,
                    Name = "Test shop 2"
                },
                new Shop
                {
                    Bouquets = new List<Bouquet>(200),
                    Id = 2,
                    Name = "Test shop 3"
                },
            };
            //Arrange
            _shopRepoMock.Setup(x => x.GetAllShops()).Returns(returnSet).Verifiable();

            //Act
            var shopResponse = _shopController.GetAllShops();

            //Assert
            shopResponse.Should().BeOfType<OkObjectResult>();

            // verify via a snapshot (https://swisslife-oss.github.io/snapshooter/)
            Snapshot.Match(shopResponse);
        }
    }
}
