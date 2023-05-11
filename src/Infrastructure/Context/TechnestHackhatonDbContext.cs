using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechnestHackhaton.Domain.Entities;
using TechnestHackhaton.Domain.Entity;
using TechnestHackhaton.Domain.Entity.Common;
using TechnestHackhaton.Domain.Entity.Identity;

namespace TechnestHackhaton.Persistence.Context
{
    public class TechnestHackhatonDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public TechnestHackhatonDbContext(DbContextOptions<TechnestHackhatonDbContext> options) : base(options)
        { }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
        public DbSet<CheckoutHistory> CheckoutHistories { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker
                .Entries<BaseAuditableEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now,
                    _ => DateTime.Now,
                };
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
