using KeyHub.Market.Models;

namespace KeyHub.Market.Services;

public interface IGameManagerService
{
        Task<Game> AddGame(string title, Genre genre, decimal price, Platform platform, int stock, IFormFile imageFile, int discount = 0);
}