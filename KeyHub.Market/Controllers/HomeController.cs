using KeyHub.Market.data;
using KeyHub.Market.Models;
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
    public IActionResult SearchGame(int page = 1, int pageSize = 10,GameSort sortBy=GameSort.ByName)//todo ogarnij paginacje
    {
        
        ViewBag.CurrentSort = sortBy.ToString();
        
        LoadGameSortOptions();
        
        IQueryable<Game> gamesQuery = SortGames(sortBy);
        int totalGames = gamesQuery.Count();
       
        var games = gamesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalGames / (double)pageSize);
        
        return View("searchedGames",games);
    }

    public enum GameSort
    {
        ByDate,
        ByName,    
        ByPriceAsc,
        ByPriceDesc
    }



    public IQueryable<Game> SortGames(GameSort sortBy = GameSort.ByName)
    {
        IQueryable<Game> games = dbContext.Games.AsQueryable();

        switch (sortBy)
        {
            case GameSort.ByName:
                games = games.OrderBy(g => g.Title);
                break;
            case GameSort.ByDate:
                games = games.OrderBy(g => g.CreatedAt);
                break;
            case GameSort.ByPriceAsc:
                games = games.OrderBy(g => g.Price);
                break;
            case GameSort.ByPriceDesc:
                games = games.OrderByDescending(g => g.Price);
                break;
        }

        return games;
    }
 
    
    public void LoadGameSortOptions()
    {
        ViewBag.ByDate = GameSort.ByDate;
        ViewBag.ByName = GameSort.ByName;
        ViewBag.ByPriceAsc = GameSort.ByPriceAsc;
        ViewBag.ByPriceDesc = GameSort.ByPriceDesc;
    }
    
}