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
    public class BouquetsControllerTests : IDisposable
    {
        // Mocking, the concept: https://stackoverflow.com/questions/2665812/what-is-mocking
        // Mocking, the library: https://github.com/Moq/moq4/wiki/Quickstart
        private readonly Mock<ILogger<BouquetController>> _loggerMock;
        private readonly Mock<IBouquetRepository> _bouquetRepoMock;
        private readonly BouquetController _bouquetsController;

        public BouquetsControllerTests()
        {
            _loggerMock = new Mock<ILogger<BouquetController>>(MockBehavior.Loose);
            _bouquetRepoMock = new Mock<IBouquetRepository>(MockBehavior.Strict);
            _bouquetsController = new BouquetController(_bouquetRepoMock.Object, _loggerMock.Object);
        }

        public void Dispose()
        {
            _loggerMock.VerifyAll();
            _bouquetRepoMock.VerifyAll();

            _loggerMock.Reset();
            _bouquetRepoMock.Reset();
        }

        [Fact]
        public async Task TestGetAllBouquets()
        {
            var returnSet = new[]
            {
                new Bouquet
                {
                    Id = 1,
                    ShopId = 3,
                    Name = "test Flower",
                    Price = 20,
                    Description = "Lorem ipsum"
                },
                new Bouquet
                {
                    Id = 2,
                    ShopId = 2,
                    Name = "test roses",
                    Price = 30,
                    Description = "Lorem ipsum"
                },
                new Bouquet
                {
                    Id = 3,
                    ShopId = 3,
                    Name = "test tulips",
                    Price = 12.15,
                    Description = "test ipsum"
                },
            };
            // Arrange
            _bouquetRepoMock.Setup(x => x.getAllBouquets(3)).ReturnsAsync(returnSet).Verifiable();

            // Act
            var bouquetResponse = await _bouquetsController.getAllBouquetsForShop(3);

            // Assert
            bouquetResponse.Should().BeOfType<OkObjectResult>();

            // verify via a snapshot (https://swisslife-oss.github.io/snapshooter/)
            // used a lot in jest (for JS)
            Snapshot.Match(bouquetResponse);
        }

        [Fact]
        public async Task TestGetOneBouquet()
        {
            var bouquet = new Bouquet()
            {
                Id = 1,
                ShopId = 2,
                Name = "Roses",
                Price = 20,
                Description = "Roses are red"
            };
            _bouquetRepoMock.Setup(x => x.getOneBouquetById(2, 1)).Returns(Task.FromResult(bouquet)).Verifiable();
            var bouquetResponse = await _bouquetController.getBouquetFromShop(2, 1);
            bouquetResponse.Should().BeOfType<OkObjectResult>();
            Snapshot.Match(bouquetResponse);
        }  

        [Fact]
        public async Task TestGetOneBouquetNotFound()
        {
            _bouquetRepoMock.Setup(x => x.getOneBouquetById(1)).Returns(Task.FromResult(null as Bouquet)).Verifiable();
            var bouquetResponse = await _bouquetController.getBouquetFromShop(2, 1);
            bouquetResponse.Should().BeOfType<NotFoundResult>();
            Snapshot.Match(bouquetResponse);
        }

        [Fact]
        public async Task TestCreateBouquet()
        {
            var bouquet = new Bouquet()
            {
                Id = 1,
                Name = "tulipso",
                Price = 10,
                Description = "red and yellow"
            };            
            _bouquetRepoMock.Setup(x => x.insert(1, "tulipso", 10, "red and yellow")).ReturnsAsync(bouquet).Verifiable();
            var bouquetResponse = await _bouquetController.addBouquetToShop(new BouquetUpsertInput()
            {
                Id = 1,
                Name = "tulipso",
                Price = 10,
                Description = "red and yellow"
            });
            bouquetResponse.Should().BeOfType<CreatedResult>();
            Snapshot.Match(bouquetResponse);
        }

        [Fact]
        public async Task TestUpdateOneBouquet()
        {
            var bouquet = new Bouquet()
            {
                Name = "test name",
                Price = 10,
                Description = "test description"
            };            
            _bouquetRepoMock.Setup(x => x.update(3, 1, "test name", 10, "test description")).Returns(Task.FromResult(bouquet)).Verifiable();
            var bouquetResponse = await _bouquetController.updateBouquetToShop(1, new BouquetUpsertInput()
            {
                Name = "test name",
                Price = 10,
                Description = "test description"
            });
            bouquetResponse.Should().BeOfType<AcceptedResult>();
            Snapshot.Match(bouquetResponse);
        }

    }
}
