using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Models.ViewModels;

public class HistoryViewModel
{
        public List<PurchaseHistoryDto> Purchases { get; set; } = new List<PurchaseHistoryDto>();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
}