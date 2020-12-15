namespace Domain.Auth
{
    public interface IAuthService
    {
        AuthResponse Login(string username, string password);
    }
}