using System.Text.Json;
using KeyHub.Market.data;
using KeyHub.Market.Exceptions;
using KeyHub.Market.Mappers;
using KeyHub.Market.Models.Dto;
using KeyHub.Market.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace KeyHub.Market.Services.impl;

public class PurchaseHistoryService : IPurchaseHistoryService
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly ILogger<PurchaseHistoryService> _logger;

    public PurchaseHistoryService(ApplicationDbContext context, IDistributedCache cache,
        ILogger<PurchaseHistoryService> logger)
    {
        _context = context;
        _cache = cache;
        _logger = logger;
    }

    
    public async Task<HistoryViewModel> GetHistoryViewModelAsync(string userId, int page, int pageSize)
    {
        var (purchases, totalCount) = await GetUserPurchaseHistoryAsync(userId, page, pageSize);

        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new HistoryViewModel
        {
            Purchases = purchases,
            TotalPages = totalPages,
            CurrentPage = page
        };
    }
    
    
    
    public async Task<(List<PurchaseHistoryDto> Purchases, int TotalPages)> GetUserPurchaseHistoryAsync(string userId,
        int page, int pageSize)
    {
        if (string.IsNullOrEmpty(userId))
            return (new List<PurchaseHistoryDto>(), 0);

        string cacheKey = $"PurchaseHistory:{userId}:Page:{page}:Size:{pageSize}";

        var cachedPurchasesData = await GetCacheUserPurchaseHistoryAsync(cacheKey);
        if (cachedPurchasesData != null)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(cachedPurchasesData);
            
            return (cachedPurchasesData.Purchases, cachedPurchasesData.TotalCount);
        }

        List<PurchaseHistoryDto> purchases;
        int totalCount;

        try
        {
            var baseQuery = _context.Purchases
                .Where(p => p.UserId == userId)
                .Include(p => p.Game);

            totalCount = await baseQuery.CountAsync();

            purchases = await baseQuery
                .OrderByDescending(p => p.PurchaseDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => HistoryMapper.MapToHistoryDto(p))
                .ToListAsync();
        }
        catch (Exception e)
        {
            throw new DatabaseException("Failed to get purchase history", e);
        }

        var cachedObject = new CachedPurchaseHistoryDto
        {
            Purchases = purchases,
            TotalCount = totalCount
        };

        await SetCacheUserPurchaseHistoryAsync(cacheKey, cachedObject);

        return (cachedObject.Purchases, cachedObject.TotalCount);
    }
    
    private async Task SetCacheUserPurchaseHistoryAsync(string cacheKey, CachedPurchaseHistoryDto cachedObject)
    {
        try
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };

            var serialized = JsonSerializer.SerializeToUtf8Bytes(cachedObject);
            await _cache.SetAsync(cacheKey, serialized, options);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Problem with caching purchases for {CacheKey}", cacheKey);
        }
    }

    private async Task<CachedPurchaseHistoryDto?> GetCacheUserPurchaseHistoryAsync(string cacheKey)
    {
        try
        {
            var cachedData = await _cache.GetAsync(cacheKey);
            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<CachedPurchaseHistoryDto>(cachedData);
                
            }
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Error getting cached user purchases for {CacheKey}", cacheKey);
        }

        return null;
    }
}