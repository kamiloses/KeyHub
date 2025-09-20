using KeyHub.Market.Enums;
using KeyHub.Market.Models;

namespace KeyHub.Market.Services;

public interface ISortingService
{
     IQueryable<Game> SortGames(IQueryable<Game> games, GameSort sortBy = GameSort.ByName);

}