

using TechnestHackhaton.Application.Repositories.Endpoint;
using TechnestHackhaton.Persistence.Context;
using TechnestHackhaton.Persistence.Repository;

namespace TechnestHackhaton.Persistence.Repositories.Endpoint;

public class EndpointWriteRepository : WriteRepository<Domain.Entity.Endpoint>, IEndpointWriteRepository
{
    public EndpointWriteRepository(TechnestHackhatonDbContext context) : base(context)
    {
    }
}
