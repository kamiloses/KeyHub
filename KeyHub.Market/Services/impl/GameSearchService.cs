using KeyHub.Market.data;
using KeyHub.Market.Enums;
using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace KeyHub.Market.Services.impl;

public class GameSearchService : IGameSearchService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IFilteringService _filteringService;
    private readonly ISortingService _sortingService;

    public GameSearchService(ApplicationDbContext dbContext, IFilteringService filteringService,
        ISortingService sortingService)
    {
        _dbContext = dbContext;
        _filteringService = filteringService;
        _sortingService = sortingService;
    }

    public (List<GameDto> Games, int TotalGames) GetSearchedGames(string? title,GameSort sortBy, Platform[]? platforms, Genre[]? genres,
        decimal? minPrice,
        decimal? maxPrice,
        int page,
        int pageSize)
    {
        IQueryable<Game> gamesQuery = GetFilteredAndSortedGames(title,sortBy, platforms, genres, minPrice, maxPrice);

        int totalGames = gamesQuery.Count();

        var games = gamesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(game => new GameDto
            {
                Id = game.Id,
                Title = game.Title,
                Genre = game.Genre,
                Price = game.Price,
                Discount = game.Discount,
                ImageUrl = game.ImageUrl,
                Platform = game.Platform,
                Stock = game.Stock,
                CreatedAt = game.CreatedAt
            })
            .ToList();

        return (games, totalGames);
    }

    public IQueryable<Game> GetFilteredAndSortedGames(string? title,GameSort sortBy, Platform[]? platforms, Genre[]? genres,
        decimal? minPrice = null, decimal? maxPrice = null)
    {
        
        IQueryable<Game> games = _dbContext.Games.AsNoTracking();

        if (!string.IsNullOrEmpty(title))
        {
            games = games.Where(game => game.Title.Contains(title));
        }


        IQueryable<Game> gamesQuery = _sortingService.SortGames(games, sortBy);
        IQueryable<Game> filteredGamesByPlatform = _filteringService.FilterByPlatform(gamesQuery, platforms);
        IQueryable<Game> filteredGamesByGenres = _filteringService.FilterByGenres(filteredGamesByPlatform, genres);

        filteredGamesByGenres = _filteringService.FilterByPrice(filteredGamesByGenres, minPrice, maxPrice);

        return filteredGamesByGenres;
    }
}