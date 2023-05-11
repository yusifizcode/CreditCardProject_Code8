
using TechnestHackhaton.Application.Repositories.Endpoint;
using TechnestHackhaton.Persistence.Context;
using TechnestHackhaton.Persistence.Repository;

namespace TechnestHackhaton.Persistence.Repositories.Endpoint;

public class EndpointReadRepository : ReadRepository<Domain.Entity.Endpoint>, IEndpointReadRepository
{
    public EndpointReadRepository(TechnestHackhatonDbContext context) : base(context)
    {
    }
}
