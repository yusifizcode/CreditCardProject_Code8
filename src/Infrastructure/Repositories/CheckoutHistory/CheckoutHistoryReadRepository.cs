using TechnestHackhaton.Application.Repositories.CheckoutHistory;
using TechnestHackhaton.Persistence.Context;
using TechnestHackhaton.Persistence.Repository;

namespace TechnestHackhaton.Infrastructure.Repositories.CheckoutHistory;

public class CheckoutHistoryReadRepository : ReadRepository<Domain.Entities.CheckoutHistory>, ICheckoutHistoryReadRepository
{
    public CheckoutHistoryReadRepository(TechnestHackhatonDbContext context) : base(context)
    {
    }
}
