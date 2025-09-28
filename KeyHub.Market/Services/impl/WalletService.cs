using KeyHub.Market.Models;

namespace KeyHub.Market.Services.impl;

public class WalletService :IWalletService
{
    public Task<(bool Success, string? ErrorMessage)> AddMoneyAsync(User user, decimal amount)
    {
        throw new NotImplementedException();
    }
}