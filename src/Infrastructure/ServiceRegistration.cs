using LTechnestHackhaton.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechnestHackhaton.Application.Abstraction.Services.Configurations;
using TechnestHackhaton.Application.Common.Interfaces;
using TechnestHackhaton.Application.Repositories.CheckoutHistory;
using TechnestHackhaton.Application.Repositories.Endpoint;
using TechnestHackhaton.Application.Repositories.Menu;
using TechnestHackhaton.Domain.Entity.Identity;
using TechnestHackhaton.Infrastructure.Repositories.CheckoutHistory;
using TechnestHackhaton.Infrastructure.Services;
using TechnestHackhaton.Infrastructure.Services.Configurations;
using TechnestHackhaton.Infrastructure.Services.Token;
using TechnestHackhaton.Persistence.Context;
using TechnestHackhaton.Persistence.Repositories.Endpoint;
using TechnestHackhaton.Persistence.Repositories.Menu;
using TechnestHackhaton.Persistence.Services;
using Twilio.Clients;

namespace TechnestHackhaton.Persistence;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECT");
        services.AddDbContext<TechnestHackhatonDbContext>(options => options.UseSqlServer(connectionString));

        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
        }).AddEntityFrameworkStores<TechnestHackhatonDbContext>().AddDefaultTokenProviders();
        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IEndpointReadRepository, EndpointReadRepository>();
        services.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();
        services.AddScoped<IMenuReadRepository, MenuReadRepository>();
        services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();
        services.AddScoped<ICheckoutHistoryWriteRepository, CheckoutHistoryWriteRepository>();
        services.AddScoped<ICheckoutHistoryReadRepository, CheckoutHistoryReadRepository>();
        services.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<ICardOperationChecker, CardOperationChecker>();
        services.AddHttpClient<ITwilioRestClient, TwilioClient>();
        services.AddScoped<IOTPService, OTPService>();
    }
}
