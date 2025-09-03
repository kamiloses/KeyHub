using KeyHub.Market.data;
using KeyHub.Market.Enums;
using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace KeyHub.Market.Services.impl;

public class GameSearchService
{
    
    private readonly ApplicationDbContext _dbContext;
    private readonly FilteringService _filteringService;
    private readonly SortingService _sortingService;
    public GameSearchService(ApplicationDbContext dbContext, SortingService sortingService, FilteringService filteringService)
    {
        _dbContext = dbContext;
        _sortingService = sortingService;
        _filteringService = filteringService;
    }

    public IQueryable<Game> GetFilteredAndSortedGames(GameSort sortBy,Platform[]? platforms,Genre[]? genres)
    {
        IQueryable<Game> games = _dbContext.Games.AsNoTracking();
        
        IQueryable<Game> gamesQuery = _sortingService.SortGames(games,sortBy);
        IQueryable<Game> filteredGamesByPlatform=_filteringService.FilterByPlatform(gamesQuery, platforms);
 return _filteringService.FilterByGenres(filteredGamesByPlatform, genres);

 
        
        
    }
    
    
}