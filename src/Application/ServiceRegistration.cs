using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TechnestHackhaton.Application.Pipelines.Validation;
using TechnestHackhaton.Application.Rules.AppUserRules;

namespace TechnestHackhaton.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationBusinessRules>();
        services.AddMediatR(typeof(ServiceRegistration));
        services.AddAutoMapper(typeof(ServiceRegistration));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddHttpClient();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
    }
}
