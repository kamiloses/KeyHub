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
    public async Task<IActionResult> AddGame(string title, Genre genre, decimal price, Platform platform, int stock, IFormFile imageFile, int discount = 0)
    {
             await _gameManagerService.AddGame(title, genre, price, platform, stock, imageFile, discount);
            return RedirectToAction("Home", "Home"); 
        }
    
    
    
    
    
}