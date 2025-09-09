using KeyHub.Market.Enums;
using KeyHub.Market.Models;
using KeyHub.Market.Models.ViewModels;
using KeyHub.Market.Services;
using Microsoft.AspNetCore.Mvc;

namespace KeyHub.Market.Controllers;
public class GameSearchController : Controller

{
    // TODO 1 filtry min max price bo nie działa , paginacja inna css

    private readonly IGameSearchService _gameSearchService;

    public GameSearchController(IGameSearchService gameSearchService)
    {
        _gameSearchService = gameSearchService;
    }
    
     

        [HttpGet("search")]
        public IActionResult SearchedGames([FromQuery] GameSearchViewModel model)
        {
            var (games, totalGames) = _gameSearchService.GetSearchedGames(
                model.Title, model.CurrentSort, model.SelectedPlatforms,
                model.SelectedGenres, model.MinPrice, model.MaxPrice,
                model.CurrentPage, pageSize: 10
            );
        
            model.Games = games;
            model.TotalPages = (int)Math.Ceiling(totalGames / 10.0);

            return View("SearchedGames", model);
        }
    }
