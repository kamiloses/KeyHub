    using KeyHub.Market.data;
    using KeyHub.Market.Mappers;
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
        
        public async Task<(List<PurchaseHistoryDto> Purchases, int TotalPages)> GetUserPurchaseHistoryAsync(string userId, int page, int pageSize)
        {
            if (string.IsNullOrEmpty(userId))
                return (new List<PurchaseHistoryDto>(), 0);

            
            var query = _context.Purchases
                .Where(purchase => purchase.UserId == userId)
                .Include(purchase => purchase.Game)
                .OrderByDescending(purchase => purchase.PurchaseDate);

            int totalCount = await query.CountAsync();

            var purchasesFromDb = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var purchases = purchasesFromDb
                .Select(p => HistoryMapper.MapToHistoryDto(p))
                .ToList();

            return (purchases, totalCount);
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
        
    }