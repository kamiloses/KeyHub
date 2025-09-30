using System.Linq;
using JetBrains.Annotations;
using KeyHub.Market.Models;
using KeyHub.Market.Services.impl;
using Xunit;

namespace KeyHub.Market.Tests.Services.impl;

[TestSubject(typeof(FilteringService))]
public class FilteringServiceTest
    {
        private readonly FilteringService _service = new FilteringService();

        private IQueryable<Game> GetSampleGames()
        {
            return new[]
            {
                new Game { Id = 1, Title = "Elden Ring", Price = 60, Discount = 10, Platform = Platform.Steam, Genre = Genre.RPG },
                new Game { Id = 2, Title = "Bloodborne", Price = 50, Discount = 0, Platform = Platform.PSN, Genre = Genre.Action },
                new Game { Id = 3, Title = "Dark Souls 3", Price = 55, Discount = 5, Platform = Platform.PSN, Genre = Genre.Action },
                new Game { Id = 4, Title = "Horizon Forbidden West", Price = 80, Discount = 0, Platform = Platform.PSN, Genre = Genre.Adventure }
            }.AsQueryable();
        }

        [Fact]
        public void FilterByPlatform_WithPlatforms_ReturnsOnlyMatching()
        {
            var games = GetSampleGames();
            var platforms = new[] { Platform.PSN };

            var result = _service.FilterByPlatform(games, platforms).ToList();

            Assert.All(result, g => Assert.Contains(g.Platform, platforms));
            Assert.Equal(3, result.Count); // Bloodborne, Dark Souls 3, Horizon
        }

        [Fact]
        public void FilterByPlatform_NullOrEmpty_ReturnsAll()
        {
            var games = GetSampleGames();
            var result1 = _service.FilterByPlatform(games, null).ToList();
            var result2 = _service.FilterByPlatform(games, new Platform[0]).ToList();

            Assert.Equal(games.Count(), result1.Count);
            Assert.Equal(games.Count(), result2.Count);
        }

        [Fact]
        public void FilterByGenres_WithGenres_ReturnsOnlyMatching()
        {
            var games = GetSampleGames();
            var genres = new[] { Genre.Action, Genre.Adventure };

            var result = _service.FilterByGenres(games, genres).ToList();

            Assert.All(result, g => Assert.Contains(g.Genre, genres));
            Assert.Equal(3, result.Count); // Bloodborne, Dark Souls 3, Horizon
        }

        [Fact]
        public void FilterByGenres_NullOrEmpty_ReturnsAll()
        {
            var games = GetSampleGames();
            var result1 = _service.FilterByGenres(games, null).ToList();
            var result2 = _service.FilterByGenres(games, new Genre[0]).ToList();

            Assert.Equal(games.Count(), result1.Count);
            Assert.Equal(games.Count(), result2.Count);
        }

        [Fact]
        public void FilterByPrice_WithMinAndMax_ReturnsOnlyMatching()
        {
            var games = GetSampleGames();

            // Price after discount: Elden Ring 54, Bloodborne 50, Dark Souls3 52.25, Horizon 80
            var result = _service.FilterByPrice(games, minPrice: 50, maxPrice: 55).ToList();

            Assert.Contains(result, g => g.Title == "Elden Ring");
            Assert.Contains(result, g => g.Title == "Bloodborne");
            Assert.Contains(result, g => g.Title == "Dark Souls 3");
            Assert.DoesNotContain(result, g => g.Title == "Horizon Forbidden West");
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void FilterByPrice_NoMinMax_ReturnsAll()
        {
            var games = GetSampleGames();
            var result = _service.FilterByPrice(games).ToList();

            Assert.Equal(games.Count(), result.Count);
        }
    }
