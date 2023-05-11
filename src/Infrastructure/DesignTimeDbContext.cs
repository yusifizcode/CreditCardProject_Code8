using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TechnestHackhaton.Persistence.Context;

namespace TechnestHackhaton.Persistence
{
    public class DesignTimeDbContext : IDesignTimeDbContextFactory<TechnestHackhatonDbContext>
    {
        public TechnestHackhatonDbContext CreateDbContext(string[] args)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=True;TrustServerCertificate=True;Integrated security=true;Trusted_Connection=True";
            DbContextOptionsBuilder<TechnestHackhatonDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(connectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
