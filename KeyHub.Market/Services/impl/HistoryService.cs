using KeyHub.Market.data;
using KeyHub.Market.Mappers;
using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;
using KeyHub.Market.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KeyHub.Market.Services.impl;

public class HistoryService : IHistoryService
{
    private readonly ApplicationDbContext _context;

    public HistoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<HistoryDto>> GetUserPurchaseHistoryAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return new List<HistoryDto>();

        List<HistoryDto> purchases = await _context.Purchases
            .Where(p => p.UserId == userId)
            .Include(p => p.Game)
            .Select(purchase=>HistoryMapper.MapToHistoryDto(purchase))
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync();

        
        return purchases;
    }
}