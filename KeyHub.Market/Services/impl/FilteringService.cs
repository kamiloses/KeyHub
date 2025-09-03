using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Services.impl;

public class FilteringService : IFilteringService
{
    public IQueryable<Game> FilterByPlatform(IQueryable<Game> games, Platform[]? platforms)
    {
        if (platforms == null || platforms.Length == 0)
            return games; 

        return games.Where(g => platforms.Contains(g.Platform));
    }
    
    
    
    public IQueryable<Game> FilterByGenres(IQueryable<Game> games, Genre[]? selectedGenres)
    {
        if (selectedGenres == null || selectedGenres.Length == 0)
            return games; 
    
        return games.Where(game => selectedGenres.Contains(game.Genre));
    }


    public IQueryable<Game> FilterByPrice(IQueryable<Game> games, int? minPrice = null, int? maxPrice = null)
    {
        if (minPrice.HasValue)
        {
            games = games.Where(game => game.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            games = games.Where(game => game.Price <= maxPrice.Value);
        }

        return games;
    }
}