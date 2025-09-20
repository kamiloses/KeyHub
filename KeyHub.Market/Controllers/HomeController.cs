using KeyHub.Market.Enums;
using KeyHub.Market.Models.Dto;
using KeyHub.Market.Services;
using Microsoft.AspNetCore.Mvc;

namespace KeyHub.Market.Controllers;

public class HomeController : Controller
{
    private readonly IHomeService _homeService;
    private const int TopGamesCount = 5;
    public HomeController(IHomeService homeService)
    {
        _homeService = homeService; 
    } 

    [HttpGet("/home")]
    public async Task<IActionResult> Home()
    {
        
        ViewData["Context"] = ViewContextType.Home;
        List<GameDto> topDiscountedGames = await _homeService.GetTopDiscountedGamesAsync(TopGamesCount);

        return View("home", topDiscountedGames);
    }
    
   
}
    
    
    
    
