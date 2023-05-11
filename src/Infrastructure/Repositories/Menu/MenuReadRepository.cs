
using TechnestHackhaton.Application.Repositories.Menu;
using TechnestHackhaton.Persistence.Context;
using TechnestHackhaton.Persistence.Repository;

namespace TechnestHackhaton.Persistence.Repositories.Menu;

public class MenuReadRepository : ReadRepository<Domain.Entity.Menu>, IMenuReadRepository
{
    public MenuReadRepository(TechnestHackhatonDbContext context) : base(context)
    {
    }
}
