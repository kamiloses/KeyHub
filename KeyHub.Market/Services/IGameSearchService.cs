using KeyHub.Market.Enums;
using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Services;

public interface IGameSearchService
{
      Task<(List<GameDto> Games, int TotalGames)> GetSearchedGames(string? title,GameSort sortBy, Platform[]? platforms,
        Genre[]? genres, decimal? minPrice,
        decimal? maxPrice,
        int page,
        int pageSize);
    
     IQueryable<Game> GetFilteredAndSortedGames(string? title,GameSort sortBy, Platform[]? platforms, Genre[]? genres,
        decimal? minPrice = null, decimal? maxPrice = null);
}