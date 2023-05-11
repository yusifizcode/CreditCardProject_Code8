using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TechnestHackhaton.Application.Repositories;
using TechnestHackhaton.Domain.Entity.Common;
using TechnestHackhaton.Persistence.Context;

namespace TechnestHackhaton.Persistence.Repository;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    readonly private TechnestHackhatonDbContext _context;
    public WriteRepository(TechnestHackhatonDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<bool> AddAsync(T datas)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(datas);
        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<T> datas)
    {
        await Table.AddRangeAsync(datas);
        return true;
    }

    public bool Remove(T datas)
    {
        EntityEntry<T> entityEntry = Table.Remove(datas);
        return entityEntry.State == EntityState.Deleted;
    }
    public bool RemoveRange(List<T> datas)
    {
        Table.RemoveRange(datas);
        return true;
    }

    public async Task<bool> RemoveAsync(int id)
    {
        T? model = await Table.FirstOrDefaultAsync(data => data.Id == id);
        return Remove(datas: model);
    }
    public bool Update(T model)
    {
        EntityEntry entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();

}
