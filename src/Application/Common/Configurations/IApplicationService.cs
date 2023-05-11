
using TechnestHackhaton.Application.DTOs.Configuration;

namespace TechnestHackhaton.Application.Abstraction.Services.Configurations;

public interface IApplicationService
{
    List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
}
