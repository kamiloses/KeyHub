namespace KeyHub.Market.Services;

public interface IAuthService
{
    public Task Register(string username, string email, string password);
}