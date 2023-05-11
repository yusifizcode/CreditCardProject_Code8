using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnestHackhaton.Domain.Entity.Identity;
using TechnestHackhaton.Persistence.Context;

namespace TechnestHackhaton.Infrastructure
{
    public class PrepDB
    {

        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var service = app.ApplicationServices.CreateScope())
            {
                SeedData(service.ServiceProvider.GetService<TechnestHackhatonDbContext>());
            }
        }
        public static void SeedData(TechnestHackhatonDbContext context)
        {
            context.Database.Migrate();
            if (!context.Users.Any())
            {
                Console.WriteLine("Already have not data");
            }
            else
            {
                Console.WriteLine("Already have data");
            }
        }
    }
}
