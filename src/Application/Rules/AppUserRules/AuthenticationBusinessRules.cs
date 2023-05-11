using Microsoft.AspNetCore.Identity;
using TechnestHackhaton.Application.Exceptions;

namespace TechnestHackhaton.Application.Rules.AppUserRules;

public class AuthenticationBusinessRules
{
    readonly SignInManager<Domain.Entity.Identity.AppUser> _signInManager;
    readonly UserManager<Domain.Entity.Identity.AppUser> _userManager;

    public AuthenticationBusinessRules(SignInManager<Domain.Entity.Identity.AppUser> signInManager, UserManager<Domain.Entity.Identity.AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    public async Task LoginUserShouldExist(string? usernameOrEmail, string? password)
    {
        Domain.Entity.Identity.AppUser? user = await _userManager.FindByNameAsync(usernameOrEmail);
        if (user is null) user = await _userManager.FindByEmailAsync(usernameOrEmail);
        if (user is null) throw new BusinessException("İstifadəçi adı və ya şifrə yanlışdır.");
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded) throw new BusinessException("İstifadəçi adı və ya şifrə yanlışdır.");
    }
}
