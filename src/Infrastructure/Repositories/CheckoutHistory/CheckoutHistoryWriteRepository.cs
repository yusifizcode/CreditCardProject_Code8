using TechnestHackhaton.Application.Repositories.CheckoutHistory;
using TechnestHackhaton.Persistence.Context;
using TechnestHackhaton.Persistence.Repository;

namespace TechnestHackhaton.Infrastructure.Repositories.CheckoutHistory;

public class CheckoutHistoryWriteRepository : WriteRepository<Domain.Entities.CheckoutHistory>, ICheckoutHistoryWriteRepository
{
    public CheckoutHistoryWriteRepository(TechnestHackhatonDbContext context) : base(context)
    {
    }
}
