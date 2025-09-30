using System;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using KeyHub.Market.data;
using KeyHub.Market.Models;
using KeyHub.Market.Services.impl;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace KeyHub.Market.Tests.Services.impl;

[TestSubject(typeof(GameManagerService))]
 public class GameManagerServiceTest
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<IWebHostEnvironment> _envMock;
        private readonly GameManagerService _service;

        public GameManagerServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;//todo

            _dbContext = new ApplicationDbContext(options);
            _envMock = new Mock<IWebHostEnvironment>();
            _envMock.Setup(e => e.WebRootPath).Returns(Path.GetTempPath());

            _service = new GameManagerService(_dbContext, _envMock.Object);
        }

        [Fact]
        public async Task AddGame_ValidInput_AddsGame()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            var content = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("fake image content"));
            mockFile.Setup(f => f.OpenReadStream()).Returns(content);
            mockFile.Setup(f => f.FileName).Returns("image.png");
            mockFile.Setup(f => f.Length).Returns(content.Length);

            // Act
            var game = await _service.AddGame(
                title: "Elden Ring",
                genre: Genre.RPG,
                price: 60,
                platform: Platform.Steam,
                stock: 10,
                imageFile: mockFile.Object,
                discount: 10
            );

            // Assert
            Assert.NotNull(game);
            Assert.Equal("Elden Ring", game.Title);
            Assert.Equal(Genre.RPG, game.Genre);
            Assert.Equal(Platform.Steam, game.Platform);
            Assert.Equal(60, game.Price);
            Assert.Equal(10, game.Stock);
            Assert.Equal(10, game.Discount);
            Assert.False(string.IsNullOrEmpty(game.ImageUrl));

            var dbGame = await _dbContext.Games.FindAsync(game.Id);
            Assert.NotNull(dbGame);
        }

        [Fact]
        public async Task AddGame_NullImage_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.AddGame(
                title: "Dark Souls 3",
                genre: Genre.Action,
                price: 55,
                platform: Platform.PSN,
                stock: 5,
                imageFile: null
            ));
        }

        [Fact]
        public async Task AddGame_EmptyFile_ThrowsArgumentException()
        {
            var emptyFileMock = new Mock<IFormFile>();
            emptyFileMock.Setup(f => f.Length).Returns(0);

            await Assert.ThrowsAsync<ArgumentException>(() => _service.AddGame(
                title: "Bloodborne",
                genre: Genre.Action,
                price: 50,
                platform: Platform.PSN,
                stock: 5,
                imageFile: emptyFileMock.Object
            ));
        }
    }
