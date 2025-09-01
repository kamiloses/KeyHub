using KeyHub.Market.data;
using Microsoft.AspNetCore.Mvc;

namespace KeyHub.Market.Controllers;
//todo zmien nazwe homeController
public class HomeController : Controller
{
    private ApplicationDbContext dbContext;

    public HomeController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet("")] 
    public IActionResult Index()
    {
        return View("home");
    }
    
    
    
    [HttpGet("search")] 
    public IActionResult SearchGame()
    {
        var games = dbContext.Games.Take(20).ToList();
        
        return View("searchedGames",games);
    }

    
    
}