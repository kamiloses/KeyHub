using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Services;

public interface IHomeService
{
      Task<List<GameDto>> GetTopDiscountedGamesFromDbAsync(int count);

      Task<List<GameDto>> GetTopDiscountedGamesAsync(int count);
}