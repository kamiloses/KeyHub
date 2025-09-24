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
    

    public HomeService(ApplicationDbContext dbContext, IDistributedCache cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }


    public async Task<List<GameDto>> GetTopDiscountedGamesFromDbAsync(int count)
    {
        if (count <= 0) return new List<GameDto>();

        var games = await _dbContext.Games
            .AsNoTracking()
            .OrderByDescending(g => g.Discount)
            .Take(count)
            .ToListAsync();

        return GameMapper.ToDtoList(games);
    }
    public async Task<List<GameDto>> GetTopDiscountedGamesAsync(int count)
    {
        if (count <= 0) return new List<GameDto>();

        string cacheKey = "HomePage";

        var cachedData = await _cache.GetAsync(cacheKey);
        if (cachedData != null)
        {
            return System.Text.Json.JsonSerializer.Deserialize<List<GameDto>>(cachedData)!;
        }

        var dtoList = await GetTopDiscountedGamesFromDbAsync(count);

        var options = new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromHours(24)
        };
        var serialized = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(dtoList);
        await _cache.SetAsync(cacheKey, serialized, options);

        return dtoList;
    }
}