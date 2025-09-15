using KeyHub.Market.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KeyHub.Market.Services;

public interface IGameManagerService
{


    public Task<EntityEntry<Game>> AddGame(string title, Genre genre, decimal price, Platform platform, int stock,
        IFormFile imageFile, int discount = 0);



}