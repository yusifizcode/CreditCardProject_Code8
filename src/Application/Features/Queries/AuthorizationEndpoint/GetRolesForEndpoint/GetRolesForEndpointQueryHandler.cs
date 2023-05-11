using MediatR;
using TechnestHackhaton.Application.Common.Interfaces;

namespace TechnestHackhaton.Application.Features.Queries.AuthorizationEndpoint.GetRolesForEndpoint;

public class GetRolesForEndpointQueryHandler : IRequestHandler<GetRolesForEndpointQueryRequest, GetRolesForEndpointQueryResponse>
{
    private readonly IAuthorizationEndpointService _authorizationEndpointService;

    public GetRolesForEndpointQueryHandler(IAuthorizationEndpointService authorizationEndpointService)
    {
        _authorizationEndpointService = authorizationEndpointService;
    }

    public async Task<GetRolesForEndpointQueryResponse> Handle(GetRolesForEndpointQueryRequest request, CancellationToken cancellationToken)
    {
        var datas = await _authorizationEndpointService.GetRolesToEndpoint(request.Code, request.Menu);
        return new()
        {
            Roles = datas,
        };
    }
}
