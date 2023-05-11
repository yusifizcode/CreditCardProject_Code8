namespace TechnestHackhaton.Application.Common.Interfaces;

public interface IAuthService
{
    Task<DTOs.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
    Task<DTOs.Token> RefreshTokenLoginAsync();

}
