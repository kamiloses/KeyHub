using KeyHub.Market.Models.ViewModels;
using KeyHub.Market.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyHub.Market.Controllers;
[Authorize(Roles = "Admin")] 
public class GameManagerController : Controller
{

    private readonly IGameManagerService _gameManagerService;

    public GameManagerController(IGameManagerService gameManagerService)
    {
        _gameManagerService = gameManagerService;
    }

    [HttpPost("/AddGame")]
    public async Task<IActionResult> AddGame(GameManagerViewModel game)
    {
             await _gameManagerService.AddGame(game.Title, game.Genre, game.Price, game.Platform, game.Stock, game.ImageFile, game.Discount);
            return RedirectToAction("SearchedGames", "GameSearch"); 
        }
    
    
    
    
    
}