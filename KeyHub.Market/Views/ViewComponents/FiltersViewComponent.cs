using KeyHub.Market.data;
using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;
using KeyHub.Market.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//todo cache
namespace KeyHub.Market.Views.ViewComponents;

public class FiltersViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _dbContext;

    public FiltersViewComponent(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IViewComponentResult> InvokeAsync(Platform[]? selectedPlatforms = null, Genre[]? selectedGenres = null)
    {
       
        var gamesQuery = _dbContext.Games.AsNoTracking();

        var platformStats = await GetPlatformStatsAsync(gamesQuery, selectedGenres);
        var genreStats = await GetGenreStatsAsync(gamesQuery, selectedPlatforms);


        FiltersViewModel filters = new FiltersViewModel
        {
            PlatformStats = platformStats,
            GenreStats = genreStats
        };

       
        return View(filters);
    }

    private async Task<List<GenreStatDto>> GetGenreStatsAsync(IQueryable<Game> games, Platform[]? selectedPlatforms = null)
    {
        if (selectedPlatforms != null && selectedPlatforms.Length > 0)
            games = games.Where(g => selectedPlatforms.Contains(g.Platform));

        return await games
            .GroupBy(game => game.Genre)
            .Select(game => new GenreStatDto
            {
                Genre = game.Key,
                Count = game.Count()
            })
            .ToListAsync();
    }

    private async Task<List<PlatformStatDto>> GetPlatformStatsAsync(IQueryable<Game> games, Genre[]? selectedGenres = null)
    {
        if (selectedGenres != null && selectedGenres.Length > 0)
            games = games.Where(g => selectedGenres.Contains(g.Genre));

        return await games
            .GroupBy(game => game.Platform)
            .Select(game => new PlatformStatDto
            {
                Platform = game.Key,
                Count = game.Count()
            })
            .ToListAsync();
    }
}