using KeyHub.Market.Models;

namespace KeyHub.Market.Services;

public interface IWalletService
{
    Task<(bool Success, string? ErrorMessage)> AddMoneyAsync(User user, decimal amount);
}