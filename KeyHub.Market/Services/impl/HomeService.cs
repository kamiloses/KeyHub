using KeyHub.Market.data;
using KeyHub.Market.Mappers;
using KeyHub.Market.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace KeyHub.Market.Services.impl;

public class HomeService : IHomeService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDistributedCache _cache;
    private readonly ILogger<HomeService> _logger;
// todo zarejestruj cache w DI    

    public HomeService(ApplicationDbContext dbContext, IDistributedCache cache, ILogger<HomeService> logger)
    {
        _dbContext = dbContext;
        _cache = cache;
        _logger = logger;
    }


    public async Task<List<GameDto>> GetTopDiscountedGamesFromDbAsync(int count)
    {
        if (count <= 0) return new List<GameDto>();
        try
        {
            var games = await _dbContext.Games
                .AsNoTracking()
                .OrderByDescending(g => g.Discount)
                .Take(count)
                .ToListAsync();

            return GameMapper.ToDtoList(games);
        }
        catch (Exception e)
        {
            //todo DatabaseFetchingException
            throw new Exception("Error fetching top discounted games", e);
        }
       
    }

    public async Task<List<GameDto>> GetTopDiscountedGamesAsync(int count)
    {
        if (count <= 0) return new List<GameDto>();

        string cacheKey = "HomePage";

        try
        {
            var cachedData = await _cache.GetAsync(cacheKey);
            if (cachedData != null)
            {
                return System.Text.Json.JsonSerializer.Deserialize<List<GameDto>>(cachedData)!;
            }

        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "Problem reading cache for key {cacheKey}", cacheKey);
        }
          var   dtoList = await GetTopDiscountedGamesFromDbAsync(count);


        try
        {
            var serialized = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(dtoList);
            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromHours(24)
            };
            await _cache.SetAsync(cacheKey, serialized, options);
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "Problem with setting cache for key {cacheKey}", cacheKey);
        }


        return dtoList;
    }
}