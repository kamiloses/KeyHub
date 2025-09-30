using System;
using System.Threading.Tasks;
using KeyHub.Market.data;
using KeyHub.Market.Models;
using KeyHub.Market.Services.impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;



namespace KeyHub.Market.Tests.Services.impl
{
    public class PurchaseHistoryServiceTest
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<IDistributedCache> _cacheMock;
        private readonly Mock<ILogger<PurchaseHistoryService>> _loggerMock;
        private readonly PurchaseHistoryService _service; 

        public PurchaseHistoryServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _cacheMock = new Mock<IDistributedCache>();
            _loggerMock = new Mock<ILogger<PurchaseHistoryService>>();
            _service = new PurchaseHistoryService(_dbContext, _cacheMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetUserPurchaseHistoryAsync_WithPurchases_ReturnsCorrectData()
        {
            var userId = Guid.NewGuid().ToString();

            var game1 = new Game
            {
                Title = "Elden Ring", Price = 60, Discount = 10, Stock = 5, ImageUrl = "eldenring.jpg",
                Genre = Genre.RPG, Platform = Platform.Steam
            };
            var game2 = new Game
            {
                Title = "Bloodborne", Price = 50, Discount = 0, Stock = 5, ImageUrl = "bloodborne.jpg",
                Genre = Genre.Action, Platform = Platform.PSN
            };
            _dbContext.Games.AddRange(game1, game2);
            await _dbContext.SaveChangesAsync();

            var purchase1 = new Purchase
            {
                UserId = userId, GameId = game1.Id, PurchasePrice = 54, PurchaseDate = DateTime.UtcNow.AddDays(-1),
                Game = game1
            };
            var purchase2 = new Purchase
            {
                UserId = userId, GameId = game2.Id, PurchasePrice = 50, PurchaseDate = DateTime.UtcNow, Game = game2
            };
            _dbContext.Purchases.AddRange(purchase1, purchase2);
            await _dbContext.SaveChangesAsync();

            int page = 1, pageSize = 10;

            var (purchases, totalCount) = await _service.GetUserPurchaseHistoryAsync(userId, page, pageSize);

            Assert.Equal(2, totalCount);
            Assert.Equal(2, purchases.Count);
            Assert.Equal("Bloodborne", purchases[0].Game.Title);
            Assert.Equal("Elden Ring", purchases[1].Game.Title);
        }

        [Fact]
        public async Task GetUserPurchaseHistoryAsync_EmptyHistory_ReturnsEmptyList()
        {
            var userId = Guid.NewGuid().ToString();
            int page = 1, pageSize = 10;

            var (purchases, totalCount) = await _service.GetUserPurchaseHistoryAsync(userId, page, pageSize);

            Assert.Empty(purchases);
            Assert.Equal(0, totalCount);
        }

        [Fact]
        public async Task GetHistoryViewModelAsync_ReturnsCorrectViewModel()
        {
            var userId = Guid.NewGuid().ToString();

            var game = new Game
            {
                Title = "Dark Souls 3", Price = 55, Discount = 0, Stock = 5, ImageUrl = "darksouls3.jpg",
                Genre = Genre.Action, Platform = Platform.PSN
            };
            _dbContext.Games.Add(game);
            await _dbContext.SaveChangesAsync();

            var purchase = new Purchase
                { UserId = userId, GameId = game.Id, PurchasePrice = 55, PurchaseDate = DateTime.UtcNow, Game = game };
            _dbContext.Purchases.Add(purchase);
            await _dbContext.SaveChangesAsync();

            int page = 1, pageSize = 5;

            var viewModel = await _service.GetHistoryViewModelAsync(userId, page, pageSize);

            Assert.Single(viewModel.Purchases);
            Assert.Equal(1, viewModel.CurrentPage);
            Assert.Equal(1, viewModel.TotalPages);
            Assert.Equal("Dark Souls 3", viewModel.Purchases[0].Game.Title);
        }

    }
}
