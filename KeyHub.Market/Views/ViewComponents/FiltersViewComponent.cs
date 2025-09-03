using KeyHub.Market.data;
using KeyHub.Market.Models.Dto;
using KeyHub.Market.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KeyHub.Market.Views.ViewComponents
{//todo cache
    public class FiltersViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _dbContext;

        public FiltersViewComponent(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> GetFiltersDataAsync()
        {
            List<PlatformStatDto> platformStats = await _dbContext.Games.AsNoTracking()
                .GroupBy(game => game.Platform)
                .Select(game => new PlatformStatDto
                {
                    Platform = game.Key,
                    Count = game.Count()
                })
                .ToListAsync();

              List<GenreStatDto> genreStats = await _dbContext.Games.AsNoTracking()
                .GroupBy(game => game.Genre)
                .Select(game => new GenreStatDto
                {
                    Genre = game.Key,
                    Count = game.Count()
                })
                .ToListAsync();

            FiltersViewModel filters = new FiltersViewModel
            {
                PlatformStats = platformStats,
                GenreStats = genreStats
            };

            return View(filters);
        }
    }
}