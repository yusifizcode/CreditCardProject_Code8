using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechnestHackhaton.Application.Common.Interfaces;
using TechnestHackhaton.Application.DTOs;
using TechnestHackhaton.Application.Exceptions;
using TechnestHackhaton.Application.Rules.AppUserRules;
using TechnestHackhaton.Infrastructure.Services;

namespace TechnestHackhaton.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ITokenHandler _tokenHandler;
        readonly AuthenticationBusinessRules _authenticationBusinessRules;
        readonly IUserService _userService;
        readonly UserManager<Domain.Entity.Identity.AppUser> _userManager;
        readonly IOTPService _oTPService;
        public AuthService(ITokenHandler tokenHandler, AuthenticationBusinessRules authenticationBusinessRules, IUserService userService, UserManager<Domain.Entity.Identity.AppUser> userManager, IHttpContextAccessor httpContextAccessor, IOTPService oTPService)
        {
            _tokenHandler = tokenHandler;
            _authenticationBusinessRules = authenticationBusinessRules;
            _userService = userService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _oTPService = oTPService;
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {


            await _authenticationBusinessRules.LoginUserShouldExist(usernameOrEmail, password);

            Domain.Entity.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user is null) user = await _userManager.FindByEmailAsync(usernameOrEmail);
            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expration, 5);
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append("X-Access-Token", token.AccessToken, new CookieOptions() { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, Expires = token.Expration });
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append("X-Username", user.UserName, new CookieOptions() { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, Expires = token.Expration });
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append("X-Refresh-Token", token.RefreshToken, new CookieOptions() { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, Expires = token.Expration });
            return new();
        }


        public async Task<Token> RefreshTokenLoginAsync()
        {
            var refreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["X-Refresh-Token"];
            Domain.Entity.Identity.AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user is not null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(5, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expration, 5);
                _httpContextAccessor?.HttpContext?.Response.Cookies.Append("X-Access-Token", token.AccessToken, new CookieOptions() { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, Expires = token.Expration });
                _httpContextAccessor?.HttpContext?.Response.Cookies.Append("X-Refresh-Token", token.RefreshToken, new CookieOptions() { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, Expires = token.Expration });
                return token;
            }
            else throw new NotFoundException("İstifadəçi tapılmadı.");
        }
    }
}
