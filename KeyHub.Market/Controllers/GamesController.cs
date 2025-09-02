using KeyHub.Market.data;
using KeyHub.Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KeyHub.Market.Controllers;

public class GamesController : Controller

{
     private readonly ApplicationDbContext dbContext;

     public GamesController(ApplicationDbContext dbContext)
     {
         this.dbContext = dbContext;
     }


     [HttpGet("search")] 
    public IActionResult SearchGame(int page = 1, int pageSize = 10,GameSort sortBy=GameSort.ByName,Platform[]? platforms = null)//todo ogarnij paginacje
    {
        
        ViewBag.CurrentSort = sortBy.ToString();
        
        LoadGameSortOptions();
        
        IQueryable<Game> gamesQuery = SortGames(sortBy);
        
        gamesQuery = FilterByPlatform(gamesQuery, platforms);
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


    public IQueryable<Game> FilterByPlatform(IQueryable<Game> games, Platform[]? platforms)
    {
        if (platforms == null || platforms.Length == 0)
            return games; // brak filtra – zwróć wszystkie gry

        // filtrujemy tylko po wybranych platformach
        return games.Where(g => platforms.Contains(g.Platform));
    }


    // public IQueryable<Game> FilterByGenres(IQueryable<Game> games, Genre[]? selectedGenres)
    // {
    //     if (selectedGenres == null || selectedGenres.Length == 0)
    //         return games; 
    //
    //     return games.Where(game => selectedGenres.Contains(game.Genre));
    // }
}