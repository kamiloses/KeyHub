using KeyHub.Market.data;
using KeyHub.Market.Models.Dto;
using KeyHub.Market.Services.impl;
using Microsoft.AspNetCore.Mvc;

namespace KeyHub.Market.Controllers;

public class HomeController : Controller
{
    private readonly HomeService _homeService;


    public HomeController(HomeService homeService)
    {
        _homeService = homeService;
    }

    [HttpGet("")] 
    public IActionResult Index()
    {//todo daj inną nazwe zmiennej oraz metody 
       List<GameDto> gamesWithBiggestDiscount = _homeService.GetFiveGamesWithTheBiggestDiscount();
        
        return View("home",gamesWithBiggestDiscount);
    }
    
    
   
}
    
    
    
    
