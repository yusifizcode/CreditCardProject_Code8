

using TechnestHackhaton.Domain.Entity.Identity;

namespace TechnestHackhaton.Application.Common.Interfaces;

public interface ITokenHandler
{
    DTOs.Token CreateAccessToken(int minute, AppUser appUser);
    string CreateRefreshToken();
}
