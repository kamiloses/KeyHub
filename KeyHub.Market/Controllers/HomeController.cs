using KeyHub.Market.Enums;
using KeyHub.Market.Models.Dto;
using KeyHub.Market.Models.ViewModels;
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

    [HttpGet("/")]
    public async Task<IActionResult> Home()
    {
        
        ViewData["Context"] = ViewContextTypeModel.Home;
        List<GameDto> topDiscountedGames = await _homeService.GetTopDiscountedGamesAsync(TopGamesCount);

        return View("home", topDiscountedGames);
    }
    
   
}
    
    
    
    
