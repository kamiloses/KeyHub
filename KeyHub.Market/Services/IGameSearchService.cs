using KeyHub.Market.Enums;
using KeyHub.Market.Models;

namespace KeyHub.Market.Services;

public interface IGameSearchService
{
    public IQueryable<Game> GetFilteredAndSortedGames(GameSort sortBy, Platform[]? platforms, Genre[]? genres);
}