using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Models.ViewModels;

public class FiltersViewModel
{
    public List<PlatformStatDto> PlatformStats { get; set; }
    public List<GenreStatDto> GenreStats { get; set; }
}