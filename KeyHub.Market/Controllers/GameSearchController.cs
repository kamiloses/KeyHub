using KeyHub.Market.Enums;
using KeyHub.Market.Models.ViewModels;
using KeyHub.Market.Services;
using Microsoft.AspNetCore.Mvc;

namespace KeyHub.Market.Controllers;
public class GameSearchController : Controller

{
    private readonly IGameSearchService _gameSearchService;
    private const int DefaultPageSize = 10;

    public GameSearchController(IGameSearchService gameSearchService)
    {
        _gameSearchService = gameSearchService;
    }
    
     

        [HttpGet("/search")]
        public async Task<IActionResult> SearchedGames([FromQuery] GameSearchViewModel model)
        {
            
            ViewData["Context"] = ViewContextType.Search;
            var (games, totalGames) =await _gameSearchService.GetSearchedGames(
                model.Title, model.CurrentSort, model.SelectedPlatforms,
                model.SelectedGenres, model.MinPrice, model.MaxPrice,
                model.CurrentPage, pageSize: DefaultPageSize
            );
        
            model.Games = games;
            model.TotalPages = (int)Math.Ceiling(totalGames / 10.0);

            return View("SearchedGames", model);
        }
    }
