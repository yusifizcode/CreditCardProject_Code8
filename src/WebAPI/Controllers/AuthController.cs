
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnestHackhaton.Application.Features.Commands.AppUser.LoginUser;
using TechnestHackhaton.Application.Features.Commands.AppUser.RefreshTokenLogin;

namespace TechnestHackhaton.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest loginUserCommandRequest)
    {
        LoginUserCommandResponse response = await Mediator.Send(loginUserCommandRequest);
        return Ok();
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> RefreshToken([FromHeader] RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
    {
        RefreshTokenLoginCommandResponse response = await Mediator.Send(refreshTokenLoginCommandRequest);
        return Ok();
    }
    [Authorize(AuthenticationSchemes = "Admin")]
    [HttpGet("[action]")]
    public async Task<IActionResult> CheckAuth()
    {
        return Ok();
    }
}
