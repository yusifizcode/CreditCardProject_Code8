using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechnestHackhaton.Application.Abstraction.Services.Configurations;
using TechnestHackhaton.Application.Common.Interfaces;
using TechnestHackhaton.Application.Repositories.Endpoint;
using TechnestHackhaton.Application.Repositories.Menu;
using TechnestHackhaton.Domain.Entity;
using TechnestHackhaton.Domain.Entity.Identity;

namespace TechnestHackhaton.Persistence.Services;

public class AuthorizationEndpointService : IAuthorizationEndpointService
{
    private readonly IApplicationService _applicationService;
    private readonly IEndpointReadRepository _endpointReadRepository;
    private readonly IEndpointWriteRepository _endpointWriteRepository;
    private readonly IMenuReadRepository _menuReadRepository;
    private readonly IMenuWriteRepository _menuWriteRepository;
    private readonly RoleManager<AppRole> _roleManager;
    public AuthorizationEndpointService(IApplicationService applicationService, IEndpointReadRepository endpointReadRepository, IEndpointWriteRepository endpointWriteRepository, IMenuReadRepository menuReadRepository, IMenuWriteRepository menuWriteRepository, RoleManager<AppRole> roleManager)
    {
        _applicationService = applicationService;
        _endpointReadRepository = endpointReadRepository;
        _endpointWriteRepository = endpointWriteRepository;
        _menuReadRepository = menuReadRepository;
        _menuWriteRepository = menuWriteRepository;
        _roleManager = roleManager;
    }

    public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
    {
        Menu? _menu = await _menuReadRepository.GetSingleAsync(x => x.Name == menu);
        if (_menu is null)
        {
            _menu = new() { Name = menu };
            await _menuWriteRepository.AddAsync(_menu);
            await _menuWriteRepository.SaveAsync();
        }
        Endpoint? endpoint = await _endpointReadRepository.Table.Include(x => x.Menu).Include(x => x.Roles).FirstOrDefaultAsync(x => x.Code == code && x.Menu.Name == menu);
        if (endpoint is null)
        {
            var action = _applicationService.GetAuthorizeDefinitionEndpoints(type).FirstOrDefault(x => x.Name == menu)?.Actions.FirstOrDefault(x => x.Code == code);
            endpoint = new() { Code = action.Code, ActionType = action.ActionType, HttpType = action.HttpType, Definition = action.Definition, Menu = _menu };
            await _endpointWriteRepository.AddAsync(endpoint);
            await _endpointWriteRepository.SaveAsync();
        }
        foreach (var role in endpoint.Roles)
        {
            endpoint.Roles.Remove(role);
        }
        var appRoles = _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToList();
        foreach (var role in appRoles)
            endpoint.Roles.Add(role);
        await _endpointWriteRepository.SaveAsync();
    }

    public async Task<List<string>> GetRolesToEndpoint(string code, string menu)
    {
        var endpoint = await _endpointReadRepository.Table.Include(e => e.Roles).Include(e => e.Menu).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

        return endpoint.Roles.Select(x => x.Name).ToList();
    }
}
