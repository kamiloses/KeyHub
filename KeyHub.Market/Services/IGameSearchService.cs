using KeyHub.Market.Enums;
using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Services;

public interface IGameSearchService
{
    public (List<GameDto> Games, int TotalGames) GetSearchedGames(GameSort sortBy, Platform[]? platforms,
        Genre[]? genres, decimal? minPrice,
        decimal? maxPrice,
        int page,
        int pageSize);
    
    
    public IQueryable<Game> GetFilteredAndSortedGames(GameSort sortBy, Platform[]? platforms, Genre[]? genres,
        decimal? minPrice = null, decimal? maxPrice = null);
}