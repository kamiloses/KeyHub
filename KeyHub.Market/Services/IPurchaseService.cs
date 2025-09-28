using KeyHub.Market.Models;

namespace KeyHub.Market.Services;

public interface IPurchaseService
{
    Task BuyGameAsync(int gameId, User user);
}

