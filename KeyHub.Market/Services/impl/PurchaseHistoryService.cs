    using KeyHub.Market.data;
    using KeyHub.Market.Mappers;
    using KeyHub.Market.Models;
    using KeyHub.Market.Models.Dto;
    using KeyHub.Market.Models.ViewModels;
    using Microsoft.EntityFrameworkCore;

    namespace KeyHub.Market.Services.impl;

    public class PurchaseHistoryService : IPurchaseHistoryService
    {
        private readonly ApplicationDbContext _context;

        public PurchaseHistoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<(List<PurchaseHistoryDto> Purchases, int TotalPages)> GetUserPurchaseHistoryAsync(string userId, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(userId))
                return (new List<PurchaseHistoryDto>(), 0);

            var query = _context.Purchases
                .Where(p => p.UserId == userId)
                .Include(p => p.Game)
                .OrderByDescending(p => p.PurchaseDate);

            int totalCount = await query.CountAsync();

            var purchasesFromDb = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var purchases = purchasesFromDb
                .Select(p => HistoryMapper.MapToHistoryDto(p))
                .ToList();

            return (purchases, totalCount);
        }
    }