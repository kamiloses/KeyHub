using KeyHub.Market.Models.Dto;
using KeyHub.Market.Models.ViewModels;

namespace KeyHub.Market.Services;

public interface IPurchaseHistoryService
{
    Task<(List<PurchaseHistoryDto> Purchases, int TotalPages)> GetUserPurchaseHistoryAsync(string userId, int page, int pageSize);
    Task<HistoryViewModel> GetHistoryViewModelAsync(string userId, int page, int pageSize);

}