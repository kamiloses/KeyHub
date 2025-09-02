using KeyHub.Market.data;
using KeyHub.Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace KeyHub.Market.Controllers;
//todo zmien nazwe homeController
public class HomeController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public HomeController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet("")] 
    public IActionResult Index()
    {
       //todo zamienić na znizke najwiekszą
       List<Game> recentlyAddedGames= dbContext.Games.AsQueryable().OrderBy(game => game.CreatedAt).Take(5).ToList();
        //todo zamienić na gameDto
        
        return View("home",recentlyAddedGames);
    }
    
    
   
}
    
    
    
    
