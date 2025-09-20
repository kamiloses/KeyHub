using KeyHub.Market.Models;

namespace KeyHub.Market.Services;

public interface IFilteringService
{
     IQueryable<Game> FilterByPlatform(IQueryable<Game> games, Platform[]? platforms);
     IQueryable<Game> FilterByGenres(IQueryable<Game> games, Genre[]? selectedGenres);
     IQueryable<Game> FilterByPrice(IQueryable<Game> games, decimal? minPrice = null, decimal? maxPrice = null);


}