using MediatR;
using TechnestHackhaton.Application.Common.Interfaces;

namespace TechnestHackhaton.Application.Features.Commands.Role.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
{
    private readonly IRoleService _roleService;

    public DeleteRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _roleService.DeleteRole(request.Id);
        return new()
        {
            Succeeded = result
        };
    }
}
