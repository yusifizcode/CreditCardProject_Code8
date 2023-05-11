using MediatR;

namespace TechnestHackhaton.Application.Features.Queries.AuthorizationEndpoint.GetRolesForEndpoint;

public class GetRolesForEndpointQueryRequest : IRequest<GetRolesForEndpointQueryResponse>
{
    public string Code { get; set; }
    public string Menu { get; set; }
}
