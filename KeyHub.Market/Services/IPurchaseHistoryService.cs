using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Services;

public interface IPurchaseHistoryService
{
    Task<(List<PurchaseHistoryDto> Purchases, int TotalPages)> GetUserPurchaseHistoryAsync(string userId, int page, int pageSize);
}