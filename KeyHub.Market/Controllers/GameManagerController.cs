using System.Globalization;
using KeyHub.Market.Models;
using KeyHub.Market.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KeyHub.Market.Controllers;

public class GameManagerController : Controller
{

    private readonly IGameManagerService _gameManagerService;

    public GameManagerController(IGameManagerService gameManagerService)
    {
        _gameManagerService = gameManagerService;
    }

    [HttpPost("/AddGame")]
    public async Task<EntityEntry<Game>> AddGame(string title,Genre genre,string price,Platform platform,int stock,IFormFile imageFile,int discount=0)
    {
        decimal parsedPrice = decimal.Parse(price.Replace(',', '.'), CultureInfo.InvariantCulture);
        Console.WriteLine("WYKONUJEEEEEEE " +price);
        
        
        
       return await _gameManagerService.AddGame(title, genre, parsedPrice, platform, stock, imageFile, discount);
        
    }

    
    
    
    
    
}