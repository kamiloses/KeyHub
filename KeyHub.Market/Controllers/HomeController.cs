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
    public IActionResult SearchGame(int page = 1, int pageSize = 10)//todo ogarnij paginacje
    {
        var gamesQuery = dbContext.Games.AsQueryable();
        int totalGames = gamesQuery.Count();
       
        var games = gamesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalGames / (double)pageSize);
        
        return View("searchedGames",games);
    }

    
    
}