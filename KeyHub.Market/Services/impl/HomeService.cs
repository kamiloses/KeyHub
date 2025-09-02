using KeyHub.Market.data;
using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Services.impl;

public class HomeService : IHomeService
{
    private readonly ApplicationDbContext _dbContext;

    public HomeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
//todo upewnij sie ze najwyzsza znizka 
    public List<GameDto> GetFiveGamesWithTheBiggestDiscount()
    {
     return   _dbContext.Games.AsQueryable()
            .OrderByDescending(game => game.Discount).Take(5)
            .Select(game => new GameDto { Id = game.Id, 
                Title = game.Title, 
                Genre = game.Genre, Price = game.Price, 
                Discount = game.Discount, 
                ImageUrl = game.ImageUrl, 
                Platform = game.Platform, 
                Stock = game.Stock,
                CreatedAt = game.CreatedAt })
            .ToList();

    }
}