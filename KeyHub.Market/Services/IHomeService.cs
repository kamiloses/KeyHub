using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Services;

public interface IHomeService
{
    public Task<List<GameDto>> GetTopDiscountedGamesAsync(int count);
}