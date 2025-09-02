using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Services;

public interface IHomeService
{
    List<GameDto> GetFiveGamesWithTheBiggestDiscount();
}