
using TechnestHackhaton.Application.Repositories.Menu;
using TechnestHackhaton.Persistence.Context;
using TechnestHackhaton.Persistence.Repository;

namespace TechnestHackhaton.Persistence.Repositories.Menu;

public class MenuWriteRepository : WriteRepository<Domain.Entity.Menu>, IMenuWriteRepository
{
    public MenuWriteRepository(TechnestHackhatonDbContext context) : base(context)
    {
    }
}
