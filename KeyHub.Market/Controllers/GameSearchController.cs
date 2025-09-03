using KeyHub.Market.Enums;
using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;
using KeyHub.Market.Services;
using Microsoft.AspNetCore.Mvc;

namespace KeyHub.Market.Controllers;

public class GameSearchController : Controller

{
    // TODO 1 filtry min max price , 2 jezeli mam właczone filtry to jak sortowanie dam to by nie usuwało filtru 3 dynamiczny filtr, 4 poprawa css  by bardziej an prawo gry były
    //todo 5 css sortowania inny 6 paginacja inna css
    
    private readonly IGameSearchService _gameSearchService;

    public GameSearchController(IGameSearchService gameSearchService)
    {
        _gameSearchService = gameSearchService;
    }


    [HttpGet("search")]
    public IActionResult SearchedGames(int page = 1, int pageSize = 10, GameSort sortBy = GameSort.ByName, Platform[]? platforms = null, Genre[]? genres = null)
    {
        LoadGameSortOptions();
        ViewBag.CurrentSort = sortBy.ToString();

        ViewBag.SelectedPlatforms= platforms;
        ViewBag.SelectedGenres= genres;
        

        IQueryable<Game> gamesQuery = _gameSearchService.GetFilteredAndSortedGames(sortBy, platforms, genres);


        int totalGames = gamesQuery.Count();

        var games = gamesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(game => new GameDto
            {
                Id = game.Id,
                Title = game.Title,
                Genre = game.Genre,
                Price = game.Price,
                Discount = game.Discount,
                ImageUrl = game.ImageUrl,
                Platform = game.Platform,
                Stock = game.Stock,
                CreatedAt = game.CreatedAt
            })
            .ToList();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalGames / (double)pageSize);

        return View("searchedGames", games);
    }


    public void LoadGameSortOptions()
    {
        ViewBag.ByDate = GameSort.ByDate;
        ViewBag.ByName = GameSort.ByName;
        ViewBag.ByPriceAsc = GameSort.ByPriceAsc;
        ViewBag.ByPriceDesc = GameSort.ByPriceDesc;
    }
}