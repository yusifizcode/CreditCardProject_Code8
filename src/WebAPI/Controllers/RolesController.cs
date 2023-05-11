using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnestHackhaton.Application.Consts;
using TechnestHackhaton.Application.CustomAttributes;
using TechnestHackhaton.Application.Enums;
using TechnestHackhaton.Application.Features.Commands.Role.CreateRole;
using TechnestHackhaton.Application.Features.Commands.Role.DeleteRole;
using TechnestHackhaton.Application.Features.Commands.Role.UpdateRole;
using TechnestHackhaton.Application.Features.Queries.Roles.GetRole;
using TechnestHackhaton.Application.Features.Queries.Roles.GetRoleById;

namespace TechnestHackhaton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RolesController : BaseController
    {
        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Reading, Definition = "Get All Roles")]
        public async Task<IActionResult> GetRoles([FromQuery] GetRoleQueryRequest getRoleQueryRequest)
        {
            GetRoleQueryResponse getRoleQueryResponse = await Mediator.Send(getRoleQueryRequest);
            return Ok(getRoleQueryResponse);
        }
        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Reading, Definition = "Get By Id Roles")]

        public async Task<IActionResult> GetRoles([FromRoute] GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
            GetRoleByIdQueryResponse getRoleByIdQueryResponse = await Mediator.Send(getRoleByIdQueryRequest);
            return Ok(getRoleByIdQueryResponse);
        }
        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Writing, Definition = "Create Role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest createRoleCommandRequest)
        {
            CreateRoleCommandResponse createRoleCommandResponse = await Mediator.Send(createRoleCommandRequest);
            return Ok(createRoleCommandResponse);
        }
        [HttpPut("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Updating, Definition = "Updating Role")]
        public async Task<IActionResult> UpdateRole([FromRoute] UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            UpdateRoleCommandResponse updateRoleCommandResponse = await Mediator.Send(updateRoleCommandRequest);
            return Ok(updateRoleCommandResponse);
        }
        [HttpDelete("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Deleting, Definition = "Deleting Role")]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            DeleteRoleCommandResponse deleteRoleCommandResponse = await Mediator.Send(deleteRoleCommandRequest);
            return Ok(deleteRoleCommandResponse);
        }
    }
}
