using KeyHub.Market.Models;

namespace KeyHub.Market.Services;

public interface IFilteringService
{
    public IQueryable<Game> FilterByPlatform(IQueryable<Game> games, Platform[]? platforms);
    public IQueryable<Game> FilterByGenres(IQueryable<Game> games, Genre[]? selectedGenres);


}