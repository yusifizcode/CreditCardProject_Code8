using ETicaretAPI.Application.Validators.Products;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TechnestHackhaton.API.Extensions;
using TechnestHackhaton.API.Filters;
using TechnestHackhaton.Application;
using TechnestHackhaton.Configurations;
using TechnestHackhaton.Infrastructure;
using TechnestHackhaton.Infrastructure.Filters;
using TechnestHackhaton.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRateLimiter(options =>
{
    options.AddSlidingWindowLimiter("Sliding", _options =>
    {
        _options.Window = TimeSpan.FromSeconds(4);
        _options.PermitLimit = 4;
        _options.QueueLimit = 2;
        _options.SegmentsPerWindow = 2;
        _options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    });
});


builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder =>
    {
        builder.Expire(TimeSpan.FromMinutes(10));
    });

    options.AddPolicy("Custom", policy =>
    {
        policy.Expire(TimeSpan.FromSeconds(15));
    });
});

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost:3000", "https://localhost:8001")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<RolePermissionFilter>();
})
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateTestValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Admin", options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
        NameClaimType = ClaimTypes.Name,
    };
    options.Events = new JwtBearerEvents()
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["X-Access-Token"];
            return Task.CompletedTask;
        }
    };
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

var app = builder.Build();
app.UseMiddleware<AuthenticationMiddleware>();
app.Use(async (context, next) =>
{

    //var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    //LogContext.PushProperty("UserName", username);
    //var host = Dns.GetHostEntry(Dns.GetHostName());
    //foreach (var ip in host.AddressList)
    //{
    //    if (ip.AddressFamily == AddressFamily.InterNetwork)
    //    {
    //        using (LogContext.PushProperty("IpAddress", ip.ToString()))
    //        {
    //            await next();
    //            break;
    //        }
    //    }
    //}
    await next();

});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseStaticFiles();

app.UseHttpLogging();
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
PrepDB.PrepPopulation(app);
app.MapControllers();

app.Run();
