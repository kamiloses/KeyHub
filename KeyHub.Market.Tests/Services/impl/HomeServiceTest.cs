using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using KeyHub.Market.data;
using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;
using KeyHub.Market.Services.impl;
using Xunit;

namespace KeyHub.Market.Tests.Services.impl
{
    public class HomeServiceTest
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<IDistributedCache> _cacheMock;
        private readonly Mock<ILogger<HomeService>> _loggerMock;
        private readonly HomeService _service;

        public HomeServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _cacheMock = new Mock<IDistributedCache>();
            _loggerMock = new Mock<ILogger<HomeService>>();
            _service = new HomeService(_dbContext, _cacheMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetTopDiscountedGamesFromDbAsync_ReturnsEmptyList_WhenCountIsZero()
        {
            var result = await _service.GetTopDiscountedGamesFromDbAsync(0);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTopDiscountedGamesFromDbAsync_ReturnsGamesFromDb()
        {
            _dbContext.Games.AddRange(
                new Game
                {
                    Id = 1,
                    Title = "Game 1",
                    Discount = 50,
                    Price = 100,
                    Stock = 10,
                    Genre = Genre.Action,
                    Platform = Platform.Steam,
                    ImageUrl = "url1"
                },
                new Game
                {
                    Id = 2,
                    Title = "Game 2",
                    Discount = 30,
                    Price = 80,
                    Stock = 5,
                    Genre = Genre.RPG,
                    Platform = Platform.PSN,
                    ImageUrl = "url2"
                }
            );
            await _dbContext.SaveChangesAsync();

            var result = await _service.GetTopDiscountedGamesFromDbAsync(2);

            Assert.Equal(2, result.Count);

            Assert.Equal("Game 1", result[0].Title);
            Assert.Equal("Game 2", result[1].Title);
        }

        [Fact]
        public async Task GetTopDiscountedGamesAsync_ReturnsCachedData_WhenCacheExists()
        {

            var gamesDto = new List<GameDto> { new GameDto { Id = 1, Title = "Cached Game" } };
            var serialized = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(gamesDto);

            _cacheMock.Setup(c => c.GetAsync(It.IsAny<string>(), default))
                .ReturnsAsync(serialized);


            var result = await _service.GetTopDiscountedGamesAsync(1);

            Assert.Single(result);
            Assert.Equal("Cached Game", result[0].Title);
            _cacheMock.Verify(c => c.GetAsync("HomePage", default), Times.Once);
        }

    }
}

