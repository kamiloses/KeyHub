using KeyHub.Market.Enums;

namespace KeyHub.Market.Models.ViewModels;

public class PaginationViewModel
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
     
    public GameSort CurrentSort { get; set; } = GameSort.ByName;
    
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    

    public Platform[]? SelectedPlatforms { get; set; }
    public Genre[]? SelectedGenres { get; set; }
    
}