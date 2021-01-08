using System;
using Xunit;
using System.Collections.Generic;
using Flowershop.API.Controllers;
using Flowershop.API.Models;
using Flowershop.API.Models.Domain;
using Flowershop.API.Models.Web;
using Flowershop.API.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Snapshooter.Xunit;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Flowershop.Tests
{
    public class ShopsControllerTests : IDisposable
    {
        // Mocking, the concept: https://stackoverflow.com/questions/2665812/what-is-mocking
        // Mocking, the library: https://github.com/Moq/moq4/wiki/Quickstart
        private readonly Mock<ILogger<ShopController>> _loggerMock;
        private readonly Mock<IShopRepository> _shopRepoMock;
        private readonly ShopController _shopsController;

        public ShopsControllerTests()
        {
            _loggerMock = new Mock<ILogger<ShopController>>(MockBehavior.Loose);
            _shopRepoMock = new Mock<IShopRepository>(MockBehavior.Strict);
            _shopsController = new ShopController(_shopRepoMock.Object, _loggerMock.Object, _basisRegisterService);
            
        }
        public void Dispose()
        {
            _loggerMock.VerifyAll();
            _shopRepoMock.VerifyAll();

            _loggerMock.Reset();
            _shopRepoMock.Reset();
        }


        [Fact]
        public async Task TestGetAllShops()
        {
            var returnSet = new[]
            {
                new Shop
                {
                    Id = 1,
                    Name = "test name 1",
                    StreetName = "Zandpoortvest",
                    StreetNumber = "60",
                    Region = "Mechelen"
                },
                new Shop
                {
                    Id = 2,
                    Name = "test name 2",
                    StreetName = "Zandpoortvest",
                    StreetNumber = "60",
                    Region = "Mechelen"
                },
                new Shop
                {
                    Id = 3,
                    Name = "test name 3",
                    StreetName = "Zandpoortvest",
                    StreetNumber = "60",
                    Region = "Mechelen"
                },
            };
            // Arrange
            _shopRepoMock.Setup(x => x.getAllShops()).ReturnsAsync(returnSet).Verifiable();

            // Act
            var shopResponse = await _shopsController.getAllShops();

            // Assert
            shopResponse.Should().BeOfType<OkObjectResult>();

            // verify via a snapshot (https://swisslife-oss.github.io/snapshooter/)
            // used a lot in jest (for JS)
            Snapshot.Match(shopResponse);
        }

        [Fact]
        public async Task TestGetOneShop()
        {
            var Shop = new Shop()
            {
                Id = 1,
                Name = "test name",
                Address = "Zandpoortvest",
                StreetNumber = "60",
                Region = "Mechelen"
            };
            _shopRepoMock.Setup(x => x.getOneShopById(1)).Returns(Task.FromResult(Shop)).Verifiable();
            var shopResponse = await _shopsController.shopById(1);
            shopResponse.Should().BeOfType<OkObjectResult>();
            Snapshot.Match(shopResponse);
        }  

        [Fact]
        public async Task TestGetOneShopNotFound()
        {
            _shopRepoMock.Setup(x => x.getOneShopById(1)).Returns(Task.FromResult(null as Shop)).Verifiable();
            var shopResponse = await _shopsController.shopById(1);
            shopResponse.Should().BeOfType<NotFoundResult>();
            Snapshot.Match(shopResponse);
        }

    }
}
