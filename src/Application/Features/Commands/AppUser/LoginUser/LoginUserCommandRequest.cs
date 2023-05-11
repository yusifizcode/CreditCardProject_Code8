using MediatR;

namespace TechnestHackhaton.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
{
    public string UsernameOrEmail { get; set; } = null!;
    public int Otp { get; set; }
    public string Password { get; set; } = null!;
}
