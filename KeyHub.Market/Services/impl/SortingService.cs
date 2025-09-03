using KeyHub.Market.data;
using KeyHub.Market.Enums;
using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Services.impl;

public class SortingService : ISortingService
{
    
    private readonly ApplicationDbContext _dbContext;

    public SortingService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Game> SortGames(IQueryable<Game> games,GameSort sortBy = GameSort.ByName)
    {

        switch (sortBy)
        {
            case GameSort.ByName:
                games = games.OrderBy(g => g.Title);
                break;
            case GameSort.ByDate:
                games = games.OrderBy(g => g.CreatedAt);
                break;
            case GameSort.ByPriceAsc:
                games = games.OrderBy(g => g.Price);
                break;
            case GameSort.ByPriceDesc:
                games = games.OrderByDescending(g => g.Price);
                break;
        }

        return games;
    }
}