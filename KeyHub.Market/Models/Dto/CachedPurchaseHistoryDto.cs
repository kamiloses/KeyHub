namespace KeyHub.Market.Models.Dto;

public class CachedPurchaseHistoryDto
{
    public List<PurchaseHistoryDto> Purchases { get; set; } = new List<PurchaseHistoryDto>();
    public int TotalCount { get; set; }
}