using MediatR;

namespace TechnestHackhaton.Application.Features.Queries.Roles.GetRole;

public class GetRoleQueryRequest : IRequest<GetRoleQueryResponse>
{
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 8;
}
