using Microsoft.AspNetCore.Mvc;

namespace KeyHub.Market.Controllers;
//todo zmien nazwe homeController
public class HomeController : Controller
{
    [HttpGet("")] 
    public IActionResult Index()
    {
        return View("home");
    }
    
    
    
    [HttpGet("search/{game}")] 
    public IActionResult SearchGame([FromQuery] string game)
    {
        return View("searchedGames");
    }

    
    
}