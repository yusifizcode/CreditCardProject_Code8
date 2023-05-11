using Microsoft.EntityFrameworkCore;
using TechnestHackhaton.Domain.Entity.Common;

namespace TechnestHackhaton.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
