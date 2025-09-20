namespace KeyHub.Market.Services;

public interface IAuthService
{
     // todo pamietaj by usunac public w kazdej metodzie interfejsu i sprawdz dlaczego
     Task Register(string username, string email, string password);
}