using System;
using System.Linq;
using JetBrains.Annotations;
using KeyHub.Market.data;
using KeyHub.Market.Enums;
using KeyHub.Market.Models;
using KeyHub.Market.Services.impl;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KeyHub.Market.Tests.Services.impl;

[TestSubject(typeof(SortingService))]
public class SortingServiceTest
{
    private readonly ApplicationDbContext _dbContext;
    private readonly SortingService _service;

    public SortingServiceTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _dbContext = new ApplicationDbContext(options);

        _dbContext.Games.AddRange(
            new Game { Id = 1, Title = "Elden Ring", Price = 60, CreatedAt = new DateTime(2022, 3, 1), Platform = Platform.Steam, Genre = Genre.RPG, ImageUrl = "eldenring.jpg" },
            new Game { Id = 2, Title = "Bloodborne", Price = 50, CreatedAt = new DateTime(2019, 5, 1), Platform = Platform.PSN, Genre = Genre.Action, ImageUrl = "bloodborne.jpg" },
            new Game { Id = 3, Title = "Dark Souls 3", Price = 55, CreatedAt = new DateTime(2018, 4, 1), Platform = Platform.PSN, Genre = Genre.Action, ImageUrl = "darksouls3.jpg" }
        );

        _dbContext.SaveChanges();

        _service = new SortingService(_dbContext);
    }

    [Fact]
    public void SortGames_ByName_ShouldSortAlphabetically()
    {
        var games = _dbContext.Games.AsQueryable();

        var sorted = _service.SortGames(games, GameSort.ByName).ToList();

        Assert.Equal(new[] { "Bloodborne", "Dark Souls 3", "Elden Ring" }, sorted.Select(g => g.Title));
    }

    [Fact]
    public void SortGames_ByPriceAsc_ShouldSortByPriceAscending()
    {
        var games = _dbContext.Games.AsQueryable();

        var sorted = _service.SortGames(games, GameSort.ByPriceAsc).ToList();

        Assert.Equal(new[] { "Bloodborne", "Dark Souls 3", "Elden Ring" }, sorted.Select(g => g.Title));
    }
}
