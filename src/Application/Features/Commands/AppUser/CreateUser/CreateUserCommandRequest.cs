using MediatR;

namespace TechnestHackhaton.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
{
    public string UserName { get; set; } = null!;
    public string NameSurname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
