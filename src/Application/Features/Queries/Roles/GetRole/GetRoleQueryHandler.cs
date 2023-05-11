using MediatR;
using TechnestHackhaton.Application.Common.Interfaces;

namespace TechnestHackhaton.Application.Features.Queries.Roles.GetRole;

public class GetRoleQueryHandler : IRequestHandler<GetRoleQueryRequest, GetRoleQueryResponse>
{
    private readonly IRoleService _roleService;

    public GetRoleQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public Task<GetRoleQueryResponse> Handle(GetRoleQueryRequest request, CancellationToken cancellationToken)
    {
        var datas = _roleService.GetAllRoles(request.Page, request.Size);
        return Task.FromResult<GetRoleQueryResponse>(new()
        {
            Datas = datas.Item1,
            TotalRoleCount = datas.Item2,
        });
    }
}
