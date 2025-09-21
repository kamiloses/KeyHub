using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Models.ViewModels;

public class HistoryViewModel
{
        public List<HistoryDto> Purchases { get; set; } = new List<HistoryDto>();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
}