using KeyHub.Market.data;
using KeyHub.Market.Exceptions;
using KeyHub.Market.Models;
using Microsoft.AspNetCore.Identity;

namespace KeyHub.Market.Services.impl;

public class PurchaseService : IPurchaseService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly Logger<PurchaseService> _logger;

    public PurchaseService(ApplicationDbContext dbContext, Logger<PurchaseService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task BuyGameAsync(int gameId, User user)
    {
        try
        {
            var game = await _dbContext.Games.FindAsync(gameId);
            if (game == null)
                throw new InvalidOperationException("Game does not exist");

            if (game.Stock <= 0)
                throw new InvalidOperationException("Game is out of stock");

            var finalPrice = game.Price - (game.Price * game.Discount / 100);

            if (user.Balance < finalPrice)
                throw new InvalidOperationException("Insufficient balance");

            var purchase = new Purchase
            {
                UserId = user.Id,
                GameId = game.Id,
                PurchasePrice = finalPrice,
                PurchaseDate = DateTime.UtcNow
            };

            game.Stock -= 1;
            user.Balance -= finalPrice;

            _dbContext.Purchases.Add(purchase);
            _dbContext.Games.Update(game);
            _dbContext.Users.Update(user);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while buying game {GameId}", gameId);
            throw; 
        }
    }
    
    public async Task<Game> GetGameByIdAsync(int gameId)
    {
        try
        {
            var game = await _dbContext.Games.FindAsync(gameId);
            if (game == null)
            {
                throw new DatabaseException($"Game with ID {gameId} not found in the database.");
            }

            return game;
        }
        catch (Exception e) when (!(e is DatabaseException))
        {
            throw new DatabaseException("There was a problem fetching the game from the database.", e);
        }
    }
    
    
}