
using Microsoft.AspNetCore.Mvc;
using TechnestHackhaton.Application.Features.Commands.AuthorizationsEndpoint.AssignRoleEndpoint;
using TechnestHackhaton.Application.Features.Queries.AuthorizationEndpoint.GetRolesForEndpoint;
using TechnestHackhaton.Controllers;

namespace LibraryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationEndpointsController : BaseController
{
    [HttpPost("get-roles-to-endpoint")]
    public async Task<IActionResult> GetRolesToEndpoint([FromBody] GetRolesForEndpointQueryRequest endpointQueryRequest)
    {
        GetRolesForEndpointQueryResponse response = await Mediator.Send(endpointQueryRequest);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommandRequest roleEndpointCommandRequest)
    {
        roleEndpointCommandRequest.Type = typeof(Program);
        AssignRoleEndpointCommandResponse response = await Mediator.Send(roleEndpointCommandRequest);
        return Ok(response);
    }
}
