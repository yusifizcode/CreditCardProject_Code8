using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechnestHackhaton.Application.Repositories;
using TechnestHackhaton.Domain.Entity.Common;
using TechnestHackhaton.Persistence.Context;

namespace TechnestHackhaton.Persistence.Repository;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly TechnestHackhatonDbContext _context;

    public ReadRepository(TechnestHackhatonDbContext context)
    {
        _context = context;
    }
    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }
    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.Where(method);
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }
    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(method);
    }
    public async Task<T> GetByIdAsync(int id, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(data => data.Id == id);
    }
}
