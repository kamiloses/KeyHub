using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Models.ViewModels;

public class FiltersViewModel
{
    public List<PlatformStatDto> PlatformStats { get; set; }
    public List<GenreStatDto> GenreStats { get; set; }

    public Platform[]? SelectedPlatforms { get; set; }
    public Genre[]? SelectedGenres { get; set; }
}