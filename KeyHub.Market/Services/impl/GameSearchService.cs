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

    public GameSearchService(ApplicationDbContext dbContext, IFilteringService filteringService, ISortingService sortingService)
    {
        _dbContext = dbContext;
        _filteringService = filteringService;
        _sortingService = sortingService;
    }


    public IQueryable<Game> GetFilteredAndSortedGames(GameSort sortBy, Platform[]? platforms, Genre[]? genres, decimal? minPrice = null, decimal? maxPrice = null)
    {
        IQueryable<Game> games = _dbContext.Games.AsNoTracking();
    
        IQueryable<Game> gamesQuery = _sortingService.SortGames(games, sortBy);
        IQueryable<Game> filteredGamesByPlatform = _filteringService.FilterByPlatform(gamesQuery, platforms);
        IQueryable<Game> filteredGamesByGenres = _filteringService.FilterByGenres(filteredGamesByPlatform, genres);
    
        filteredGamesByGenres = _filteringService.FilterByPrice(filteredGamesByGenres, minPrice, maxPrice);
    
        return filteredGamesByGenres;
    }
 
        
        
    
    
    
}