using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Models;

public class GameSearchPageViewModel
{
    public IEnumerable<GameDto> Games { get; set; }
    public string CurrentSort { get; set; }
}