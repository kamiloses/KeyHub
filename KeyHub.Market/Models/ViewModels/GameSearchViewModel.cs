using KeyHub.Market.Enums;
using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Models.ViewModels;


public class GameSearchViewModel
{
    public string? Title { get; set; }
    public List<GameDto> Games { get; set; } = new();
    public GameSort CurrentSort { get; set; } = GameSort.ByName;
    public Platform[]? SelectedPlatforms { get; set; }
    public Genre[]? SelectedGenres { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
    public List<GameSort> SortOptions { get; set; } = Enum.GetValues<GameSort>().ToList();
}