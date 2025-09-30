using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using KeyHub.Market.data;
using KeyHub.Market.Exceptions;
using KeyHub.Market.Models;
using KeyHub.Market.Services.impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KeyHub.Market.Tests.Services.impl;

[TestSubject(typeof(PurchaseService))]
public class PurchaseServiceTest
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<ILogger<PurchaseService>> _loggerMock;
        private readonly PurchaseService _service;

        public PurchaseServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unikalna baza dla każdego testu
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _loggerMock = new Mock<ILogger<PurchaseService>>();
            _service = new PurchaseService(_dbContext, _loggerMock.Object);
        }

        [Fact]
        public async Task BuyGameAsync_SuccessfulPurchase_DecreasesStockAndBalance()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid().ToString(), UserName = "testuser", Balance = 100 };
            var game = new Game 
            { 
                Title = "Elden Ring", Price = 50, Discount = 10, Stock = 5, 
                ImageUrl = "eldenring.jpg", Genre = Genre.RPG, Platform = Platform.Steam 
            };
            _dbContext.Users.Add(user);
            _dbContext.Games.Add(game);
            await _dbContext.SaveChangesAsync();

            // Act
            await _service.BuyGameAsync(game.Id, user);

            // Assert
            var updatedGame = await _dbContext.Games.FindAsync(game.Id);
            var updatedUser = await _dbContext.Users.FindAsync(user.Id);
            var purchase = await _dbContext.Purchases.FirstOrDefaultAsync();

            Assert.Equal(4, updatedGame.Stock);
            Assert.Equal(100 - (50 - (50 * 0.1m)), updatedUser.Balance); // 10% discount
            Assert.NotNull(purchase);
            Assert.Equal(game.Id, purchase.GameId);
            Assert.Equal(user.Id, purchase.UserId);
        }

        [Fact]
        public async Task BuyGameAsync_GameDoesNotExist_ThrowsException()
        {
            var user = new User { Id = Guid.NewGuid().ToString(), UserName = "testuser", Balance = 100 };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.BuyGameAsync(999, user));
        }

        [Fact]
        public async Task BuyGameAsync_GameOutOfStock_ThrowsException()
        {
            var user = new User { Id = Guid.NewGuid().ToString(), UserName = "testuser", Balance = 100 };
            var game = new Game 
            { 
                Title = "Bloodborne", Price = 50, Discount = 10, Stock = 0, 
                ImageUrl = "bloodborne.jpg", Genre = Genre.Action, Platform = Platform.PSN 
            };
            _dbContext.Users.Add(user);
            _dbContext.Games.Add(game);
            await _dbContext.SaveChangesAsync();

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.BuyGameAsync(game.Id, user));
        }

        [Fact]
        public async Task BuyGameAsync_InsufficientBalance_ThrowsException()
        {
            var user = new User { Id = Guid.NewGuid().ToString(), UserName = "testuser", Balance = 10 };
            var game = new Game 
            { 
                Title = "Dark Souls 3", Price = 55, Discount = 0, Stock = 5, 
                ImageUrl = "darksouls3.jpg", Genre = Genre.Action, Platform = Platform.PSN 
            };
            _dbContext.Users.Add(user);
            _dbContext.Games.Add(game);
            await _dbContext.SaveChangesAsync();

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.BuyGameAsync(game.Id, user));
        }

        [Fact]
        public async Task GetGameByIdAsync_GameExists_ReturnsGame()
        {
            var game = new Game 
            { 
                Title = "Horizon Forbidden West", Price = 80, Discount = 0, Stock = 5, 
                ImageUrl = "horizon.jpg", Genre = Genre.Adventure, Platform = Platform.PSN 
            };
            _dbContext.Games.Add(game);
            await _dbContext.SaveChangesAsync();

            var result = await _service.GetGameByIdAsync(game.Id);

            Assert.NotNull(result);
            Assert.Equal(game.Title, result.Title);
        }

        [Fact]
        public async Task GetGameByIdAsync_GameDoesNotExist_ThrowsDatabaseException()
        {
            await Assert.ThrowsAsync<DatabaseException>(() => _service.GetGameByIdAsync(999));
        }
    }
