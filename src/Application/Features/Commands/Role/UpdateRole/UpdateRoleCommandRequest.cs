using MediatR;

namespace TechnestHackhaton.Application.Features.Commands.Role.UpdateRole;

public class UpdateRoleCommandRequest : IRequest<UpdateRoleCommandResponse>
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}
