using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Services;

public interface IHistoryService
{
    Task<List<HistoryDto>> GetUserPurchaseHistoryAsync(string userId);
}